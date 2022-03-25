using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Intento.MT.Plugin.PropertiesForm.Models;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using Intento.SDK.DI;
using Intento.SDK.Translate;
using Intento.SDK.Translate.DTO;

namespace Intento.MT.Plugin.PropertiesForm.States
{
    public class ProviderState : BaseState
    {
        public SmartRoutingState SmartRoutingState { get; }

        private ITranslateService TranslateService => Locator.Resolve<ITranslateService>();

        // Controlled components
        private AuthState authState;

        private readonly Dictionary<string, Provider> providersData;
        private readonly Dictionary<string, string> providersNames;

        private Provider providerData;
        public string CurrentProviderId { get; private set; }
        private string currentProviderName;
        public Dictionary<string, string> FromLanguages { get; private set; }
        public Dictionary<string, string> ToLanguages { get; private set; }
        private List<string> enPairs;

        private readonly LangPair[] languagePairs;

        private readonly bool isInitialized;

        // Provider features
        public bool Billable { get; private set; }
        public bool StockModel { get; private set; }
        public bool OwnAuth { get; private set; }
        public bool CustomModel { get; private set; }
        public bool CustomGlossary { get; private set; }
        public bool DelegatedCredentials { get; private set; }
        public bool IntentoGlossary { get; private set; }
        public List<string> ProviderAuthList { get; private set; }

        private string format;

        public ProviderState(SmartRoutingState smartRoutingState, IntentoMTFormOptions options) : base(
            smartRoutingState, options)
        {
            SmartRoutingState = smartRoutingState;

            Init();

            languagePairs = Form.LanguagePairs;
            CurrentProviderId = options.ProviderId;
            currentProviderName = options.ProviderName;

            var providersRaw = FilterByLanguagePairs(smartRoutingState.ApiKeyState.Providers);

            providersData = providersRaw.ToDictionary(s => s.Id, q => q);
            providersNames = providersRaw.ToDictionary(s => s.Name, q => q.Id);

            var items = providersNames.Keys.OrderBy(x => x).ToArray();

            FormMt.comboBoxProviders.Items.Clear();
            FormMt.comboBoxProviders.Items.AddRange(items);

            if (!string.IsNullOrEmpty(CurrentProviderId) &&
                providersData.TryGetValue(CurrentProviderId, out var providerDataFromList))
            {
                // Set current provider in combo box 
                FormMt.comboBoxProviders.SelectedItem = providerDataFromList.Name;
                currentProviderName = providerDataFromList.Name;
            }
            else
            {
                CurrentProviderId = null;
                currentProviderName = null;
            }

            ExtractProviderData();
            isInitialized = true;
        }

        private void Init()
        {
            providerData = null;
            Billable = false;
            ;
            StockModel = false;
            OwnAuth = false;
            CustomModel = false;
            CustomGlossary = false;
            IntentoGlossary = false;
            DelegatedCredentials = false;
            ProviderAuthList = null;
            format = null;
        }

        private void ExtractProviderData()
        {
            // Getting provider parameters
            // Important! Provider flags are obtained for SYNC mode (not async) to provide best translation in case
            // provider may process xml or html formats and sent translate request with correspondent format option.

            ClearFeatures();

            try
            {
                if (string.IsNullOrEmpty(CurrentProviderId))
                {
                    // Provider is not chosen
                    Init();
                    return;
                }
                providerData = TranslateService.Provider(CurrentProviderId,
                    new Dictionary<string, string> { { "fields", "auth,custom_glossary" } });
                if (providerData == null)
                {
                    return;
                }

                //set flags for selected provider
                Billable = providerData.Billable;
                StockModel = providerData.StockModel;
                OwnAuth = providerData.Auth != null;
                CustomModel = providerData.CustomModel;
                CustomGlossary = providerData.CustomGlossary;
                IntentoGlossary = providerData.IntentoGlossary;

                // forced installation
                DelegatedCredentials = true;
                ProviderAuthList = null;
                if (DelegatedCredentials)
                {
                    ProviderAuthList = new List<string> { "credential_id" };
                }
                else
                {
                    ProviderAuthList = new List<string>();
                    /*foreach (var authField in providerData.Auth)
                            providerAuthList.Add((string)authField.Name);*/
                }

                format = providerData.Format != null ? providerData.Format.ToString() : "";
                var languages = providerData.Languages;
                if (languages != null)
                {
                    var from = new List<string>();
                    var to = new List<string>();
                    enPairs = new List<string>();
                    if (languages.Symmetric != null)
                    {
                        foreach (var p in languages.Symmetric)
                        {
                            from.Add(p);
                            to.Add(p);
                            enPairs.Add(p);
                        }

                        if (from.All(x => x != "en"))
                        {
                            enPairs.Clear();
                        }
                    }

                    // Used in Language_Comboboxes_Fill to select testing pair
                    // for symmetris ==null
                    // for pairs it contains all target langs for en->xx

                    if (languages.Pairs != null)
                    {
                        foreach (var p in languages.Pairs)
                        {
                            from.Add(p.From);
                            to.Add(p.To);
                            if (p.From == "en")
                            {
                                enPairs.Add(p.To);
                            }
                        }
                    }

                    from = from.Distinct().ToList();
                    to = to.Distinct().ToList();
                    enPairs = enPairs.Distinct().ToList();
                    FromLanguages ??= new Dictionary<string, string>();
                    ToLanguages ??= new Dictionary<string, string>();
                    FillLanguageDictionary(FromLanguages, from);
                    FillLanguageDictionary(ToLanguages, to);

                    Language_Comboboxes_Fill(FromLanguages, ToLanguages);
                }
            }
            catch
            {
                providerData = null;
                ClearFeatures();
            }
        }

