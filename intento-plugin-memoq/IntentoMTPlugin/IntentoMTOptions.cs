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
        public string[] supportedLanguages = new string[0];
        public string providerId;
        public string providerName;
        public string providerFormats;
        public string providerKey;      // customAuth
        public string customModel;
        public string fromLanguage;
        public string toLanguage;
		public string intentoTagReplacement;
		public string traceEndTimeSpan;

		public DateTime TraceEndTime
        {
            get {
				long ticks;
				long.TryParse(traceEndTimeSpan, out ticks);
				return new DateTime(ticks);
			}
			set { traceEndTimeSpan = value.Ticks.ToString(); }
        }


		public string[] SupportedLanguages
        {
            get { return supportedLanguages; }
            set { supportedLanguages = value; }
        }

        public string ProviderId
        {
            get { return providerId; }
            set { providerId = value; }
        }

        public string ProviderName
        {
            get { return providerName; }
            set { providerName = value; }
        }

        public string ProviderFormats
        {
            get { return providerFormats; }
            set { providerFormats = value; }
        }

        // CustomAuth
        public string ProviderKey
        {
            get { return providerKey; }
            set { providerKey = value; }
        }

        public string CustomModel
        {
            get { return customModel; }
            set { customModel = value; }
        }

        public string FromLanguage
        {
            get { return fromLanguage; }
            set { fromLanguage = value; }
        }

        public string ToLanguage
        {
            get { return toLanguage; }
            set { toLanguage = value; }
        }

		public bool IntentoTagReplacement
		{
			get { return intentoTagReplacement != "0"; }
			set { intentoTagReplacement = value ? "1" : "0"; }
		}
		public bool ForbidSaveApikey => true;
	}

	/// <summary>
	/// Secure settings, content not preserved when settings leave the machine.
	/// </summary>
	public class IntentoMTSecureSetting
    {
        public string ApiKey;
        public string glossary;
        public string customModel;
    }
}
