using Intento.MT.Plugin.PropertiesForm.WinForms;
using IntentoSDK;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using static Intento.MT.Plugin.PropertiesForm.AuthState;
using static Intento.MT.Plugin.PropertiesForm.GlossaryState;
using static Intento.MT.Plugin.PropertiesForm.IntentoMTFormOptions;
using static Intento.MT.Plugin.PropertiesForm.ModelState;

namespace Intento.MT.Plugin.PropertiesForm
{
    public partial class IntentoTranslationProviderOptionsForm : Form //, IForm
    {
        public class LangPair
        {
            string _from;
            string _to;

            public LangPair(string from, string to)
            {
                if (from.Contains("-"))
                    from = from.Substring(0, from.IndexOf('-'));
                if (to.Contains("-"))
                    to = to.Substring(0, to.IndexOf('-'));
                _from = from;
                _to = to;
            }

            public string from { get => _from; set => _from = value; }
            public string to { get => _to; set => _to = value; }
        }

        #region vars
        public IntentoMTFormOptions originalOptions;
        public IntentoMTFormOptions currentOptions;
        public IntentoAiTextTranslate _translate;

        // Languages filter 
        public IList<dynamic> languages;
        private LangPair[] _languagePairs;

        public static DateTime TraceEndTime;

        //private int numberOfFlashes;

        public ApiKeyState apiKeyState;

        public List<string> errors;

        // Fabric to create intento connection. Parameters: apiKey and UserAgent for Settings Form 
        public Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric;

        public string version;

        public IntentoFormOptionsAPI formApi;
        public IntentoFormOptionsMT formMT;
        public IntentoFormAdvanced formAdvanced;
        private int cursorCount = 0;
        public bool settingsIsSet;
        public bool insideEnableDisable = false;


        // Glossary data was obtained directly, without a request to the Intento service
        public List<dynamic> testListProvidersData;
        // Provider data was obtained directly, without a request to the Intento service
        public dynamic testOneProviderData;
        // Model data was obtained directly, without a request to the Intento service
        public IList<dynamic> testModelData;
        // Credentional data was obtained directly, without a request to the Intento service
        public List<dynamic> testAuthData;
        // Glossary data was obtained directly, without a request to the Intento service
        public IList<dynamic> testGlossaryData;


        #endregion vars

        public IntentoTranslationProviderOptionsForm(
            IntentoMTFormOptions options,
            LangPair[] languagePairs,
            Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric
            )
        {
            var splashForm = new IntentoFormSplash();
            splashForm.Show();
            this.Visible = false;
            this.fabric = fabric;

            InitializeComponent();
            LocalizeContent();


            buttonHelp.Visible = options.сallHelpAction != null;
            
            Assembly currentAssem = typeof(IntentoTranslationProviderOptionsForm).Assembly;
            version = String.Format("{0}-{1}",
                IntentoHelpers.GetVersion(currentAssem),
                IntentoHelpers.GetGitCommitHash(currentAssem));

            originalOptions = options;
            currentOptions = originalOptions.Duplicate();
            formAdvanced = new IntentoFormAdvanced(this);
            formApi = new IntentoFormOptionsAPI(this);
            formMT = new IntentoFormOptionsMT(this);
            apiKeyState = new ApiKeyState(this, currentOptions);
            if (apiKeyState.GetValueFromRegistry("ProxyEnabled") != null && apiKeyState.GetValueFromRegistry("ProxyEnabled") == "1")
            {
                currentOptions.proxySettings = new ProxySettings()
                {
                    ProxyAddress = apiKeyState.GetValueFromRegistry("ProxyAddress"),
                    ProxyPort = apiKeyState.GetValueFromRegistry("ProxyPort"),
                    ProxyUserName = apiKeyState.GetValueFromRegistry("ProxyUserName"),
                    ProxyPassword = apiKeyState.GetValueFromRegistry("ProxyPassw"),
                    ProxyEnabled = true
                };
            }

            _languagePairs = languagePairs;
            DialogResult = DialogResult.None;
            var arr = originalOptions.Signature.Split('/');
            formAdvanced.toolStripStatusLabel1.Text = arr.Count() == 3 ? String.Format("{0}/{1}", arr[0], arr[2]) : originalOptions.Signature;
            groupBoxMTConnect2.Location = groupBoxMTConnect.Location;

            if (!string.IsNullOrWhiteSpace(apiKeyState.apiKey))
            {
                apiKeyState.ReadProviders();
            }
            if (apiKeyState.IsOK)
            {
                buttonMTSetting.Select();
                apiKeyState.EnableDisable();
                FillOptions(currentOptions);
            }
            else
                buttonSetApi.Select();

            apiKeyState.EnableDisable();
            RefreshFormInfo();
            splashForm.Close();
            this.Visible = true;

        }

