using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Intento.MT.Plugin.PropertiesForm.WinForms;

namespace Intento.MT.Plugin.PropertiesForm
{
    public partial class IntentoFormOptionsAPI : Form
    {
        private IntentoTranslationProviderOptionsForm parent;
        IntentoMTFormOptions _options;

        public IntentoMTFormOptions currentOptions;
        public string errorInfo;

        public IntentoFormOptionsAPI(IntentoTranslationProviderOptionsForm form)
        {
            InitializeComponent();
            LocalizeContent();
            parent = form;
            if (form.GetOptions().HideHiddenTextButton)
                checkBoxShowHidden.Visible = false;
        }

        private void LocalizeContent()
        {
            Text = Resource.EnterIntentoAPIKey;
            labelAPI.Text = Resource.APIFlabelAPI;
            checkBoxShowHidden.Text = Resource.APIFcheckBoxShowHidden;
            buttonCancel.Text = Resource.Cancel;
            buttonSave.Text = Resource.TestAndSave;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
			this.Cursor = Cursors.WaitCursor;
			var testOptions = new IntentoMTFormOptions();
			testOptions.Hidden = true;
			var _apiKeyState = new ApiKeyState(new IntentoTranslationProviderOptionsForm(testOptions, parent.LanguagePairs, parent.fabric), _options);
			_apiKeyState.SetValue(apiKey_tb.Text.Trim());
			_apiKeyState.ReadProvidersAndRouting();
			string err = _apiKeyState.Error();
			IEnumerable<string> errDetail = _apiKeyState.ErrorDetail();
			this.Cursor = Cursors.Default;
			var nl = Environment.NewLine;
			if (!string.IsNullOrWhiteSpace(err))
			{
				string errorMsg = err == Resource.InvalidApiKeyMessage ? err : Resource.APIFlabelErrorSeePopup;
				labelError.Text = string.Format("ERROR: {0}", errorMsg); labelError.Visible = true;
				if (errDetail != null)
				{
					errorInfo = (
						string.Format("{0}{1}---------------------------{1}{2}",
						DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
						nl,
						string.Join(nl, errDetail.ToArray())
						));

					toolTip1.SetToolTip(labelError, Resource.APIToolTipMessage);
				}
			}
			else
			{
				this.Cursor = Cursors.WaitCursor;
				parent.apiKeyState.SetValue(apiKey_tb.Text.Trim());
				parent.apiKeyState.ReadProvidersAndRouting();
				parent.apiKeyState.EnableDisable();
				this.Cursor = Cursors.Default;
				this.DialogResult = DialogResult.OK;
			}
        }

        private void checkBoxShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            apiKey_tb.UseSystemPasswordChar = !checkBoxShowHidden.Checked;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IntentoFormOptionsAPI_Shown(object sender, EventArgs e)
        {
            _options = currentOptions;
            labelError.Visible = false;
            buttonSave.Enabled = !string.IsNullOrWhiteSpace(apiKey_tb.Text);
            checkBoxShowHidden.Checked = false;
			apiKey_tb.Text = parent.apiKeyState.apiKey;
			apiKey_tb.BackColor = parent.apiKeyState.apiKeyStatus == ApiKeyState.EApiKeyStatus.ok ?
				SystemColors.Window : Color.LightPink;
			apiKey_tb.TextChanged += apiKey_tb_TextChanged;
		}

        private void labelError_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(errorInfo);
        }

		private void apiKey_tb_TextChanged(object sender, EventArgs e)
		{
			buttonSave.Enabled = !string.IsNullOrWhiteSpace(apiKey_tb.Text);
			apiKey_tb.BackColor = Color.LightPink;
		}

		private void IntentoFormOptionsAPI_FormClosed(object sender, FormClosedEventArgs e)
		{
			apiKey_tb.TextChanged -= apiKey_tb_TextChanged;
		}
	}
}
