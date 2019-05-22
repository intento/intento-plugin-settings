using IntentoMT.Plugin.PropertiesForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    // Version history
    // 1.2.4: 2019-05-21
    // - List of providers is requested now in sync mode to get real format options to send translate request with th best html or xml option

    public partial class Form1 : Form
    {
        IntentoMTFormOptions options;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var path = "c:\\Development\\acc.txtq";
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path, System.Text.Encoding.UTF8);
                char[] splitchar = { '\r', '\n' };
                string[] values = reader.ReadToEnd().Split(splitchar, StringSplitOptions.RemoveEmptyEntries);
                textBoxApiKey.Text = values[0];
                textBoxAuth.Text = values[1];
                checkBoxAuth.Checked = bool.Parse(values[2]);
                textBoxProviderId.Text = values[3];
                textBoxProviderName.Text = values[4];
                options.ProviderName = textBoxProviderName.Text;
                options.ProviderId = textBoxProviderId.Text;
                options.UseCustomAuth = checkBoxAuth.Checked;
                options.CustomAuth = textBoxAuth.Text;
            }
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            options = new IntentoMTFormOptions();
            options.ApiKey = textBoxApiKey.Text;
            options.SmartRouting = checkBoxSmartRouting.Checked;
            smartRoutingChangeControls();
            options.ProviderId = textBoxProviderId.Text;
            options.ProviderName = textBoxProviderName.Text;
            options.UseCustomAuth = checkBoxAuth.Checked;
            options.CustomAuth = textBoxAuth.Text;
            options.UseCustomModel = checkBoxModel.Checked;
            options.CustomModel = textBoxModel.Text;
            options.AssemblyVersion = "1.0.0";
            options.PluginName = "Intento.SDLTradosPlugin.TestForm";
            options.Glossary = checkBoxSmartRouting.Checked ? string.Empty : textBoxGlossary.Text;

            IntentoTranslationProviderOptionsForm.LangPair[] languagePair = new IntentoTranslationProviderOptionsForm.LangPair[1] 
                { new IntentoTranslationProviderOptionsForm.LangPair("en", "de") };
            

            IntentoTranslationProviderOptionsForm form = new IntentoTranslationProviderOptionsForm(options, null, languagePair);
            form.FormClosed += Form_Closed;
            form.Show(); 
        }

        private void Form_Closed(Object sender, FormClosedEventArgs e)
        {
            textBoxApiKey.Text = options.ApiKey;
            checkBoxSmartRouting.Checked = options.SmartRouting;
            textBoxProviderId.Text = options.ProviderId;
            textBoxProviderName.Text = options.ProviderName;
            checkBoxAuth.Checked = options.UseCustomAuth;
            textBoxAuth.Text = options.CustomAuth;
            checkBoxModel.Checked = options.UseCustomModel;
            textBoxModel.Text = options.CustomModel;
            textBoxGlossary.Text = options.Glossary;
        }

        private void checkBoxSmartRouting_CheckedChanged(object sender, EventArgs e)
        {
            smartRoutingChangeControls();
        }
        private void smartRoutingChangeControls()
        {
            if (checkBoxSmartRouting.Checked)
            {
                textBoxProviderId.Text = string.Empty;
                textBoxProviderName.Text = string.Empty;
                checkBoxAuth.Checked = false;
                textBoxAuth.Text = string.Empty;
                checkBoxModel.Checked = false;
                textBoxModel.Text = string.Empty;
            }
            //else
            //{
            //    textBoxProviderId.Text = options.ProviderId;
            //    textBoxProviderName.Text = options.ProviderName;
            //    checkBoxAuth.Checked = options.UseCustomAuth;
            //    textBoxAuth.Text = options.CustomAuth;
            //    checkBoxModel.Checked = options.UseCustomModel;
            //    textBoxModel.Text = options.CustomModel;
            //    checkBoxAuthName.Checked = options.UseAuthName;
            //}
        }
    }
}
