using System;
using System.Collections.Generic;
using System.Linq;
using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.SDK.DI;
using Intento.SDK.Settings;
using Newtonsoft.Json.Linq;

namespace Intento.MT.Plugin.PropertiesForm
{
	/// <summary>
	/// Used to store plugin settings. The application provides the previously saved settings as an instance of the class. 
	/// After changing the settings, they are saved to an instance of this class and, thus, passed to the application
	/// </summary>
	public partial class IntentoMTFormOptions
	{

		// --------------------------- Main parameters -------------------------------
		/// <summary>
		/// Api Key for Intento API
		/// </summary>
		public string ApiKey { get; set; }

		/// <summary>
		/// Use Smart Routing: automatic provider selection
		/// </summary>
		public bool SmartRouting { get; set; }

		/// <summary>
		/// Selected type of smart routing
		/// </summary>
		public string Routing { get; set; }
		
		/// <summary>
		/// Display name for routing
		/// </summary>
		public string RoutingDisplayName { get; set; }

		/// <summary>
		/// Selected provider id (in case of !SmartRouting)
		/// </summary>
		public string ProviderId { get; set; }

		/// <summary>
		/// Selected provider name
		/// </summary>
		public string ProviderName { get; set; }

		// --------------------------- Account and provider authentication parameters -------------------------------
		/// <summary>
		/// Using external authentication (not "Via Intento")
		/// </summary>
		public bool UseCustomAuth { get; set; }

		/// <summary>
		/// External authentication string
		/// </summary>
		public string CustomAuth { get; set; }

		/// <summary>
		/// Use of stored credentials, currently direct auth settings is not allowed in plugins
		/// </summary>
		public bool IsAuthDelegated { get; set; }

		/// <summary>
		/// Stored credential Id for Delegated account
		/// </summary>
		public string AuthDelegatedCredentialId { get; set; }

		/// <summary>
		/// Url to console
		/// </summary>
		public string ConsoleUrl { get; set; }

		/// <summary>
		/// Url of api
		/// </summary>
		public string ApiPath { get; set; }

		/// <summary>
		/// Url of tms backend
		/// </summary>
		public string TmsApiPath { get; set; }

		/// <summary>
		/// Authentication mode from provider, ignored in settings received from the application
		/// </summary>
		public StateModeEnum AuthMode { get; set; }

		// --------------------------- Custom model/glossary settings -------------------------------

		/// <summary>
		/// Use custom model
		/// </summary>
		public bool UseCustomModel { get; set; }

		/// <summary>
		/// Custom model id
		/// </summary>
		public string CustomModel { get; set; }

		/// <summary>
		/// Custom model name (if evailable), ignored in settings received from the application
		/// </summary>
		public string CustomModelName { get; set; }

		/// <summary>
		/// Source language for the selected model (if available). Used to select language pair during testing settings
		/// </summary>
		public string FromLanguage { get; set; }

		/// <summary>
		/// Target language for the selected model (if available). Used to select language pair during testing settings
		/// </summary>
		public string ToLanguage { get; set; }

		/// <summary>
		/// Model required/optional/prohibited (from provider). 
		/// </summary>
		public StateModeEnum CustomModelMode { get; set; }

		// string of custom glossary
		public string Glossary { get; set; }

		/// <summary>
		/// Array of selected intento glossaries
		/// </summary>
		public int[] IntentoGlossaries { get; set; }

		// string of custom glossary name
		public string GlossaryName { get; set; }

		//
		public StateModeEnum GlossaryMode { get; set; }

		// --------------------------- Internal settings, ignored if received from the application -------------------------------

		/// <summary>
		/// Formats supported by provider (text, html, xml), not used for smart routing mode
		/// </summary>
		public string Format { get; set; }

		/// <summary>
		/// User-Agent of plugin: together with the UserMode of the SettingsForm is sent 
		/// to the Intento API to identify requests
		/// </summary>
		public string UserAgent { get; set; }

		/// <summary>
		/// Application signature/version - shown in Advanced window together with plugin version
		/// </summary>
		public string Signature { get; set; }

		string appName;
		/// <summary>
		/// Plugin name. Use to store ApiKey in registry by this name and as an identifier for the log entry. 
		/// </summary>
		public string AppName
		{
			get => appName;
			set
			{
				appName = value;
				//Locator.Resolve<IRemoteLogService>().Init(value);
			}
		}

