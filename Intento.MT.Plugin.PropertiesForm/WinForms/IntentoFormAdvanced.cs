using IntentoSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    public partial class IntentoFormAdvanced : Form
    {
        IntentoTranslationProviderOptionsForm parent;
        ProxySettings proxySettings;

        public IntentoFormAdvanced(IntentoTranslationProviderOptionsForm form)
        {
            InitializeComponent();
            parent = form;
            LocalizeContent();
        }

        private void LocalizeContent()
        {
            checkBoxAuth.Text = Resource.FAcheckBoxAuth;
            checkBoxProxy.Text = Resource.FAcheckBoxProxy;
            labelAddress.Text = Resource.FAlabelAddress;
            checkBoxTrace.Text = Resource.FAcheckBoxTrace;
            labelPassword.Text = Resource.FAlabelPassword;
            labelPort.Text = Resource.FAlabelPort;
            labelUserName.Text = Resource.FAlabelUserName;
            buttonCancel.Text = Resource.Cancel;
            buttonSave.Text = Resource.Save;
            checkBoxTrace.Text = Resource.FAcheckBoxTrace;
            Text = Resource.FAcaption;
            labelCustomSettingsName.Text = Resource.FAlabelCustomSettingsName;
			checkBoxCustomTagParser.Text = Resource.FAcheckBoxCustomTagParser;
			checkBoxCutTags.Text = Resource.FACheckBoxCutTags;
			checkBoxSaveLocally.Text = Resource.FAcheckBoxSaveLocally;

			var options = parent.currentOptions;
			if (options.ForbidSaveApikey)
                checkBoxSaveApiKeyInRegistry.Visible = false;
			checkBoxCustomTagParser.Enabled = options.MemoqAdditional != null && (bool)options.MemoqAdditional["advancedSdk"];

		}

        private void checkBoxProxy_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxProxy.Enabled = checkBoxProxy.Checked;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            IntentoTranslationProviderOptionsForm.TraceEndTime = DateTime.Now.AddMinutes(checkBoxTrace.Checked ? 30 : -40);
            parent.currentOptions.CustomSettingsName = string.IsNullOrWhiteSpace(textBoxCustomSettingsName.Text) ? null : textBoxCustomSettingsName.Text;
			parent.currentOptions.CustomTagParser = checkBoxCustomTagParser.Checked;
			parent.currentOptions.CutTag = checkBoxCutTags.Checked;
			parent.currentOptions.SaveLocally = checkBoxSaveLocally.Checked;

			if (!checkBoxProxy.Checked)
            {
                parent.currentOptions.proxySettings = null;
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (isValidParams())
            {
                parent.currentOptions.proxySettings = new ProxySettings();
                parent.currentOptions.proxySettings.ProxyAddress = textBoxAddress.Text;
                parent.currentOptions.proxySettings.ProxyPort = textBoxPort.Text;
                parent.currentOptions.proxySettings.ProxyUserName = textBoxUserName.Text;
                parent.currentOptions.proxySettings.ProxyPassword = textBoxPassword.Text;
                parent.currentOptions.proxySettings.ProxyEnabled = true;
                var _intento = IntentoSDK.Intento.Create(parent.currentOptions.ApiKey, null,
                    path: "https://api.inten.to/",
                    userAgent: "ProxyForm",
                    loggingCallback: Logs.Logging,
                    proxySet: parent.currentOptions.proxySettings
                );
                var _translate = _intento.Ai.Text.Translate;
                try
                {
                    _translate.Providers(filter: new Dictionary<string, string> { { "integrated", "true" }, { "mode", "async" } });
                }
                catch (AggregateException ex2)
                {
                    Exception ex = ex2.InnerExceptions[0];
                    if (ex is HttpRequestException)
                    {
                        parent.currentOptions.proxySettings = proxySettings;
                        labelError.Text = Resource.ProxyConnectionError;
                        labelError.Visible = true;
                        return;
                    }
                }
                    DialogResult = DialogResult.OK;
                    Close();
            }
        }

        private void checkBoxAuth_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAuth.Enabled = checkBoxAuth.Checked;
            if (checkBoxAuth.Checked)
            {
                textBoxUserName.Text = parent.GetValueFromRegistry("ProxyUserName");
                textBoxPassword.Text = parent.GetValueFromRegistry("ProxyPassw");
            }
            else
            {
                textBoxPassword.Clear();
                textBoxUserName.Clear();
            }
            textBoxAuth_TextChanged(textBoxUserName, null);
            textBoxAuth_TextChanged(textBoxPassword, null);
        }

        private bool isValidParams()
        {
            try
            {
                var port = int.Parse(textBoxPort.Text);
                new Uri($"http://{textBoxAddress.Text}:{textBoxPort.Text}");
            }
            catch
            {
                textBoxAddress.BackColor = Color.LightPink;
                textBoxPort.BackColor = Color.LightPink;
                return false;
            }
            textBoxAddress.BackColor = SystemColors.Window;
            textBoxPort.BackColor = SystemColors.Window;
            if (checkBoxAuth.Checked)
            {
                return !string.IsNullOrWhiteSpace(textBoxUserName.Text) && !string.IsNullOrWhiteSpace(textBoxPassword.Text);
            }
            return true;
        }

        private void textBoxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;
            if (!Char.IsDigit(symbol) && !Char.IsControl(symbol))
            {
                e.Handled = true;
            }
        }

        private void textBoxAuth_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (checkBoxAuth.Checked && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BackColor = Color.LightPink;
            }
            else
            {
                tb.BackColor = SystemColors.Window;
            }
        }

        private void IntentoFormAdvanced_Shown(object sender, EventArgs e)
        {
            proxySettings = parent.currentOptions.proxySettings;
			textBoxCustomSettingsName.Text = parent.currentOptions.CustomSettingsName;
			checkBoxCustomTagParser.Checked = parent.currentOptions.CustomTagParser;
			checkBoxCutTags.Checked = parent.currentOptions.CutTag;
			checkBoxCustomTagParser.Location = checkBoxCutTags.Location;
			checkBoxSaveLocally.Checked = parent.currentOptions.SaveLocally;
			checkBoxSaveLocally.Visible = parent.isTrados || !parent.memoqPublic;

			// Specific setting for Trados
			textBoxCustomSettingsName.Visible = parent.isTrados;
			labelCustomSettingsName.Visible = parent.isTrados;
			checkBoxCutTags.Visible = parent.isTrados;

			// Specific setting for Memoq
			checkBoxCustomTagParser.Visible = !parent.isTrados && !parent.memoqPublic;
			checkBoxCustomTagParser.Enabled = !parent.isTrados && parent.memoqPublic;

			if (proxySettings == null)
            {
                textBoxAddress.Text = parent.GetValueFromRegistry("ProxyAddress");
                textBoxPort.Text = parent.GetValueFromRegistry("ProxyPort");
                textBoxUserName.Text = parent.GetValueFromRegistry("ProxyUserName");
                checkBoxAuth.Checked = !string.IsNullOrWhiteSpace(textBoxUserName.Text);
                textBoxPassword.Text = parent.GetValueFromRegistry("ProxyPassw");
                checkBoxProxy.Checked = false;
            }
            else
            {
                textBoxAddress.Text = proxySettings.ProxyAddress;
                textBoxPort.Text = proxySettings.ProxyPort;
                checkBoxAuth.Checked = !string.IsNullOrWhiteSpace(proxySettings.ProxyUserName);
                if (checkBoxAuth.Checked)
                {
                    textBoxUserName.Text = proxySettings.ProxyUserName;
                    textBoxPassword.Text = proxySettings.ProxyPassword;
                    checkBoxAuth.Checked = true;
                }
                checkBoxProxy.Checked = proxySettings.ProxyEnabled;
            }
            labelError.Visible = false;
            checkBoxTrace.Checked = IntentoTranslationProviderOptionsForm.TraceEndTime > DateTime.Now;
        }

    }
}
