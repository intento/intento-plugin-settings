using Intento.MT.Plugin.PropertiesForm;
using IntentoSDK;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Intento.MT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm
{ 
    public class ProviderState : BaseState
    {
        public SmartRoutingState smartRoutingState;

        // Controlled components
        public AuthState authState;

        List<dynamic> providersRaw;
        private Dictionary<string, dynamic> providersData;
        private Dictionary<string, string> providersNames;

        dynamic providerData;
        public string currentProviderId;
        public string currentProviderName;

        private Dictionary<string, dynamic> _providerModels;
        private Dictionary<string, dynamic> _providerGlossaries;

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

            form.Providers_ComboBox_Clear();
            form.Providers_ComboBox_AddRange(providersNames.Select(x => (string)x.Key).OrderBy(x => x).ToArray());

            dynamic providerDataFromList = null;
            if (!string.IsNullOrEmpty(currentProviderId) && providersData.TryGetValue(currentProviderId, out providerDataFromList))
            {   // Set current provider in combo box 
                form.Providers_ComboBox_SelectedItem = (string)providerDataFromList.name;
            }
            else
            {
                currentProviderId = null;
                currentProviderName = null;
            }

            ExtractProviderData();
            isInitialized = true;

            CreateChildStates();
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

                providerData = form.Provider(currentProviderId, "?fields=auth,custom_glossary");
                if (providerData != null)
                {
                    //set flags for selected provider
                    billable = providerData.billable != null && (bool)providerData.billable;
                    stock_model = providerData.stock_model != null && (bool)providerData.stock_model;
                    own_auth = providerData.auth != null && ((JContainer)providerData.auth).HasValues;
                    custom_model = providerData.custom_model != null && (bool)providerData.custom_model;
                    custom_glossary = providerData.custom_glossary != null && (bool)providerData.custom_glossary;
                    delegated_credentials = providerData.delegated_credentials != null && (bool)providerData.delegated_credentials;

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
            if (string.IsNullOrWhiteSpace(form.Providers_ComboBox_Text))
            {
                // No provider choosed
                providerData = null;
                currentProviderId = null;
                currentProviderName = null;
                ExtractProviderData();

                CreateChildStates();
            }
            else if (providersNames != null && currentProviderId != providersNames[form.Providers_ComboBox_Text])
            {
                if (!string.IsNullOrEmpty(currentProviderName))
                {   // Prev provider was not empty - need to clear parameters
                    authState = null;
                    AuthState.ClearOptions(options);
                }

                // another provider choosed
                currentProviderId = providersNames[form.Providers_ComboBox_Text];
                providerData = providersData[currentProviderId];
                currentProviderName = providerData.name;
                ExtractProviderData();

                CreateChildStates();
            }
            else
            { // Selected same provider as was selected before. No changes in settings
            }

            EnableDisable();

            return;

        }

        private void CreateChildStates()
        {
            if (IsOK)
                authState = new AuthState(this, options);
            else
                authState = null;
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

        public static string Draw(IForm form, ProviderState state)
        {
            if (state == null)
            {
                form.Providers_Group_Visible = false;
                form.Providers_Group_Enabled = false;

                AuthState.Draw(form, null);
                return null;
            }

            return state.Draw();
        }


        public string Draw()
        {
            string errors;

            form.Providers_Group_Visible = true;
            form.Providers_Group_Enabled = true;
            // comboBoxProviders.Visible = true;
            // comboBoxProviders.Enabled = true;

            if (string.IsNullOrEmpty(form.Providers_ComboBox_Text))
            {
                form.Providers_ComboBox_BackColor = Color.LightPink;
                return "You need to choose a provider";
            }

            form.Providers_ComboBox_BackColor = Color.White;

            errors = AuthState.Draw(form, authState);

            return errors;
        }

        public bool IsOK
        { get { return providerData != null && isInitialized; } }

        public string CurrentProviderName
        { get { return IsOK ? currentProviderName : null; } }

        string authModelHash = null;
        public Dictionary<string, dynamic> GetModels(Dictionary<string, string> providerDataAuthDict)
        {
            string z = providerDataAuthDict ==null ? "" : string.Join(",", providerDataAuthDict.Keys.Select(i => string.Format("{0}-{1}", i, providerDataAuthDict[i])));
            if (_providerModels != null && z == authModelHash)
                return _providerModels;

            authModelHash = z;
            _providerModels = new Dictionary<string, dynamic>();
            try
            {
                IList<dynamic> providerModelsRec = form.Models(currentProviderId, providerDataAuthDict);
                if (providerModelsRec != null)
                    _providerModels = providerModelsRec.ToDictionary(s => (string)s.name, q => q);
            }
            catch { }
                
            return _providerModels;
        }

        string authGlossaryHash = null;
        public Dictionary<string, dynamic> GetGlossaries(Dictionary<string, string> providerDataAuthDict)
        {
            string z = string.Join(",", providerDataAuthDict.Keys.Select(i => string.Format("{0}-{1}", i, providerDataAuthDict[i])));
            if (_providerGlossaries != null && z == authGlossaryHash)
                return _providerGlossaries;

            authGlossaryHash = z;
            _providerGlossaries = new Dictionary<string, dynamic>();
            try
            {
                IList<dynamic> providerGlossariesRec = form.Glossaries(currentProviderId, providerDataAuthDict);
                if (providerGlossariesRec != null && providerGlossariesRec.Any())
                    _providerGlossaries = providerGlossariesRec.ToDictionary(s => (string)s.name, q => q);
            }
            catch { }

            return _providerGlossaries;
        }

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

                AuthState.FillOptions(state.authState, options);
            }
        }

    }
    }
