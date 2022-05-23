namespace Intento.MT.Plugin.PropertiesForm.States
{
    public partial class ApiKeyState
    {
        public enum EApiKeyStatus
        {
            Start,          // just after start of plugin
            Download,       // during download list of providers
            Changed,        // apikey was changed recently and was not checked
            Ok,             // apikey checked
            Error           // apikey check error
        };
    }
}