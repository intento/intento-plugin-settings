using System;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    public partial class IntentoFormIgnoreError : Form
    {
	    private string errorInfo;

		public IntentoFormIgnoreError()
        {
            InitializeComponent();
            buttonContinueEdit.Text = Resource.ButtonContinueEdit;
            buttonIgnoreAndSave.DialogResult = DialogResult.OK;
			toolTip1.SetToolTip(labelError, Resource.APIToolTipMessage);
		}

		public void SetButtonContinueEdit(string text, bool visibility)
		{
			buttonContinueEdit.Visible = visibility;
			if (!string.IsNullOrEmpty(text))
			{
				buttonContinueEdit.Text = text;
			}
		}

		public void SetButtonIgnoreAndSave(string text, bool visibility)
		{
			buttonIgnoreAndSave.Visible = visibility;
			if (!string.IsNullOrEmpty(text))
			{
				buttonIgnoreAndSave.Text = text;
			}
		}

		public void SetAdditionalErrorInfo(string msg)
		{
			if (string.IsNullOrWhiteSpace(msg))
			{
				return;
			}

			textBoxResponse.Text = msg;
			labelError.Cursor = Cursors.Hand;
			labelError.Click += labelError_Click;
			toolTip1.Active = true;
			errorInfo = msg;
		}

		private void labelError_Click(object sender, EventArgs e)
		{
			try
			{
				Clipboard.ContainsText();
				Clipboard.SetDataObject(errorInfo, true, 10, 150);
			}
			catch (Exception)
			{
				// ignored
			}
		}        
    }
}
