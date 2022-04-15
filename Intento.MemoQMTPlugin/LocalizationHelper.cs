using System.Collections.Generic;
using MemoQ.MTInterfaces;

namespace IntentoMemoQMTPlugin
{
    /// <summary>
    /// Singleton helper class to be able to localize the plugin's textual information.
    /// </summary>
    internal class LocalizationHelper
    {
        /// <summary>
        /// The default text to be used when the IEnvironment.GetResourceString returns with null.
        /// </summary>
        private readonly Dictionary<string, string> defaultTexts = new()
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
        /// Private constructor to avoid multiple instances.
        /// </summary>
        private LocalizationHelper()
        { }

        /// <summary>
        /// The singleton instance of the localization helper.
        /// </summary>
        public static LocalizationHelper Instance { get; } = new();

        /// <summary>
        /// Sets the environment to be able to get localized texts.
        /// </summary>
        /// <param name="environment"></param>
        public void SetEnvironment(IEnvironment environment)
        {
        }

        /// <summary>
        /// Gets the localized text belonging to the specified key.
        /// </summary>
        public string GetResourceString(string key)
        {
            return defaultTexts[key];
        }

    }
}