		/// <summary>
		/// Proxy server configuration class for service requests
		/// </summary>
		public ProxySettings ProxySettings { get; set; }

		/// <summary>
		/// Sign of saving settings in the registry
		/// </summary>
		public bool SaveLocally { get; set; }

		// --------------------------- SDL Trados Studio and memoQ only ---------------------------

		// Custom settings name. Using  in trados plugin
		public string CustomSettingsName { get; set; }

		// Custom html tags parser. Using  in MemoQ plugin
		public bool CustomTagParser { get; set; }

		// Remove tags from source text. Now (2020-08-10) used only in the Trados plugin
		public bool CutTag { get; set; }

		// Special options for public memoQ plugin special requirements
		// Forbid to save ApiKey in registry 
		public bool ForbidSaveApikey { get; set; }

		// Hide "Show hidden text" button
		public bool HideHiddenTextButton { get; set; }

		// Action on pressing Help button. In case it is not empty Help button is shown
		public Action СallHelpAction { get; set; }

		// --------------------------- Internal (not serialized by application) ---------------------------

		/// <summary>
		/// Log text (30 min) end time
		/// </summary>
		public DateTime TraceEndTime { get; set; }

		/// <summary>
		/// Additional data from Memoq plugin
		/// not used now, preparation for the future
		/// </summary>
		public Dictionary<string, object> MemoqAdditional { get; set; }

		/// <summary>
		/// Parameter to launch the form in hidden view
		/// </summary>
		public bool Hidden { get; set; }

		private Dictionary<string, string> _authDict;

		public void SetAuthDict(Dictionary<string, string> authDict)
		{
			if (authDict == null)
				CustomAuth = "";
			else
			{
				var entr = authDict.Select(d => $"\"{d.Key}\": \"{d.Value}\"");
				CustomAuth = "{" + string.Join(",", entr) + "}";
			}
		}

		public Dictionary<string, string> AuthDict()
		{
			if (_authDict != null)
			{
				return _authDict;
			}

			if (string.IsNullOrWhiteSpace(CustomAuth))
			{
				return null;
			}

			try
			{
				dynamic credential = JToken.Parse(CustomAuth);
				if (((JObject)credential).HasValues)
				{
					_authDict = new Dictionary<string, string>();
					foreach (JProperty param in credential)
					{
						_authDict.Add(param.Name, (string)param.Value);
					}
					return _authDict;
				}
			}
			catch
			{
				// ignored
			}
			return null;
		}

		public IntentoMTFormOptions Duplicate()
		{
			var res = new IntentoMTFormOptions();
			Fill(res);
			return res;
		}

		public void Fill(IntentoMTFormOptions res)
		{
			res.ApiKey = ApiKey;
			res.SmartRouting = SmartRouting;
			res.ProviderId = ProviderId;
			res.ProviderName = ProviderName;
			res.UseCustomAuth = UseCustomAuth;
			res.CustomAuth = CustomAuth;
			res.UseCustomModel = UseCustomModel;
			res.Glossary = Glossary;
			res.IntentoGlossaries = IntentoGlossaries;
			res.CustomModel = CustomModel;
			res.Format = Format;
			res.UserAgent = UserAgent;
			res.Signature = Signature;
			res.AppName = AppName;
			res.ForbidSaveApikey = ForbidSaveApikey;
			res.HideHiddenTextButton = HideHiddenTextButton;
			res.CustomSettingsName = CustomSettingsName;
			res.CustomTagParser = CustomTagParser;
			res.CutTag = CutTag;
			res.СallHelpAction = СallHelpAction;
			res.FromLanguage = FromLanguage;
			res.ToLanguage = ToLanguage;
			res.TraceEndTime = TraceEndTime;
			res.MemoqAdditional = MemoqAdditional;
			res.SaveLocally = SaveLocally;
			res.Routing = Routing;
			res.RoutingDisplayName = RoutingDisplayName;
			res._authDict = _authDict == null ? null : new Dictionary<string, string>(_authDict);
		}

		public IntentoMTFormOptions()
		{
			AppName = string.Empty;
			Signature = string.Empty;
		}
	}
}
