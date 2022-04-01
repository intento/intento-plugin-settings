using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Intento.MT.Plugin.PropertiesForm
{
    public static class Utilities
    {
        public static IDictionary<string, string> AuthToDictionary(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }

            try
            {
                var credential = JToken.Parse(source);
                return ((JObject)credential).HasValues
                    ? credential.Cast<JProperty>().ToDictionary(param => param.Name, param => (string)param.Value)
                    : null;
            }
            catch
            {
                return null;
            }
        }
    }
}