        public IntentoTranslationProviderOptionsForm(
            IntentoMTFormOptions options,
            LangPair[] languagePairs,
            Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric,
            bool testings
            )
        {
            this.fabric = fabric;

            InitializeComponent();
            LocalizeContent();

            originalOptions = options;
            currentOptions = originalOptions.Duplicate();

            formAdvanced = new IntentoFormAdvanced(this);
            formApi = new IntentoFormOptionsAPI(this);
            formMT = new IntentoFormOptionsMT(this);

            _languagePairs = languagePairs;
            DialogResult = DialogResult.None;

        }

        public IntentoMTFormOptions GetOptions()
        {
            return currentOptions;
        }

        public LangPair[] LanguagePairs
        { get { return _languagePairs; } }

        private void CreateIntentoConnection()
        {
            _translate = fabric(apiKeyState.apiKey, String.Format("{1}/{2}", originalOptions.UserAgent, "Intento.PluginSettingsForm", version), currentOptions.proxySettings);
        }

        public static bool IsTrace()
        {
            return (TraceEndTime - DateTime.Now).Minutes > 0;
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

        private bool filterBy(dynamic x, string lang)
        {
            if (x == null) return true;
            if (x.GetType().Name == "JArray")
                return ((JArray)x).Any(q => (string)q == lang);
            if (x.GetType().Name == "JValue")
                return (string)x == lang;
            return true;
        }

        #region events

        public void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
            {
                if (apiKeyState != null && apiKeyState.smartRoutingState != null && apiKeyState.smartRoutingState.providerState != null)
                    // Can happen during loading data from Options - constructor of ProviderState change settings in a list of providers
                    apiKeyState.smartRoutingState.providerState.SelectedIndexChanged();
            }
        }

        public void apiKey_tb_TextChanged(object sender, EventArgs e)
        {
            apiKeyState.SetValue(((Control)sender).Text.Trim());
            apiKeyState.EnableDisable();
        }

        public void checkBoxUseOwnCred_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
            {
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.checkBoxUseOwnCred_CheckedChanged();
                AuthState.internalControlChange = false;
            }
        }

        public void checkBoxUseCustomModel_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
            {
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetModelState()?.checkBoxUseCustomModel_CheckedChanged();
                ModelState.internalControlChange = false;
            }
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            using (new CursorForm(this))
            {
                apiKeyState.SaveValueToRegistry("ProxyEnabled", "0");
                if (currentOptions.proxySettings != null)
                {
                    if (currentOptions.proxySettings.ProxyEnabled)
                    {
                        apiKeyState.SaveValueToRegistry("ProxyAddress", currentOptions.proxySettings.ProxyAddress);
                        apiKeyState.SaveValueToRegistry("ProxyPort", currentOptions.proxySettings.ProxyPort);
                        apiKeyState.SaveValueToRegistry("ProxyUserName", currentOptions.proxySettings.ProxyUserName);
                        apiKeyState.SaveValueToRegistry("ProxyPassw", currentOptions.proxySettings.ProxyPassword);
                        apiKeyState.SaveValueToRegistry("ProxyEnabled", "1");
                    }
                }
                originalOptions.Translate = _translate;
                FillOptions(originalOptions);

                if (!currentOptions.ForbidSaveApikey)
                {
                    if (!string.IsNullOrEmpty(originalOptions.ApiKey))
                        apiKeyState.SaveValueToRegistry("ApiKey", originalOptions.ApiKey);
                    else
                        apiKeyState.SaveValueToRegistry("ApiKey", originalOptions.ApiKey);
                }
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        public void linkLabel_LinkClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(((Control)sender).Tag.ToString());
        }

