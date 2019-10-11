using Intento.MT.Plugin.PropertiesForm;
using IntentoSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm
{
    public partial class IntentoTranslationProviderProxySettingsForm : Form
    {
        internal IntentoTranslationProviderOptionsForm _parent;
        ProxySettings currentProxy;
        //bool errAdr, errPort;
        public IntentoTranslationProviderProxySettingsForm(IntentoTranslationProviderOptionsForm parent)
        {
            InitializeComponent();
            LocalizeContent();
            DialogResult = DialogResult.None;
            _parent = parent;
            currentProxy = _parent.currentOptions.proxySettings;
            if (currentProxy == null)
            {
                textBoxAddress.Text = _parent.apiKeyState.GetValueFromRegistry("ProxyAddress");
                textBoxPort.Text = _parent.apiKeyState.GetValueFromRegistry("ProxyPort");
                textBoxUserName.Text = _parent.apiKeyState.GetValueFromRegistry("ProxyUserName");
                checkBoxAuth.Checked = !string.IsNullOrWhiteSpace(textBoxUserName.Text);
                textBoxPassword.Text = _parent.apiKeyState.GetValueFromRegistry("ProxyPassw");
            }
            else
            {
                textBoxAddress.Text = currentProxy.ProxyAddress;
                textBoxPort.Text = currentProxy.ProxyPort;
                if (string.IsNullOrWhiteSpace(currentProxy.ProxyUserName))
                {
                    textBoxUserName.Text = currentProxy.ProxyUserName;
                    textBoxPassword.Text = currentProxy.ProxyPassword;
                    checkBoxAuth.Checked = true;
                }
            }
        }

        private void checkBoxAuth_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAuth.Enabled = checkBoxAuth.Checked;
            if (checkBoxAuth.Checked)
            {
                textBoxUserName.Text = _parent.apiKeyState.GetValueFromRegistry("ProxyUserName");
                textBoxPassword.Text = _parent.apiKeyState.GetValueFromRegistry("ProxyPassw");
            }
            else
            {
                textBoxPassword.Clear();
                textBoxUserName.Clear();
            }
            textBoxAuth_TextChanged(textBoxUserName, null);
            textBoxAuth_TextChanged(textBoxPassword, null);
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (isValidParams())
            {
                if (currentProxy == null)
                    currentProxy = new ProxySettings();
                currentProxy.ProxyAddress = textBoxAddress.Text;
                currentProxy.ProxyPort = textBoxPort.Text;
                currentProxy.ProxyUserName = textBoxUserName.Text;
                currentProxy.ProxyPassword = textBoxPassword.Text;
                currentProxy.ProxyEnabled = true;
                _parent.currentOptions.proxySettings = currentProxy;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {

            }
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

        private void LocalizeContent()
        {
            Text = Resource.PSFCaption;
            labelAddress.Text = Resource.PSFLabelAddress;
            labelPort.Text = Resource.PSFLabelPort;
            checkBoxAuth.Text = Resource.PSFCheckBoxAuth;
            labelUserName.Text = Resource.PSFLabelUserName;
            labelPassword.Text = Resource.PSFLabelPassword;
            buttonDone.Text = Resource.OKLabel;
        }

    }
}
