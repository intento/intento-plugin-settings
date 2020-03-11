using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.CSharp;

using IntentoSDK;
using System.Reflection;
using System.Diagnostics;
using Intento.MT.Plugin.PropertiesForm;
using Newtonsoft.Json.Linq;

namespace IntentoMTPlugin
{
    /// <summary>
    /// Helper class to be able to communicate with the web service.
    /// </summary>
    public partial class IntentoMTServiceHelper
    {
        public IntentoAiTextTranslate intentoAiTextTranslate;
        public IntentoMTOptions options;

        // static Dictionary<string, string> langConvertionIntento2MemoQ = new Dictionary<string, string>();
        static Dictionary<string, string> langConvertionMemoQ2Intento = new Dictionary<string, string>();
        IList<IList<string>> pairs = null;

        public static string version = GetVersion();
        public static string memoqVersion = GetMemoQVersion();
        public static string memoqVersion2 = memoqVersion == null ? null : memoqVersion.Substring(0, 3);
        public static bool isServer = IsServer();
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

        private static string GetMemoQVersion()
        { 
            AppDomain domain = AppDomain.CurrentDomain;
            Assembly[] domainAssemblies = domain.GetAssemblies();
            foreach (Assembly ass in domainAssemblies)
            {
                var manifest = ass.ManifestModule;
                if (manifest == null)
                    continue;
                string name = manifest.Name;
                if (name == null)
                    continue;
                name = name.ToLower();
                if (name != null && name.ToLower() == "memoq.exe")
                {
                    string version = string.Format("{0}", ass.GetName().Version);
                    return version;
                }
            }

            return null;
        }

        private static string GetVersion()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var fvi = assembly.GetName().Version;
                string version = string.Format("{0}.{1}.{2}", fvi.Major, fvi.Minor, fvi.Build);

                return version;
            }
            catch { }

            return "unknown";
        }

        private static bool IsServer()
        {
            try
            {
                Assembly currentAssem = typeof(IntentoMTServiceHelper).Assembly;
                string codebase = string.Format("{0}", currentAssem.GetName().CodeBase);
                if (!codebase.Contains("MemoQ Server"))
                {
                    Logs.Write(string.Format("Client version, path: {0}", codebase));
                    return false;
                }
                Logs.Write(string.Format("Server version, path: {0}", codebase));
                return true;
            }
            catch { }

            Logs.Write(string.Format("Server version, no path"));
            return true;
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
        /// Translates a single string with the help of the Intento MT service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <param name="input">The string to translate.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        /// <returns>The translated string.</returns>
        public string Translate(IntentoMTOptions options, string input, string srcLangCode, string trgLangCode, string format = null, string routing = null)
        {
            using (new Logs.Pair("Helper.Translate"))
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
                    custom_model: !string.IsNullOrEmpty(options.SecureSettings.customModel) ? options.SecureSettings.customModel : options.GeneralSettings.CustomModel,
                    glossary: options.SecureSettings.glossary,
                    trace: IntentoTranslationProviderOptionsForm.IsTrace());

                if (res.error != null && ((JContainer)res.error).HasValues)
                    throw new Exception("The service returned a translation error. Try checking your Intento settings.");
                List<string> list = new List<string>();
                foreach (string z in res.response[0].results)
                    list.Add(z);
                return list[0];
            }
        }

        /// <summary>
        /// Translates multiple strings with the help of the Intento MT service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <param name="input">The strings to translate.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        /// <returns>The translated strings.</returns>
        public List<string> BatchTranslate(IntentoMTOptions options, IEnumerable<string> input, string srcLangCode, string trgLangCode, string format=null, string routing = null)
        {
            using (new Logs.Pair("Helper.BatchTranslate"))
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
                return list;
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

        public IList<IList<string>> IntentoLanguagePairs()
        {
            if (string.IsNullOrEmpty(options.GeneralSettings.providerId))
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
            IList<IList<string>> p = IntentoAiTextTranslate().ProviderLanguagePairs(options.GeneralSettings.providerId);
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

        /*
        public IList<IList<string>> LanguagePairs()
        {
            if (pairs != null)
                return pairs;

            using (new Logs.Pair("Helper.Translate"))
            {
                // this.options = options;

                if (string.IsNullOrEmpty(options.GeneralSettings.providerId))
                {   // smart routing 
                    IList<dynamic> data = IntentoAiTextTranslate().Languages();

                    List<string>langs = data.Select(i => (string)i.intento_code).ToList();
                    pairs = new List<IList<string>>();
                    foreach (string lang1 in langs)
                        foreach (string lang2 in langs)
                        {
                            if (lang1 == lang2)
                                continue;
                            pairs.Add(new List<string>() { ConvertLangCodeToMemoQ(lang1), ConvertLangCodeToMemoQ(lang2) });
                        }
                    return pairs;
                }

                IList<IList<string>> p = IntentoAiTextTranslate().ProviderLanguagePairs(options.GeneralSettings.providerId);
                pairs = p
                    .Select(i => (IList<string>)new List<string> { ConvertLangCodeToMemoQ(i[0]), ConvertLangCodeToMemoQ(i[1]) })
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
        } */


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
            else if (options.GeneralSettings.ProviderId != this.options.GeneralSettings.ProviderId)
            {
                pairs = null;
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
    }
}