        //private void linkLabel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        //{
        //    System.Windows.Forms.LinkLabel control = (System.Windows.Forms.LinkLabel)sender;
        //    SizeF stringSize = e.Graphics.MeasureString(control.Text, control.Font);
        //    control.Font = new Font(FontFamily.GenericSansSerif, control.Font.Size);
        //}

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            GetOptions().сallHelpAction?.Invoke();
        }

        public void buttonWizard_Click(object sender, EventArgs e)
        {
            apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.buttonWizard_Click();
        }

        public void modelControls_ValueChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
            {
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetModelState()?.modelControls_ValueChanged();
            }
        }

        public void comboBoxCredentialId_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.comboBoxCredentialId_SelectedIndexChanged();
        }

        public void checkBoxSmartRouting_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
                apiKeyState?.smartRoutingState?.CheckedChanged();
        }

        public void textBoxCredentials_Enter(object sender, EventArgs e)
        {
            buttonWizard_Click(null, null);
        }

        public void glossaryControls_ValueChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetGlossaryState()?.glossaryControls_ValueChanged();
        }

        private void buttonSetApi_Click(object sender, EventArgs e)
        {
            settingsIsSet = false;
            string apiKey = apiKeyState?.apiKey;
            formApi.currentOptions = currentOptions;
            formApi.ShowDialog();
            if (formApi.DialogResult == DialogResult.OK && apiKeyState.IsOK && apiKeyState.apiKey != apiKey)
            {
                settingsIsSet = true;
                RefreshFormInfo();
            }
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {
            formAdvanced.ShowDialog();
        }

        private void buttonMTSetting_Click(object sender, EventArgs e)
        {
            var smartRoutingState = apiKeyState.smartRoutingState;
            formMT.ShowDialog();
            using (new CursorForm(this))
            {
                if (formMT.DialogResult == DialogResult.OK)
                {
                    FillOptions(currentOptions);
                    settingsIsSet = false;
                    RefreshFormInfo();
                }
                else
                    apiKeyState.smartRoutingState = smartRoutingState;
            }
        }

        #endregion events

        public static void Logging(string subject, string comment = null, Exception ex = null)
        {
            if (!IntentoTranslationProviderOptionsForm.IsTrace())
                return;

            try
            {

                string path = Environment.GetEnvironmentVariable("temp");
                if (string.IsNullOrEmpty(path))
                    path = Environment.GetEnvironmentVariable("tmp");
                if (string.IsNullOrEmpty(path))
                    return;

                DateTime now = DateTime.UtcNow;
                List<string> content = new List<string>();
                content.Add("------------------------");
                content.Add(string.Format("{0} {1}", now.ToString("yyyy-MM-dd HH:mm:ss.fffff"), subject));
                if (comment != null)
                    content.Add(comment);
                if (ex != null)
                    content.AddRange(LoggingEx(ex));

                string filename = string.Format("{0}\\Intento_Logs_{1}", path, now.ToString("yyyy-MM-dd-HH"));
                File.AppendAllLines(filename, content);
            }
            catch { }
        }

        public static IEnumerable<string> LoggingEx(Exception ex)
        {
            List<string> items = new List<string>();
            items.Add(string.Format("Exception {0}", ex.Message));
            if (ex.StackTrace != null)
            {
                items.Add("Stack Trace:");
                items.Add(ex.StackTrace);
            }
            if (ex.InnerException != null)
                items.AddRange(LoggingEx(ex.InnerException));
            return items;
        }

        private void LocalizeContent()
        {
            groupBoxBillingAccount.Text = Resource.BillingAccount;
            groupBoxModel.Text = Resource.Model;
            groupBoxGlossary.Text = Resource.Glossary;
            buttonContinue.Text = Resource.MFbuttonClose;
            buttonMTSetting.Text = Resource.MFbuttonMTSetting;
            buttonSetApi.Text = Resource.EnterIntentoAPIKey;
            Text = Resource.MFcaption;
            groupBoxMTSettings.Text = Resource.MFgroupBoxMTSettings;
            labelRegister1.Text = Resource.MFlabelRegister1;
            labelRegister2.Text = Resource.MFlabelRegister2;
            buttonCheck.Text = Resource.MFbuttonCheck;
            labelIAK.Text = Resource.APIFlabelAPI;
            buttonAdvanced.Text = Resource.MFbuttonAdvanced;
            Icon = Resource.intento;
            labelApiKeyIsChanged.Text = Resource.MFlabelApiKeyIsChanged;
        }

        public class CursorForm : IDisposable
        {
            IntentoTranslationProviderOptionsForm form;
            public CursorForm(IntentoTranslationProviderOptionsForm form)
            {
                this.form = form;
                if (form.cursorCount == 0)
                    form.Cursor = Cursors.WaitCursor;
                form.cursorCount++;
            }

            public void Dispose()
            {
                form.cursorCount--;
                if (form.cursorCount == 0)
                    form.Cursor = Cursors.Default;
            }
        }

        public class CursorFormMT : IDisposable
        {
            IntentoFormOptionsMT form;
            public CursorFormMT(IntentoFormOptionsMT form)
            {
                this.form = form;
                if (form.cursorCountMT == 0)
                    form.Cursor = Cursors.WaitCursor;
                form.cursorCountMT++;
            }

            public void Dispose()
            {
                form.cursorCountMT--;
                if (form.cursorCountMT == 0)
                    form.Cursor = Cursors.Default;
            }
        }

        public void FillOptions(IntentoMTFormOptions options)
        {
            options.ForbidSaveApikey = currentOptions.ForbidSaveApikey;
            options.HideHiddenTextButton = currentOptions.HideHiddenTextButton;
            apiKeyState.FillOptions(options);
        }

        public void RefreshFormInfo()
        {
            SmartRoutingState smartRoutingState = apiKeyState?.smartRoutingState;
            buttonContinue.Enabled = false;
            labelApiKeyIsChanged.Visible = false;
            IntentoMTFormOptions tmpOptions = new IntentoMTFormOptions();
            apiKeyState.FillOptions(tmpOptions);
            if (smartRoutingState != null && smartRoutingState.SmartRouting)
            {
                textBoxAccount.UseSystemPasswordChar = false;
                textBoxProviderName.Text = Resource.MFSmartRoutingText;
                textBoxAccount.Text = Resource.MFNa;
                textBoxModel.Text = Resource.MFNa;
                textBoxGlossary.Text = Resource.MFNa;
                buttonContinue.Enabled = true;
                if (apiKeyState.IsOK)
                    apiKey_tb.Text = apiKeyState.apiKey;
            }
            else
            {
                if (apiKeyState.IsOK)
                {
                    apiKey_tb.Text = apiKeyState.apiKey;
                    if (String.IsNullOrEmpty(tmpOptions.ProviderId))
                        textBoxProviderName.Text = Resource.NeedAChoise;
                    else
                    {
                        textBoxProviderName.Text = tmpOptions.ProviderName;
                        buttonContinue.Enabled = true;
                    }
                }
                else
                    textBoxProviderName.Text = Resource.MFNa;

                if (currentOptions.AuthMode == StateModeEnum.prohibited || currentOptions.AuthMode == StateModeEnum.unknown)
                {
                    textBoxAccount.UseSystemPasswordChar = false;
                    textBoxAccount.Text = Resource.MFNa;
                }
                else if (String.IsNullOrEmpty(tmpOptions.CustomAuth))
                {
                    textBoxAccount.UseSystemPasswordChar = false;
                    textBoxAccount.Text = Resource.Empty;
                }
                else
                {
                    textBoxAccount.UseSystemPasswordChar = !tmpOptions.IsAuthDelegated;
                    textBoxAccount.Text = tmpOptions.IsAuthDelegated ? tmpOptions.AuthDelegatedCredentialId : tmpOptions.CustomAuth;
                    labelApiKeyIsChanged.Visible = settingsIsSet;
                }

                if (tmpOptions.CustomModelMode == StateModeEnum.prohibited || tmpOptions.CustomModelMode == StateModeEnum.unknown)
                    textBoxModel.Text = Resource.MFNa;
                else if (tmpOptions.CustomModelMode == StateModeEnum.optional && !tmpOptions.UseCustomModel)
                    textBoxModel.Text = Resource.Empty;
                else
                {
                    textBoxModel.Text = tmpOptions.CustomModelName;
                    labelApiKeyIsChanged.Visible = settingsIsSet;
                }
                if (tmpOptions.GlossaryMode == StateModeEnum.prohibited || tmpOptions.GlossaryMode == StateModeEnum.unknown)
                    textBoxGlossary.Text = Resource.MFNa;
                else
                {
                    textBoxGlossary.Text = String.IsNullOrEmpty(tmpOptions.GlossaryName) ? Resource.Empty : tmpOptions.GlossaryName;
                    labelApiKeyIsChanged.Visible = settingsIsSet;
                }
            }

        }

    }

}
