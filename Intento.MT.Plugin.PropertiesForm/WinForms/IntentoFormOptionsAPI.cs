using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Intento.MT.Plugin.PropertiesForm.States;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
	/// <inheritdoc />
	// ReSharper disable once InconsistentNaming
	public partial class IntentoFormOptionsAPI : Form
    {
        private readonly IntentoTranslationProviderOptionsForm parent;
        private IntentoMTFormOptions options;

        public IntentoMTFormOptions CurrentOptions { get; set; }
        
        private string errorInfo;

        public IntentoFormOptionsAPI(IntentoTranslationProviderOptionsForm form)
        {
            InitializeComponent();
            LocalizeContent();
            parent = form;
            if (form.GetOptions().HideHiddenTextButton)
            {
	            checkBoxShowHidden.Visible = false;
            }
        }

        private void LocalizeContent()
        {
            Text = Resource.EnterIntentoAPIKey;
            labelAPI.Text = Resource.APIFlabelAPI;
            checkBoxShowHidden.Text = Resource.APIFcheckBoxShowHidden;
            buttonCancel.Text = Resource.Cancel;
            buttonSave.Text = Resource.TestAndSave;
			toolTip1.SetToolTip(labelError, Resource.APIToolTipMessage);
		}

        private void buttonSave_Click(object sender, EventArgs e)
        {
	        if (string.IsNullOrWhiteSpace(apiKey_tb.Text))
	        {
		        parent.ApiKeyState.SetValue(string.Empty);
		        parent.ApiKeyState.ReadProvidersAndRouting();
		        DialogResult = DialogResult.OK;
		        return;
	        }

	        Cursor = Cursors.WaitCursor;
			var testOptions = new IntentoMTFormOptions
			{
				Hidden = true
			};
			var apiKeyState = new ApiKeyState(new IntentoTranslationProviderOptionsForm(testOptions, parent.LanguagePairs, parent.Locator, parent.InitLocatorFunc), options);
			apiKeyState.SetValue(apiKey_tb.Text.Trim());
			apiKeyState.ReadProvidersAndRouting();
			var err = apiKeyState.Error();
			var errDetail = apiKeyState.ErrorDetail();
			Cursor = Cursors.Default;
			var nl = Environment.NewLine;
			if (!string.IsNullOrWhiteSpace(err))
			{
				var errorMsg = err == Resource.InvalidApiKeyMessage ? err : Resource.APIFlabelErrorSeePopup;
				labelError.Text = $@"{errorMsg}"; 
				labelError.Visible = true;
				if (errDetail != null)
				{
					errorInfo = string.Format("{0:yyyy-MM-dd HH:mm:ss}{1}---------------------------{1}{2}", DateTime.UtcNow,
						nl,
						string.Join(nl, errDetail.ToArray())
					);

					toolTip1.SetToolTip(labelError, Resource.APIToolTipMessage);
				}
			}
			else
			{
				Cursor = Cursors.WaitCursor;
				parent.ApiKeyState.SetValue(apiKey_tb.Text.Trim());
				parent.ApiKeyState.ReadProvidersAndRouting();
				parent.ApiKeyState.EnableDisable();
				Cursor = Cursors.Default;
				DialogResult = DialogResult.OK;
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
            options = CurrentOptions;
            labelError.Visible = false;
            checkBoxShowHidden.Checked = false;
			apiKey_tb.Text = parent.ApiKeyState.ApiKey;
			apiKey_tb.BackColor = parent.ApiKeyState.ApiKeyStatus == ApiKeyState.EApiKeyStatus.Ok ?
				SystemColors.Window : Color.LightPink;
			apiKey_tb.TextChanged += apiKey_tb_TextChanged;
		}

        private void labelError_Click(object sender, EventArgs e)
        {
			try
			{
				Clipboard.ContainsText();
				Clipboard.SetDataObject(errorInfo, true, 10, 150);
				using var form = new IntentoFormIgnoreError();
				form.labelError.Visible = false;
				form.SetButtonContinueEdit(null, false);
				form.SetButtonIgnoreAndSave("Close", true);
				form.SetAdditionalErrorInfo(errorInfo);
				form.ShowDialog();
			}
			catch (Exception)
			{
				// ignored
			}
        }

		private void apiKey_tb_TextChanged(object sender, EventArgs e)
		{
			buttonSave.Enabled = true;
			buttonSave.Text = string.IsNullOrWhiteSpace(apiKey_tb.Text) ? "Save" : "Test and save";
			apiKey_tb.BackColor = Color.LightPink;
		}

		private void IntentoFormOptionsAPI_FormClosed(object sender, FormClosedEventArgs e)
		{
			apiKey_tb.TextChanged -= apiKey_tb_TextChanged;
		}
	}
}
