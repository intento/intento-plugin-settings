using Intento.MT.Plugin.PropertiesForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class SmartRoutingState : BaseState
    {
        bool smartRouting;
        public ApiKeyState apiKeyState;
        public ProviderState providerState;

        public SmartRoutingState(ApiKeyState apiKeyState, IntentoMTFormOptions _options) : base(apiKeyState, _options)
        {
            this.apiKeyState = apiKeyState;
            smartRouting = _options.SmartRouting;
            form.SmartRouting_CheckBox_Checked = smartRouting;

            CreateChildStates();
        }

        public static string Draw(IForm form, SmartRoutingState state)
        {
            if (state == null)
            {
                form.SmartRouting_CheckBox_Enabled = false;
                ProviderState.Draw(form, null);
                return null;
            }

            return state.Draw();
        }

        public string Draw()
        {
            form.SmartRouting_CheckBox_Enabled = true;
            return ProviderState.Draw(form, providerState);
        }

        public void CheckedChanged()
        {
            smartRouting = form.SmartRouting_CheckBox_Checked;

            CreateChildStates();

            if (smartRouting)
                options.Format = "[\"text\",\"html\",\"xml\"]";

            EnableDisable();
        }

        public bool IsOK
        {
            get
            {
                return true;
            }
        }

        public bool SmartRouting
        { get { return smartRouting; } }

        public static void FillOptions(SmartRoutingState state, IntentoMTFormOptions options)
        {
            if (state == null)
            {
                options.SmartRouting = true;
                ProviderState.FillOptions(null, options);
            }
            else
            {
                options.SmartRouting = state.SmartRouting;
                ProviderState.FillOptions(state.providerState, options);
            }
        }

        private void CreateChildStates()
        {
            if (IsOK)
            {
                if (!SmartRouting)
                    providerState = new ProviderState(this, options);
                else
                    providerState = null;
            }
            else
                providerState = null;
        }

    }
}
