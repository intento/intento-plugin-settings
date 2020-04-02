using Intento.MT.Plugin.PropertiesForm;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class BaseState
    {
        public IntentoTranslationProviderOptionsForm form;
        public IntentoFormOptionsMT formMT;
        public BaseState parent;
        public IntentoMTFormOptions options;

        public BaseState(BaseState parent, IntentoMTFormOptions options)
        {
            if (parent != null)
            {
                this.parent = parent;
                this.form = parent.form;
                this.formMT = parent.formMT;
            }
            this.options = options;
        }

        public BaseState(IntentoTranslationProviderOptionsForm form, IntentoMTFormOptions options)
        {
            this.form = form;
            this.formMT = form.formMT;
            this.options = options;
        }

        public string GetValueFromRegistry(string name)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento\\{0}", options.AppName));
                return (string)key.GetValue(name, null);
            }
            catch { }
            return null;
        }
        public void SaveValueToRegistry(string name, string value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento\\{0}", options.AppName));
                key.SetValue(name, value);
            }
            catch { }
        }

        #region custom helper methods
        public void EnableDisable()
        {
            if (form.insideEnableDisable)
                return;

            form.SuspendLayout();
            try
            {
                form.insideEnableDisable = true;
                form.errors = new List<string>();

                form.errors.Add(form.apiKeyState.Draw());

            }
            finally
            {
                form.insideEnableDisable = false;
            }
            ShowErrorMessage();
            form.ResumeLayout();
        }

        private bool ShowErrorMessage()
        {
            form.errors = form.errors.Where(i => i != null).ToList();
            if ((form.errors == null || form.errors.Count == 0))
            {
                form.buttonContinue.Enabled = true;
                form.formMT.buttonSave.Enabled = true;
                SetErrorMessage();
                return true;
            }
            else
            {
                SetErrorMessage(string.Join(", ", form.errors.Where(i => i != null)));
                form.buttonContinue.Enabled = false;
                form.formMT.buttonSave.Enabled = false;
                return false;
            }
        }

        private void SetErrorMessage(string message = null)
        {
            if (message != null)
            {
                form.formMT.labelTMP.Text = message;
                form.formMT.labelTMP.BackColor = Color.LightPink;
            }
            else
            {
                form.formMT.labelTMP.Text = string.Empty;
                form.formMT.labelTMP.BackColor = SystemColors.Control;
            }
        }
        #endregion custom helper methods

    }
}