using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm
{
    public static class PluginHelper
    {
        public static string PrepareText(string format, string data)
        {
            if (format == "xml")
                return string.Format("<root>{0}</root>", data);
            return data;
        }

        public static string PrepareResult(string format, string text)
        {
            if (format == "xml")
            {
                // Remove <? > tag
                int n1 = text.IndexOf("<?");
                string text2 = text;
                if (n1 != -1)
                {
                    int n2 = text.IndexOf(">");
                    text2 = text.Substring(n2 + 1);
                }

                // Remove <root> and </root> tags
                string text3 = text2.Replace("<root>", "").Replace("</root>", "");
                return text3;
            }

            if (format == "html")
            {
                // Remove <meta> and </meta> tags
                int n1 = text.IndexOf("<meta");
                string text2 = text;
                if (n1 != -1)
                {
                    int n2 = text.IndexOf(">");
                    text2 = text.Substring(n2 + 1);
                }

                return text2;
            }

            return text;
        }


    }
}
