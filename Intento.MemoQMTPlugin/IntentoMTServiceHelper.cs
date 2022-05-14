using System;
using System.Collections.Generic;
using System.Linq;
using Intento.MT.Plugin.PropertiesForm;
using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.SDK.DependencyInjection.Lite;
using Intento.SDK.DI;
using Intento.SDK.Settings;
using Intento.SDK.Translate;
using Intento.SDK.Translate.Options;
using IntentoMemoQMTPlugin;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace IntentoMTPlugin
{
    /// <summary>
    /// Helper class to be able to communicate with the web service.
    /// </summary>
    public partial class IntentoMTServiceHelper
    {
        private IRemoteLogService RemoteLogService => this.Locator?.Resolve<IRemoteLogService>();

        private ITranslateService TranslateService => this.Locator?.Resolve<ITranslateService>();

        /// <summary>
        /// Container for all  services
        /// </summary>
        public ILocatorImpl Locator { get; set; }
        
#if WRITELOG
        /// <summary>
        /// Logger for app
        /// </summary>
        public ILogger Logger { get; } = new MemoQLoggerProvider().CreateLogger("[MT Plugin UI]");
#else
        /// <summary>
        /// Logger for app
        /// </summary>
        public ILogger Logger { get; } = new NullLoggerFactory().CreateLogger("[MT Plugin UI]");
#endif


        /// <summary>
        /// Current options
        /// </summary>
        public IntentoMTOptions Options { get; set; }
        
        /// <summary>
        /// Batch result translate
        /// </summary>
		public class ResultBatchTranslate
		{
			public List<string> ResultTranslate { get; }
			public string ViaProvider { get; }

			public ResultBatchTranslate(List<string> res, string provider)
			{
				ResultTranslate = res;
				ViaProvider = provider;
			}
		}

        /// <summary>
        /// Format
        /// </summary>
        public string Format { get; set; }
        
        private string lastApiKey;

        /// <summary>
        /// Translates multiple strings with the help of the Intento MT service.
        /// </summary>
        /// <param name="intentoOptions"></param>
        /// <param name="input">The strings to translate.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        /// <param name="format"></param>
        /// <param name="routing"></param>
        /// <returns>ResultBatchTranslate: The translated strings & translation provider.</returns>
        public ResultBatchTranslate BatchTranslate(IntentoMTOptions intentoOptions, IEnumerable<string> input,
            string srcLangCode, string trgLangCode, string format, string routing)
        {
            
            AuthProviderInfo auth = null;
            if (!string.IsNullOrEmpty(intentoOptions.GeneralSettings.ProviderKey))
            {
                var authDict = intentoOptions.GeneralSettings.ProviderKey.AuthToDictionary();
                auth = new AuthProviderInfo
                {
                    Provider = intentoOptions.GeneralSettings.ProviderId,
                    Key = new[]
                    {
                        new KeyInfo
                        {
                            CredentialId = authDict["credential_id"]
                        }
                    }
                };
            }

            Options = intentoOptions;
            var request = new TranslateOptions
            {
                Text = input,
                To = trgLangCode,
                From = srcLangCode,
                Async = true,
                WaitAsync = true,
                Provider = intentoOptions.GeneralSettings.ProviderId,
                Format = format,
                Auth = auth != null ? new[] { auth } : null,
                Routing = routing,
                CustomModel = intentoOptions.SecureSettings.CustomModel ?? intentoOptions.GeneralSettings.CustomModel,
                Glossary = intentoOptions.SecureSettings.Glossary,
                IntentoGlossary = intentoOptions.GeneralSettings.IntentoGlossaries,
                Trace = RemoteLogService.IsTrace()
            };

            Logger.LogInformation("Start BatchTranslate: {Request}", JsonConvert.SerializeObject(request));
            try
            {
                var res = TranslateService.Fulfill(request);
                return new ResultBatchTranslate(res.Results.ToList(), $"{res.Service.Provider.Vendor} {res.Service.Provider.Description}");
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error in BatchTranslate");
                throw new Exception("The service returned a translation error. Try checking your Intento settings.");
            }
        }

        /// <summary>
        /// Stores a single string pair as translation with the help of the Intento MT service.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="source">The source string.</param>
        /// <param name="target">The target string.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        public void StoreTranslation(IntentoMTOptions options, string source, string target, string srcLangCode, string trgLangCode)
        {
            throw new Exception("Intento plugin do not support StoreTranslation");
        }

        /// <summary>
        /// Stores multiple string pairs as translation with the help of the Intento MT service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <param name="sources">The source strings.</param>
        /// <param name="targets">The target strings.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        /// <returns>The indices of the translation units that were succesfully stored.</returns>
        public int[] BatchStoreTranslation(IntentoMTOptions options, List<string> sources, List<string> targets, string srcLangCode, string trgLangCode)
        {
            throw new Exception("Intento plugin do not support BatchStoreTranslation");
        }

        public IList<IList<string>> IntentoLanguagePairs(string providerId, bool smartRouting)
        {
            var additionalParams = new Dictionary<string, string>
            {
                { "convertCodes", "true" }
            };
            Logger.LogInformation("Start request languages for {Providers}", smartRouting ? "routing" : "providers");
            var p = smartRouting
                ? TranslateService.RoutingLanguagePairs(providerId, additionalParams)
                : TranslateService.ProviderLanguagePairs(providerId, additionalParams);
            var pairs = p
                .Select(i => (IList<string>)new List<string> { i[0], i[1] })
                .Where(i => i[0] != null && i[1] != null)
                .ToList();
            Logger.LogInformation("IntentoLanguagePairs: {Pairs}",
                string.Join(";", pairs.Select(p => string.Join(",", p))));
            return pairs;
        }

        public IntentoMTOptions SetOptions(MemoQ.MTInterfaces.PluginSettings settings)
        {
            Logger.LogInformation("Start SetOptions");
            
            var options = new IntentoMTOptions(settings);

            if (Options == null || lastApiKey != null && options.SecureSettings.ApiKey != lastApiKey)
            {
                Options = options;
                lastApiKey = options.SecureSettings.ApiKey;
            }

            try
            {
                Locator = new DefaultLocatorImpl();
                Locator.Init(new Options
                {
                    ClientUserAgent = PluginInfo.UserAgent,
                    ServerUrl = PluginInfo.ApiPath,
                    TmsServerUrl = PluginInfo.TmsApiPath,
                    ApiKey = options.SecureSettings.ApiKey,
                    SyncwrapperUrl = PluginInfo.SyncwrapperApiPath,
                    Proxy = PluginInfo.GetProxySettings()
                });

                return options;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error in init locator in SetOptions");
                throw;
            }
            
        }

        /// <summary>
        /// The plugin's non-localized name.
        /// </summary>
        public static string PluginID
        {
            get
            {
#if VARIANT_PUBLIC
                return "IntentoMT";
#elif VARIANT_PRIVATE
                return "IntentoMT_private";
#else
				// It is necessary to define the plug-in version (VARIANT_PUBLIC and VARIANT_PRIVATE)
				// in the project properties
				return 1;
#endif
            }
        }

	}
}
