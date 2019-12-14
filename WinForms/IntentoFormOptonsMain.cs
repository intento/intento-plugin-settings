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
    public partial class IntentoTranslationProviderOptionsForm : Form, IForm
    {
        public class LangPair
        {
            public string _from;
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
        public IntentoMTFormOptions originalOptions;
        public IntentoMTFormOptions currentOptions;
        public IntentoAiTextTranslate _translate;

        // Languages filter 
        public IList<dynamic> languages;
        private LangPair[] _languagePairs;

        public static DateTime TraceEndTime;

        //private int numberOfFlashes;

        public ApiKeyState apiKeyState;

        List<string> errors;

        // Fabric to create intento connection. Parameters: apiKey and UserAgent for Settings Form 
        Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric;

        string version;

        private IntentoFormOptionsAPI formApi;
        private IntentoFormOptionsMT formMT;
        private IntentoFormAdvanced formAdvanced;
        private int cursorCount = 0;
        private bool settingsIsSet;

        #endregion vars

        public IntentoTranslationProviderOptionsForm(
            IntentoMTFormOptions options,
            LangPair[] languagePairs,
            Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric
            )
        {
            this.fabric = fabric;

            InitializeComponent();
            LocalizeContent();


            //if (options.HideHiddenTextButton)
            //    checkBoxShowHidden.Visible = false;
            //if (options.ForbidSaveApikey)
            //    checkBoxSaveApiKeyInRegistry.Visible = false;
            
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

            apiKeyState.apiKeyChangedEvent += ChangeApiKeyStatusDelegate;
            if (!string.IsNullOrWhiteSpace(apiKeyState.apiKey))
            {
                apiKeyState.ReadProviders();
            }
            if (apiKeyState.IsOK)
            {
                buttonMTSetting.Select();
                FillOptions(currentOptions);
            }
            else
                buttonSetApi.Select();

            apiKeyState.EnableDisable();
            //RefreshFormInfo();
        }

        public IntentoMTFormOptions GetOptions()
        {
            return currentOptions;
        }

        public LangPair[] LanguagePairs
        { get { return _languagePairs; } }

        public void ChangeApiKeyStatusDelegate(bool isOK)
        {
            if (apiKeyState.apiKeyStatus == ApiKeyState.EApiKeyStatus.download)
                CreateIntentoConnection();
            apiKeyState.EnableDisable();
        }

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

        #region paramount events 

        public void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
            {
                if (apiKeyState != null && apiKeyState.smartRoutingState != null && apiKeyState.smartRoutingState.providerState != null)
                    // Can happen during loading data from Options - constructor of ProviderState change settings in a list of providers
                    apiKeyState.smartRoutingState.providerState.SelectedIndexChanged();
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
        #endregion paramount events 

        #region events

        public void apiKey_tb_TextChanged(object sender, EventArgs e)
        {
            apiKeyState.SetValue(((Control)sender).Text.Trim());
            apiKeyState.EnableDisable();
        }

        public void checkBoxUseOwnCred_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.checkBoxUseOwnCred_CheckedChanged();
        }

        public void checkBoxUseCustomModel_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetModelState()?.checkBoxUseCustomModel_CheckedChanged();
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
                //IntentoMTFormOptions tmp = new IntentoMTFormOptions();
                //FillOptions(tmp);
                //if (tmp.IsEqualsOptions(currentOptions))
                //{

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
                //}
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

        public void buttonWizard_Click(object sender, EventArgs e)
        {
            apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.buttonWizard_Click();
            apiKeyState.EnableDisable();
        }

        public void comboBoxModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
            {
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetModelState()?.comboBoxModels_SelectedIndexChanged();
                apiKeyState.EnableDisable();
            }
        }

        public void comboBoxCredentialId_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(formMT))
                apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.comboBoxCredentialId_SelectedIndexChanged();
        }

        public void textBoxModel_TextChanged(object sender, EventArgs e)
        {
            apiKeyState.EnableDisable();
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

        private void IntentoFormOptonsMain_Shown(object sender, EventArgs e)
        {
            RefreshFormInfo();
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

        #region IForm

        ApiKeyState IForm.ApiKeyState { get { return apiKeyState; } }

        // Continue Button (buttonContinue)
        bool Continue_Button_Enabled
        {
            get => buttonContinue.Enabled;
            set => buttonContinue.Enabled = value;
        }

        // ApiKey textBox (apiKey_tb)
        string IForm.ApiKey_TextBox_Text { get { return formApi.apiKey_tb.Text; } set { formApi.apiKey_tb.Text = value; } }
        bool IForm.ApiKey_TextBox_Enabled { set { formApi.apiKey_tb.Enabled = value; } }
        Color IForm.ApiKey_TextBox_BackColor { set { formApi.apiKey_tb.BackColor = value; } }

        // ApiKey Check Button (buttonCheck)
        bool IForm.ApiKeyCheck_Button_Enabled { set { formApi.buttonSave.Enabled = value; } get { return formApi.buttonSave.Enabled; } }

        // Select panel "Connect to Intento MT HUB" on main form
        // -----------------------------------------
        void IForm.ApiKey_Set_Panel()
        {
            groupBoxMTConnect.Visible = !apiKeyState.IsOK;
            groupBoxMTConnect2.Visible = apiKeyState.IsOK;
            buttonMTSetting.Enabled = apiKeyState.IsOK;
            apiKey_tb.Text = apiKeyState.apiKey;
            buttonMTSetting.Enabled = apiKeyState.IsOK;
        }

        // SmartRouting_CheckBox (checkBoxSmartRouting)
        bool IForm.SmartRouting_CheckBox_Checked { get { return formMT.checkBoxSmartRouting.Checked; } set { formMT.checkBoxSmartRouting.Checked = value; } }
        bool IForm.SmartRouting_CheckBox_Visible { set { formMT.checkBoxSmartRouting.Visible = value; } }
        bool IForm.SmartRouting_CheckBox_Enabled { set { formMT.checkBoxSmartRouting.Enabled = value; } }

        // Providers_ComboBox (comboBoxProviders)
        void IForm.Providers_ComboBox_Clear() { formMT.comboBoxProviders.Items.Clear(); }
        void IForm.Providers_ComboBox_AddRange(object[] items) { formMT.comboBoxProviders.Items.AddRange(items); }
        string IForm.Providers_ComboBox_SelectedItem { set { formMT.comboBoxProviders.SelectedItem = value; } }
        string IForm.Providers_ComboBox_Text { get { return formMT.comboBoxProviders.Text; } }
        void IForm.Providers_ComboBox_ClearSelection()
        {
            formMT.comboBoxProviders.SelectedIndex = -1;
            formMT.comboBoxProviders.Text = null;
        }
        //Color IForm.Providers_ComboBox_BackColor { set { formMT.comboBoxProviders.BackColor = value; } }

        bool IForm.Providers_Group_Enabled { set { formMT.groupBoxProvider.Enabled = value; } }

        // -----------------------------------------------------
        void IForm.Providers_ComboBox_BackColor_State(bool hasErrors)
        {
            if (hasErrors)
                formMT.comboBoxProviders.BackColor = Color.LightPink;
            else
                formMT.comboBoxProviders.BackColor = formMT.groupBoxProvider.Enabled ? Color.White : SystemColors.Control;// .Window; ;
        }

        // Auth_CheckBox (checkBoxUseOwnCred)
        //        bool IForm.Auth_CheckBox_Visible { set { formMT.checkBoxUseOwnCred.Visible = value; } }
        bool IForm.Auth_CheckBox_Visible { set { formMT.groupBoxBillingAccount.Enabled = value; } }
        bool IForm.Auth_CheckBox_Enabled { set { formMT.checkBoxUseOwnCred.Enabled = value; } }
        bool IForm.Auth_GroupBox_Enabled { set { formMT.groupBoxBillingAccount.Enabled = value; } }
        bool IForm.Auth_CheckBox_Checked { get { return formMT.checkBoxUseOwnCred.Checked; } set { formMT.checkBoxUseOwnCred.Checked = value; } }

        // AuthText_Group (groupBoxAuth)
        bool IForm.AuthText_Group_Visible
        {
            set
            {
                formMT.groupBoxBillingAccount.Enabled = formMT.groupBoxBillingAccount.Enabled;
                formMT.textBoxCredentials.Visible = value;
                formMT.buttonWizard.Visible = value;
                formMT.comboBoxCredentialId.Visible = !value;
            }
        }
        void IForm.Auth_GroupBox_Disable()
        {
            formMT.groupBoxBillingAccount.Enabled = false;
            formMT.textBoxCredentials.Visible = false;
            formMT.buttonWizard.Visible = false;
            formMT.comboBoxCredentialId.Visible = true;
            formMT.checkBoxUseOwnCred.Checked = false;
            formMT.textBoxCredentials.Text = "";
            formMT.comboBoxCredentialId.Items.Clear();
        }

        // AuthText_TextBox (textBoxCredentials)
        //Color IForm.AuthText_TextBox_BackColor { set { formMT.textBoxCredentials.BackColor = value; } }
        string IForm.AuthText_TextBox_Text { set { formMT.textBoxCredentials.Text = value; } }

        // AuthCombo_Group (groupBoxAuthCredentialId)
        bool IForm.AuthCombo_Group_Visible { set { formMT.comboBoxCredentialId.Enabled = value; } }

        void IForm.Auth_Control_BackColor_State(bool hasErrors)
        {
            if (hasErrors)
            {
                formMT.comboBoxCredentialId.BackColor = Color.LightPink;
                formMT.textBoxCredentials.BackColor = Color.LightPink;
            }
            else
            {
                formMT.comboBoxCredentialId.BackColor = formMT.checkBoxUseOwnCred.Checked ? Color.White : SystemColors.Control;
                formMT.textBoxCredentials.BackColor = formMT.checkBoxUseOwnCred.Checked ? Color.White : SystemColors.Control;
            }
        }

        // AuthCombo_ComboBox (comboBoxCredentialId)
        void IForm.Auth_Control_Clear()
        {
            formMT.comboBoxCredentialId.Items.Clear();
            formMT.textBoxCredentials.Text = "";
        }
        void IForm.AuthCombo_ComboBox_AddRange(object[] items) { formMT.comboBoxCredentialId.Items.AddRange(items); }
        void IForm.AuthCombo_ComboBox_Insert(int n, string text) { formMT.comboBoxCredentialId.Items.Insert(n, text); }
        bool IForm.AuthCombo_ComboBox_Contains(string text) { return formMT.comboBoxCredentialId.Items.Contains(text); }
        object IForm.AuthCombo_ComboBox_SelectedItem { get { return formMT.comboBoxCredentialId.SelectedItem; } set { formMT.comboBoxCredentialId.SelectedItem = value; } }
        bool IForm.AuthCombo_ComboBox_Enabled { set { formMT.comboBoxCredentialId.Enabled = value; } }
        int IForm.AuthCombo_ComboBox_Count { get { return formMT.comboBoxCredentialId.Items.Count; } }
        int IForm.AuthCombo_ComboBox_SelectedIndex { set { formMT.comboBoxCredentialId.SelectedIndex = value; } }
        //Color IForm.AuthCombo_ComboBox_BackColor { set { formMT.comboBoxCredentialId.BackColor = value; } }
        string IForm.AuthCombo_ComboBox_Text { get { return formMT.comboBoxCredentialId.Text; } }

        // Model_CheckBox (checkBoxUseCustomModel)
        bool IForm.Model_CheckBox_Checked
        {
            get { return formMT.checkBoxUseCustomModel.Checked; }
            set
            {
                formMT.checkBoxUseCustomModel.Checked = value;
                formMT.comboBoxModels.Enabled = value;
                formMT.textBoxModel.Enabled = value;
            }
        }

        //bool IForm.Model_CheckBox_Visible { set { formMT.checkBoxUseCustomModel.Enabled = value; } }
        bool IForm.Model_CheckBox_Enabled
        {
            set
            {
                formMT.checkBoxUseCustomModel.Enabled = value;
                formMT.comboBoxModels.Enabled = value || formMT.checkBoxUseCustomModel.Checked;
                formMT.textBoxModel.Enabled = value || formMT.checkBoxUseCustomModel.Checked;
            }
        }

        bool IForm.Model_Group_Enabled
        {
            set
            {
                //if (!value)
                //    ((IForm)this).Model_Control_BackColor_State(false);
                formMT.groupBoxModel.Enabled = value;
                formMT.comboBoxModels.Enabled = formMT.checkBoxUseCustomModel.Checked;
                formMT.textBoxModel.Enabled = formMT.checkBoxUseCustomModel.Checked;
            }
        }

        void IForm.Model_GroupBox_Disable()
        {
            formMT.groupBoxModel.Enabled = false;
            formMT.comboBoxModels.Visible = false;
            formMT.textBoxModel.Visible = true;
            formMT.checkBoxUseCustomModel.Checked = false;
            formMT.textBoxModel.Text = "";
            formMT.comboBoxModels.Items.Clear();
        }


        // Model_ComboBox (comboBoxModels)
        void IForm.Model_ComboBox_Clear() { formMT.comboBoxModels.Items.Clear(); }
        int IForm.Model_ComboBox_Add(string text) { return formMT.comboBoxModels.Items.Add(text); }
        int IForm.Model_ComboBox_SelectedIndex { set { formMT.comboBoxModels.SelectedIndex = value; } }
        int IForm.Model_ComboBox_Count { get { return formMT.comboBoxModels.Items.Count; } }
        bool IForm.Model_ComboBox_Visible { set { formMT.comboBoxModels.Visible = value; } }
        //Color IForm.Model_ComboBox_BackColor { set { formMT.comboBoxModels.BackColor = value; } }
        string IForm.Model_ComboBox_Text { get { return formMT.comboBoxModels.Text; } set { formMT.comboBoxModels.Text = value; } }
        // -----------------------------------------------------
        void IForm.Model_Control_BackColor_State(bool hasErrors)
        {

            if (hasErrors)
            {
                formMT.comboBoxModels.BackColor = Color.LightPink;
                formMT.textBoxModel.BackColor = Color.LightPink;
            }
            else
            {
                formMT.comboBoxModels.BackColor = formMT.comboBoxModels.Enabled ? Color.White : SystemColors.Window;
                formMT.textBoxModel.BackColor = formMT.comboBoxModels.Enabled ? Color.White : SystemColors.Window;
            }
        }

        // Model_TextBox (textBoxModel)
        bool IForm.Model_TextBox_Visible { set { formMT.textBoxModel.Visible = value; } }
        string IForm.Model_TextBox_Text { get { return formMT.textBoxModel.Text; } set { formMT.textBoxModel.Text = value; } }
        //Color IForm.Model_TextBox_BackColor { set { formMT.textBoxModel.BackColor = value; } }

        // Glossary_Group (groupBoxGlossary)
        bool IForm.Glossary_Group_Visible { get { return formMT.groupBoxGlossary.Enabled; } set { formMT.groupBoxGlossary.Enabled = value; } }
        void IForm.Glossary_GroupBox_Disable()
        {
            formMT.groupBoxGlossary.Enabled = false;
            formMT.comboBoxGlossaries.Visible = false;
            formMT.textBoxGlossary.Visible = true;
            formMT.checkBoxUseCustomModel.Checked = false;
            formMT.textBoxGlossary.Text = "";
            formMT.comboBoxGlossaries.Items.Clear();
        }


        // Glossary_TextBox (textBoxGlossary)
        bool IForm.Glossary_TextBox_Visible { set { formMT.textBoxGlossary.Visible = value; } }
        bool IForm.Glossary_TextBox_Enabled { set { formMT.textBoxGlossary.Enabled = value; } }
        string IForm.Glossary_TextBox_Text { get { return formMT.textBoxGlossary.Text; } set { formMT.textBoxGlossary.Text = value; } }

        // Glossary_ComboBox (comboBoxGlossaries)
        void IForm.Glossary_ComboBox_Clear() { formMT.comboBoxGlossaries.Items.Clear(); }
        int IForm.Glossary_ComboBoxAdd(string text) { return formMT.comboBoxGlossaries.Items.Add(text); }
        void IForm.Glossary_ComboBox_Insert(int n, string text) { formMT.comboBoxGlossaries.Items.Insert(n, text); }
        int IForm.Glossary_ComboBox_SelectedIndex { set { formMT.comboBoxGlossaries.SelectedIndex = value; } }
        bool IForm.Glossary_ComboBox_Visible { set { formMT.comboBoxGlossaries.Visible = value; } }
        bool IForm.Glossary_ComboBox_Enabled { set { formMT.comboBoxGlossaries.Enabled = value; } }
        string IForm.Glossary_ComboBox_Text { get { return formMT.comboBoxGlossaries.Text; } }

        // Continue Button (buttonContinue)
        bool IForm.Continue_Button_Enabled { get { return formMT.buttonSave.Enabled; } set { formMT.buttonSave.Enabled = value; } }

        // ErrorMessage TextBox (toolStripStatusLabel1)
        //string IForm.ErrorMessage_TextBox_Text { get { return toolStripStatusLabel1.Text; } set { toolStripStatusLabel1.Text = value; } }
        //Color IForm.ErrorMessage_TextBox_BackColor { set { toolStripStatusLabel1.BackColor = value; } }
        string IForm.ErrorMessage_TextBox_Text { get { return formMT.labelTMP.Text; } set { formMT.labelTMP.Text = value; } }
        Color IForm.ErrorMessage_TextBox_BackColor { set { formMT.labelTMP.BackColor = value; } }

        void IForm.Language_Comboboxes_Fill(Dictionary<string, string> from, Dictionary<string, string> to)
        {
            formMT.comboBoxFrom.Items.Clear();
            formMT.comboBoxTo.Items.Clear();
            formMT.comboBoxFrom.Items.AddRange(from.Select(x => x.Value).ToArray());
            formMT.comboBoxTo.Items.AddRange(to.Select(x => x.Value).ToArray());
            if (from.ContainsKey("en"))
                formMT.comboBoxFrom.SelectedItem = from["en"];
            else
                formMT.comboBoxFrom.SelectedIndex = 1;
            if (to.ContainsKey("es"))
                formMT.comboBoxTo.SelectedItem = to["es"];
            else
                formMT.comboBoxTo.SelectedIndex = 1;
        }

        bool IForm.Optional_Group_Enabled
        {
            get { return formMT.groupBoxOptional.Enabled; }
            set
            {
                if (value)
                    formMT.groupBoxOptional.Enabled = value;
                else
                {
                    formMT.groupBoxOptional.Enabled = apiKeyState.options.UseCustomModel
                        || !string.IsNullOrWhiteSpace(apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetGlossaryState()?.currentGlossary);
                }
            }
        }


        // SaveApiKeyInRegistry checkBox ()
        bool IForm.SaveApiKeyInRegistry_CheckBox_Checked { get { return formAdvanced.checkBoxSaveApiKeyInRegistry.Checked; } set { formAdvanced.checkBoxSaveApiKeyInRegistry.Checked = value; } }

        // ShowHidden_CheckBox (checkBoxShowHidden)
        bool IForm.ShowHidden_CheckBox_Checked { get { return formApi.checkBoxShowHidden.Checked; } }

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
        bool IForm.ButtonContinue_Button_Enabled { get { return buttonContinue.Enabled; } set { buttonContinue.Enabled = value; } }

        bool insideEnableDisable = false;
        bool IForm.InsideEnableDisable { get { return insideEnableDisable; } set { insideEnableDisable = value; } }

        public static ResourceManager resourceManager = new ResourceManager(typeof(Resource));
        ResourceManager IForm.ResourceManager { get { return resourceManager; } }
        IntentoFormOptionsMT IForm.FormMT { get { return formMT; } }

        #endregion IForm

        private void FillOptions(IntentoMTFormOptions options)
        {
            options.ForbidSaveApikey = currentOptions.ForbidSaveApikey;
            options.HideHiddenTextButton = currentOptions.HideHiddenTextButton;
            apiKeyState.FillOptions(options);
        }

        private void RefreshFormInfo()
        {
            SmartRoutingState smartRoutingState = apiKeyState?.smartRoutingState;
            buttonContinue.Enabled = false;
            labelApiKeyIsChanged.Visible = false;
            IntentoMTFormOptions tmpOptions = new IntentoMTFormOptions();
            apiKeyState.FillOptions(tmpOptions);
            if (smartRoutingState != null && smartRoutingState.SmartRouting)
            {
                textBoxProviderName.Text = Resource.MFSmartRoutingText;
                textBoxAccount.Text = Resource.MFNa;
                textBoxModel.Text = Resource.MFNa;
                textBoxGlossary.Text = Resource.MFNa;
                buttonContinue.Enabled = true;
            }
            else
            {
                if (apiKeyState.IsOK)
                {
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
