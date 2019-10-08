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
        public IForm form;
        public BaseState parent;
        public IntentoMTFormOptions options;

        public BaseState(BaseState parent, IntentoMTFormOptions options)
        {
            if (parent != null)
            {
                this.parent = parent;
                this.form = parent.form;
            }
            this.options = options;
        }

        public BaseState(IForm form, IntentoMTFormOptions options)
        {
            this.form = form;
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
            if (form.InsideEnableDisable)
                return;

            form.SuspendLayout();
            try
            {
                form.InsideEnableDisable = true;
                form.Errors = new List<string>();

                form.Errors.Add(form.ApiKeyState.Draw());

            }
            finally
            {
                form.InsideEnableDisable = false;
            }
            ShowErrorMessage();
            form.ResumeLayout();
        }

        private bool ShowErrorMessage()
        {
            form.Errors = form.Errors.Where(i => i != null).ToList();
            if ((form.Errors == null || form.Errors.Count == 0))
            {
                form.Continue_Button_Enabled = true;
                SetErrorMessage();
                return true;
            }
            else
            {
                SetErrorMessage(string.Join(", ", form.Errors.Where(i => i != null)));
                form.Continue_Button_Enabled = false;
                return false;
            }
        }

        private void SetErrorMessage(string message = null)
        {
            if (message != null)
            {
                form.ErrorMessage_TextBox_Text = message;
                form.ErrorMessage_TextBox_BackColor = Color.LightPink;
            }
            else
            {
                form.ErrorMessage_TextBox_Text = string.Empty;
                form.ErrorMessage_TextBox_BackColor = SystemColors.Control;
            }
        }
        #endregion custom helper methods

    }
}