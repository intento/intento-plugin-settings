using System.Collections.Generic;
using MemoQ.Addins.Common.DataStructures;
using MemoQ.Addins.Common.Utils;
using MemoQ.MTInterfaces;
using System.Reflection;
using System.Linq;

namespace IntentoMTPlugin
{
    /// <summary>
    /// Singleton helper class to be able to localize the plugin's textual information.
    /// </summary>
    internal class LocalizationHelper
    {
        /// <summary>
        /// The default text to be used when the IEnvironment.GetResourceString returns with null.
        /// </summary>
        private Dictionary<string, string> defaultTexts = new Dictionary<string, string>()
        {
            { "OptionsFormCaption", "Intento MT plugin settings" },
            { "ApiKeyLabelText", "Intento ApiKey" },
            { "RetrieveLanguagesLinkText", "Check login and retrieve language information"},
            { "SupportedLanguagesLabelText", "Supported languages" },
            { "OkButtonText", "OK" },
            { "CancelButtonText", "Cancel" },
            { "CommunicationErrorCaption", "Login error" },
            { "CommunicationErrorText", "There was an error during the communication with the service.\n\n{0}" },
            { "InvalidUserNameCaption", "Login error" },
            { "InvalidUserNameText", "Invalid user name or password." },
            { "NetworkError", "A network error occured ({0})" }
        };

        /// <summary>
        /// The singleton instance of the localization helper.
        /// </summary>
        private static LocalizationHelper instance = new LocalizationHelper();

        /// <summary>
        /// Private constructor to avoid multiple instances.
        /// </summary>
        private LocalizationHelper()
        { }

        /// <summary>
        /// The environment to be used to get localized texts from memoQ.
        /// </summary>
        private IEnvironment environment;

        /// <summary>
        /// The singleton instance of the localization helper.
        /// </summary>
        public static LocalizationHelper Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Sets the environment to be able to get localized texts.
        /// </summary>
        /// <param name="environment"></param>
        public void SetEnvironment(IEnvironment environment)
        {
            this.environment = environment;
        }

        /// <summary>
        /// Gets the localized text belonging to the specified key.
        /// </summary>
        public string GetResourceString(string key)
        {
            return defaultTexts[key];

            // // try to get the localized text from the environment
            // string localizedText = environment.GetResourceString(IntentoMTPluginDirector.PluginId, key);

            // // use the default texts if the environment returns with null
            // if (string.IsNullOrEmpty(localizedText))
            // localizedText = defaultTexts[key];

            // return localizedText;
        }

        public static string MemoQConvertSegment2Html(Segment seg, bool tagged)
        {
            Assembly ass = Assembly.LoadFrom("MemoQ.Addins.Common.dll");
            System.Type[] types = ass.GetExportedTypes();
            System.Type type = types.Where(i => i.Name == "SegmentHtmlConverter").FirstOrDefault();
            MethodInfo method = type.GetMethod("ConvertSegment2Html");
            object res;
            try
            {
                res = method.Invoke(null, new object[] { seg, tagged, false });
            }
            catch
            {
                res = method.Invoke(null, new object[] { seg, tagged });
            }

            return (string)res;
        }
        public static Segment MemoQConvertHtml2Segment(string html, IList<InlineTag> tags)
        {
            return SegmentHtmlConverter.ConvertHtml2Segment(html, tags);
        }
        
    }
}
