using Intento.MT.Plugin.PropertiesForm;
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
        public IntentoMTFormOptions originalOptions;
        public IntentoMTFormOptions Options;
        public IntentoAiTextTranslate _translate;

        // Languages filter 
        public IList<dynamic> languages;
        private LangPair[] _languagePairs;

        public static DateTime TraceEndTime;

        private int numberOfFlashes;

        private ApiKeyState apiKeyState;
        public ProviderState providerState;
        public AuthState authState;
        public ModelState modelState;

        List<string> errors;

        // Fabric to create intento connection. Parameters: apiKey and UserAgent for Settings Form 
        Func<string, string, IntentoAiTextTranslate> intentoConnection;

        string version;

        #endregion vars

        public IntentoTranslationProviderOptionsForm(IntentoMTFormOptions options, 
            LangPair[] languagePairs, Func<string, string, IntentoAiTextTranslate> intentoConnection)
        {
            this.intentoConnection = intentoConnection;

            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            this.version = string.Format("{0}.{1}.{2}", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart);

            Options = options;
            _languagePairs = languagePairs;
            DialogResult = DialogResult.None;

            var tmp = TraceEndTime;
            checkBoxTrace.Checked = (TraceEndTime - DateTime.Now).Minutes > 0;
            TraceEndTime = tmp;
            // string pluginFor = string.IsNullOrEmpty(Options.PluginFor) ? "" : Options.PluginFor + '/';
            // toolStripStatusLabel2.Text = String.Format("{0} {1}{2}", Options.PluginName, pluginFor, Options.AssemblyVersion);
            toolStripStatusLabel2.Text = Options.Signature;
            textBoxModel.Location = comboBoxModels.Location; // new Point(comboBoxModels.Location.X, comboBoxModels.Location.Y);
            textBoxGlossary.Location = comboBoxGlossaries.Location; // new Point(comboBoxGlossaries.Location.X, comboBoxGlossaries.Location.Y);
            groupBoxAuthCredentialId.Location = groupBoxAuth.Location; // new Point(groupBoxAuth.Location.X, groupBoxAuth.Location.Y)

            apiKeyState = new ApiKeyState(this, apiKey_tb, Options);
            apiKeyState.apiKeyChangedEvent += ChangeApiKeyStatusDelegate;

            EnableDisable();
        }

        public void ChangeApiKeyStatusDelegate(bool isOK)
        {
            if (isOK)
            {
                providerState = new ProviderState(this, groupBoxProviderSettings, comboBoxProviders, Options, _languagePairs);
                providerState.Fill(apiKeyState.Providers);
                authState = new AuthState(this, checkBoxUseOwnCred, groupBoxAuthCredentialId, comboBoxCredentialId, groupBoxAuth, textBoxCredentials, Options);
                authState.Fill();
                modelState = new ModelState(this, Options);
                modelState.Fill();
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
            _translate = intentoConnection(apiKeyState.apiKey, String.Format("{1}/{2}", Options.UserAgent, "Intento.PluginSettingsForm", version));
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
        private void EnableDisable()
        {
            if (insideEnableDisable)
                return;

            try
            {
                insideEnableDisable = true;
                errors = new List<string>();

                errors.Add(apiKeyState.Draw());

                errors.Add(ProviderState.Draw(this, providerState));
                errors.Add(AuthState.Draw(this, authState));
                errors.Add(ModelState.Draw(this, modelState));

                groupBoxProviderSettings.Enabled = !checkBoxSmartRouting.Checked && apiKeyState.IsOK;
                buttonCheck.Enabled = apiKeyState.CheckPossible;
                checkBoxSmartRouting.Enabled = apiKeyState.IsOK;

                if (groupBoxProviderSettings.Enabled)
                {
                    if (apiKeyState.IsOK)
                    {
                        if (providerState.IsOK)
                        {
                            // set state of glossary selection control
                            groupBoxGlossary.Visible = providerState.custom_glossary && authState.IsSelected;
                            if (groupBoxGlossary.Visible)
                            {
                                if (providerState.GetGlossaries(authState.providerDataAuthDict) != null)
                                {
                                    textBoxGlossary.Visible = false;
                                    comboBoxGlossaries.Visible = true;
                                    comboBoxGlossaries.Enabled = true;
                                }
                                else
                                {
                                    textBoxGlossary.Visible = true;
                                    textBoxGlossary.Enabled = true;
                                    comboBoxGlossaries.Visible = false;
                                }

                            }
                        }
                    }
                }
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

        private void clearParameters()
        {
            // clear old parameters
            textBoxGlossary.Text = string.Empty;
            comboBoxGlossaries.Items.Clear();
        }

        #endregion custom helper methods

        #region paramount events 

        private void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearParameters();

            providerState.SelectedIndexChanged();
            authState = new AuthState(this, checkBoxUseOwnCred, groupBoxAuthCredentialId, comboBoxCredentialId, groupBoxAuth, textBoxCredentials, Options);
            authState.Fill();
            modelState = new ModelState(this, Options);
            modelState.Fill();

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

        private void FillProviderGlossaries()
        {
            if (!authState.IsSelected)
                return;
            comboBoxGlossaries.Items.Clear();
            textBoxGlossary.Text = string.Empty;
            Dictionary<string, dynamic> _providerGlossaries;
            try
            {
                _providerGlossaries = providerState.GetGlossaries(authState.providerDataAuthDict);
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
            checkBoxSmartRouting.Checked = Options.SmartRouting;
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
            if (authState != null)
                authState.checkBoxUseOwnCred_CheckedChanged();
            EnableDisable();
        }

        private void checkBoxUseCustomModel_CheckedChanged(object sender, EventArgs e)
        {
            modelState.checkBoxUseCustomModel_CheckedChanged();
            EnableDisable();
        }

        // Save settings
        private void buttonContinue_Click(object sender, EventArgs e)
        {
            Options.ApiKey = apiKeyState.apiKey;
            Options.Translate = _translate;

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
                Options.Format = null;
            }
            else
            {
                Options.ProviderId = providerState.currentProviderId;
                Options.ProviderName = providerState.CurrentProviderName;

                Options.UseCustomAuth = authState.UseCustomAuth;
                if (Options.UseCustomAuth)
                    Options.SetAuthDict(authState.providerDataAuthDict);
                else
                    Options.CustomAuth = null;

                Options.UseCustomModel = modelState.UseCustomModel;
                Options.CustomModel = modelState.CustomModel;

                Options.Glossary = textBoxGlossary.Visible ? textBoxGlossary.Text : 
                        string.IsNullOrEmpty(comboBoxGlossaries.Text) ? null : (string)providerState.GetGlossaries(authState.providerDataAuthDict)[comboBoxGlossaries.Text].id;

                Options.Format = providerState.format;
            }
            Close();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(((LinkLabel)sender).Text);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {   // text box in auth WizardForm
            label7.Text = textBoxCredentials.Text;
            EnableDisable();
        }

        private void buttonWizard_Click(object sender, EventArgs e)
        {
            authState.buttonWizard_Click();

            modelState = new ModelState(this, Options);
            modelState.Fill();

            if (providerState.custom_glossary)
                FillProviderGlossaries();
            EnableDisable();
        }

        private void comboBoxModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            modelState.comboBoxModels_SelectedIndexChanged();
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
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        }

        private void comboBoxCredentialId_SelectedIndexChanged(object sender, EventArgs e)
        {
            authState.comboBoxCredentialId_SelectedIndexChanged();

            modelState = new ModelState(this, Options);
            modelState.Fill();

            FillProviderGlossaries();

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
            providerState = new ProviderState(this, groupBoxProviderSettings, comboBoxProviders, Options, _languagePairs);
            providerState.Fill(apiKeyState.Providers);
            authState = new AuthState(this, checkBoxUseOwnCred, groupBoxAuthCredentialId, comboBoxCredentialId, groupBoxAuth, textBoxCredentials, Options);
            authState.Fill();
            modelState = new ModelState(this, Options);
            modelState.Fill();

            if (checkBoxSmartRouting.Checked)
            {
                comboBoxGlossaries.Items.Clear();
                textBoxGlossary.Text = null;
                Options.Format = "[\"text\",\"html\",\"xml\"]";

                providerState = null;
            }
            else
            {
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
