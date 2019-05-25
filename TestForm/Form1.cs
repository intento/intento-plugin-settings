using IntentoMT.Plugin.PropertiesForm;
using Microsoft.Win32;
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
        string REG_PATH = "HKEY_CURRENT_USER\\Software\\Intento\\PluginForm\\TestForm";

        public Form1()
        {
            InitializeComponent();
        }

        private bool str2bool(object z)
        {
            if (z is bool)
                return (bool)z;
            if (z == null)
                return false;
            if (z is string)
                return bool.Parse((string)z);
            throw new Exception();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxApiKey.Text = (string)Registry.GetValue(REG_PATH, "ApiKey", null);

            checkBoxSmartRouting.Checked = str2bool(Registry.GetValue(REG_PATH, "SmartRouting", null));

            textBoxProviderId.Text = (string)Registry.GetValue(REG_PATH, "ProviderId", null);
            textBoxProviderName.Text = (string)Registry.GetValue(REG_PATH, "ProviderName", null);

            checkBoxAuth.Checked = str2bool(Registry.GetValue(REG_PATH, "AuthUse", null));
            textBoxAuth.Text = (string)Registry.GetValue(REG_PATH, "Auth", null);

            checkBoxModel.Checked = str2bool(Registry.GetValue(REG_PATH, "ModelUse", null));
            textBoxModel.Text = (string)Registry.GetValue(REG_PATH, "Model", null);
            textBoxGlossary.Text = (string)Registry.GetValue(REG_PATH, "Glossary", null);
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            options = new IntentoMTFormOptions();
            options.ApiKey = textBoxApiKey.Text;
            options.SmartRouting = checkBoxSmartRouting.Checked;
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

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            Registry.SetValue(REG_PATH, "ApiKey", textBoxApiKey.Text);

            Registry.SetValue(REG_PATH, "SmartRouting", checkBoxSmartRouting.Checked);

            Registry.SetValue(REG_PATH, "ProviderId", textBoxProviderId.Text);
            Registry.SetValue(REG_PATH, "ProviderName", textBoxProviderName.Text);

            Registry.SetValue(REG_PATH, "AuthUse", checkBoxAuth.Checked);
            Registry.SetValue(REG_PATH, "Auth", textBoxAuth.Text);

            Registry.SetValue(REG_PATH, "ModelUse", checkBoxModel.Checked);
            Registry.SetValue(REG_PATH, "Model", textBoxModel.Text);
            Registry.SetValue(REG_PATH, "Glossary", textBoxGlossary.Text);
        }
    }
}
