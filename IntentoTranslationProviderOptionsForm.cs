using IntentoSDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntentoMT.Plugin.PropertiesForm
{
    public partial class IntentoTranslationProviderOptionsForm : Form
    {
        public class LangPair
        {
            string _from;
            string _to;

            public LangPair(string from, string to)
            {
                this.from = from;
                this.to = to;
            }

            public string from { get => _from; set => _from = value; }
            public string to { get => _to; set => _to = value; }
        }

        #region vars
        public IntentoMTFormOptions Options;
        private IntentoAiTextTranslate _translate;
        private Dictionary<string, string> providers;
        // Current provider 
        dynamic providerData;
        public IList<dynamic> languages;

        // private bool ApiKeyValid;
        private LangPair[] _languagePairs;
        enum EApiKeyStatus
        {
            start,          // just after start of plugin
            download,       // during download list of providers
            changed,        // apikey was changed recently and was not checked
            ok,             // apikey checked
            error           // apikey check error
        };
        EApiKeyStatus apiKeyStatus = EApiKeyStatus.start;
        string error_reason;    // used only in case of error check
        public static DateTime TraceEndTime;
        const string ulrAdditionalParams = "?fields=auth,custom_glossary&mode=async";
        private Dictionary<string, string> providerDataAuthDict;
        private Dictionary<string, dynamic> _providerModels;
        private Dictionary<string, dynamic> _providerGlossaries;
        private List<string> _delegatedCredentials;

        private int numberOfFlashes;
        //Google V3 provider selection indicator
        //private bool googleV3;
        private string errorMessage;
        private bool own_auth, custom_model, billable, stock_model, custom_glossary, delegated_credentials;

        #endregion vars

        public IntentoTranslationProviderOptionsForm(IntentoMTFormOptions options, IntentoAiTextTranslate translate, LangPair[] languagePairs)
        {
            InitializeComponent();
            //Options.SetAuthDict(null);
            Options = options;
            _translate = translate;
            _languagePairs = languagePairs;
            if (options != null)
            {
                apiKey_tb.Text = options.ApiKey;
            }
            apiKey_tb.TextChanged += ApiKey_tb_TextChanged;
            DialogResult = DialogResult.None;
            

            var tmp = TraceEndTime;
            checkBoxTrace.Checked = (TraceEndTime - DateTime.Now).Minutes > 0;
            TraceEndTime = tmp;
            toolStripStatusLabel2.Text = String.Format("{0} {1}/{2}", Options.PluginName, Options.PluginFor, Options.AssemblyVersion);
            textBoxModel.Location = comboBoxModels.Location; // new Point(comboBoxModels.Location.X, comboBoxModels.Location.Y);
            textBoxGlossary.Location = comboBoxGlossaries.Location; // new Point(comboBoxGlossaries.Location.X, comboBoxGlossaries.Location.Y);
            groupBoxAuthCredentialId.Location = groupBoxAuth.Location; // new Point(groupBoxAuth.Location.X, groupBoxAuth.Location.Y)
            apiKeyStatus = EApiKeyStatus.start;

            //EnableDisable();
        }

        private void ApiKey_tb_TextChanged(object sender, EventArgs e)
        {
            apiKeyStatus = EApiKeyStatus.changed;
            comboBoxProviders.Items.Clear();
            clearParameters();
            EnableDisable();
        }

        public static bool IsTrace()
        {
            return (TraceEndTime - DateTime.Now).Minutes > 0;
        }

        #region custom helper methods
        public void ConnectIntento(IntentoAiTextTranslate translate)
        {
            DisableAllSettings();
            textBoxCredentials.BorderStyle = BorderStyle.None;
            providers = null;
            providerData = null;
            if (translate == null)
            {
                var _intento = Intento.Create(
                    Options.ApiKey,
                    path: "https://api.inten.to/",
                    userAgent: String.Format("{1}/{0}", Options.AssemblyVersion, Options.PluginName)
                );
                _translate = _intento.Ai.Text.Translate;
            }

            this.Refresh();
            checkApiKey();
            this.Refresh();

            // Get MT providers
            ReadProviders(Options.ProviderId);
            checkApiKey();
            this.Refresh();
            EnableDisable();
            if (apiKeyStatus == EApiKeyStatus.ok)
            {
                // set initial parameters
                if (checkBoxUseOwnCred.Enabled) 
                    checkBoxUseOwnCred.Checked = Options.UseCustomAuth;

                
                if (Options.authDict() != null && providerDataAuthDict != null)
                {
                    if (delegated_credentials)
                    {
                        if (Options.authDict().ContainsKey("credential_id"))
                        {
                            var val = Options.authDict()["credential_id"];
                            if (comboBoxCredentialId.Items.Contains(val))
                            {
                                providerDataAuthDict["credential_id"] = val;
                                comboBoxCredentialId.SelectedItem = val;
                            }
                        }
                    }
                    else
                        providerDataAuthDict = Options.authDict();
                    filltextBoxCredentials();
                }
                EnableDisable();
                if (checkBoxUseCustomModel.Enabled)
                    checkBoxUseCustomModel.Checked = Options.UseCustomModel;
            }
            textBoxModel.Text = Options.CustomModel;

            EnableDisable();
        }

        static bool forceGoogleV3 = false;
        private void ReadProviders(string currentProviderId)
        {
            try
            {
                providers = null;
                providerData = null;
                apiKeyStatus = EApiKeyStatus.download;
                checkApiKey();

                List<dynamic> recProviders = _translate.Providers(filter: new Dictionary<string, string> { { "integrated", "true" }, { "mode", "async" } }).ToList();

                // remove this line after after finalizing api!
                bool hardcode = forceGoogleV3 && !recProviders.Any(x => x.id == "ai.text.translate.google.translate_api.v3");

                recProviders = filterByLanguagePairs(recProviders);
                providers = recProviders.ToDictionary(s => (string)s.name, q => (string)q.id);
                
                // remove this line after after finalizing api!
                if (hardcode)
                    providers.Add("google.translate_api.v3", "ai.text.translate.google.translate_api.v3");

                // if (_languagePairs != null && _languagePairs.Length > 1) providers = providers.Where(x => x.Value != "ai.text.translate.google.translate_api.v3").ToDictionary(s => s.Key, q => q.Value);
                if (_languagePairs != null && _languagePairs.Length > 1) providers = providers.ToDictionary(s => s.Key, q => q.Value);

                comboBoxProviders.Items.AddRange(providers.Select(x => (string)x.Key).OrderBy(x => x).ToArray());
                if (!string.IsNullOrEmpty(currentProviderId))
                {
                    object provId = providers.FirstOrDefault(x => x.Value == currentProviderId);
                    if (provId != null)
                        comboBoxProviders.SelectedItem = ((KeyValuePair<string, string>)provId).Key;
                }

                apiKeyStatus = EApiKeyStatus.ok;

            }
            catch (AggregateException ex2)
            {
                providers = null;
                providerData = null;

                Exception ex = ex2.InnerExceptions[0];
                if (ex is IntentoInvalidApiKeyException)
                {
                    error_reason = "Invalid API key";
                }
                else
                {
                    if (ex is IntentoInvalidApiKeyException)
                        error_reason = string.Format("Forbitten. {0}", ((IntentoSDK.IntentoApiException)ex).Content);
                    else if (ex is IntentoApiException)
                        error_reason = string.Format("Api Exception {2}: {0}: {1}", ex.Message, ((IntentoApiException)ex).Content, ex.GetType().Name);
                    else if (ex is IntentoSdkException)
                        error_reason = string.Format("Sdk Exception {1}: {0}", ex.Message, ex.GetType().Name);
                    else
                        error_reason = string.Format("Unexpected exception {0}: {1}", ex.GetType().Name, ex.Message);
                }
                apiKeyStatus = EApiKeyStatus.error;
            }
        }

        private void checkApiKey()
        {
            buttonContinue.Enabled = false;

            switch (apiKeyStatus)
            {
                case EApiKeyStatus.start:
                case EApiKeyStatus.changed:
                    DisableAllSettings();

                    if (string.IsNullOrWhiteSpace(apiKey_tb.Text))
                    {
                        buttonCheck.Enabled = false;
                        setErrorMessage("Enter your API key and press \"Check\" button.");
                    }
                    else
                    {
                        buttonCheck.Enabled = true;
                        setErrorMessage("API key verification required. Press \"Check\" button.");
                    }
                    comboBoxProviders.Enabled = false;
                    break;
                case EApiKeyStatus.download:
                    DisableAllSettings();
                    setErrorMessage("API key verification in progress ....");
                    comboBoxProviders.Enabled = false;
                    break;
                case EApiKeyStatus.error:
                    DisableAllSettings();
                    setErrorMessage(error_reason);
                    comboBoxProviders.Enabled = false;
                    break;
                case EApiKeyStatus.ok:
                    setErrorMessage();
                    comboBoxProviders.Enabled = true;
                    break;
            }
            apiKey_tb.BackColor = apiKeyStatus == EApiKeyStatus.ok ? SystemColors.Window : Color.LightPink;
            this.Refresh();
        }


        bool insideEnableDisable = false;
        private void EnableDisable()
        {
            if (insideEnableDisable)
                return;

            try
            {
                insideEnableDisable = true;

                groupBoxProviderSettings.Enabled = !checkBoxSmartRouting.Checked && apiKeyStatus == EApiKeyStatus.ok;
                buttonCheck.Enabled = apiKeyStatus != EApiKeyStatus.ok;

                if (groupBoxProviderSettings.Enabled)
                {
                    if (apiKeyStatus == EApiKeyStatus.ok)
                    {
                        if (string.IsNullOrWhiteSpace(comboBoxProviders.Text))
                        {
                            groupBoxAuth.Enabled = false;
                            groupBoxModel.Enabled = false;
                        }
                        else
                        {
                            // set  state of checkBoxUseOwnCred
                            if (!billable || !stock_model)  //Required
                            {
                                checkBoxUseOwnCred.Enabled = false;
                                checkBoxUseOwnCred.Checked = true;
                                groupBoxAuth.Enabled = true;
                            }
                            else if (!own_auth) //Prohibited 
                            {
                                checkBoxUseOwnCred.Enabled = false;
                                checkBoxUseOwnCred.Checked = false;
                                groupBoxAuth.Enabled = false;
                            }
                            else
                            {
                                checkBoxUseOwnCred.Enabled = true;
                                groupBoxAuth.Enabled = checkBoxUseOwnCred.Checked;

                            }

                            groupBoxAuthCredentialId.Visible = delegated_credentials;
                            groupBoxAuth.Visible = !delegated_credentials;
                            if (delegated_credentials)
                            {
                                if (comboBoxCredentialId.Items.Count == 1)
                                {
                                    comboBoxCredentialId.SelectedIndex = 0;
                                    comboBoxCredentialId.Enabled = false;
                                }
                                else
                                    comboBoxCredentialId.Enabled = true;
                            }

                            // set state of checkBoxUseCustomModel 
                            switch(customModelMode)
                            {
                                case "required":
                                    checkBoxUseCustomModel.Enabled = false;
                                    checkBoxUseCustomModel.Checked = true;
                                    break;

                                case "prohibited":
                                    checkBoxUseCustomModel.Enabled = false;
                                    checkBoxUseCustomModel.Checked = false;
                                    groupBoxModel.Enabled = false;
                                    break;

                                case "optional":
                                    checkBoxUseCustomModel.Enabled = true;
                                    break;

                                default:
                                    throw new Exception("EnableDisable.customModelMode");
                            }

                            // set state of model selection control
                            groupBoxModel.Enabled =
                                checkBoxUseCustomModel.Checked && !string.IsNullOrWhiteSpace(textBoxCredentials.Text);
                            if (checkBoxUseCustomModel.Checked)
                            {
                                if (_providerModels != null)
                                {
                                    textBoxModel.Visible = false;
                                    comboBoxModels.Visible = true;
                                    //comboBoxModels.Enabled = _providerModels.Count > 1;
                                }
                                else
                                {
                                    textBoxModel.Visible = true;
                                    comboBoxModels.Visible = false;
                                }
                            }

                            // set state of glossary selection control
                            groupBoxGlossary.Enabled = custom_glossary && !string.IsNullOrWhiteSpace(textBoxCredentials.Text);
                            if (groupBoxGlossary.Enabled)
                            {
                                if (_providerGlossaries != null)
                                {
                                    textBoxGlossary.Visible = false;
                                    comboBoxGlossaries.Visible = true;
                                    comboBoxGlossaries.Enabled = true;
                                }
                                else
                                {
                                    textBoxGlossary.Visible = true;
                                    comboBoxGlossaries.Visible = false;
                                }

                            }
                        }
                    }
                }

                checkConditions();
            }
            finally
            {
                insideEnableDisable = false;
            }
        }

        private string customModelMode
        {
            get
            {
                if (!stock_model && custom_model) return "required";
                if (!custom_model) return "prohibited";
                return "optional";
            }
        }

        private void DisableAllSettings()
        {
            groupBoxProviderSettings.Enabled = false;
            checkBoxUseOwnCred.Enabled = false;
            checkBoxUseOwnCred.Checked = false;
            checkBoxUseCustomModel.Enabled = false;
            checkBoxUseCustomModel.Checked = false;
            buttonCheck.Enabled = false;
            buttonContinue.Enabled = false;
        }

        private void checkConditions()
        {
            bool good = true;
            if (groupBoxProviderSettings.Enabled && string.IsNullOrWhiteSpace(comboBoxProviders.Text))
            {
                comboBoxProviders.BackColor = Color.LightPink;
                good = false;
            }
            else  
                comboBoxProviders.BackColor = SystemColors.Window;

            if (good && checkBoxUseOwnCred.Checked && string.IsNullOrWhiteSpace(textBoxCredentials.Text))
            {
                textBoxCredentials.BackColor = Color.LightPink;
                comboBoxCredentialId.BackColor = Color.LightPink;
                good = false;
            }
            else
            {
                textBoxCredentials.BackColor = SystemColors.Window;
                comboBoxCredentialId.BackColor = SystemColors.Window;
            }
            if (checkBoxUseCustomModel.Checked)
            {
                if (textBoxModel.Visible)
                    textBoxModel.BackColor = good && string.IsNullOrEmpty(textBoxModel.Text) ? Color.LightPink : SystemColors.Window;
                else
                    comboBoxModels.BackColor = good && string.IsNullOrEmpty(comboBoxModels.Text) ? Color.LightPink : SystemColors.Window;
            }
            else
            {
                comboBoxModels.BackColor = SystemColors.Window;
                textBoxModel.BackColor = SystemColors.Window;
            }

                // Validate parameters and enable Continue button
            ValidateParameters();
            if (errorMessage == null)
            {
                buttonContinue.Enabled = true;
                setErrorMessage();
            }
            else
            {
                setErrorMessage(errorMessage);
                buttonContinue.Enabled = false;
            }
        }

        private void ValidateParameters()
        {
            errorMessage = null;
            if (apiKeyStatus == EApiKeyStatus.ok && !checkBoxSmartRouting.Checked)
            if (string.IsNullOrWhiteSpace(apiKey_tb.Text))
            {
                errorMessage = "No MT provider selected";
            }
            else
            {
                if (!checkBoxSmartRouting.Checked)
                {
                    if (checkBoxUseOwnCred.Checked && (providerDataAuthDict == null || providerDataAuthDict.Any(x => string.IsNullOrWhiteSpace(x.Value))))
                    {
                        errorMessage = "You must provide your own credentials for this provider. ";
                    }
                    else if (!stock_model && string.IsNullOrEmpty(textBoxModel.Text)) 
                    {
                        errorMessage = "You must specify a model for this provider";
                    }
                    else if (checkBoxUseCustomModel.Checked &&
                        (
                        (textBoxModel.Visible && string.IsNullOrEmpty(textBoxModel.Text))
                        || (comboBoxModels.Visible && string.IsNullOrEmpty(comboBoxModels.Text))
                        ))
                    {
                        errorMessage = "You must specify a custom model or uncheck \"use your custom model\"";
                    }

                }
            }
            else
                errorMessage = error_reason;
        }

        private void filltextBoxCredentials()
        {
            var str = string.Empty;
            if (providerDataAuthDict != null)
            {
                foreach (KeyValuePair<string, string> val in providerDataAuthDict)
                {
                    if (val.Value != null && !string.IsNullOrWhiteSpace(val.Value))
                        str += String.Format("{0}:{1} ", val.Key, val.Value);
                }
            }
            if (textBoxCredentials.Text != str.TrimEnd(' '))
            {
                textBoxCredentials.Text = str;
            }
        }

        private string customAuthJsonToString(string authJsonString)
        {
            string ret = String.Empty;
            if (IsValidJson(authJsonString) == null)
            {
                var js = JToken.Parse(authJsonString);
                foreach (dynamic val in js)
                    ret += String.Format("{0}:{1} ", val.Name, val.Value);
            }
            return ret;
        }

        private string IsValidJson(string strInput)
        {
            strInput = String.IsNullOrEmpty(strInput) ? String.Empty : strInput.Trim();
            string result = "Invalid json for own authorization parameters";
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    if (obj.HasValues)
                        result = null;
                }
                catch { }
            }
            return result;
        }

        private void setErrorMessage(string message = null)
        {
            if (message != null)
            {
                toolStripStatusLabel1.Text = message;
                toolStripStatusLabel1.BackColor = Color.LightPink;
            }
            else
            {
                toolStripStatusLabel1.Text = string.Empty;
                toolStripStatusLabel1.BackColor = SystemColors.Control;
            }
        }

        private List<dynamic> filterByLanguagePairs(List<dynamic> recProviders)
        {
            if (_languagePairs == null) return recProviders;
            List<dynamic> ret = new List<dynamic>();

            foreach (dynamic prov in recProviders)
            {
                bool f = false;
                foreach (LangPair lp in _languagePairs)
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

        private void clearParameters()
        {
            // clear old parameters
            providerData = null;
            _providerModels = null;
            _providerGlossaries = null;
            textBoxCredentials.Text = string.Empty;
            textBoxModel.Text = string.Empty;
            textBoxGlossary.Text = string.Empty;
            comboBoxGlossaries.Items.Clear();
            comboBoxModels.Items.Clear();
            checkBoxUseCustomModel.Checked = false;
            checkBoxUseOwnCred.Checked = false;

        }

        #endregion custom helper methods

        #region paramount events 

        private void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxProviders.Text))
                clearParameters();
            else
            {
                if (Options.ProviderId != providers[comboBoxProviders.Text])
                {
                    clearParameters();
                    Options.ProviderId = providers[comboBoxProviders.Text];
                    Options.ProviderName = comboBoxProviders.Text;
                }

                // Getting provider parameters
                providerData = _translate.Provider(providers[comboBoxProviders.Text], ulrAdditionalParams);
                if (providerData != null)
                {
                    //set flags for selected provider
                    billable = providerData.billable != null && (bool)providerData.billable;
                    stock_model = providerData.stock_model != null && (bool)providerData.stock_model;
                    own_auth = providerData.auth != null && ((JContainer)providerData.auth).HasValues;
                    custom_model = providerData.custom_model != null && (bool)providerData.custom_model;
                    custom_glossary = providerData.custom_glossary != null && (bool)providerData.custom_glossary;
                    delegated_credentials = providerData.delegated_credentials != null && (bool)providerData.delegated_credentials;

                    providerDataAuthDict = null;
                    if (delegated_credentials)
                    {
                        comboBoxCredentialId.Items.Clear();
                        providerDataAuthDict = new Dictionary<string, string>();
                        providerDataAuthDict.Add("credential_id", "");
                        var credentials = _translate.DelegatedCredentials();
                        var cred = credentials.Where(q => q.temporary_credentials != null
                            && q.temporary_credentials_created_at != null
                            && q.temporary_credentials_expiry_at != null);
                        _delegatedCredentials = cred.Select(q => (string)q.credential_id).ToList();
                        comboBoxCredentialId.Items.Clear();
                        comboBoxCredentialId.Items.AddRange(_delegatedCredentials.ToArray());
                        if (_delegatedCredentials.Count > 1) comboBoxCredentialId.Items.Insert(0, "");


                    }
                    else if (own_auth)
                    {
                        _delegatedCredentials = null;
                        providerDataAuthDict = new Dictionary<string, string>();
                        foreach (dynamic authField in providerData.auth)
                        {
                            providerDataAuthDict.Add((string)authField.Name, string.Empty);
                        }
                    }
                    Options.Format = providerData.format != null ? providerData.format.ToString() : "";
                    EnableDisable();
                }
            }
        }

        private bool filterBy(dynamic x, string lang)
        {
            if (x == null) return true; 
            if (x.GetType().Name == "JArray")
                return ((JArray)x).Any(q => (string)q == lang);
            if (x.GetType().Name == "JValue")
                return (string)x == lang;
            return true;
        }

        private void FillProviderModels()
        {   // Fill combo or text box depending on provider features
            if (!checkBoxUseCustomModel.Checked || string.IsNullOrWhiteSpace(textBoxCredentials.Text))
                return;
            IList<dynamic> providerModelsRec = null;
            comboBoxModels.Items.Clear();
            _providerModels = null;
            try
            {
                providerModelsRec = _translate.Models(providers[comboBoxProviders.Text], providerDataAuthDict);
                if (providerModelsRec != null)
                {
                    //if (_languagePairs != null)
                    //{
                    //    var from = _languagePairs[0].from;
                    //    var to = _languagePairs[0].to;
                    //    providerModelsRec = providerModelsRec.Where(x => (filterBy(x.from, from) && filterBy(x.to, to)) || (bool)x.stock).ToList();
                    //}
                    _providerModels = providerModelsRec.ToDictionary(s => (string)s.name, q => q);
                    if (_providerModels.Any())
                    {
                        //_providerModels = _providerModels.Take(1).ToDictionary(s => s.Key, q => q.Value);
                        if (_providerModels.Count > 1 || customModelMode == "optional")
                            comboBoxModels.Items.Add("");

                        // Fill comboBoxModels and choose SelectedIndex{"dt": "2019-04-11 00:54:31.132266+00:00", "ts": 1554944071.132266, "kong_request_id": "172.18.0.4-8443-18-73563306-1-1554944070.920", "tornado_request_id": "1dc2a0a6-c447-48e7-a6b2-b99a1e4eb615", "client_key_id": "fb8fb351-4b64-47be-b79a-a2cf7fef706b", "client_key_name": "intento_integration", "request_mode": "production", "error_type": null, "code": null, "reason": null, "op": "async", "intent_id": "ai.text.translate", "instance_id": null, "instance_ids": [], "intent_dependent": null, "intent_request_data": {"request": {"method": "POST", "uri": "/ai/text/translate", "version": "HTTP/1.1", "headers": "Host: prod_intent_api_05.aws-usw2.inten.to:8844\nConnection: keep-alive\nX-Forwarded-For: 23.111.23.189\nX-Forwarded-Proto: https\nX-Forwarded-Host: api.inten.to\nX-Forwarded-Port: 8443\nX-Real-Ip: 23.111.23.189\nContent-Length: 483\nApikey: 1d2dfc89fad9403a8599af9372b0a909\nUser-Agent: Intento.CSharpSDK/1.2.0 Intento.MemoqPlugin/1.2.0 memoq/8.6.6.27345\nContent-Type: text/plain; charset=utf-8\nX-Consumer-Id: fb8fb351-4b64-47be-b79a-a2cf7fef706b\nX-Consumer-Username: intento_integration\nX-Consumer-Groups: integration\nKong-Request-Id: 172.18.0.4-8443-18-73563306-1-1554944070.920\n", "intento_user_agents": ["intento.csharpsdk", "intento.memoqplugin"], "remote_ip": "172.31.12.124", "arguments": "{}", "cookies": "", "body": {"service": {"provider": "ai.text.translate.google.translate_api.v3", "async": true, "auth": {"ai.text.translate.google.translate_api.v3": [{"credential_id": "testcred"}]}}, "context": {"text": "hash: 4e9601c3f20b43aa0b91f79bdf5c2131f805d265e2783efd65aaebe1b7d2e618", "to": "de", "from": "en", "category": "projects/1091715416487/locations/us-central1/models/TRL4634621755906527763", "glossary": "hash: b12b64fd1fe0843d6f55058885bb90013f20e8f1d19f0a77b7cf6d2995f851c4", "provider": "hash: bf2dbb74741c7ad4c16e0d92c7593011feee24fc457e548843ea0f93177f1261", "model": "hash: b417eadd75484331936326c6ce9d1afb28e09178384d70552a1165df6d618d1b"}}}, "intent": {"service": {"provider": "ai.text.translate.google.translate_api.v3", "async": true, "auth": {"ai.text.translate.google.translate_api.v3": [{"credential_id": "testcred"}]}}, "context": {"text": "hash: 4e9601c3f20b43aa0b91f79bdf5c2131f805d265e2783efd65aaebe1b7d2e618", "to": "de", "from": "en", "category": "projects/1091715416487/locations/us-central1/models/TRL4634621755906527763", "glossary": "hash: b12b64fd1fe0843d6f55058885bb90013f20e8f1d19f0a77b7cf6d2995f851c4", "provider": "hash: bf2dbb74741c7ad4c16e0d92c7593011feee24fc457e548843ea0f93177f1261", "model": "hash: b417eadd75484331936326c6ce9d1afb28e09178384d70552a1165df6d618d1b"}}, "metainfo": {"symbols": 40, "items": 1, "words": 7}}, "provider_request_data": {}, "provider_answer_data": {}, "intent_answer_data": {}, "timestamp": "2019-04-11 00:54:31.132266+00:00"}
                        foreach (string x in _providerModels.Select(x => (string)x.Key).OrderBy(x => x))
                        {
                            int n = comboBoxModels.Items.Add(x);
                            if (_providerModels[x].id == Options.CustomModel)
                                comboBoxModels.SelectedIndex = n;
                        }
                        comboBoxModels.Visible = !(textBoxModel.Visible = false);
                        if (comboBoxModels.Items.Count == 1)
                        {
                            comboBoxModels.SelectedIndex = 0;
                        }
                    }
                    else
                        _providerModels = null;
                }
            }
            catch
            {
                _providerModels = null;
            }

            if (_providerModels == null)
                textBoxModel.Text = Options.CustomModel;

            EnableDisable();
        }

        private void FillProviderClossaries()
        {
            if (string.IsNullOrWhiteSpace(textBoxCredentials.Text)) return;
            comboBoxGlossaries.Items.Clear();
            textBoxGlossary.Text = string.Empty;

            _providerGlossaries = null;
            try
            {
                IList<dynamic> providerGlossariesRec = _translate.Glossaries(providers[comboBoxProviders.Text], providerDataAuthDict);
                if (providerGlossariesRec != null && providerGlossariesRec.Any())
                {
                    // if (_languagePairs != null)
                    // {
                    // var from = _languagePairs[0].from;
                    // var to = _languagePairs[0].to;
                    // providerGlossariesRec = providerGlossariesRec.Where(x => (string)x.from == from && ((JArray)(x.to)).Any(y => (string)y == to))
                    // .ToList();
                    // }
                    _providerGlossaries = providerGlossariesRec.ToDictionary(s => (string)s.name, q => q);
                    if (_providerGlossaries.Any())
                    {

                        // Fill Glossary and choose SelectedIndex
                        comboBoxGlossaries.Items.Insert(0, "");
                        foreach (string x in _providerGlossaries.Select(x => (string)x.Key).OrderBy(x => x))
                        {
                            int n = comboBoxGlossaries.Items.Add(x);
                            if ((string)_providerGlossaries[x].id == Options.Glossary)
                                comboBoxGlossaries.SelectedIndex = n;
                        }
                        textBoxGlossary.Text = null;
                    }
                    else
                        _providerGlossaries = null;
                }
            }
            catch
            {
                _providerGlossaries = null;
            }

            if (_providerGlossaries == null)
                textBoxGlossary.Text = Options.Glossary;

            EnableDisable();
        }

        #endregion paramount events 

        #region events

        private void IntentoTranslationProviderOptionsForm_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(apiKey_tb.Text))
            {
                ConnectIntento(_translate);
                checkBoxSmartRouting.Checked = Options.SmartRouting;
                EnableDisable();
            }
            else
            {
                checkApiKey();
            }
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            apiKey_tb.Text = apiKey_tb.Text.Trim();
            Options.ApiKey = apiKey_tb.Text;
            ConnectIntento(null);
            //ApiKeyValid = ConnectIntento(null);
            //EnableDisable();
        }

        private void apiKey_tb_TextChanged(object sender, EventArgs e)
        {
            // ApiKeyValid = false;
            //comboBoxProviders.Items.Clear();
            apiKeyStatus = EApiKeyStatus.changed;
            buttonCheck.Enabled = true;
            checkApiKey();
            EnableDisable();
        }

        private void checkBoxUseOwnCred_CheckedChanged(object sender, EventArgs e)
        {
            //Options.SetAuthDict(null);
            groupBoxAuth.Enabled = checkBoxUseOwnCred.Checked;
            textBoxCredentials.Clear();
            //if (!checkBoxUseOwnCred.Checked)
            //    Options.SetAuthDict(null);
            groupBoxAuthCredentialId.Enabled = checkBoxUseOwnCred.Checked;
            comboBoxCredentialId.Items.Clear();
            if (checkBoxUseOwnCred.Checked && delegated_credentials)
            {
                comboBoxCredentialId.Items.AddRange(_delegatedCredentials.ToArray());
                if (_delegatedCredentials.Count > 1) comboBoxCredentialId.Items.Insert(0, "");
                providerDataAuthDict = new Dictionary<string, string>();
                providerDataAuthDict.Add("credential_id", "");
            }
            EnableDisable();
        }

        private void checkBoxUseCustomModel_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            groupBoxModel.Enabled = checkBoxUseCustomModel.Checked;
            EnableDisable();
            if (checkBoxUseCustomModel.Checked)
                FillProviderModels();
            EnableDisable();
            this.Cursor = Cursors.Default;
        }

        // Save settings
        private void buttonContinue_Click(object sender, EventArgs e)
        {
            Options.SmartRouting = checkBoxSmartRouting.Checked;
            if (Options.SmartRouting)
            {
                Options.ProviderId = null;
                Options.ProviderName = null;
                Options.UseCustomAuth = false;
                Options.CustomAuth = null;
                Options.UseCustomModel = false;
                Options.CustomModel = null;
                Options.Glossary = null;
            }
            else
            {
                Options.UseCustomAuth = checkBoxUseOwnCred.Checked;
                if (!checkBoxUseOwnCred.Checked)
                    Options.CustomAuth = null;

                Options.UseCustomModel = checkBoxUseCustomModel.Checked;
                if (checkBoxUseCustomModel.Checked)
                    Options.CustomModel = textBoxModel.Visible ? textBoxModel.Text : (string)_providerModels[comboBoxModels.Text].id;
                else
                    Options.CustomModel = null;

                Options.Glossary = textBoxGlossary.Visible ? textBoxGlossary.Text : 
                        string.IsNullOrEmpty(comboBoxGlossaries.Text) ? null : (string)_providerGlossaries[comboBoxGlossaries.Text].id;

                ValidateParameters();
                setErrorMessage(errorMessage);

                if (checkBoxUseOwnCred.Checked)
                    Options.SetAuthDict(checkBoxUseOwnCred.Checked ? providerDataAuthDict : null);
            }
            Close();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(((LinkLabel)sender).Text);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            label7.Text = textBoxCredentials.Text;
            FillProviderModels();
            if (custom_glossary)
                FillProviderClossaries();
            EnableDisable();
        }

        private void buttonWizard_Click(object sender, EventArgs e)
        {
            var dialog = new IntentoTranslationProviderAuthWizardForm(providerDataAuthDict, checkBoxShowHidden.Checked);
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.OK)
            {
                providerDataAuthDict = dialog.authParam;
                filltextBoxCredentials();
                ValidateParameters();
            }
        }

        private void comboBoxModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkConditions();
        }

        private void checkBoxShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            apiKey_tb.UseSystemPasswordChar = !checkBoxShowHidden.Checked;
            textBoxCredentials.UseSystemPasswordChar = !checkBoxShowHidden.Checked;
        }

        private void checkBoxTrace_CheckedChanged(object sender, EventArgs e)
        {
            TraceEndTime = DateTime.Now.AddMinutes(checkBoxTrace.Checked ? 30 : -40);
        }

        private void IntentoTranslationProviderOptionsForm_Load(object sender, EventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
#if SLDTrados2015
            this.toolStripStatusLabel2.Text = string.Format("{0}.{1}.{2}/2015-2017", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart);
#endif
#if SLDTrados2019
            this.toolStripStatusLabel2.Text = string.Format("{0}.{1}.{2}/2019", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart);
#endif
        }

        private void apiKey_tb_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBoxCredentialId_SelectedIndexChanged(object sender, EventArgs e)
        {
            providerDataAuthDict["credential_id"] = comboBoxCredentialId.Text;
            textBoxCredentials.Text = string.IsNullOrEmpty(comboBoxCredentialId.Text) ? string.Empty : "credential_id:" + comboBoxCredentialId.Text;
            EnableDisable();
            ValidateParameters();
        }

        private void textBoxModel_TextChanged(object sender, EventArgs e)
        {
            EnableDisable();
            ValidateParameters();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            comboBoxGlossaries.BackColor = comboBoxGlossaries.BackColor != Color.LightPink ? Color.LightPink : SystemColors.Window;
            numberOfFlashes++;
            if (numberOfFlashes > 5)
            {
                timer1.Stop();
                numberOfFlashes = 0;
            }
        }

        private void checkBoxSmartRouting_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSmartRouting.Checked)
            {
                comboBoxProviders.Items.Clear();
                checkBoxUseOwnCred.Checked = false;
                checkBoxUseCustomModel.Checked = false;
                comboBoxModels.Items.Clear();
                textBoxModel.Text = null;
                comboBoxGlossaries.Items.Clear();
                textBoxGlossary.Text = null;
                Options.Format = "[\"text\",\"html\",\"xml\"]";
            }
            else
            {
                if (providers == null)
                    ReadProviders(null);
                else if (comboBoxProviders.Items.Count == 0)
                    comboBoxProviders.Items.AddRange(providers.Select(x => (string)x.Key).OrderBy(x => x).ToArray());
            }
            EnableDisable();
        }

        private void textBoxCredentials_Enter(object sender, EventArgs e)
        {
            buttonWizard_Click(null, null);
        }

        #endregion events

    }
}
