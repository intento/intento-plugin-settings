using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Intento.MT.Plugin.PropertiesForm.WinForms;

namespace Intento.MT.Plugin.PropertiesForm.States
{
    public class BaseState
    {
        public IntentoTranslationProviderOptionsForm Form { get; }
        protected IntentoFormOptionsMT FormMt { get; set; }
        protected IntentoMTFormOptions Options { get; set; }

        protected BaseState(BaseState parent, IntentoMTFormOptions options)
        {
            if (parent != null)
            {
                Form = parent.Form;
                FormMt = parent.FormMt;
            }
            Options = options;
        }

        protected BaseState(IntentoTranslationProviderOptionsForm form, IntentoMTFormOptions options)
        {
            Form = form;
            FormMt = form.FormMt;
            Options = options;
        }

        #region custom helper methods
        public void EnableDisable()
        {
            if (Form.InsideEnableDisable)
                return;

            Form.SuspendLayout();
            try
            {
                Form.InsideEnableDisable = true;
                Form.Errors = new List<string>
                {
                    Form.ApiKeyState.Draw()
                };
            }
            finally
            {
                Form.InsideEnableDisable = false;
            }
            ShowErrorMessage();
            Form.ResumeLayout();
        }

        private void ShowErrorMessage()
        {
            Form.Errors = Form.Errors.Where(i => i != null).ToList();
            if (Form.Errors == null || Form.Errors.Count == 0)
            {
                Form.FormMt.buttonSave.Enabled = true;
                SetErrorMessage();
            }
            else
            {
                SetErrorMessage(string.Join(", ", Form.Errors.Where(i => i != null)));
                Form.FormMt.buttonSave.Enabled = false;
            }
        }

        private void SetErrorMessage(string message = null)
        {
            if (message != null)
            {
                Form.FormMt.labelTMP.Text = message;
                Form.FormMt.labelTMP.BackColor = Color.LightPink;
            }
            else
            {
                Form.FormMt.labelTMP.Text = string.Empty;
                Form.FormMt.labelTMP.BackColor = SystemColors.Control;
            }
        }
        #endregion custom helper methods
    }
}