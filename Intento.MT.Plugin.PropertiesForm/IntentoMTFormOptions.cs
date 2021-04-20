using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntentoSDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Intento.MT.Plugin.PropertiesForm
{
	/// <summary>
	/// Used to store plugin settings. The application provides the previously saved settings as an instance of the class. 
	/// After changing the settings, they are saved to an instance of this class and, thus, passed to the application
	/// </summary>
	public class IntentoMTFormOptions
	{

		// --------------------------- Main parameters -------------------------------
		string _apiKey;
		/// <summary>
		/// Api Key for Intento API
		/// </summary>
		public string ApiKey
		{
			get { return _apiKey; }
			set
			{
				_apiKey = value;
				Logs.ApiKey = value;
			}
		}

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

		public enum StateModeEnum
		{
			unknown = 0,

			/// <summary>
			/// External autherntication is prohibited, only "via Intento"
			/// </summary>
			prohibited,

			/// <summary>
			/// External autherntication is required, "via Intento" is prohibited
			/// </summary>
			required,

			/// <summary>
			/// External autherntication is optional
			/// </summary>
			optional
		}

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

		string _appName;
		/// <summary>
		/// Plugin name. Use to store ApiKey in registry by this name and as an identifier for the log entry. 
		/// </summary>
		public string AppName
		{
			get { return _appName; }
			set
			{
				_appName = value;
				Logs.PluginName = value;
			}
		}

		/// <summary>
		/// Proxy server configuration class for service requests
		/// </summary>
		public ProxySettings proxySettings { get; set; }

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
		// Forbit to save ApiKey in registry 
		public bool ForbidSaveApikey { get; set; }

		// Hide "Show hidden text" button
		public bool HideHiddenTextButton { get; set; }

		// Action on pressing Help button. In case it is not empty Help button is shown
		public Action сallHelpAction;

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
		/// Intento C# SDK
		/// </summary>
		public IntentoAiTextTranslate Translate { get; set; }

		/// <summary>
		/// Parameter to launch the form in hidden view
		/// </summary>
		public bool Hidden { get; set; }

		private Dictionary<string, string> _authDict = null;

		public void SetAuthDict(Dictionary<string, string> _authDict)
		{
			if (_authDict == null)
				CustomAuth = "";
			else
			{
				var entr = _authDict.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key, d.Value));
				CustomAuth = "{" + string.Join(",", entr) + "}";
			}
		}

		public Dictionary<string, string> authDict()
		{
			if (_authDict != null)
				return _authDict;
			if (!string.IsNullOrWhiteSpace(CustomAuth))
			{
				dynamic credential;
				try
				{
					credential = JToken.Parse(CustomAuth);
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
				catch { }
			}
			return null;
		}

		public IntentoMTFormOptions Duplicate()
		{
			IntentoMTFormOptions res = new IntentoMTFormOptions();
			Fill(res);
			return res;
		}

		public void Fill(IntentoMTFormOptions res)
		{
			res.ApiKey = this.ApiKey;
			res.SmartRouting = this.SmartRouting;
			res.ProviderId = this.ProviderId;
			res.ProviderName = this.ProviderName;
			res.UseCustomAuth = this.UseCustomAuth;
			res.CustomAuth = this.CustomAuth;
			res.UseCustomModel = this.UseCustomModel;
			res.Glossary = this.Glossary;
			res.CustomModel = this.CustomModel;
			res.Format = this.Format;
			res.UserAgent = this.UserAgent;
			res.Signature = this.Signature;
			res.AppName = this.AppName;
			res.Translate = this.Translate;
			res.ForbidSaveApikey = this.ForbidSaveApikey;
			res.HideHiddenTextButton = this.HideHiddenTextButton;
			res.CustomSettingsName = this.CustomSettingsName;
			res.CustomTagParser = this.CustomTagParser;
			res.CutTag = this.CutTag;
			res.сallHelpAction = this.сallHelpAction;
			res.FromLanguage = this.FromLanguage;
			res.ToLanguage = this.ToLanguage;
			res.TraceEndTime = this.TraceEndTime;
			res.MemoqAdditional = this.MemoqAdditional;
			res.SaveLocally = this.SaveLocally;
			res.Routing = this.Routing;
			res._authDict = _authDict == null ? null : new Dictionary<string, string>(_authDict);
		}

		public IntentoMTFormOptions()
		{
			AppName = string.Empty;
			Signature = string.Empty;
		}
	}
}
