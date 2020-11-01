using Intento.MT.Plugin.PropertiesForm.WinForms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using static Intento.MT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class ProviderState : BaseState
    {
        public SmartRoutingState smartRoutingState;

        // Controlled components
        private AuthState authState;

        List<dynamic> providersRaw;
        private Dictionary<string, dynamic> providersData;
        private Dictionary<string, string> providersNames;

        dynamic providerData;
        public string currentProviderId;
        public string currentProviderName;
        public Dictionary<string, string> fromLanguages;
        public Dictionary<string, string> toLanguages;
		public List<string> enPairs;

		LangPair[] languagePairs;

        bool isInitialized = false;

        // Provider features
        public bool billable;
        public bool stock_model;
        public bool own_auth;
        public bool custom_model;
        public bool custom_glossary;
        public bool delegated_credentials;
        public List<string> providerAuthList;
        public string format;

        public ProviderState(SmartRoutingState smartRoutingState, IntentoMTFormOptions options) : base(smartRoutingState, options)
        {
            this.smartRoutingState = smartRoutingState;

            Init();

            languagePairs = form.LanguagePairs;
            currentProviderId = options.ProviderId;
            currentProviderName = options.ProviderName;

            providersRaw = FilterByLanguagePairs(smartRoutingState.apiKeyState.Providers);

            providersData = providersRaw.ToDictionary(s => (string)s.id, q => q);
            providersNames = providersRaw.ToDictionary(s => (string)s.name, q => (string)q.id);

            formMT.comboBoxProviders.Items.Clear();
            formMT.comboBoxProviders.Items.AddRange(providersNames.Select(x => (string)x.Key).OrderBy(x => x).ToArray());

            dynamic providerDataFromList = null;
            if (!string.IsNullOrEmpty(currentProviderId) && providersData.TryGetValue(currentProviderId, out providerDataFromList))
            {   // Set current provider in combo box 
                formMT.comboBoxProviders.SelectedItem = (string)providerDataFromList.name;
                currentProviderName = (string)providerDataFromList.name;
            }
            else
            {
                currentProviderId = null;
                currentProviderName = null;
            }

            ExtractProviderData();
            isInitialized = true;
        }

        private void Init()
        {
            providerData = null;

            billable = false; ;
            stock_model = false;
            own_auth = false; 
            custom_model = false;
            custom_glossary = false;
            delegated_credentials = false;
            providerAuthList = null;
            format = null;
        }

        private void ExtractProviderData()
        {
            // Getting provider parameters
            // Importnant! Provider flags are obtained for SYNC mode (not async) to provide best tranlstion in case
            // provider may process xml or html formats and sent translate request with correspondent format option.

            ClearFeatures();

            try
            {
                if (string.IsNullOrEmpty(currentProviderId))
                {   // Provider is not choosen
                    Init();
                    return;
                }

                if (form.testOneProviderData != null)
                    providerData = form.testOneProviderData;
                else
                    providerData = form._translate.Provider(currentProviderId, 
                        new Dictionary<string, string> { { "fields", "auth,custom_glossary" } });

                if (providerData != null)
                {
                    //set flags for selected provider
                    billable = providerData.billable != null && (bool)providerData.billable;
                    stock_model = providerData.stock_model != null && (bool)providerData.stock_model;
                    own_auth = providerData.auth != null && ((JContainer)providerData.auth).HasValues;
                    custom_model = providerData.custom_model != null && (bool)providerData.custom_model;
                    custom_glossary = providerData.custom_glossary != null && (bool)providerData.custom_glossary;

                    // forced installation
                    //delegated_credentials = providerData.delegated_credentials != null && (bool)providerData.delegated_credentials;
                    delegated_credentials = true;

                    providerAuthList = null;

                    if (delegated_credentials)
                    {
                        providerAuthList = new List<string>();
                        providerAuthList.Add("credential_id");
                    }
                    else
                    {
                        providerAuthList = new List<string>();
                        foreach (dynamic authField in providerData.auth)
                            providerAuthList.Add((string)authField.Name);
                    }

                    format = providerData.format != null ? providerData.format.ToString() : "";
                    var languages = providerData.languages;
                    if (languages != null)
                    {
                        List<string> from = new List<string>();
                        List<string> to = new List<string>();
						enPairs = new List<string>();
						if (languages.symmetric != null)
                        {
                            foreach (dynamic p in languages.symmetric)
                            {
                                from.Add((string)p.Value);
                                to.Add((string)p.Value);
								enPairs.Add((string)p.Value);

							}
							if (!from.Any(x => x == "en"))
								enPairs.Clear();
                        }

                        // Used in Language_Comboboxes_Fill to select testing pair
                        // for symmetris ==null
                        // for pairs it contains all target langs for en->xx

                        if (languages.pairs != null)
                        {
                            foreach (dynamic p in languages.pairs)
                            {
                                from.Add((string)p.from);
                                to.Add((string)p.to);
                                if (p.from == "en")
									enPairs.Add((string)p.to);
                            }
                        }
                        from = from.Distinct().ToList();
                        to = to.Distinct().ToList();
						enPairs = enPairs.Distinct().ToList();
						FillLanguageDictionary(ref fromLanguages, from);
                        FillLanguageDictionary(ref toLanguages, to);

                        Language_Comboboxes_Fill(fromLanguages, toLanguages);
                    }
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
            billable = false;
            stock_model = false;
            own_auth = false;
            custom_model = false;
            custom_glossary = false;
            delegated_credentials = false;
            format = null;
        }

        public void SelectedIndexChanged()
        {
            string provName = formMT.comboBoxProviders.Text;
			if (string.IsNullOrWhiteSpace(provName))
			{
				// No provider choosed
				ClearOptions(options);
				providerData = null;
				currentProviderId = null;
				currentProviderName = null;
				ExtractProviderData();
			}
			else
			{
				// Need to clear parameters
				GetAuthState()?.ClearOptions(options);
				authState = null;
				// another provider choosed
				if (providersNames != null && currentProviderId != providersNames[provName])
				{
					ClearOptions(options);
					currentProviderId = providersNames[provName];
					providerData = providersData[currentProviderId];
					currentProviderName = providerData.name;
					ExtractProviderData();
				}
				else
				{ // Selected same provider as was selected before. No changes in settings
				}
			}

			EnableDisable();

            return;

        }

        public AuthState GetAuthState()
        {
            if (IsOK)
            {
                if (authState == null)
                    authState = new AuthState(this, options);
            }
            else
                authState = null;
            return authState;
        }

        private List<dynamic> FilterByLanguagePairs(List<dynamic> recProviders)
        {
            if (languagePairs == null)
                return recProviders;
            List<dynamic> ret = new List<dynamic>();

            foreach (dynamic prov in recProviders)
            {
                bool f = false;
                foreach (LangPair lp in languagePairs)
                {
                    foreach (dynamic p in prov.pairs)
                    {
                        f = (p.from == lp.from && p.to == lp.to);
                        if (f) break;
                    }
                    if (f) continue;
                    List<string> symmetric = ((JArray)prov.symmetric).Select(x => (string)x).ToList();
                    f = symmetric.Any(x => x == lp.from) && symmetric.Any(x => x == lp.to);
                    if (!f) break;
                }
                if (f) ret.Add(prov);
            }

            return ret;
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, ProviderState state)
        {
            if (state == null)
            {
                form.formMT.groupBoxProvider.Enabled = false;
                Providers_ComboBox_BackColor_State(form.formMT, false);

                AuthState.Draw(form, null);
                return null;
            }

            return state.Draw();
        }

        public string Draw()
        {
            string errors;
            formMT.groupBoxProvider.Enabled = true;
            if (string.IsNullOrEmpty(formMT.comboBoxProviders.Text))
            {
                Providers_ComboBox_BackColor_State(formMT, true);
                AuthState.Draw(form, null);
                return Resource.YouNeedToChooseAProviderMessage;
            }
            Providers_ComboBox_BackColor_State(formMT, false);
            errors = AuthState.Draw(form, GetAuthState());
            return errors;
        }

        public bool IsOK
        { get { return providerData != null && isInitialized; } }

        public string CurrentProviderName
        { get { return IsOK ? currentProviderName : null; } }

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
                options.ProviderId = state.currentProviderId;
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

        private void FillLanguageDictionary(ref Dictionary<string, string> dct, List<string> source)
        {
            List<string> unknownCodes = new List<string>();
            dct = new Dictionary<string, string>();
            foreach (string code in source)
                dct.Add(code, GetCultureDisplayName(code));
            dct = dct.OrderBy(x => x.Value, System.StringComparer.OrdinalIgnoreCase).ToDictionary(x => x.Key, x => x.Value);
        }

        private string GetCultureDisplayName (string code)
        {
            try
            {
                CultureInfo ci = new CultureInfo(code);
                if (!ci.DisplayName.StartsWith("Unknown Language"))
                    return string.Format("{0} [{1}]", ci.DisplayName, code);
            }
            catch { }

            return string.Format("[{0}]", code);
        }
        #region methods for managing a group of controls

        void Language_Comboboxes_Fill(Dictionary<string, string> from, Dictionary<string, string> to)
        {
            formMT.comboBoxFrom.Items.Clear();
            formMT.comboBoxTo.Items.Clear();
            if (from != null)
                formMT.comboBoxFrom.Items.AddRange(from.Select(x => x.Value).ToArray());
            if (to != null)
                formMT.comboBoxTo.Items.AddRange(to.Select(x => x.Value).ToArray());
			SetLanguageComboBoxes(options.FromLanguage, options.ToLanguage);
        }

		public void SetLanguageComboBoxes(string from, string to)
		{
			if (fromLanguages != null)
			{
				if (!string.IsNullOrWhiteSpace(from) && fromLanguages.ContainsKey(from))
					formMT.comboBoxFrom.SelectedItem = fromLanguages[from];
				else if (!string.IsNullOrWhiteSpace(options.FromLanguage) && fromLanguages.ContainsKey(options.FromLanguage))
					formMT.comboBoxFrom.SelectedItem = fromLanguages[options.FromLanguage];
				else if (fromLanguages.ContainsKey("en"))
					formMT.comboBoxFrom.SelectedItem = fromLanguages["en"];
				else if (!string.IsNullOrWhiteSpace(form.originalOptions.FromLanguage) || fromLanguages.ContainsKey(form.originalOptions.FromLanguage))
					formMT.comboBoxFrom.SelectedItem = fromLanguages[form.originalOptions.FromLanguage];
				else
					formMT.comboBoxFrom.SelectedIndex = 1;
			}

			if (toLanguages != null)
			{
				if (!string.IsNullOrWhiteSpace(to) && toLanguages.ContainsKey(to))
					formMT.comboBoxTo.SelectedItem = toLanguages[to];
				else if (!string.IsNullOrWhiteSpace(options.ToLanguage) && fromLanguages.ContainsKey(options.ToLanguage))
					formMT.comboBoxTo.SelectedItem = fromLanguages[options.ToLanguage];
				else if (enPairs.Contains("es"))   // en-xx, need to check is provider support en-es or en-zh
					formMT.comboBoxTo.SelectedItem = toLanguages["es"];
				else if (enPairs.Contains("zh"))
					formMT.comboBoxTo.SelectedItem = toLanguages["zh"];
				else if (!string.IsNullOrWhiteSpace(form.originalOptions.ToLanguage) && toLanguages.ContainsKey(form.originalOptions.ToLanguage) && enPairs.Contains(form.originalOptions.ToLanguage))
					formMT.comboBoxTo.SelectedItem = toLanguages[form.originalOptions.ToLanguage];
				else
					formMT.comboBoxTo.SelectedIndex = 1;
			}
		}


		static void Providers_ComboBox_BackColor_State(IntentoFormOptionsMT formMT, bool hasErrors)
        {
            if (hasErrors)
                formMT.comboBoxProviders.BackColor = Color.LightPink;
            else
                formMT.comboBoxProviders.BackColor = SystemColors.Window;
                // formMT.comboBoxProviders.BackColor = formMT.groupBoxProvider.Enabled ? SystemColors.Window : SystemColors.Control;
        }

        #endregion methods for managing a group of controls

    }
}
