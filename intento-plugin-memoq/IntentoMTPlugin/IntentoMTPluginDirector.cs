using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using MemoQ.Addins.Common.Framework;
using MemoQ.MTInterfaces;
using System.Diagnostics;
using Intento.MT.Plugin.PropertiesForm;
using IntentoSDK;
using Microsoft.Win32;

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
        /// Switching the Plugin variant: 
        /// VARIANT_PRIVATE or VARIANЕ_PUBLIC in "conditional compilation symbolsЭ settings of Visual Studio
        /// !!! Also! Need to change field AssemblyTitle in AssemblyInfo !!!
        /// </summary>
#if VARIANT_PUBLIC
        const bool memoQVariant = true;
#else
        const bool memoQVariant = false;
#endif

        /// <summary>
        /// The identifier of the plugin.
        /// </summary>
        //public const string PluginId = "IntentoMT";
        IntentoMTServiceHelper intentoMTServiceHelper;
        string userAgent;

        /// <summary>
        /// The memoQ's application envi ronment; e.g., to provide UI language settings etc. to the plugin.
        /// </summary>
        private IEnvironment environment;
        string path = "https://api.inten.to/";

        public IntentoMTOptions Options { get; set; }

        // If no private version of settings public versions readed, but saved only to private. 
        const string appNamePublic = "MemoQPlugin";
        string appNameCurrent;
        
        public IntentoMTPluginDirector()
        {
            appNameCurrent = memoQVariant ? appNamePublic : appNamePublic + "Private";

            userAgent = string.Format("memoQ/{0} Intento.{1}/{2}{3}-{4}",
                IntentoMTServiceHelper.memoqVersion ?? "unknown",
                (IntentoMTServiceHelper.isServer ? "MemoqServerPlugin" : "MemoqPlugin"),
                IntentoMTServiceHelper.version,
                IntentoMTServiceHelper.isServer ? "s" : "c",
                IntentoMTServiceHelper.GetGitCommitHash(Assembly.GetExecutingAssembly()));
            using (new Logs.Pair("IntentoMTPluginDirector.ctor"))
                intentoMTServiceHelper = new IntentoMTServiceHelper(() => Fabric(Options.SecureSettings.ApiKey, null, userAgent));
        }

        public IntentoSDK.IntentoAiTextTranslate Fabric(string apiKey, string userAgent, string pluginUserAgent)
        {
            var _intento = IntentoSDK.Intento.Create(apiKey, null,
                userAgent: String.Format("{0} {1}", userAgent, pluginUserAgent),
                path: path,
                proxySet: proxySettings
            );
            var _translate = _intento.Ai.Text.Translate;
            return _translate;
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

        public bool IsActivated
        {
            get { return true; }
        }

#endregion

#region IPluginDirector Members

        /// <summary>
        /// Indicates whether interactive lookup (in the translation grid) is supported or not.
        /// </summary>
        public override bool InteractiveSupported
        {
            get { return true; }
        }

        /// <summary>
        /// Indicates whether batch lookup is supported or not.
        /// </summary>
        public override bool BatchSupported
        {
            get { return true; }
        }

        /// <summary>
        /// Indicates whether storing translations is supported.
        /// </summary>
        public override bool StoringTranslationSupported
        {
            get { return false; }
        }

        /// <summary>
        /// The plugin's non-localized name.
        /// </summary>
        public override string PluginID
        {
            get
            {
#if VARIANT_PUBLIC
                return "IntentoMT";
#else
                return "IntentoMT_private";
#endif
            }
        }

        /// <summary>
        /// Returns the friendly name to show in memoQ's Tools / Options.
        /// </summary>
        public override string FriendlyName
        {
            get {
                string a = "a";
                try
                {
                    Assembly currentAssem = typeof(IntentoMTPluginDirector).Assembly;
                    object[] attribs = currentAssem.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
                    if (attribs.Length > 0)
                    {
                        string title = ((AssemblyTitleAttribute)attribs[0]).Title;
                        if (!string.IsNullOrEmpty(title))
                            return title;
                    }
                }
                catch {
                    a = "z";
                }

                return string.Format("Intento MT Plugin ({0})", a);
            }
        }

        /// <summary>
        /// Return the copyright text to show in memoQ's Tools / Options.
        /// </summary>
        public override string CopyrightText
        {
            get
            {
                string a = "a";
                // return "Copyright © Intento 2018-2019";
                try
                {
                    Assembly currentAssem = typeof(IntentoMTPluginDirector).Assembly;
                    object[] attribs = currentAssem.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
                    if (attribs.Length > 0)
                    {
                        string copyright = ((AssemblyCopyrightAttribute)attribs[0]).Copyright;
                        if (!string.IsNullOrEmpty(copyright))
                            return copyright;
                    }
                }
                catch {
                    a = "z";
                }
                return string.Format("Copyright © Intento 2018-2020 ({0})", a);
            }
        }

        /// <summary>
        /// Return a 48x48 display icon to show in MemoQ's Tools / Options. Black is the transparent color.
        /// </summary>
        public override Image DisplayIcon
        {
            get { return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("IntentoMemoQMTPlugin." + "Icon.bmp")); }
        }

        /// <summary>
        /// The memoQ's application environment; e.g., to provide UI language settings etc. to the plugin.
        /// </summary>
        public override IEnvironment Environment
        {
            set
            {
                this.environment = value;

                // initialize the localization helper
                LocalizationHelper.Instance.SetEnvironment(value);
            }
        }

        private IList<IList<string>> langPairs = null;
        private IList<IList<string>> GetIntentoLangPairs()
        {
            if (langPairs == null)
                langPairs = intentoMTServiceHelper.IntentoLanguagePairs();
            return langPairs;
        }

        /// <summary>
        /// Tells memoQ if the plugin supports the provided language combination. The strings provided are memoQ language codes.
        /// </summary>
        public override bool IsLanguagePairSupported(LanguagePairSupportedParams args)
        {
            Options = intentoMTServiceHelper.options = GetOptions(args.PluginSettings);
            IList<IList<string>> pairs = GetIntentoLangPairs();

            string sourceCode = intentoMTServiceHelper.ConvertLangCodeToIntento(args.SourceLangCode);
            string sourceCode2 = sourceCode;
            if (sourceCode2.Contains("-"))
                sourceCode2 = sourceCode2.Substring(0, sourceCode2.IndexOf('-'));

            string targetCode = intentoMTServiceHelper.ConvertLangCodeToIntento(args.TargetLangCode);
            string targetCode2 = targetCode;
            if (targetCode2 != null && targetCode2.Contains("-"))
                targetCode2 = targetCode2.Substring(0, targetCode2.IndexOf('-'));

            foreach (IList<string> pair in pairs)
            {
                if ((pair[0] == sourceCode || pair[0] == sourceCode2) && (pair[1] == targetCode || pair[1] == targetCode2))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Creates an MT engine for the supplied language pair.
        /// </summary>
        public override IEngine2 CreateEngine(CreateEngineParams args)
        {
            using (new Logs.Pair("IntentoMTPluginDirector.CreateEngine", "{0}-{1}", args.SourceLangCode, args.TargetLangCode))
            {
                return new IntentoMTEngine(intentoMTServiceHelper, args.SourceLangCode, args.TargetLangCode, GetOptions(args.PluginSettings));
            }
        }

        /// <summary>
        /// Shows the plugin's options form.
        /// </summary>
        public override PluginSettings EditOptions(IWin32Window parentForm, PluginSettings settings)
        {
            
            using (new Logs.Pair("IntentoMTPluginDirector.EditOptions"))
            {
                Options = intentoMTServiceHelper.options = GetOptions(settings);
                string customModel = !string.IsNullOrEmpty(Options.SecureSettings.customModel) ? Options.SecureSettings.customModel : Options.GeneralSettings.CustomModel;
                IntentoMTFormOptions formOptions = new IntentoMTFormOptions()
                {
                    ApiKey = Options.SecureSettings.ApiKey,
                    Signature = string.Format("v{0}{2}{3}",
                        IntentoMTServiceHelper.version,
                        IntentoMTServiceHelper.memoqVersion2,
                        IntentoMTServiceHelper.isServer ? "s" : "c",
                        IntentoMemoQMTPlugin.Properties.Settings.Default.Variant != null ? ":" + IntentoMemoQMTPlugin.Properties.Settings.Default.Variant : ""),
                    // If settings == null it is first usage of plugin
                    SmartRouting = settings != null && string.IsNullOrEmpty(Options.GeneralSettings.ProviderId),
                    ProviderId = Options.GeneralSettings.ProviderId,
                    ProviderName = Options.GeneralSettings.ProviderName,
                    UseCustomAuth = !string.IsNullOrEmpty(Options.GeneralSettings.ProviderKey),
                    CustomAuth = _ProviderKeyMigration(Options.GeneralSettings.ProviderKey),
                    UseCustomModel = !string.IsNullOrEmpty(customModel),
                    CustomModel = customModel,
                    Glossary = Options.SecureSettings.glossary,
                    Format = "text",
                    AppName = appNameCurrent,
                };

#if VARIANT_PUBLIC
                formOptions.ForbidSaveApikey = true;
                formOptions.HideHiddenTextButton = true;
                formOptions.сallHelpAction = CallHelp;
#endif

                using (var form = new IntentoTranslationProviderOptionsForm(
                        formOptions, 
                        null, 
                        (apiKey, settingsUserAgent, proxySettings) => Fabric(apiKey, settingsUserAgent, userAgent)
                    ))
                {
                    form.Visible = false;
                    if (form.ShowDialog(parentForm) == DialogResult.OK)
                    {   // Settings form was exited with Continue button
                        environment.PluginAvailabilityChanged();

                        intentoMTServiceHelper.intentoAiTextTranslate = formOptions.Translate;
                        intentoMTServiceHelper.format = formOptions.Format;
                        Options.GeneralSettings.ProviderFormats = formOptions.Format;

                        Options.GeneralSettings.ProviderName = intentoMTServiceHelper.format;
                        Options.SecureSettings.ApiKey = formOptions.ApiKey;
                        Options.GeneralSettings.ProviderKey = formOptions.CustomAuth;
                        Options.SecureSettings.customModel = formOptions.CustomModel;
                        Options.GeneralSettings.customModel = null;
                        Options.SecureSettings.glossary = formOptions.Glossary;
                        Options.GeneralSettings.ProviderId = formOptions.ProviderId;
                        Options.GeneralSettings.ProviderName = formOptions.ProviderName;
                    }
                    else
                    {   // Settings form was canceled 
                        intentoMTServiceHelper.intentoAiTextTranslate = Fabric(Options.SecureSettings.ApiKey, null, userAgent);

                        if (string.IsNullOrEmpty(Options.GeneralSettings.ProviderFormats))
                        {   // migration: need to read provider formats from IntentoAPI
                            if (string.IsNullOrEmpty(Options.GeneralSettings.ProviderId))
                            {   // smart routing
                                Options.GeneralSettings.ProviderFormats = null;
                            }
                            else
                            {
                                dynamic providerData = intentoMTServiceHelper.intentoAiTextTranslate.Provider(Options.GeneralSettings.ProviderId, "?fields=auth,custom_glossary");
                                Options.GeneralSettings.ProviderFormats = providerData.format != null ? providerData.format.ToString() : "";

                            }
                        }
                        intentoMTServiceHelper.format = Options.GeneralSettings.ProviderFormats;
                    }

                    return Options.GetSerializedSettings();
                }
            }
        }

        private string _ProviderKeyMigration(string providerKey)
        {
            if (string.IsNullOrEmpty(providerKey))
                return providerKey;
            if (providerKey[0] == '{')
                return providerKey;
            return string.Format("{{\"credential_id\":\"{0}\"}}", providerKey);
        }

        private IntentoMTOptions GetOptions(PluginSettings settings)
        {
            return intentoMTServiceHelper.SetOptions(settings);
        }

#endregion

        public ProxySettings proxySettings
        {
            get
            {
                ProxySettings settings = null;
                string address, port, name, pass, enabled;
                try
                {
                    RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento\\{0}", appNameCurrent));
                    enabled = (string)key.GetValue("ProxyEnabled", "0");
                    if (enabled == "1")
                    {
                        address = (string)key.GetValue("ProxyAddress", null);
                        port = (string)key.GetValue("ProxyPort", null);
                        name = (string)key.GetValue("ProxyUserName", null);
                        pass = (string)key.GetValue("ProxyPassw", null);
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
                catch { }
                return settings;
            }
        }

        public void CallHelp()
        {
#if VARIANT_PUBLIC
            (environment as IEnvironment2)?.ShowHelp("intento-plugin-settings.html");
#endif
        }

    }
}
