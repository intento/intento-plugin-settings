using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntentoSDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class IntentoMTFormOptions
    {

        public enum StateModeEnum
        {
            unknown = 0,
            prohibited,
            required,
            optional
        }

        // Api Key for Intento
        public string ApiKey { get; set; }
        // sign of automatic provider selection
        public bool SmartRouting { get; set; }
        // selected provider id
        public string ProviderId { get; set; }
        // selected provider name
        public string ProviderName { get; set; }
        // sign of using external authentication
        public bool UseCustomAuth { get; set; }
        // string of external authentication 
        public string CustomAuth { get; set; }
        // sign of stored credentials
        public bool IsAuthDelegated { get; set; }
        // stored credential Id
        public string AuthDelegatedCredentialId { get; set; }
        // 
        public StateModeEnum AuthMode { get; set; }
        // sign of using user custom model
        public bool UseCustomModel { get; set; }
        // string of custom model
        public string CustomModel { get; set; }
        // string of custom model name
        public string CustomModelName { get; set; }
        //
        public StateModeEnum CustomModelMode { get; set; }
        // string of custom glossary
        public string Glossary { get; set; }
        // string of custom glossary name
        public string GlossaryName { get; set; }
        //
        public StateModeEnum GlossaryMode { get; set; }
        // formats supported by provider (text, html, xml.. - to check by contains), for smart routing not used
        public string Format { get; set; }
        // information about the assembly version of the plugin that uses this form
        // public string AssemblyVersion { get; set; }
        // information about version of the the main program that uses plugin
        // public string PluginFor { get; set; }
        // Plugin name
        // public string PluginName { get; set; }

        // User-Agent of plugin
        public string UserAgent { get; set; }
        // Signature in botton right corner of settinga form
        public string Signature { get; set; }
        // Plugin name. User to store ApiKey in registry by this name
        public string AppName { get; set; }
        // Proxy server configuration class for service requests
        public ProxySettings proxySettings { get; set; }

        // Forbit to save ApiKey in registry 
        public bool ForbidSaveApikey { get; set; }
        // Hide "Show hidden text" button
        public bool HideHiddenTextButton { get; set; }

        public IntentoAiTextTranslate Translate { get; set; }

        private Dictionary<string, string> _authDict = null;

        public void SetAuthDict(Dictionary<string, string> _authDict)
        {
            if (_authDict == null)
                CustomAuth = "";
            else
            {
                var entr = _authDict.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key, d.Value));
                CustomAuth = "{" + string.Join(",", entr) + "}";
            }
        }

        public Dictionary<string, string> authDict()
        {
            if (_authDict != null)
                return _authDict;
            if (!string.IsNullOrWhiteSpace(CustomAuth))
            {
                dynamic credential = JToken.Parse(CustomAuth);
                if (((JObject)credential).HasValues)
                {
                    _authDict = new Dictionary<string, string>();
                    foreach (JProperty param in credential)
                    {
                        _authDict.Add(param.Name, (string)param.Value);
                    }
                    return _authDict;
                }
            }
            return null;
        }

        public IntentoMTFormOptions Duplicate()
        {
            IntentoMTFormOptions res = new IntentoMTFormOptions()
            {
                ApiKey = this.ApiKey,
                SmartRouting = this.SmartRouting,
                ProviderId = this.ProviderId,
                ProviderName = this.ProviderName,
                UseCustomAuth = this.UseCustomAuth,
                CustomAuth = this.CustomAuth,
                UseCustomModel = this.UseCustomModel,
                Glossary = this.Glossary,
                CustomModel = this.CustomModel,
                Format = this.Format,
                UserAgent = this.UserAgent,
                Signature = this.Signature,
                AppName = this.AppName,
                Translate = this.Translate,
                ForbidSaveApikey = this.ForbidSaveApikey,
                _authDict = _authDict == null ? null : new Dictionary<string, string>(_authDict),
            };
            return res;
        }

        //public bool IsEqualsOptions (IntentoMTFormOptions value)
        //{
        //    return value.ApiKey == this.ApiKey &&
        //        value.SmartRouting == this.SmartRouting &&
        //        value.ProviderId == this.ProviderId &&
        //        value.ProviderName == this.ProviderName &&
        //        value.UseCustomAuth == this.UseCustomAuth &&
        //        value.CustomAuth == this.CustomAuth &&
        //        value.UseCustomModel == this.UseCustomModel &&
        //        value.Glossary == this.Glossary &&
        //        value.CustomModel == this.CustomModel &&
        //        value.Format == this.Format &&
        //        value.ForbidSaveApikey == this.ForbidSaveApikey;
        //        //value._authDict == this._authDict;
        //}

    }

}
