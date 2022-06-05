using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Intento.MT.Plugin.PropertiesForm.Models;
using Intento.MT.Plugin.PropertiesForm.States;
using Intento.SDK.DI;
using Intento.SDK.Settings;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
	/// <inheritdoc />
	// ReSharper disable once InconsistentNaming
	public partial class IntentoFormOptionsAPI : Form
    {
        private IntentoMTFormOptions options;
		private readonly Func<IntentoMTFormOptions, string, ValidateInfo> testConnectionFunc;

		public string ApiKey
		{
			get => apiKey_tb.Text;
			set => apiKey_tb.Text = value;
		}

		public ApiKeyState.EApiKeyStatus ApiKeyStatus
		{
			get;
			set;
		}

		public IntentoMTFormOptions CurrentOptions { get; set; }
        
        private string errorInfo;

        public IntentoFormOptionsAPI(bool hideHiddenTextButton, Func<IntentoMTFormOptions, string, ValidateInfo> testConnectionFunc)
        {
            InitializeComponent();
            LocalizeContent();
			this.testConnectionFunc = testConnectionFunc;
            checkBoxShowHidden.Visible = !hideHiddenTextButton;            
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
		        DialogResult = DialogResult.OK;
		        return;	        
			}

			if (testConnectionFunc != null)
			{
				var res = testConnectionFunc.Invoke(options, apiKey_tb.Text.Trim());
				if (res.IsValid)
				{
					DialogResult = DialogResult.OK;
				}
				else
				{
					labelError.Text = res.Message; 
					labelError.Visible = true;
					if (res.Description != null)
					{
						errorInfo = res.Description;
						toolTip1.SetToolTip(labelError, Resource.APIToolTipMessage);
					}
				}
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
            apiKey_tb.BackColor = ApiKeyStatus == ApiKeyState.EApiKeyStatus.Ok ?
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
