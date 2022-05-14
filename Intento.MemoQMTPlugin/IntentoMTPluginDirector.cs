using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MemoQ.Addins.Common.Framework;
using MemoQ.MTInterfaces;
using Intento.MT.Plugin.PropertiesForm;
using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using Intento.SDK.DependencyInjection;
using Intento.SDK.DependencyInjection.Lite;
using Intento.SDK.Settings;
using Intento.SDK.Translate;
using IntentoMemoQMTPlugin;
using Microsoft.Extensions.Logging;

namespace IntentoMTPlugin
{
	/*
     * When plugin class instances are created
     * - One per plugin start: IntentoMTPluginDirector
     * - after change of lang pair: IntentoMTEngine
     * - During translate request: IntentoMTSession
     */
	/// <summary>
	/// The main class of the Intento MT plugin.
	/// </summary>
	public class IntentoMTPluginDirector : PluginDirectorBase, IModule
	{
		/// <summary>
		/// The identifier of the plugin.
		/// </summary>
		//public const string PluginId = "IntentoMT";
		private readonly IntentoMTServiceHelper intentoMTServiceHelper;

		/// <summary>
		/// The memoQ's application envi ronment; e.g., to provide UI language settings etc. to the plugin.
		/// </summary>
		private IEnvironment environment;

		private IntentoMTOptions Options { get; set; }

		private IRemoteLogService RemoteLogService => intentoMTServiceHelper.Locator?.Resolve<IRemoteLogService>();

		private ITranslateService TranslateService => intentoMTServiceHelper.Locator?.Resolve<ITranslateService>();

