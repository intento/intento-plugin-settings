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
        // IntentoAiTextTranslate translate;

        public SmartRoutingPluginFormatted()
        {
            InitializeComponent();
            // IntentoSDK.Intento intento /*= new IntentoSDK.Intento()*/;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            dynamic json = JArray.Parse(textBoxSrc.Text);
            Dictionary<string, string> formats = new Dictionary<string, string>();

            foreach (dynamic table in json)
            {
                string providerName = table.action.service.provider;
                if (!formats.ContainsKey(providerName))
                    formats[providerName] = ReadFormat(providerName);
                table.action.context = new JObject();
                table.action.context.format = formats[providerName];
            }

            textBoxResult.Text = JsonConvert.SerializeObject(json, Formatting.Indented);
        }

        private string ReadFormat(string providerName)
        {
            return null;
        }
    }
}
