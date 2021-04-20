using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    public partial class IntentoFormIgnoreError : Form
    {
		string errorInfo;

		public IntentoFormIgnoreError()
        {
            InitializeComponent();
            buttonContinueEdit.Text = Resource.ButtonContinueEdit;
            buttonIgnoreAndSave.DialogResult = DialogResult.OK;
			toolTip1.SetToolTip(labelError, Resource.APIToolTipMessage);
		}

		public void setAdditionalErrorInfo(string msg)
		{
			if (!string.IsNullOrWhiteSpace(msg))
			{
				labelError.Cursor = Cursors.Hand;
				this.labelError.Click += this.labelError_Click;
				toolTip1.Active = true;
				errorInfo = msg;
			}
		}

		private void labelError_Click(object sender, EventArgs e)
		{
			try
			{
				Clipboard.ContainsText();
				Clipboard.SetDataObject(errorInfo, true, 10, 150);
			}
			catch (Exception) { }
		}
	}
}