        private void ClearFeatures()
        {
            Billable = false;
            StockModel = false;
            OwnAuth = false;
            CustomModel = false;
            CustomGlossary = false;
            DelegatedCredentials = false;
            format = null;
        }

        public void SelectedIndexChanged()
        {
            var provName = FormMt.comboBoxProviders.Text;
            if (string.IsNullOrWhiteSpace(provName))
            {
                // No provider chosen
                ClearOptions(Options);
                providerData = null;
                CurrentProviderId = null;
                currentProviderName = null;
                ExtractProviderData();
            }
            else
            {
                // Need to clear parameters
                GetAuthState()?.ClearOptions(Options);
                authState = null;
                // another provider chosen
                if (providersNames != null && CurrentProviderId != providersNames[provName])
                {
                    ClearOptions(Options);
                    CurrentProviderId = providersNames[provName];
                    providerData = providersData[CurrentProviderId];
                    currentProviderName = providerData.Name;
                    ExtractProviderData();
                }
                else
                {
                    // Selected same provider as was selected before. No changes in settings
                }
            }

            EnableDisable();

            return;
        }

        public AuthState GetAuthState()
        {
            if (IsOk)
            {
                authState ??= new AuthState(this, Options);
            }
            else
            {
                authState = null;
            }

            return authState;
        }

        private static bool ProviderSupportsPair(Provider provider, LangPair pair)
        {
            if (provider.Pairs.Any(p => p.From == pair.From && p.To == pair.To))
            {
                return true;
            }

            return provider.Symmetric.Any(x => x == pair.From) &&
                   provider.Symmetric.Any(x => x == pair.To);
        }

        private IList<Provider> FilterByLanguagePairs(IList<Provider> recProviders)
        {
            if (languagePairs == null)
            {
                return recProviders;
            }

            // TODO this is very slow algorithm, but will do for now

            // copy the list to keep original recProviders intact
            var ret = new List<Provider>(recProviders);
            foreach (var pair in languagePairs)
            {
                ret.RemoveAll(provider => !ProviderSupportsPair(provider, pair));
            }

            return ret;
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, ProviderState state)
        {
            if (state == null)
            {
                form.FormMt.groupBoxProvider.Enabled = false;

                AuthState.Draw(form, null);
                return null;
            }

            return state.Draw();
        }

        private string Draw()
        {
            FormMt.groupBoxProvider.Enabled = true;
            if (string.IsNullOrEmpty(FormMt.comboBoxProviders.Text))
            {
                AuthState.Draw(Form, null);
                return Resource.YouNeedToChooseAProviderMessage;
            }

            var errors = AuthState.Draw(Form, GetAuthState());
            return errors;
        }

        public bool IsOk => providerData != null && isInitialized;

        private string CurrentProviderName => IsOk ? currentProviderName : null;

