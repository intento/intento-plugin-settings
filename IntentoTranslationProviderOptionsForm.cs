using Intento.MT.Plugin.PropertiesForm;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntentoMT.Plugin.PropertiesForm
{
    // Version history
    // 1.0.0: 2019-03-13
    //   - A separate solution has been created for the plugin configuration form.
    // 1.0.1: 2019-04-05
    //   - Supports Smart Routing setting
    // 1.2.4: 2019-05-21
    // - List of providers is requested now in sync mode to get real format options to send translate request with th best html or xml option
    // 1.3.0: 
    // - Smart routing
    // - Refactoring form processing
    // 1.3.1: 2019-06-10
    // - Local logs
    // 1.3.3: 2019-06-18
    // - Using GitHub submodules to assemble result
    // 1.3.4: 2019-06-25
    // - Bug with extracting version from dll
    // 1.3.5: 2019-06-26
    // - Bug with visiblity of credential_if list
    // 1.3.6: 2019-07-02
    // - waitAsyncDelay
    // 1.3.7: 2019-07-02
    // - The version in useragent now has a commit hash in git


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
        public IntentoMTFormOptions originalOptions;
        public IntentoMTFormOptions currentOptions;
        public IntentoAiTextTranslate _translate;

        // Languages filter 
        public IList<dynamic> languages;
        private LangPair[] _languagePairs;

        public static DateTime TraceEndTime;

        private int numberOfFlashes;

        public ApiKeyState apiKeyState;
        public SmartRoutingState smartRoutingState;
        public ProviderState providerState;

        List<string> errors;

        // Fabric to create intento connection. Parameters: apiKey and UserAgent for Settings Form 
        Func<string, string, IntentoAiTextTranslate> fabric;

        string version;

        #endregion vars

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="languagePairs"></param>
        /// <param name="fabric"></param>
        public IntentoTranslationProviderOptionsForm(
            IntentoMTFormOptions options, 
            LangPair[] languagePairs, 
            Func<string, string, IntentoAiTextTranslate> fabric
            )
        {
            this.fabric = fabric;

            InitializeComponent();

            Assembly currentAssem = typeof(IntentoTranslationProviderOptionsForm).Assembly;
            version = String.Format("{0}-{1}",
                IntentoHelpers.GetVersion(currentAssem),
                IntentoHelpers.GetGitCommitHash(currentAssem));

            originalOptions = options;
            currentOptions = originalOptions;
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

            apiKeyState = new ApiKeyState(this, currentOptions);
            apiKeyState.apiKeyChangedEvent += ChangeApiKeyStatusDelegate;
            apiKey_tb.Select();

            EnableDisable();
        }

        public IntentoMTFormOptions GetOptions()
        {
            if (providerState == null || !providerState.IsOK || smartRoutingState.SmartRouting)
                return currentOptions;
            if (providerState.currentProviderId == currentOptions.ProviderId && apiKeyState.apiKey == currentOptions.ApiKey)
                return currentOptions;

            // return to original
            if (providerState.currentProviderId == originalOptions.ProviderId && apiKeyState.apiKey == originalOptions.ApiKey)
            {
                currentOptions = originalOptions;
                return currentOptions;
            }

            currentOptions = new IntentoMTFormOptions()
            {
                ApiKey = apiKeyState.apiKey,
                ProviderId = providerState.currentProviderId
            };
            return currentOptions;
        }

        public LangPair[] LanguagePairs
        {  get { return _languagePairs; } }

        public void ChangeApiKeyStatusDelegate(bool isOK)
        {
            if (isOK)
            {
                smartRoutingState = new SmartRoutingState(this, GetOptions());
                providerState = new ProviderState(this, GetOptions());
            }
            else if (apiKeyState.apiKeyStatus == ApiKeyState.EApiKeyStatus.download)
            {
                CreateIntentoConnection();

                providerState = null;
            }
            else
            {
                providerState = null;
            }
            EnableDisable();
        }

        private void CreateIntentoConnection()
        {
            _translate = fabric(apiKeyState.apiKey, String.Format("{1}/{2}", originalOptions.UserAgent, "Intento.PluginSettingsForm", version));
        }

        private void apiKey_tb_TextChanged(object sender, EventArgs e)
        {
            apiKeyState.SetValue(apiKey_tb.Text.Trim());
            EnableDisable();
        }

        public static bool IsTrace()
        {
            return (TraceEndTime - DateTime.Now).Minutes > 0;
        }

        #region custom helper methods
        public void ConnectIntento()
        {
            apiKeyState.Validate();
            EnableDisable();
        }

        bool insideEnableDisable = false;
        public void EnableDisable()
        {
            if (insideEnableDisable)
                return;

            try
            {
                insideEnableDisable = true;
                errors = new List<string>();

                errors.Add(apiKeyState.Draw());
                errors.Add(SmartRoutingState.Draw(this, smartRoutingState));
                errors.Add(ProviderState.Draw(this, providerState));

            }
            finally
            {
                insideEnableDisable = false;
            }
            ShowErrorMessage();
            this.ResumeLayout();
        }

        private bool ShowErrorMessage()
        {
            errors = errors.Where(i => i != null).ToList();
            if ((errors == null || errors.Count == 0))
            {
                buttonContinue.Enabled = true;
                setErrorMessage();
                return true;
            }
            else
            {
                setErrorMessage(string.Join(", ", errors.Where(i => i != null)));
                buttonContinue.Enabled = false;
                return false;
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

        #endregion custom helper methods

        #region paramount events 

        private void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            providerState.SelectedIndexChanged();
            EnableDisable();
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
            checkBoxSmartRouting.Checked = GetOptions().SmartRouting;
            ConnectIntento();
            EnableDisable();
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            apiKeyState.Validate();
            EnableDisable();
        }

        private void checkBoxUseOwnCred_CheckedChanged(object sender, EventArgs e)
        {
            AuthState.checkBoxUseOwnCred_CheckedChanged(providerState, GetOptions());
        }

        private void checkBoxUseCustomModel_CheckedChanged(object sender, EventArgs e)
        {
            ModelState.checkBoxUseCustomModel_CheckedChanged(providerState.authState, currentOptions);
        }

        // Save settings
        private void buttonContinue_Click(object sender, EventArgs e)
        {
            originalOptions.Translate = _translate;
            apiKeyState.FillOptions(originalOptions);
            smartRoutingState.FillOptions(originalOptions);
            providerState.FillOptions(originalOptions);

            // originalOptions.StoreApikeyInRegistry = checkBoxSaveApiKeyInRegistry.Checked;
            if (!string.IsNullOrEmpty(originalOptions.ApiKey))
                SaveValueToRegistry("ApiKey", originalOptions.ApiKey);
            else
                SaveValueToRegistry("ApiKey", originalOptions.ApiKey);

            Close();
        }

        public string GetValueFromRegistry(string name)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento\\{0}", originalOptions.AppName));
                return (string)key.GetValue(name, null);
            }
            catch { }
            return null;
        }
        public void SaveValueToRegistry(string name, string value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento\\{0}", originalOptions.AppName));
                key.SetValue(name, value);
            }
            catch { }
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(((LinkLabel)sender).Text);
        }

        private void linkLabel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            System.Windows.Forms.LinkLabel control = (System.Windows.Forms.LinkLabel)sender;
            SizeF stringSize = e.Graphics.MeasureString(control.Text, control.Font);
            // control.Size = new Size((int)(stringSize.Width + 0.99), (int)(stringSize.Height + 0.99));
            control.Font = new Font(FontFamily.GenericSansSerif, control.Font.Size);
            // control.Width = (int)(stringSize.Width + 0.99);
            // control.Height = (int)(stringSize.Height + 0.99);
        }

        // private void textBox_TextChanged(object sender, EventArgs e)
        // {   // text box in auth WizardForm
        // label7.Text = textBoxCredentials.Text;
        // EnableDisable();
        // }

        private void buttonWizard_Click(object sender, EventArgs e)
        {
            providerState.authState.buttonWizard_Click();
            EnableDisable();
        }

        private void comboBoxModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            providerState.authState.modelState.comboBoxModels_SelectedIndexChanged();
            EnableDisable();
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
        }

        private void comboBoxCredentialId_SelectedIndexChanged(object sender, EventArgs e)
        {
            providerState.authState.comboBoxCredentialId_SelectedIndexChanged();
            EnableDisable();
        }

        private void textBoxModel_TextChanged(object sender, EventArgs e)
        {
            EnableDisable();
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
            if (smartRoutingState == null)
                return;
            smartRoutingState.CheckedChanged();
        }

        private void textBoxCredentials_Enter(object sender, EventArgs e)
        {
            buttonWizard_Click(null, null);
        }

        #endregion events

        private void checkBoxSaveApiKeyInRegistry_CheckedChanged(object sender, EventArgs e)
        {
        }

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

        private void textBoxLabel_Enter(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start(((TextBox)sender).Text);
        }
    }
}
