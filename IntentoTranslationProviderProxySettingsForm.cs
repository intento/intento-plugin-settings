using Intento.MT.Plugin.PropertiesForm;
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
        //bool errAdr, errPort;
        public IntentoTranslationProviderProxySettingsForm(IntentoTranslationProviderOptionsForm parent)
        {
            InitializeComponent();
            DialogResult = DialogResult.None;
            _parent = parent;
            var proxy = _parent.currentOptions.proxySettings;
            if (proxy == null)
            {
                textBoxAddress.Text = _parent.GetValueFromRegistry("ProxyAddress");
                textBoxPort.Text = _parent.GetValueFromRegistry("ProxyPort");
                textBoxUserName.Text = _parent.GetValueFromRegistry("ProxyUserName");
                textBoxPassword.Text = _parent.GetValueFromRegistry("ProxyPassw");
                checkBoxAuth.Checked = _parent.GetValueFromRegistry("ProxyPassw") == "1";
            }
            else
            {
                textBoxAddress.Text = proxy.ProxyAddress;
                textBoxPort.Text = proxy.ProxyPort;
                textBoxUserName.Text = proxy.ProxyUserName;
                textBoxPassword.Text = proxy.ProxyPassword;
                checkBoxAuth.Checked = proxy.ProxyEnabled;
            }
        }

        private void checkBoxAuth_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAuth.Enabled = checkBoxAuth.Checked;
            if (checkBoxAuth.Checked)
            {
                textBoxUserName.Text = _parent.GetValueFromRegistry("ProxyUserName");
                textBoxPassword.Text = _parent.GetValueFromRegistry("ProxyPassw");
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
                var set = new IntentoSDK.ProxySettings()
                {
                    ProxyAddress = textBoxAddress.Text,
                    ProxyPort = textBoxPort.Text,
                    ProxyUserName = textBoxUserName.Text,
                    ProxyPassword = textBoxPassword.Text
                };
                _parent.currentOptions.proxySettings = set;
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
                new Uri($"http://{textBoxAddress.Text}:{textBoxPort.Text}");
            }
            catch (Exception)
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
            if (!Char.IsDigit(symbol))
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
    }
}
