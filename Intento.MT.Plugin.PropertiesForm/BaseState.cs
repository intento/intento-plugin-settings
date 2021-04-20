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
                form.formMT.buttonSave.Enabled = true;
                SetErrorMessage();
                return true;
            }
            else
            {
                SetErrorMessage(string.Join(", ", form.errors.Where(i => i != null)));
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

        private string MakeModelName(dynamic model, bool longName = false, bool isGoogle = false)
        {
            string name = model.name;
            string id = model.id;
            string from_lang = model.from;
            string to_lang = model.to;

            if (longName)
            {
                if (isGoogle)
                    id = id.Substring(id.LastIndexOf('/') + 1);
                if (from_lang == null || to_lang == null)
                    return string.Format("{0} ({1})", name, id);
                else
                    return string.Format("{0} [{1}/{2}] ({3})", name, from_lang, to_lang, id);
            }
            else
            {
                if (from_lang == null || to_lang == null)
                    return string.Format("{0}", name);
                else
                    return string.Format("{0} [{1}/{2}]", name, from_lang, to_lang);
            }
        }

        protected Dictionary<string, dynamic> ProcessModels(ProviderState providerState, IList<dynamic> raw)
        {   // check for name duplicates and provide special naming for duplicates
            Dictionary<string, List<dynamic>> data = new Dictionary<string, List<dynamic>>();
            foreach (dynamic item in raw)
            {
                string name = item.name;
                if (data.ContainsKey(name))
                    data[name].Add(item);
                else
                    data[name] = new List<dynamic> { item };
            }

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
            foreach (KeyValuePair<string, List<dynamic>> pair in data)
            {
                if (pair.Value.Count == 1)
                    res[MakeModelName(pair.Value[0])] = pair.Value[0];
                else
                {
                    foreach (dynamic item in pair.Value)
                    {
                        string name = MakeModelName(item, true, providerState.currentProviderId.Contains("google"));
                        res[name] = item;
                    }
                }
            }
            return res;
        }

    }
}