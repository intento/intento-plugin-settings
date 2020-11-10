using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.CSharp;
using Microsoft.Win32;

using IntentoSDK;
using System.Reflection;
using System.Diagnostics;
using Intento.MT.Plugin.PropertiesForm;
using Newtonsoft.Json.Linq;
using MemoQ.Addins.Common.DataStructures;
using MemoQ.Addins.Common.Utils;

namespace IntentoMTPlugin
{
    /// <summary>
    /// Helper class to be able to communicate with the web service.
    /// </summary>
    public partial class IntentoMTServiceHelper
    {
        public IntentoAiTextTranslate intentoAiTextTranslate;
        public IntentoMTOptions options;

		public class ResultBatchTranslate
		{
			public List<string> resultTranslate { get; set; }
			public string viaProvider { get; set; }

			public ResultBatchTranslate(List<string> res, string provider)
			{
				resultTranslate = res;
				viaProvider = provider;
			}
		}

		// static Dictionary<string, string> langConvertionIntento2MemoQ = new Dictionary<string, string>();
		static Dictionary<string, string> langConvertionMemoQ2Intento = new Dictionary<string, string>();

        public static string pluginVersion = GetPluginVersion();
        public static string memoqVersion = GetMemoQVersion();
        public static string memoqVersion2 = memoqVersion == null ? null : memoqVersion.Substring(0, 3);
        public static string locationLetter = LocationLetter();
        public string format;
        string lastApiKey;
        Func<IntentoAiTextTranslate> fabric;

        public IntentoMTServiceHelper(Func<IntentoAiTextTranslate> fabric)
        {
            this.fabric = fabric;
            foreach (List<string> ll in langList)
            {
                string ll1 = ll[1];
                string ll2 = ll[2].ToLower();
                if (langConvertionMemoQ2Intento.ContainsKey(ll1))
                    throw new Exception(ll1);
                langConvertionMemoQ2Intento[ll1] = ll2;
            }
        }

        private static string GetCodebase(Assembly ass)
        {
            try
            {
                return ass.CodeBase;
            }
            catch
            {
                return "unknown";
            }
        }

        private static void LogAssemblies(Assembly[] domainAssemblies)
        {
            IEnumerable<string> res = domainAssemblies.Select(i => string.Format("CodeBase: '{0}', FullName: '{1}'", GetCodebase(i), i.FullName));
            Logs.Write2("Assemblies", string.Join("\r\n", res));
        }

        private static string GetMemoQVersion()
        { 
            AppDomain domain = AppDomain.CurrentDomain;
            Assembly[] domainAssemblies = domain.GetAssemblies();
            LogAssemblies(domainAssemblies);

            List<string> names = new List<string>();
            foreach (Assembly ass in domainAssemblies)
            {
                Module manifest = ass.ManifestModule;
                if (manifest == null)
                    continue;
                string name = manifest.Name;
                names.Add(name);
                if (name == null)
                    continue;
                name = name.ToLower();
                if (name != null && name.ToLower() == "memoq.exe")
                {
                    string version = string.Format("{0}", ass.GetName().Version);
                    Logs.Write2(string.Format("memoq version: {0}", version), string.Join("\r\n", names));
                    return version;
                }
            }

            Logs.Write2("Memoq version not found", string.Join("\r\n", names));
            return null;
        }

        private static string GetPluginVersion()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var fvi = assembly.GetName().Version;
				string version;
				if (fvi.Revision == -1)
					version = string.Format("{0}.{1}.{2}", fvi.Major, fvi.Minor, fvi.Build);
				else
					version = string.Format("{0}.{1}.{2}.{3}", fvi.Major, fvi.Minor, fvi.Build, fvi.Revision);

				Logs.Write2(string.Format("plugin version: {0}", version), null);

                return version;
            }
            catch { }

