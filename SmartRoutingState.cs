using IntentoMT.Plugin.PropertiesForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class SmartRoutingState
    {
        bool smartRouting;
        IntentoTranslationProviderOptionsForm form;

        public SmartRoutingState(IntentoTranslationProviderOptionsForm _form, IntentoMTFormOptions _options)
        {
            form = _form;
            smartRouting = _options.SmartRouting;
            form.checkBoxSmartRouting.Checked = smartRouting;
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, SmartRoutingState state)
        {
            if (state == null)
            {
                form.checkBoxSmartRouting.Enabled = false;
                return null;
            }

            return state.Draw();
        }

        public string Draw()
        {
            if (!form.apiKeyState.IsOK)
            {
                form.groupBoxProviderSettings.Visible = true;
                form.groupBoxProviderSettings.Enabled = false;
                form.checkBoxSmartRouting.Visible = true;
                form.checkBoxSmartRouting.Enabled = false;
                return null;
            }
            form.groupBoxProviderSettings.Visible = !smartRouting;
            form.groupBoxProviderSettings.Enabled = !smartRouting;
            form.checkBoxSmartRouting.Enabled = true;
            return null;
        }

        public void CheckedChanged()
        {
            smartRouting = form.checkBoxSmartRouting.Checked;

            form.providerState = new ProviderState(form, form.GetOptions());

            if (smartRouting)
            {
                form.comboBoxGlossaries.Items.Clear();
                form.textBoxGlossary.Text = null;
                form.GetOptions().Format = "[\"text\",\"html\",\"xml\"]";
            }
            else
            {
            }
            form.EnableDisable();
        }

        public bool IsOK
        {
            get
            {
                if (!form.apiKeyState.IsOK)
                    return false;
                return true;
            }
        }

        public bool SmartRouting
        { get { return smartRouting; } }

        public void FillOptions(IntentoMTFormOptions options)
        {
            options.SmartRouting = SmartRouting;
        }

    }
}