		/// <summary>
		/// Ctor
		/// </summary>
		public IntentoMTPluginDirector()
		{
			intentoMTServiceHelper = new IntentoMTServiceHelper();
			intentoMTServiceHelper.Logger.LogInformation("Init WrapperMemoQAddinsCommonFactory");
			WrapperMemoQAddinsCommonFactory.Current.Init(typeof(IModule).Assembly);
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="apiKey"></param>
		/// <returns></returns>
		private void Fabric(string apiKey)
		{
			intentoMTServiceHelper.Logger.LogInformation("Start init Locator in Fabric");
			try
			{
				intentoMTServiceHelper.Locator = new DefaultLocatorImpl();
				intentoMTServiceHelper.Locator.Init(new Options
				{
					ApiKey = apiKey,
					ClientUserAgent = PluginInfo.UserAgent,
					ServerUrl = PluginInfo.ApiPath,
					TmsServerUrl = PluginInfo.TmsApiPath,
					SyncwrapperUrl = PluginInfo.SyncwrapperApiPath,
					Proxy = PluginInfo.GetProxySettings()
				});
			}
			catch (Exception e)
			{
				intentoMTServiceHelper.Logger.LogError(e, "Error in init Locator in Fabric");
			}

			try
			{
				RemoteLogService.Init(PluginInfo.AppName);
			}
			catch (Exception e)
			{
				intentoMTServiceHelper.Logger.LogError(e, "Error in init RemoteLogService in Fabric");
			}
			
		}


		#region IModule Members

		public void Cleanup()
		{
			// write your cleanup code here
		}

		public void Initialize(IModuleEnvironment env)
		{
			// write your initialization code here
		}

		public bool IsActivated => true;

		#endregion

		#region IPluginDirector Members

		/// <summary>
		/// Indicates whether interactive lookup (in the translation grid) is supported or not.
		/// </summary>
		public override bool InteractiveSupported => true;

		/// <summary>
		/// Indicates whether batch lookup is supported or not.
		/// </summary>
		public override bool BatchSupported => true;

		/// <summary>
		/// Indicates whether storing translations is supported.
		/// </summary>
		public override bool StoringTranslationSupported => false;

		/// <summary>
		/// The plugin's non-localized name.
		/// </summary>
		public override string PluginID => IntentoMTServiceHelper.PluginID;

		/// <summary>
		/// Returns the friendly name to show in memoQ's Tools / Options.
		/// </summary>
		public override string FriendlyName
		{
			get
			{
				var marker = "a";
				try
				{
					var currentAssem = typeof(IntentoMTPluginDirector).Assembly;
					var titleAttribute = currentAssem.GetCustomAttribute<AssemblyTitleAttribute>();
					if (titleAttribute != null)
					{
						var title = titleAttribute.Title;
						if (!string.IsNullOrEmpty(title))
						{
							return title;
						}
					}
				}
				catch (Exception e)
				{
					intentoMTServiceHelper.Logger.LogError(e, "Error in get FriendlyName");
					marker = "z";
				}

				return $"Intento MT Plugin ({marker})";
			}
		}

		/// <summary>
		/// Return the copyright text to show in memoQ's Tools / Options.
		/// </summary>
		public override string CopyrightText
		{
			get
			{
				var a = "a";
				try
				{
					var currentAssem = typeof(IntentoMTPluginDirector).Assembly;
					var copyrightAttribute = currentAssem.GetCustomAttribute<AssemblyCopyrightAttribute>();
					if (copyrightAttribute != null)
					{
						var copyright = copyrightAttribute.Copyright;
						if (!string.IsNullOrEmpty(copyright))
						{
							return copyright;
						}
					}
				}
				catch (Exception e)
				{
					intentoMTServiceHelper.Logger.LogError(e, "Error in get CopyrightText");
					a = "z";
				}
				return $"Copyright © Intento 2018-2022 ({a})";
			}
		}

		/// <summary>
		/// Return a 48x48 display icon to show in MemoQ's Tools / Options. Black is the transparent color.
		/// </summary>
		public override Image DisplayIcon => Image.FromStream(
			Assembly.GetExecutingAssembly().GetManifestResourceStream("IntentoMemoQMTPlugin." + "Icon.bmp") ??
			throw new InvalidOperationException("Icon.bmp not founded"));

		/// <summary>
		/// The memoQ's application environment; e.g., to provide UI language settings etc. to the plugin.
		/// </summary>
		public override IEnvironment Environment
		{
			set
			{
				environment = value;
				// initialize the localization helper
				LocalizationHelper.Instance.SetEnvironment(value);
			}
		}

		private IDictionary<string, IList<IList<string>>> providerPairsDct;
		private IEnumerable<IList<string>> GetIntentoLangPairs(string providerId, string routing)
		{
			intentoMTServiceHelper.Logger.LogInformation("Start GetIntentoLangPairs method");
			IList<IList<string>> langPairs;
			string key;
			var sr = false; //smart routing sign
			providerPairsDct ??= new Dictionary<string, IList<IList<string>>>();

			if (string.IsNullOrEmpty(providerId))
			{
				// "smart routing"
				sr = true;
				key = string.IsNullOrWhiteSpace(routing) ? PluginInfo.DefaultRoutingName : routing;
			}
			else
			{
				key = providerId;
			}

			if (providerPairsDct.ContainsKey(key) && providerPairsDct[key] != null)
			{
				langPairs = providerPairsDct[key];
			}
			else
			{
				langPairs = intentoMTServiceHelper.IntentoLanguagePairs(key, sr);
				providerPairsDct[key] = langPairs;
			}

			intentoMTServiceHelper.Logger.LogInformation("GetIntentoLangPairs: {Pair}",
				string.Join("; ", langPairs.Select(p => string.Join(",", p))));
			return langPairs;
		}

		/// <summary>
		/// Tells memoQ if the plugin supports the provided language combination. The strings provided are memoQ language codes.
		/// </summary>
		public override bool IsLanguagePairSupported(LanguagePairSupportedParams args)
		{
			Options = intentoMTServiceHelper.Options = GetOptions(args.PluginSettings);
			var pairs = GetIntentoLangPairs(Options.GeneralSettings.ProviderId, Options.GeneralSettings.Routing);
			
			var sourceCode = args.SourceLangCode;
			var sourceCode2 = sourceCode;
			if (sourceCode2.Contains("-"))
			{
				sourceCode2 = sourceCode2.Substring(0, sourceCode2.IndexOf('-'));
			}

			var targetCode = args.TargetLangCode;
			var targetCode2 = targetCode;
			if (targetCode2 != null && targetCode2.Contains("-"))
			{
				targetCode2 = targetCode2.Substring(0, targetCode2.IndexOf('-'));
			}

			if (pairs.Any(pair => (pair[0] == sourceCode || pair[0] == sourceCode2 || pair[0] == "") &&
			                      (pair[1] == targetCode || pair[1] == targetCode2 || pair[1] == "")))
			{
				RemoteLogService.Write(PluginInfo.LogIdentificator,
					$"IntentoMTPluginDirector.IsLanguagePairSupported: true from:{sourceCode}/{sourceCode2} to:{targetCode}{targetCode2}",
					null);
				return true;
			}

			RemoteLogService.Write(PluginInfo.LogIdentificator,
				$"IntentoMTPluginDirector.IsLanguagePairSupported: false from:{sourceCode}/{sourceCode2} to:{targetCode}{targetCode2}",
				null);
			return false;

		}

		/// <summary>
		/// Creates an MT engine for the supplied language pair.
		/// </summary>
		public override IEngine2 CreateEngine(CreateEngineParams args)
		{
			intentoMTServiceHelper.Logger.LogInformation("Create IntentoMTEngine");
			return new IntentoMTEngine(intentoMTServiceHelper, args.SourceLangCode, args.TargetLangCode, GetOptions(args.PluginSettings));
		}

		/// <summary>
		/// Shows the plugin's options form.
		/// </summary>
		public override PluginSettings EditOptions(IWin32Window parentForm, PluginSettings settings)
		{
			intentoMTServiceHelper.Logger.LogInformation("Start prepare EditOptions");
			Options = intentoMTServiceHelper.Options = GetOptions(settings);
			var customModel = !string.IsNullOrEmpty(Options.SecureSettings.CustomModel)
				? Options.SecureSettings.CustomModel
				: Options.GeneralSettings.CustomModel;
			var formOptions = new IntentoMTFormOptions()
			{
				ApiKey = Options.SecureSettings.ApiKey,
				Signature =
					$"v{PluginInfo.PluginVersion}{PluginInfo.LocationLetter}{(IntentoMemoQMTPlugin.Properties.Settings.Default.Variant != null ? ":" + IntentoMemoQMTPlugin.Properties.Settings.Default.Variant : "")}",
				// If settings == null it is first usage of plugin
				SmartRouting = settings != null && string.IsNullOrEmpty(Options.GeneralSettings.ProviderId),
				Routing = Options.GeneralSettings.Routing,
				ProviderId = Options.GeneralSettings.ProviderId,
				ProviderName = Options.GeneralSettings.ProviderName,
				UseCustomAuth = !string.IsNullOrEmpty(Options.GeneralSettings.ProviderKey),
				CustomAuth = ProviderKeyMigration(Options.GeneralSettings.ProviderKey),
				UseCustomModel = !string.IsNullOrEmpty(customModel),
				CustomTagParser = Options.GeneralSettings.IntentoTagReplacement,
				CustomModel = customModel,
				FromLanguage = Options.GeneralSettings.FromLanguage,
				ToLanguage = Options.GeneralSettings.ToLanguage,
				Glossary = Options.SecureSettings.Glossary,
				IntentoGlossaries = Options.GeneralSettings.IntentoGlossaries,
				Format = "text",
				AppName = PluginInfo.AppName,
				TraceEndTime = Options.GeneralSettings.TraceEndTime,
				UserAgent = PluginInfo.UserAgent,
				СallHelpAction = CallHelp,
				MemoqAdditional = new Dictionary<string, object>()
				{
					{ "advancedSdk", WrapperMemoQAddinsCommonFactory.Current.Wrapper.AdvancedSdk },
					{ "VARIANT_PUBLIC", PluginInfo.MemoQVariant },
				}
			};

#if VARIANT_PUBLIC
                formOptions.ForbidSaveApikey = true;
                formOptions.HideHiddenTextButton = true;
#endif
			if (!string.IsNullOrWhiteSpace(PluginInfo.ConsoleUrl))
			{
				formOptions.ConsoleUrl = PluginInfo.ConsoleUrl;
			}

			formOptions.ApiPath = PluginInfo.ApiPath;
			formOptions.TmsApiPath = PluginInfo.TmsApiPath;

			intentoMTServiceHelper.Logger.LogInformation("Start open EditOptions");
			try
			{
				using var form = new IntentoTranslationProviderOptionsForm(
					formOptions,
					null,
					intentoMTServiceHelper.Locator,
					o =>
					{
						var impl = new DefaultLocatorImpl();
						impl.Init(o);
						return impl;
					}
				);
				form.Visible = false;
				if (form.ShowDialog(parentForm) == DialogResult.OK)
				{
					// Settings form was exited with Continue button
					environment.PluginAvailabilityChanged();

					Options.GeneralSettings.Routing = formOptions.Routing;
					if (Options.GeneralSettings.ProviderId != formOptions.ProviderId)
						providerPairsDct = null;

					Fabric(formOptions.ApiKey);
					intentoMTServiceHelper.Format = formOptions.Format;
					Options.GeneralSettings.ProviderFormats = formOptions.Format;
					Options.GeneralSettings.ProviderName = intentoMTServiceHelper.Format;
					Options.SecureSettings.ApiKey = formOptions.ApiKey;
					Options.GeneralSettings.ProviderKey = formOptions.CustomAuth;
					Options.SecureSettings.CustomModel = formOptions.CustomModel;
					Options.GeneralSettings.CustomModel = null;
					Options.GeneralSettings.FromLanguage = string.IsNullOrWhiteSpace(formOptions.CustomModel)
						? null
						: formOptions.FromLanguage;
					Options.GeneralSettings.ToLanguage =
						string.IsNullOrWhiteSpace(formOptions.CustomModel) ? null : formOptions.ToLanguage;
					Options.SecureSettings.Glossary = formOptions.Glossary;
					Options.GeneralSettings.ProviderId = formOptions.ProviderId;
					Options.GeneralSettings.ProviderName = formOptions.ProviderName;
					Options.GeneralSettings.IntentoTagReplacement = formOptions.CustomTagParser;
					Options.GeneralSettings.IntentoGlossaries = formOptions.IntentoGlossaries;
				}
				else
				{
					// Settings form was canceled 
					Fabric(Options.SecureSettings.ApiKey);
					if (string.IsNullOrEmpty(Options.GeneralSettings.ProviderFormats))
					{
						// migration: need to read provider formats from IntentoAPI
						if (string.IsNullOrEmpty(Options.GeneralSettings.ProviderId))
						{
							// smart routing
							Options.GeneralSettings.ProviderFormats = null;
						}
						else
						{
							var providerData = TranslateService.Provider(Options.GeneralSettings.ProviderId,
								new Dictionary<string, string> { { "fields", "auth,custom_glossary" } });
							Options.GeneralSettings.ProviderFormats =
								providerData.Format != null ? string.Join(" ", providerData.Format) : "";

						}
					}

					intentoMTServiceHelper.Format = Options.GeneralSettings.ProviderFormats;
				}

				Options.GeneralSettings.TraceEndTime = formOptions.TraceEndTime;
				return Options.GetSerializedSettings();
			}
			catch (Exception e)
			{
				intentoMTServiceHelper.Logger.LogError(e, "Error in open EditOptions");
				throw;
			}
		}

		private static string ProviderKeyMigration(string providerKey)
		{
			if (string.IsNullOrEmpty(providerKey))
			{
				return providerKey;
			}

			return providerKey[0] == '{' ? providerKey : $"{{\"credential_id\":\"{providerKey}\"}}";
		}

		private IntentoMTOptions GetOptions(PluginSettings settings)
		{
			intentoMTServiceHelper.Logger.LogInformation("Start GetOptions method");
			return intentoMTServiceHelper.SetOptions(settings);
		}

		#endregion

		private void CallHelp()
		{
#if VARIANT_PUBLIC
            (environment as IEnvironment2)?.ShowHelp("intento-plugin-settings.html");
#else
			Process.Start(PluginInfo.HelpUrl);
#endif
		}

	}
}
