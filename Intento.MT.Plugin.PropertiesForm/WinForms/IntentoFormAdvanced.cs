using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.SDK.Autofac;
using Intento.SDK.DI;
using Intento.SDK.Settings;
using Intento.SDK.Translate;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    public partial class IntentoFormAdvanced : Form
    {
        private readonly IntentoTranslationProviderOptionsForm parent;
        private ProxySettings proxySettings;
        
        private ITranslateService TranslateService => parent.Locator?.Resolve<ITranslateService>();

        private IRemoteLogService RemoteLogService => parent.Locator?.Resolve<IRemoteLogService>();

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

			var options = parent.CurrentOptions;
            if (options.ForbidSaveApikey)
            {
                checkBoxSaveApiKeyInRegistry.Visible = false;
            }

            checkBoxCustomTagParser.Enabled = options.MemoqAdditional != null && (bool)options.MemoqAdditional["advancedSdk"];

		}

        private void checkBoxProxy_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxProxy.Enabled = checkBoxProxy.Checked;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            RemoteLogService.SetTraceEndTime(DateTime.Now.AddMinutes(checkBoxTrace.Checked ? 30 : -40));
            parent.CurrentOptions.CustomSettingsName = string.IsNullOrWhiteSpace(textBoxCustomSettingsName.Text) ? null : textBoxCustomSettingsName.Text;
			parent.CurrentOptions.CustomTagParser = checkBoxCustomTagParser.Checked;
			parent.CurrentOptions.CutTag = checkBoxCutTags.Checked;
			parent.CurrentOptions.SaveLocally = checkBoxSaveLocally.Checked;

			if (!checkBoxProxy.Checked)
            {
                parent.CurrentOptions.ProxySettings = null;
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (IsValidParams())
            {
                parent.CurrentOptions.ProxySettings = new ProxySettings
                {
                    ProxyAddress = textBoxAddress.Text,
                    ProxyPort = textBoxPort.Text,
                    ProxyUserName = textBoxUserName.Text,
                    ProxyPassword = textBoxPassword.Text,
                    ProxyEnabled = true
                };
                parent.Locator = new DefaultLocatorImpl();
                parent.Locator.Init(new Options
                {
                    ApiKey = parent.CurrentOptions.ApiKey,
                    ClientUserAgent = "ProxyForm",
                    ServerUrl = parent.CurrentOptions.ApiPath,
                    Proxy = parent.CurrentOptions.ProxySettings
                });
                try
                {
                    TranslateService.Providers(filter: new Dictionary<string, string> { { "integrated", "true" }, { "mode", "async" } });
                }
                catch (AggregateException ex2)
                {
                    var ex = ex2.InnerExceptions[0];
                    if (ex is HttpRequestException)
                    {
                        parent.CurrentOptions.ProxySettings = proxySettings;
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

        private bool IsValidParams()
        {
            try
            {
                // ReSharper disable once UnusedVariable
                var port = int.Parse(textBoxPort.Text);
                // ReSharper disable once UnusedVariable
                var uri = new Uri($"http://{textBoxAddress.Text}:{textBoxPort.Text}");
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
            proxySettings = parent.CurrentOptions.ProxySettings;
			textBoxCustomSettingsName.Text = parent.CurrentOptions.CustomSettingsName;
			checkBoxCustomTagParser.Checked = parent.CurrentOptions.CustomTagParser;
			checkBoxCutTags.Checked = parent.CurrentOptions.CutTag;
			checkBoxCustomTagParser.Location = checkBoxCutTags.Location;
			checkBoxSaveLocally.Checked = parent.CurrentOptions.SaveLocally;
			checkBoxSaveLocally.Visible = parent.IsTrados || !parent.MemoqPublic;

			// Specific setting for Trados
			textBoxCustomSettingsName.Visible = parent.IsTrados;
			labelCustomSettingsName.Visible = parent.IsTrados;
			checkBoxCutTags.Visible = parent.IsTrados;

			// Specific setting for Memoq
			checkBoxCustomTagParser.Visible = !parent.IsTrados && !parent.MemoqPublic;
			checkBoxCustomTagParser.Enabled = !parent.IsTrados && parent.MemoqPublic;

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
            checkBoxTrace.Checked = RemoteLogService.GetTraceEndTime() > DateTime.Now;
        }

    }
}