        public static void FillOptions(ProviderState state, IntentoMTFormOptions options)
        {
            if (state == null)
            {
                options.ProviderId = null;
                options.ProviderName = null;
                options.Format = null;
                AuthState.FillOptions(null, options);
            }
            else
            {
                options.ProviderId = state.CurrentProviderId;
                options.ProviderName = state.CurrentProviderName;
                options.Format = state.format;
                AuthState.FillOptions(state.GetAuthState(), options);
            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.ProviderId = null;
            options.ProviderName = null;
            options.Format = null;
            Language_Comboboxes_Fill(null, null);

            if (authState != null)
            {
                authState.ClearOptions(options);
                authState.Clear();
            }
        }

        private void FillLanguageDictionary(IDictionary<string, string> dct, IEnumerable<string> source)
        {
            var d = source.ToDictionary(code => code, GetCultureDisplayName)
                .OrderBy(x => x.Value, System.StringComparer.OrdinalIgnoreCase)
                .ToDictionary(x => x.Key, x => x.Value);
            dct.Clear();
            foreach (var val in d)
            {
                dct.Add(val.Key, val.Value);
            }
        }

        private static string GetCultureDisplayName(string code)
        {
            try
            {
                var ci = new CultureInfo(code);
                if (!ci.DisplayName.StartsWith("Unknown Language"))
                {
                    return $"{ci.DisplayName} [{code}]";
                }
            }
            catch
            {
            }

            return $"[{code}]";
        }

        #region methods for managing a group of controls

        void Language_Comboboxes_Fill(Dictionary<string, string> from, Dictionary<string, string> to)
        {
            FormMt.comboBoxFrom.Items.Clear();
            FormMt.comboBoxTo.Items.Clear();
            if (from != null)
                FormMt.comboBoxFrom.Items.AddRange(from.Select(x => x.Value).ToArray());
            if (to != null)
                FormMt.comboBoxTo.Items.AddRange(to.Select(x => x.Value).ToArray());
            SetLanguageComboBoxes(Options.FromLanguage, Options.ToLanguage);
        }

        public void SetLanguageComboBoxes(string from, string to)
        {
            if (FromLanguages != null)
            {
                if (!string.IsNullOrWhiteSpace(from) && FromLanguages.ContainsKey(from))
                    FormMt.comboBoxFrom.SelectedItem = FromLanguages[from];
                else if (!string.IsNullOrWhiteSpace(Options.FromLanguage) &&
                         FromLanguages.ContainsKey(Options.FromLanguage))
                    FormMt.comboBoxFrom.SelectedItem = FromLanguages[Options.FromLanguage];
                else if (FromLanguages.ContainsKey("en"))
                    FormMt.comboBoxFrom.SelectedItem = FromLanguages["en"];
                else if (!string.IsNullOrWhiteSpace(Form.OriginalOptions.FromLanguage) &&
                         FromLanguages.ContainsKey(Form.OriginalOptions.FromLanguage))
                    FormMt.comboBoxFrom.SelectedItem = FromLanguages[Form.OriginalOptions.FromLanguage];
                else if (FormMt.comboBoxFrom.Items.Count > 0)
                    FormMt.comboBoxFrom.SelectedIndex = 0;
            }

            if (ToLanguages != null)
            {
                if (to == "zh-TW") to = "zh-hant";
                if (!string.IsNullOrWhiteSpace(to) && ToLanguages.ContainsKey(to))
                    FormMt.comboBoxTo.SelectedItem = ToLanguages[to];
                else if (FromLanguages != null && !string.IsNullOrWhiteSpace(Options.ToLanguage) &&
                         FromLanguages.ContainsKey(Options.ToLanguage))
                    FormMt.comboBoxTo.SelectedItem = FromLanguages[Options.ToLanguage];
                else if (enPairs.Contains("es")) // en-xx, need to check is provider support en-es or en-zh
                    FormMt.comboBoxTo.SelectedItem = ToLanguages["es"];
                else if (enPairs.Contains("zh"))
                    FormMt.comboBoxTo.SelectedItem = ToLanguages["zh"];
                else if (!string.IsNullOrWhiteSpace(Form.OriginalOptions.ToLanguage) &&
                         ToLanguages.ContainsKey(Form.OriginalOptions.ToLanguage) &&
                         enPairs.Contains(Form.OriginalOptions.ToLanguage))
                    FormMt.comboBoxTo.SelectedItem = ToLanguages[Form.OriginalOptions.ToLanguage];
                else if (FormMt.comboBoxTo.Items.Count > 0)
                    FormMt.comboBoxTo.SelectedIndex = 0;
            }
        }

        #endregion methods for managing a group of controls
    }
}