using System;
using System.Reflection;
using Intento.SDK.Settings;
using Microsoft.Win32;

namespace IntentoMemoQMTPlugin
{
    public static class PluginInfo
    {

#if AMADEUS
        public const string ApiPath = "https://api-amadeus.inten.to/";
        public const string TmsApiPath = "https://connectors-amadeus.inten.to/tms";
        public const string SyncwrapperApiPath = "";
        public const string PluginTitle = "Intento MT Plugin (Private) for Amadeus";
        public const string ConsoleUrl = "https://console-amadeus.inten.to";
#elif LOCAL
        public const string ApiPath = "https://api2.inten.to";
        public const string TmsApiPath = "https://localhost:5001";
        public const string SyncwrapperApiPath = "";
        public const string PluginTitle = "Intento MT Plugin (Private) dev";
        public const string ConsoleUrl = "";
#elif STAGE
        public const string ApiPath = "https://api2.inten.to";
        public const string TmsApiPath = "https://connectors-stage.inten.to/tms";
        public const string SyncwrapperApiPath = "";
        public const string PluginTitle = "Intento MT Plugin (Private)";
        public const string ConsoleUrl = "";
#else
        public const string ApiPath = "https://api.inten.to";
        public const string TmsApiPath = "https://connectors.inten.to/tms";
        public const string SyncwrapperApiPath = "https://syncwrapper-memoq.inten.to";
        public const string PluginTitle = "Intento MT Plugin (Private)";
        public const string ConsoleUrl = "";
#endif
        public const string DefaultRoutingName = "best";
        public const char LogIdentificator = 'M';
        
        /// <summary>
        /// Switching the Plugin variant: 
        /// VARIANT_PRIVATE or VARIANЕ_PUBLIC in "conditional compilation symbols" settings of Visual Studio
        /// !!! Also! Need to change field AssemblyTitle in AssemblyInfo !!!
        /// </summary>
#if VARIANT_PUBLIC
        public const bool MemoQVariant = true;
#else
        public const bool MemoQVariant = false;
#endif

        public const string HelpUrl = "https://help.inten.to/hc/en-us/sections/360004542600";

        /// <summary>
        /// Get proxy settings from registry
        /// </summary>
        /// <returns></returns>
        public static ProxySettings GetProxySettings()
        {
            ProxySettings settings = null;
            try
            {
                var key = Registry.CurrentUser.CreateSubKey($"Software\\Intento\\{AppName}");
                if (key != null)
                {
                    var enabled = (string)key.GetValue("ProxyEnabled", "0");
                    if (enabled == "1")
                    {
                        var address = (string)key.GetValue("ProxyAddress", null);
                        var port = (string)key.GetValue("ProxyPort", null);
                        var name = (string)key.GetValue("ProxyUserName", null);
                        var pass = (string)key.GetValue("ProxyPassw", null);
                        settings = new ProxySettings()
                        {
                            ProxyAddress = address,
                            ProxyPort = port,
                            ProxyUserName = name,
                            ProxyPassword = pass,
                            ProxyEnabled = true
                        };
                    }
                }
            }
            catch
            {
                // ignored
            }

            return settings;
        }

        /// <summary>
        /// If no private version of settings public versions readed, but saved only to private.
        /// </summary>
        public static string AppName => MemoQVariant ? AppNamePublic : AppNamePublic + "Private";
        private const string AppNamePublic = "MemoQPlugin";
        
        /// <summary>
        /// Version of MemoQ
        /// </summary>
        public static string MemoqVersion { get; } = GetMemoQVersion();
        
        /// <summary>
        /// Location
        /// </summary>
        public static string LocationLetter { get; } = GetLocationLetter();
        
        /// <summary>
        /// Version of plugin
        /// </summary>
        public static string PluginVersion { get; } = GetPluginVersion();
        
        public static string UserAgent =>
            $"memoQ/{MemoqVersion ?? "unknown"} Intento.{(LocationLetter == "s" ? "MemoqServerPlugin" : "MemoqPlugin")}/{PluginVersion}{LocationLetter}";
        
        private static string GetMemoQVersion()
        { 
            var domain = AppDomain.CurrentDomain;
            var domainAssemblies = domain.GetAssemblies();
            foreach (var ass in domainAssemblies)
            {
                var manifest = ass.ManifestModule;
                var name = manifest.Name;
                name = name.ToLower();
                if (name.ToLower() != "memoq.exe")
                {
                    continue;
                }
                var version = $"{ass.GetName().Version}";
                return version;
            }
            return null;
        }

        private static string GetLocationLetter()
        {
            var currentAssem = typeof(PluginInfo).Assembly;
            var codebase = $"{currentAssem.GetName().CodeBase}";
            codebase = codebase.ToLower();
            if (codebase.Contains("/memoq server/"))
            {
                return "s";
            }
            if (codebase.Contains("/addins/"))
            {
               return "c";
            }
            if (codebase.Contains("memoq.translationenvironment.gui.dll"))
            {
                return "q";
            }
            return "u";
        }
        
        private static string GetPluginVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = assembly.GetName().Version;
            var version = fvi.Revision == -1 ? $"{fvi.Major}.{fvi.Minor}.{fvi.Build}" : $"{fvi.Major}.{fvi.Minor}.{fvi.Build}.{fvi.Revision}";
            return version;
        }

    }
}
