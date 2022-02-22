namespace Intento.MT.Plugin.PropertiesForm.States
{
    public partial class ApiKeyState
    {
        public enum EApiKeyStatus
        {
            start,          // just after start of plugin
            download,       // during download list of providers
            changed,        // apikey was changed recently and was not checked
            ok,             // apikey checked
            error           // apikey check error
        };
    }
}