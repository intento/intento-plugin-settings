using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntentoMT.Plugin.PropertiesForm
{
    public class IntentoMTFormOptions
    {
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
        // sign of using user custom model
        public bool UseCustomModel { get; set; }
        // string of custom glossary
        public string Glossary { get; set; }
        // string of custom model
        public string CustomModel { get; set; }
        // source format that the selected provider accepts for translation (text, html, xml....)
        public string Format { get; set; }
        // information about the assembly version of the plugin that uses this form
        public string AssemblyVersion { get; set; }
        // information about version of the the main program that uses plugin
        public string PluginFor { get; set; }
        // Plugin name
        public string PluginName { get; set; }

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

    }

}