            return "unknown";
        }

        private static string LocationLetter()
        {
            try
            {
                Assembly currentAssem = typeof(IntentoMTServiceHelper).Assembly;
                string codebase = string.Format("{0}", currentAssem.GetName().CodeBase);
                codebase = codebase.ToLower();
                if (codebase.Contains("/memoq server/"))
                {
                    Logs.Write2("Server version", string.Format("path: {0}", codebase));
                    return "s";
                }
                if (codebase.Contains("/addins/"))
                {
                    Logs.Write2("Client version", string.Format("path: {0}", codebase));
                    return "c";
                }
                if (codebase.Contains("memoq.translationenvironment.gui.dll"))
                {
                    Logs.Write2("Server settings version", string.Format("path: {0}", codebase));
                    return "q";
                }
                Logs.Write2("Unknown version", string.Format("path: {0}", codebase));
                return "u";
            }
            catch { }

            Logs.Write2("Unknown version, no path", null);
            return "x";
        }

        public string ApiKey
        {
            get { return options.SecureSettings.ApiKey; }
        }


        /// <summary>
        /// Converts MemoQ lang code to Intento lang code
        /// </summary>
        /// <param name="langs_raw"></param>
        /// <returns></returns>
        public string ConvertLangCodeToIntento(string lang_memoq)
        {
            string lang_intento;
            if (!langConvertionMemoQ2Intento.TryGetValue(lang_memoq, out lang_intento))
                return null;
            return lang_intento;
        }

        public IList<dynamic> Providers()
        {
            return intentoAiTextTranslate.Providers(/*fields: new List<string> { "auth" },*/ filter: new Dictionary<string, string> { { "integrated", "true" }, { "published", "true" } });
        }

		/// <summary>
		/// Translates multiple strings with the help of the Intento MT service.
		/// </summary>
		/// <param name="tokenCode">The token code.</param>
		/// <param name="input">The strings to translate.</param>
		/// <param name="srcLangCode">The source language code.</param>
		/// <param name="trgLangCode">The target language code.</param>
		/// <returns>ResultBatchTranslate: The translated strings & translation provider.</returns>
		public ResultBatchTranslate BatchTranslate(IntentoMTOptions options, IEnumerable<string> input, string srcLangCode, string trgLangCode, string format=null, string routing = null)
        {
            // using (new Logs.Pair("Helper.BatchTranslate"))
            {
                string auth = null;
                if (!string.IsNullOrEmpty(options.GeneralSettings.ProviderKey))
                    auth = string.Format("{{\"{0}\":[{1}]}}", options.GeneralSettings.ProviderId, options.GeneralSettings.ProviderKey);
                this.options = options;
                dynamic res = intentoAiTextTranslate.Fulfill(
                    input,
                    ConvertLangCodeToIntento(trgLangCode),
                    from: ConvertLangCodeToIntento(srcLangCode),
                    async: true, wait_async: true,
                    provider: options.GeneralSettings.ProviderId,
                    format: format,
                    auth: auth,
                    routing: routing,
                    custom_model: options.SecureSettings.customModel != null ? options.SecureSettings.customModel : options.GeneralSettings.CustomModel,
                    glossary: options.SecureSettings.glossary,
                    trace: IntentoTranslationProviderOptionsForm.IsTrace());

                if (res.response == null || !((JContainer)res.response).HasValues)
                    throw new Exception("The service returned a translation error. Try checking your Intento settings.");
                List<string> list = new List<string>();
                foreach (string z in res.response[0].results)
                    list.Add(z);
				string provider = (string)res.meta?.providers[0]?.name;
                return new ResultBatchTranslate(list, provider);
            }
        }

        /// <summary>
        /// Stores a single string pair as translation with the help of the Intento MT service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
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

        public IList<IList<string>> IntentoLanguagePairs(string providerId)
        {
			IList<IList<string>> pairs = null;
			if (string.IsNullOrEmpty(providerId))
            {   // smart routing 
                IList<dynamic> data = IntentoAiTextTranslate().Languages();

                List<string> langs = data.Select(i => (string)i.intento_code).ToList();
                pairs = new List<IList<string>>();
                foreach (string lang1 in langs)
                    foreach (string lang2 in langs)
                    {
                        if (lang1 == lang2)
                            continue;
                        pairs.Add(new List<string>() { lang1, lang2 });
                    }
                return pairs;
            }

            // direct routing 
            IList<IList<string>> p = IntentoAiTextTranslate().ProviderLanguagePairs(providerId);
            pairs = p
                .Select(i => (IList<string>)new List<string> { i[0], i[1] })
                .Where(i => (i[0] != null && i[1] != null))
                .ToList();

            Dictionary<string, IList<string>> d = new Dictionary<string, IList<string>>();
            foreach (IList<string> pair in p)
            {
                IList<string> val;
                if (!d.TryGetValue(pair[0], out val))
                    d[pair[0]] = new List<string> { pair[1] };
                else
                    d[pair[0]].Add(pair[1]);
            }

            return pairs;
        }

        private IntentoAiTextTranslate IntentoAiTextTranslate()
        {
            if (intentoAiTextTranslate != null)
                return intentoAiTextTranslate;
            intentoAiTextTranslate = fabric();
            return intentoAiTextTranslate;
        }


        public IntentoMTOptions SetOptions(MemoQ.MTInterfaces.PluginSettings settings)
        {
            IntentoMTOptions options = new IntentoMTOptions(settings);

            if (this.options == null || lastApiKey != null && options.SecureSettings.ApiKey != lastApiKey)
            {
                this.options = options;
                lastApiKey = options.SecureSettings.ApiKey;
            }

            return options;
        }

        public static string GetGitCommitHash(Assembly currentAssem)
        {
            string hash = "unknown";
            try
            {
                var attr = currentAssem.GetCustomAttributes(typeof(AssemblyGitHash), false).Cast<AssemblyGitHash>().FirstOrDefault();
                if (attr != null)
                    hash = attr.Hash().Substring(0, 7);
            }
            catch { }

            return hash;
        }

        private static void AddString(List<string> res, string name, Func<string>f)
        {
            try
            {
                res.Add(string.Format("{0}/{1}", name, f()));
            }
            catch(Exception ex)
            {
                res.Add(string.Format("{0}: Error {1}", name, ex.Message));
            }
        }

        public static string GetEnvDataExtended()
        {
            List<string> res = new List<string>();
            AddString(res, "MachineName", ()=>Environment.MachineName);
            AddString(res, "CurrentDirectory", () => Environment.CurrentDirectory);
            AddString(res, "intento_plugin_logging", () => Environment.GetEnvironmentVariable("intento_plugin_logging"));
            AddString(res, "OSVersion", () => Environment.OSVersion.VersionString);
            AddString(res, "StackTrace", () => Environment.StackTrace);
            AddString(res, "UserName", () => Environment.UserName);
            AddString(res, "Version", () => Environment.Version.ToString());
            RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento\\{0}", PluginID));
            AddString(res, "RegistryLogging", () => (string)key.GetValue("Logging", null));

            return string.Join("\r\n", res);
        }

        public static string GetEnvData()
        {
            List<string> res = new List<string>();
            AddString(res, "MachineName", () => Environment.MachineName);
            AddString(res, "UserName", () => Environment.UserName);

            if (res.Count != 0)
                return string.Join(" ", res);
            else
                return "";
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
