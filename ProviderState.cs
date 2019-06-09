using IntentoSDK;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntentoMT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace IntentoMT.Plugin.PropertiesForm
{
    public class ProviderState
    {
        private IntentoAiTextTranslate _translate;

        List<dynamic> providersRaw;
        private Dictionary<string, dynamic> providersData;
        private Dictionary<string, string> providersNames;

        dynamic providerData;
        public string currentProviderId;
        public string currentProviderName;

        private Dictionary<string, dynamic> _providerModels;
        private Dictionary<string, dynamic> _providerGlossaries;

        LangPair[] languagePairs;
        System.Windows.Forms.ComboBox comboBoxProviders;
        System.Windows.Forms.GroupBox groupBoxProviderSettings;

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
        IntentoTranslationProviderOptionsForm form;

        public ProviderState(IntentoTranslationProviderOptionsForm _form, System.Windows.Forms.GroupBox _groupBoxProviderSettings,
            System.Windows.Forms.ComboBox _comboBoxProviders,
            IntentoMTFormOptions options, LangPair[] _languagePairs)
        {
            form = _form;
            Init();
            languagePairs = _languagePairs;
            comboBoxProviders = _comboBoxProviders;
            groupBoxProviderSettings = _groupBoxProviderSettings;
            currentProviderId = options.ProviderId;
            currentProviderName = options.ProviderName;
            _translate = _form._translate;
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

            _providerModels = null;
            _providerGlossaries = null;
    }

    // process a list of providers with their features from Intento API
    public void Fill(List<dynamic> data)
        {
            if (form.checkBoxSmartRouting.Checked)
                return;

            providersRaw = filterByLanguagePairs(data);

            providersData = providersRaw.ToDictionary(s => (string)s.id, q => q);
            providersNames = providersRaw.ToDictionary(s => (string)s.name, q => (string)q.id);

            comboBoxProviders.Items.Clear();
            comboBoxProviders.Items.AddRange(providersNames.Select(x => (string)x.Key).OrderBy(x => x).ToArray());

            dynamic providerDataFromList = null;
            if (!string.IsNullOrEmpty(currentProviderId) && providersData.TryGetValue(currentProviderId, out providerDataFromList))
            {   // Set current provider in combo box 
                comboBoxProviders.SelectedItem = (string)providerDataFromList.name;
            }
            else
            {
                currentProviderId = null;
                currentProviderName = null;
            }

            ExtractProviderData();
            isInitialized = true;
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

                providerData = _translate.Provider(currentProviderId, "?fields=auth,custom_glossary");
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

        public void HideProviderInformation()
        {
            providerData = null;
            currentProviderId = null;
            currentProviderName = null;
            ExtractProviderData();
            return;

        }

        public void SelectedIndexChanged()
        {
            if (string.IsNullOrWhiteSpace(comboBoxProviders.Text) || form.checkBoxSmartRouting.Checked)
            {
                // No provider choosed
                HideProviderInformation();
                return;
            }

            if (currentProviderId != providersNames[comboBoxProviders.Text])
            {
                // another provider choosed
                currentProviderId = providersNames[comboBoxProviders.Text];
                providerData = providersData[currentProviderId];
                currentProviderName = providerData.name;
                ExtractProviderData();
                return;
            }

            return;

        }

        private List<dynamic> filterByLanguagePairs(List<dynamic> recProviders)
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
                form.comboBoxProviders.Enabled = false;
                return null;
            }

            return state.Draw();
        }
        public string Draw()
        {
            if (form.checkBoxSmartRouting.Checked)
            {
                groupBoxProviderSettings.Visible = false;
                return null;
            }

            groupBoxProviderSettings.Visible = true;
            comboBoxProviders.Visible = true;
            comboBoxProviders.Enabled = true;

            if (string.IsNullOrEmpty(comboBoxProviders.Text))
            {
                comboBoxProviders.BackColor = Color.LightPink;
                return "You need to choose a provider";
            }

            comboBoxProviders.BackColor = Color.White;
            return null;
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
                IList<dynamic> providerModelsRec = _translate.Models(currentProviderId, providerDataAuthDict);
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
                IList<dynamic> providerGlossariesRec = _translate.Glossaries(currentProviderId, providerDataAuthDict);
                if (providerGlossariesRec != null && providerGlossariesRec.Any())
                    _providerGlossaries = providerGlossariesRec.ToDictionary(s => (string)s.name, q => q);
            }
            catch { }

            return _providerGlossaries;
        }
    }
}
