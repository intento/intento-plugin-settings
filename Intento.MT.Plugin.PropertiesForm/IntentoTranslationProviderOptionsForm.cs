using IntentoSDK;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Intento.MT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm
{

    public partial class IntentoTranslationProviderOptionsForm1 : Form, IForm
    {

        #region vars
        public IntentoMTFormOptions originalOptions;
        public IntentoMTFormOptions currentOptions;
        public IntentoAiTextTranslate _translate;

        // Languages filter 
        public IList<dynamic> languages;
        private LangPair[] _languagePairs;

        public static DateTime TraceEndTime;

        private int numberOfFlashes;

        public ApiKeyState apiKeyState;

        List<string> errors;

        // Fabric to create intento connection. Parameters: apiKey and UserAgent for Settings Form 
        Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric;

        string version;

        #endregion vars

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="languagePairs"></param>
        /// <param name="fabric"></param>
        public IntentoTranslationProviderOptionsForm1(
            IntentoMTFormOptions options, 
            LangPair[] languagePairs, 
            Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric
            )
        {
            this.fabric = fabric;

            InitializeComponent();
            LocalizeContent();

            if (options.HideHiddenTextButton)
                checkBoxShowHidden.Visible = false;
            if (options.ForbidSaveApikey)
                checkBoxSaveApiKeyInRegistry.Visible = false;

            Assembly currentAssem = typeof(IntentoTranslationProviderOptionsForm1).Assembly;
            version = String.Format("{0}-{1}",
                IntentoHelpers.GetVersion(currentAssem),
                IntentoHelpers.GetGitCommitHash(currentAssem));

            originalOptions = options;
            currentOptions = originalOptions.Duplicate();
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
            else
                checkBoxProxy.Checked = false;

            checkBoxProxy.CheckedChanged += checkBoxProxy_CheckedChanged;

            _languagePairs = languagePairs;
            DialogResult = DialogResult.None;

            var tmp = TraceEndTime;
            checkBoxTrace.Checked = (TraceEndTime - DateTime.Now).Minutes > 0;
            TraceEndTime = tmp;
            // string pluginFor = string.IsNullOrEmpty(Options.PluginFor) ? "" : Options.PluginFor + '/';
            // toolStripStatusLabel2.Text = String.Format("{0} {1}{2}", Options.PluginName, pluginFor, Options.AssemblyVersion);
            var arr = originalOptions.Signature.Split('/');
            toolStripStatusLabel2.Text = arr.Count()==3 ? String.Format("{0}/{1}",arr[0], arr[2]) : originalOptions.Signature;
            textBoxModel.Location = comboBoxModels.Location; // new Point(comboBoxModels.Location.X, comboBoxModels.Location.Y);
            textBoxGlossary.Location = comboBoxGlossaries.Location; // new Point(comboBoxGlossaries.Location.X, comboBoxGlossaries.Location.Y);
            groupBoxAuthCredentialId.Location = groupBoxAuth.Location; // new Point(groupBoxAuth.Location.X, groupBoxAuth.Location.Y)

            apiKeyState.apiKeyChangedEvent += ChangeApiKeyStatusDelegate;
            apiKey_tb.Select();

            apiKeyState.EnableDisable();
        }

        public IntentoMTFormOptions GetOptions()
        {
            return currentOptions;
        }

        public LangPair[] LanguagePairs
        {  get { return _languagePairs; } }

        public void ChangeApiKeyStatusDelegate(bool isOK)
        {
            if (apiKeyState.apiKeyStatus == ApiKeyState.EApiKeyStatus.download)
            {
                CreateIntentoConnection();
            }
            apiKeyState.EnableDisable();
        }

        private void CreateIntentoConnection()
        {
            _translate = fabric(apiKeyState.apiKey, String.Format("{1}/{2}", originalOptions.UserAgent, "Intento.PluginSettingsForm", version), currentOptions.proxySettings);
        }

        private void apiKey_tb_TextChanged(object sender, EventArgs e)
        {
            apiKeyState.SetValue(apiKey_tb.Text.Trim());
            apiKeyState.EnableDisable();
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

        private void LocalizeContent()
        {
            Text = Resource.MFCaption;
            label2.Text = Resource.MFLabel2;
            label3.Text = Resource.MFLabel3;
            checkBoxProxy.Text = Resource.MFcheckBoxProxy;
            label5.Text = Resource.ApiKeyLabel;
            buttonCheck.Text = Resource.MFButtonCheck;
            checkBoxSmartRouting.Text = Resource.MFCheckBoxSmartRouting;
            groupBoxProviderSettings.Text = Resource.MFGroupBoxProviderSettings;
            label1.Text = Resource.ProviderLabel;
            groupBoxAuthCredentialId.Text = Resource.MFGroupBoxAuthCredentialId;
            checkBoxUseOwnCred.Text = Resource.MFCheckBoxUseOwnCred;
            groupBoxAuth.Text = Resource.MFGroupBoxAuth;
            buttonWizard.Text = Resource.MFButtonWizard;
            checkBoxUseCustomModel.Text = Resource.MFCheckBoxUseCustomModel;
            groupBoxModel.Text = Resource.MFGroupBoxModel;
            groupBoxGlossaries.Text = Resource.MFGroupBoxGlossary;
            buttonContinue.Text = Resource.OKLabel;
            checkBoxSaveApiKeyInRegistry.Text = Resource.MFCheckBoxSaveApiKeyInRegistry;
            checkBoxShowHidden.Text = Resource.ShowHiddenTextLabel;
            checkBoxTrace.Text = Resource.MFCheckBoxTrace;

            checkBoxShowHidden.Text = Resource.ShowHiddenTextLabel;
        }

        #region paramount events 

        private void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (apiKeyState != null && apiKeyState.smartRoutingState != null && apiKeyState.smartRoutingState.providerState != null)
                // Can happen during loading data from Options - constructor of ProviderState change settings in a list of providers
                apiKeyState.smartRoutingState.providerState.SelectedIndexChanged();
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

        #endregion paramount events 

        #region events

        private void IntentoTranslationProviderOptionsForm_Shown(object sender, EventArgs e)
        {
            using (new CursorForm(this))
            {
                checkBoxSmartRouting.Checked = GetOptions().SmartRouting;
                apiKeyState.ReadProviders();
                CreateIntentoConnection();
            }
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            using (new CursorForm(this))
            { 
                apiKeyState.ReadProviders();
                apiKeyState.EnableDisable();
            }
        }

        private void checkBoxUseOwnCred_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorForm(this))
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.checkBoxUseOwnCred_CheckedChanged();
        }

        private void checkBoxUseCustomModel_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorForm(this))
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetModelState()?.checkBoxUseCustomModel_CheckedChanged();
        }

        // Save settings
        private void buttonContinue_Click(object sender, EventArgs e)
        {
            using (new CursorForm(this))
            {
                originalOptions.Translate = _translate;
                apiKeyState.FillOptions(originalOptions);

                // originalOptions.StoreApikeyInRegistry = checkBoxSaveApiKeyInRegistry.Checked;
                if (!string.IsNullOrEmpty(originalOptions.ApiKey))
                    apiKeyState.SaveValueToRegistry("ApiKey", originalOptions.ApiKey);
                else
                    apiKeyState.SaveValueToRegistry("ApiKey", originalOptions.ApiKey);

                Close();
            }
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(((LinkLabel)sender).Text);
        }

        private void linkLabel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            System.Windows.Forms.LinkLabel control = (System.Windows.Forms.LinkLabel)sender;
            SizeF stringSize = e.Graphics.MeasureString(control.Text, control.Font);
            control.Font = new Font(FontFamily.GenericSansSerif, control.Font.Size);
        }

        private void buttonWizard_Click(object sender, EventArgs e)
        {
            apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.buttonWizard_Click();
            apiKeyState.EnableDisable();
        }

        private void comboBoxModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorForm(this))
            {
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetModelState()?.comboBoxModels_SelectedIndexChanged();
                apiKeyState.EnableDisable();
            }
        }

        private void checkBoxShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorForm(this))
            {
                apiKey_tb.UseSystemPasswordChar = !checkBoxShowHidden.Checked;
                textBoxCredentials.UseSystemPasswordChar = !checkBoxShowHidden.Checked;
            }
        }

        private void checkBoxTrace_CheckedChanged(object sender, EventArgs e)
        {
            TraceEndTime = DateTime.Now.AddMinutes(checkBoxTrace.Checked ? 30 : -40);
        }

        private void IntentoTranslationProviderOptionsForm_Load(object sender, EventArgs e)
        {
        }

        private void comboBoxCredentialId_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorForm(this))
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.comboBoxCredentialId_SelectedIndexChanged();
        }

        private void textBoxModel_TextChanged(object sender, EventArgs e)
        {
            apiKeyState.EnableDisable();
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
            using (new CursorForm(this))
                apiKeyState?.smartRoutingState?.CheckedChanged();
        }

        private void textBoxCredentials_Enter(object sender, EventArgs e)
        {
            buttonWizard_Click(null, null);
        }

        private void checkBoxProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxProxy.Checked)
            {
                var form = new IntentoTranslationProviderProxySettingsForm(this);
                if (form.ShowDialog() == DialogResult.OK)
                {

                    apiKeyState.SaveValueToRegistry("ProxyAddress", currentOptions.proxySettings.ProxyAddress);
                    apiKeyState.SaveValueToRegistry("ProxyPort", currentOptions.proxySettings.ProxyPort);
                    apiKeyState.SaveValueToRegistry("ProxyUserName", currentOptions.proxySettings.ProxyUserName);
                    apiKeyState.SaveValueToRegistry("ProxyPassw", currentOptions.proxySettings.ProxyPassword);
                    apiKeyState.SaveValueToRegistry("ProxyEnabled", "1");
                    apiKeyState.ReadProviders();
                }
                else
                {
                    checkBoxProxy.Checked = false;
                    apiKeyState.SaveValueToRegistry("ProxyEnabled", "0");
                }
            }
            else
            {
                currentOptions.proxySettings = null;
                apiKeyState.SaveValueToRegistry("ProxyEnabled", "0");
                apiKeyState.ReadProviders();
            }
        }

        private void checkBoxSaveApiKeyInRegistry_CheckedChanged(object sender, EventArgs e)
        {
        }
        #endregion events

        public static void Logging(string subject, string comment = null, Exception ex = null)
        {
            if (!IntentoTranslationProviderOptionsForm1.IsTrace())
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

        private void textBoxLabel_Enter(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start(((TextBox)sender).Text);
        }

        private int cursorCount = 0;
        public class CursorForm : IDisposable
        {
            IForm form;
            public CursorForm(IForm form)
            {
                this.form = form;
                if (form.CursorCount == 0)
                    form.Cursor = Cursors.WaitCursor;
                form.CursorCount++;
            }

            public void Dispose()
            {
                form.CursorCount--;
                if (form.CursorCount == 0)
                    form.Cursor = Cursors.Default;
            }
        }

        #region IForm
        ApiKeyState IForm.ApiKeyState { get { return apiKeyState; } }

        // ApiKey textBox (apiKey_tb)
        string IForm.ApiKey_TextBox_Text { get { return apiKey_tb.Text; } set { apiKey_tb.Text = value; } }
        bool IForm.ApiKey_TextBox_Enabled { set { apiKey_tb.Enabled = value; } }
        Color IForm.ApiKey_TextBox_BackColor { set { apiKey_tb.BackColor = value; } }


        // ApiKey Check Button (buttonCheck)
        bool IForm.ApiKeyCheck_Button_Enabled { set { buttonCheck.Enabled = value; } get { return buttonCheck.Enabled; } }

        // SmartRouting_CheckBox (checkBoxSmartRouting)
        bool IForm.SmartRouting_CheckBox_Checked { get { return checkBoxSmartRouting.Checked; } set { checkBoxSmartRouting.Checked = value; } }
        bool IForm.SmartRouting_CheckBox_Visible { set { checkBoxSmartRouting.Visible = value; } }
        bool IForm.SmartRouting_CheckBox_Enabled { set { checkBoxSmartRouting.Enabled = value; } }

        // Providers_ComboBox (comboBoxProviders)
        void IForm.Providers_ComboBox_Clear() { comboBoxProviders.Items.Clear(); }
        void IForm.Providers_ComboBox_AddRange(object[] items) { comboBoxProviders.Items.AddRange(items); }
        string IForm.Providers_ComboBox_SelectedItem { set { comboBoxProviders.SelectedItem = value; } }
        string IForm.Providers_ComboBox_Text { get { return comboBoxProviders.Text; } }
        //Color IForm.Providers_ComboBox_BackColor { set { comboBoxProviders.BackColor = value; } }

        void IForm.Providers_ComboBox_BackColor_State(bool hasErrors)
        {
            if (hasErrors)
                comboBoxProviders.BackColor = Color.LightPink;
            else
                comboBoxProviders.BackColor = Color.White;
        }

        // Providers_GroupBox (groupBoxProviderSettings)
        bool IForm.Providers_Group_Enabled { set { groupBoxProviderSettings.Enabled = value; } }

        // Auth_CheckBox (checkBoxUseOwnCred)
        bool IForm.Auth_CheckBox_Visible { set { checkBoxUseOwnCred.Visible = value; } }
        bool IForm.Auth_CheckBox_Enabled { set { checkBoxUseOwnCred.Enabled = value; } }
        bool IForm.Auth_CheckBox_Checked { get { return checkBoxUseOwnCred.Checked; } set { checkBoxUseOwnCred.Checked = value; } }

        // AuthText_Group (groupBoxAuth)
        bool IForm.AuthText_Group_Visible { set { groupBoxAuth.Visible = value; } }

        // AuthText_TextBox (textBoxCredentials)
        Color IForm.AuthText_TextBox_BackColor { set { textBoxCredentials.BackColor = value; } }
        string IForm.AuthText_TextBox_Text { set { textBoxCredentials.Text = value; } }

        // AuthCombo_Group (groupBoxAuthCredentialId)
        bool IForm.AuthCombo_Group_Visible { set { groupBoxAuthCredentialId.Visible = value; } }

        // AuthCombo_ComboBox (comboBoxCredentialId)
        void IForm.AuthCombo_ComboBox_Clear() { comboBoxCredentialId.Items.Clear(); }
        void IForm.AuthCombo_ComboBox_AddRange(object[] items) { comboBoxCredentialId.Items.AddRange(items); }
        void IForm.AuthCombo_ComboBox_Insert(int n, string text) { comboBoxCredentialId.Items.Insert(n, text); }
        bool IForm.AuthCombo_ComboBox_Contains(string text) { return comboBoxCredentialId.Items.Contains(text); }
        object IForm.AuthCombo_ComboBox_SelectedItem { get { return comboBoxCredentialId.SelectedItem; } set { comboBoxCredentialId.SelectedItem = value; } }
        bool IForm.AuthCombo_ComboBox_Enabled { set { comboBoxCredentialId.Enabled = value; } }
        int IForm.AuthCombo_ComboBox_Count { get { return comboBoxCredentialId.Items.Count; } }
        int IForm.AuthCombo_ComboBox_SelectedIndex { set { comboBoxCredentialId.SelectedIndex = value; } }
        Color IForm.AuthCombo_ComboBox_BackColor { set { comboBoxCredentialId.BackColor = value; } }
        string IForm.AuthCombo_ComboBox_Text { get { return comboBoxCredentialId.Text; } }

        // Model_CheckBox (checkBoxUseCustomModel)
        bool IForm.Model_CheckBox_Checked { get { return checkBoxUseCustomModel.Checked; } set { checkBoxUseCustomModel.Checked = value; } }
        //bool IForm.Model_CheckBox_Visible { set { checkBoxUseCustomModel.Visible = value; } }
        bool IForm.Model_CheckBox_Enabled { set { checkBoxUseCustomModel.Enabled = value; } }

        // Model_Group (groupBoxModel)
        //bool IForm.Model_Group_Visible { set { groupBoxModel.Visible = value; } }
        bool IForm.Model_Group_Enabled { set { groupBoxModel.Enabled = value; } }

        // Model_ComboBox (comboBoxModels)
        void IForm.Model_ComboBox_Clear() { comboBoxModels.Items.Clear(); }
        int IForm.Model_ComboBox_Add(string text) { return comboBoxModels.Items.Add(text); }
        int IForm.Model_ComboBox_SelectedIndex { set { comboBoxModels.SelectedIndex = value; } }
        int IForm.Model_ComboBox_Count { get { return comboBoxModels.Items.Count; } }
        bool IForm.Model_ComboBox_Visible { set { comboBoxModels.Visible = value; } }
        //Color IForm.Model_ComboBox_BackColor { set { comboBoxModels.BackColor = value; } }
        string IForm.Model_ComboBox_Text { get { return comboBoxModels.Text; } set { comboBoxModels.Text = value; } }
        void IForm.Model_Control_BackColor_State(bool hasErrors)
        {
            if (hasErrors)
            {
                comboBoxModels.BackColor = Color.LightPink;
                textBoxModel.BackColor = Color.LightPink;
            }
            else
            {
                comboBoxModels.BackColor = Color.White;
                textBoxModel.BackColor = Color.White;
            }
        }

        // Model_TextBox (textBoxModel)
        bool IForm.Model_TextBox_Visible { set { textBoxModel.Visible = value; } }
        string IForm.Model_TextBox_Text { get { return textBoxModel.Text; } set { textBoxModel.Text = value; } }
        //Color IForm.Model_TextBox_BackColor { set { textBoxModel.BackColor = value; } }

        // Glossary_Group (groupBoxGlossary)
        bool IForm.Glossary_Group_Visible { get { return groupBoxGlossaries.Visible; } set { groupBoxGlossaries.Visible = value; } }

        // Glossary_TextBox (textBoxGlossary)
        bool IForm.Glossary_TextBox_Visible { set { textBoxGlossary.Visible = value; } }
        bool IForm.Glossary_TextBox_Enabled { set { textBoxGlossary.Enabled = value; } }
        string IForm.Glossary_TextBox_Text { get { return textBoxGlossary.Text; } set { textBoxGlossary.Text = value; } }

        // Glossary_ComboBox (comboBoxGlossaries)
        void IForm.Glossary_ComboBox_Clear() { comboBoxGlossaries.Items.Clear(); }
        int IForm.Glossary_ComboBoxAdd(string text) { return comboBoxGlossaries.Items.Add(text); }
        void IForm.Glossary_ComboBox_Insert(int n, string text) { comboBoxGlossaries.Items.Insert(n, text); }
        int IForm.Glossary_ComboBox_SelectedIndex { set { comboBoxGlossaries.SelectedIndex = value; } }
        bool IForm.Glossary_ComboBox_Visible { set { comboBoxGlossaries.Visible = value; } }
        bool IForm.Glossary_ComboBox_Enabled { set { comboBoxGlossaries.Enabled = value; } }
        string IForm.Glossary_ComboBox_Text { get { return comboBoxGlossaries.Text; } }

        // Continue Button (buttonContinue)
        bool IForm.Continue_Button_Enabled { get { return buttonContinue.Enabled; } set { buttonContinue.Enabled = value; } }

        // ErrorMessage TextBox (toolStripStatusLabel1)
        string IForm.ErrorMessage_TextBox_Text { get { return toolStripStatusLabel1.Text; } set { toolStripStatusLabel1.Text = value; } }
        Color IForm.ErrorMessage_TextBox_BackColor { set { toolStripStatusLabel1.BackColor = value; } }

        // SaveApiKeyInRegistry checkBox ()
        bool IForm.SaveApiKeyInRegistry_CheckBox_Checked { get { return checkBoxSaveApiKeyInRegistry.Checked; } set { checkBoxSaveApiKeyInRegistry.Checked = value; } }

        // ShowHidden_CheckBox (checkBoxShowHidden)
        bool IForm.ShowHidden_CheckBox_Checked { get { return checkBoxShowHidden.Checked; } }

        // Intento API
        IEnumerable<dynamic> IForm.Providers(Dictionary<string, string> filter) { return _translate.Providers(filter: filter); }
        dynamic IForm.Provider(string provider, string additionalParams) { return _translate.Provider(provider: provider, additionalParams: additionalParams); }
        IList<dynamic> IForm.DelegatedCredentials() { return _translate.DelegatedCredentials(); }
        IList<dynamic> IForm.Models(string provider, Dictionary<string, string> credential_id) { return _translate.Models(provider: provider, credentials: credential_id); }
        IList<dynamic> IForm.Glossaries(string provider, Dictionary<string, string> credential_id) { return _translate.Glossaries(provider: provider, credentials: credential_id); }

        // Other
        int IForm.CursorCount { get { return cursorCount; } set { cursorCount = value; } }
        Cursor IForm.Cursor { set { Cursor = value; } }
        void IForm.SuspendLayout() { SuspendLayout(); }
        void IForm.ResumeLayout() { ResumeLayout(); }
        List<string> IForm.Errors { get { return errors; } set { errors = value; } }
        LangPair[] IForm.LanguagePairs { get { return _languagePairs; } }

        bool insideEnableDisable = false;
        bool IForm.InsideEnableDisable { get { return insideEnableDisable; } set { insideEnableDisable = value; } }

        public static ResourceManager resourceManager = new ResourceManager(typeof(Resource));
        ResourceManager IForm.ResourceManager { get { return resourceManager; } }



        // Added for NewUI
        void IForm.ApiKey_Set_Panel() { }


            #endregion

            private void buttonWizard_EnabledChanged(object sender, EventArgs e)
        {

        }
    }
}
