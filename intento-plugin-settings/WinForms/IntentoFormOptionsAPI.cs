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
            apiKey_tb.TextChanged += parent.apiKey_tb_TextChanged;
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
			toolTip1.SetToolTip(labelError, Resource.APIToolTipMessage);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            parent.apiKeyState.ReadProviders();
            string err = parent.apiKeyState.Error();
            IEnumerable <string> errDetail = parent.apiKeyState.ErrorDetail();
            var nl = Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(err))
            {
				string errorMsg = err == Resource.InvalidApiKeyMessage ? err : Resource.APIFlabelErrorSeePopup;
				labelError.Text = string.Format("ERROR: {0}", errorMsg);

				toolTip1.ToolTipTitle = Resource.APIToolTipMessage;

				labelError.Visible = true;
                if (errDetail != null)
                {
                    errorInfo = (
                        string.Format("{0}{1}---------------------------{1}{2}", 
                        DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), 
                        nl, 
                        string.Join(nl, errDetail.ToArray()) 
                        ));

                    toolTip1.SetToolTip(labelError, err );
                }
            }
            else
                this.DialogResult = DialogResult.OK;
        }

        private void checkBoxShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            apiKey_tb.UseSystemPasswordChar = !checkBoxShowHidden.Checked;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            apiKey_tb.Text = _options.ApiKey;
            parent.currentOptions = _options;
            Close();
        }

        private void IntentoFormOptionsAPI_Shown(object sender, EventArgs e)
        {
            _options = currentOptions;
            labelError.Visible = false;
            buttonSave.Enabled = !string.IsNullOrWhiteSpace(apiKey_tb.Text);
            checkBoxShowHidden.Checked = false;
        }

        private void labelError_Click(object sender, EventArgs e)
        {
			try
			{
				Clipboard.ContainsText();
				Clipboard.SetDataObject(errorInfo, true, 10, 150);
			}
			catch (Exception){}

        }
    }
}
