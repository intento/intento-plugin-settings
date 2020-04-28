namespace Intento.MT.Plugin.PropertiesForm
{
	public class SmartRoutingState : BaseState
    {
        private bool smartRouting;

        // Parent state
        public ApiKeyState apiKeyState;

        // Controlled components
        public ProviderState providerState;

        public SmartRoutingState(ApiKeyState apiKeyState, IntentoMTFormOptions _options) : base(apiKeyState, _options)
        {
            this.apiKeyState = apiKeyState;
            smartRouting = _options.SmartRouting;
            formMT.checkBoxSmartRouting.Checked = smartRouting;

            CreateChildStates();
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, SmartRoutingState state)
        {
            if (state == null)
            {
                form.formMT.checkBoxSmartRouting.Enabled = false;

                ProviderState.Draw(form, null);
                return null;
            }

            return state.Draw();
        }

        public string Draw()
        {
            formMT.checkBoxSmartRouting.Enabled = true;
            return ProviderState.Draw(form, providerState);
        }

        public void CheckedChanged()
        {
            smartRouting = formMT.checkBoxSmartRouting.Checked;

            CreateChildStates();

            if (smartRouting)
                options.Format = "[\"text\",\"html\",\"xml\"]";

            EnableDisable();
			if (formMT.checkBoxSmartRouting.Checked)
				formMT.groupBoxOptional.Enabled = false;
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
            if (options.SmartRouting)
                options.Format = "[\"text\",\"html\",\"xml\"]";
        }

        private void CreateChildStates()
        {
            if (IsOK)
            {
                providerState = new ProviderState(this, options);
                if (SmartRouting)
                {
                    providerState.ClearOptions(options);
                    providerState = null;
                }
            }
            else
                providerState = null;
        }

    }
}
