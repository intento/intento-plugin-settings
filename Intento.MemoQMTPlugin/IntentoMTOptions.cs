using IntentoMemoQMTPlugin;
using System;

namespace IntentoMTPlugin
{
    /// <summary>
    /// Class for storing the Intento MT plugin settings.
    /// </summary>
    public class IntentoMTOptions : MemoQ.MTInterfaces.PluginSettingsObject<IntentoMTGeneralSettings, IntentoMTSecureSetting>
    {
	    /// <summary>
        /// Create instance by deserializing from provided serialized settings.
        /// </summary>
        public IntentoMTOptions(MemoQ.MTInterfaces.PluginSettings serializedSettings)
            : base(serializedSettings)
        {
        }

        /// <summary>
        /// Create instance by providing the settings objects.
        /// </summary>
        public IntentoMTOptions(IntentoMTGeneralSettings generalSettings, IntentoMTSecureSetting secureSettings)
            : base(generalSettings, secureSettings)
        {
        }
    }

    /// <summary>
    /// General settings, content preserved when settings are exported.
    /// </summary>
    public class IntentoMTGeneralSettings
    {
        public bool smartRouting = false;
        public string intentoTagReplacement;
		private string traceEndTimeSpan;

		public string Routing { get; set; } = "";

		public DateTime TraceEndTime
        {
            get {
	            long.TryParse(traceEndTimeSpan, out var ticks);
				return new DateTime(ticks);
			}
			set => traceEndTimeSpan = value.Ticks.ToString();
        }

		public string[] SupportedLanguages { get; set; } = Array.Empty<string>();

		public string ProviderId { get; set; }

		public string ProviderName { get; set; }

		public string ProviderFormats { get; set; }

		// CustomAuth
        public string ProviderKey { get; set; }

        public string CustomModel { get; set; }

        public string FromLanguage { get; set; }

        public string ToLanguage { get; set; }

        public int[] IntentoGlossaries { get; set; }

		public bool IntentoTagReplacement
		{
#if VARIANT_PUBLIC
				//get { return intentoTagReplacement == "1" &&  WrapperMemoQAddinsCommonFactory.Current.Wrapper.AdvancedSdk; }
				get { return false; }
#else
            get { return intentoTagReplacement != "0" && WrapperMemoQAddinsCommonFactory.Current.Wrapper.AdvancedSdk; }
#endif
			set { intentoTagReplacement = value ? "1" : "0"; }
		}
		public bool ForbidSaveApikey => true;
	}

	/// <summary>
	/// Secure settings, content not preserved when settings leave the machine.
	/// </summary>
	public class IntentoMTSecureSetting
    {
	    public string ApiKey { get; set; }
	    public string Glossary { get; set; }
	    public string CustomModel { get; set; }
    }
}
