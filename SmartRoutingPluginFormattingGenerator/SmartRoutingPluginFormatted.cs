using IntentoSDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm
{
    public partial class SmartRoutingPluginFormatted : Form
    {
        class Data {
            public string name;
            public string from;
            public string to;
            public string rank;
            public string provider;
            public dynamic data;
            public string format;
        };

        static List<string> prohibited = new List<string> {
            "ai.text.translate.gtcom.yeecloud_translation_api",
            "ai.text.translate.modernmt.enterprise",
            "ai.text.translate.modernmt.realtime",
            "ai.text.translate.sdl.beglobal_translate_api.4-0",
            "ai.text.translate.sdl.language_cloud_translation_toolkit",
            "ai.text.translate.systran.pnmt",
            "ai.text.translate.systran.translation_api.1-0-0",
             };

        Dictionary<string, string> formats = new Dictionary<string, string>();
        List<string> providers = new List<string>();

        public SmartRoutingPluginFormatted()
        {
            InitializeComponent();
            // IntentoSDK.Intento intento /*= new IntentoSDK.Intento()*/;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            // Use sandbox key
            IntentoSDK.Intento _intento = IntentoSDK.Intento.Create("FHwlXptn2QDZAz5s9gcSPh06QLXPAbgf", null, "https://api.inten.to/");

            dynamic json = JArray.Parse(textBoxSrc.Text);
            Dictionary<string, Dictionary<string, Data>> dict = new Dictionary<string, Dictionary<string, Data>>();

            foreach (dynamic table in json)
            {
                Data item = new Data();
                item.from = table.match.from;
                item.to = table.match.to;
                item.rank = table.meta.rank;
                item.provider = table.action.service.provider;
                item.data = table;
                item.name = string.Format("{0}-{1}", item.from, item.to);
                item.format = GetHtmlXmlFormat(_intento, item.provider);

                if (dict.ContainsKey(item.name))
                    dict[item.name][item.rank] = item;
                else
                {
                    dict[item.name] = new Dictionary<string, Data> { { item.rank, item } };
                }
            }

            List<string> names = dict.Keys.ToList();
            names.Sort(string.CompareOrdinal);
            JArray resJson = new JArray();
            foreach (string name in names)
            {
                List<float> ranks = dict[name].Keys.Select(i => float.Parse(i)).ToList();
                ranks.Sort();
                foreach (float rank in ranks.ToList())
                {
                    Data item = dict[name][rank.ToString()];
                    if (prohibited.Contains(item.provider))
                        continue;
                    if (item.format == null)
                        continue;
                    switch (comboBox_formats.SelectedItem)
                    {
                        case "html":
                            if (item.format != "html")
                                continue;
                            break;
                        case "xml":
                            if (item.format != "xml")
                                continue;
                            break;
                    }

                    if (item.data.action.context == null)
                        item.data.action.context = new JObject();
                    item.data.action.context.format = item.format;
                    resJson.Add(item.data);
                    if (!providers.Contains(item.provider))
                        providers.Add(item.provider);
                }
            }

            // Check for empty pairs
            foreach(string key in dict.Keys)
            {
                if (dict[key].Count == 0)
                    throw new Exception(key);
            }

            textBoxResult.Text = JsonConvert.SerializeObject(resJson, Formatting.Indented);
            providers.Sort();
            textBoxProviders.Text = string.Join("\r\n", providers);
        }

        private string GetHtmlXmlFormat(IntentoSDK.Intento _intento, string providerName)
        {
            string f;
            if (!formats.ContainsKey(providerName))
            {
                f = formats[providerName] = ReadFormat(_intento, providerName);
            }
            else
                f = formats[providerName];
            return f;
        }

        private string ReadFormat(IntentoSDK.Intento _intento, string providerName)
        {
            dynamic providerJson = _intento.Ai.Text.Translate.Provider(providerName);
            // if (providerJson.flags.smart_routin_allowed != true)
            //     return null;

            dynamic formats = providerJson["format"];

            if (formats == null)
                return null;

            bool html = false;
            bool xml = false;
            foreach (string f in formats)
            {
                string f1 = f.ToLower();
                if (f1 == "html")
                    html = true;
                if (f1 == "xml")
                    xml = true;
            }
            if (html)
                return "html";
            if (xml)
                return "xml";
            return null;
        }
    }
}
