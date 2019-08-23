using Intento.MT.Plugin.PropertiesForm;
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

    public partial class Form1 : Form
    {
        IntentoMTFormOptions options;
        string REG_PATH = "Software\\Intento\\PluginForm\\TestForm";

        public Form1()
        {
            InitializeComponent();
            FillTestNames();
        }

        private void FillTestNames()
        {
            comboBoxTestName.Items.Clear();
            comboBoxTestName.Items.AddRange(GetSettingNames());
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

        private RegistryKey GetKey(string name)
        {
            var res = Registry.CurrentUser.CreateSubKey(REG_PATH);
            if (!string.IsNullOrEmpty(name))
                res = res.CreateSubKey(name);
            return res;
        }

        private void ReadSettings(string name)
        {
            var key = GetKey(name);

            checkBoxStage.Checked = str2bool(key.GetValue("Stage", false));
            textBoxApiKey.Text = (string)key.GetValue("ApiKey", null);

            checkBoxSmartRouting.Checked = str2bool(key.GetValue("SmartRouting", null));

            textBoxProviderId.Text = (string)key.GetValue("ProviderId", null);
            textBoxFormat.Text = (string)key.GetValue("Format", null);

            checkBoxAuth.Checked = str2bool(key.GetValue("AuthUse", false));
            textBoxAuth.Text = (string)key.GetValue("Auth", null);

            checkBoxModel.Checked = str2bool(key.GetValue("ModelUse", false));
            textBoxModel.Text = (string)key.GetValue("Model", null);
            textBoxGlossary.Text = (string)key.GetValue("Glossary", null);

            textBoxFrom.Text = (string)key.GetValue("From", null);
            textBoxTo.Text = (string)key.GetValue("To", null);
            textBoxText.Text = (string)key.GetValue("Text", null);
            textBoxExpected.Text = (string)key.GetValue("Expected", null);
            checkBoxFormatted.Checked = str2bool(key.GetValue("Formatted", false));
        }

        private void SaveSettings(string name)
        {
            var key = GetKey(name);

            key.SetValue("Stage", checkBoxStage.Checked);
            key.SetValue("ApiKey", textBoxApiKey.Text);

            key.SetValue("SmartRouting", checkBoxSmartRouting.Checked);

            key.SetValue("ProviderId", textBoxProviderId.Text);
            key.SetValue("Format", textBoxFormat.Text);

            key.SetValue("AuthUse", checkBoxAuth.Checked);
            key.SetValue("Auth", textBoxAuth.Text);

            key.SetValue("ModelUse", checkBoxModel.Checked);
            key.SetValue("Model", textBoxModel.Text);
            key.SetValue("Glossary", textBoxGlossary.Text);

            key.SetValue("From", textBoxFrom.Text);
            key.SetValue("To", textBoxTo.Text);
            key.SetValue("Text", textBoxText.Text);
            key.SetValue("Expected", textBoxExpected.Text);
            key.SetValue("Formatted", checkBoxFormatted.Checked);
        }

        private string[] GetSettingNames()
        {
            var key = GetKey(null);
            var res = key.GetSubKeyNames().ToList();
            res.Add("");
            return res.ToArray();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadSettings(null);
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            options = new IntentoMTFormOptions();
            options.ApiKey = textBoxApiKey.Text;
            options.SmartRouting = checkBoxSmartRouting.Checked;
            options.ProviderId = textBoxProviderId.Text;
            options.Format = textBoxFormat.Text;
            options.UseCustomAuth = checkBoxAuth.Checked;
            options.CustomAuth = textBoxAuth.Text;
            options.UseCustomModel = checkBoxModel.Checked;
            options.CustomModel = textBoxModel.Text;
            options.UserAgent = "TestForm/1.0.0";
            options.Signature = "TestForm";
            options.Glossary = checkBoxSmartRouting.Checked ? string.Empty : textBoxGlossary.Text;
            options.AppName = "PluginForm\\TestForm";

            IntentoTranslationProviderOptionsForm.LangPair[] languagePair = new IntentoTranslationProviderOptionsForm.LangPair[1] 
                { new IntentoTranslationProviderOptionsForm.LangPair("en", "de") };

            IntentoTranslationProviderOptionsForm form = new IntentoTranslationProviderOptionsForm(options, languagePair, Fabric);
            form.FormClosed += Form_Closed;
            form.Show(); 
        }

        private IntentoSDK.IntentoAiTextTranslate Fabric(string apiKey, string userAgent)
        {
            var _intento = IntentoSDK.Intento.Create(apiKey, null,
                path: checkBoxStage.Checked ? "https://api2.inten.to/" : "https://api.inten.to/", 
                userAgent: String.Format("{0} {1}", userAgent, "TestForm"),
                loggingCallback: IntentoTranslationProviderOptionsForm.Logging
            );
            var _translate = _intento.Ai.Text.Translate;
            return _translate;
        }

        private void Form_Closed(Object sender, FormClosedEventArgs e)
        {
            textBoxApiKey.Text = options.ApiKey;
            checkBoxSmartRouting.Checked = options.SmartRouting;
            textBoxProviderId.Text = options.ProviderId;
            textBoxFormat.Text = options.Format;
            checkBoxAuth.Checked = options.UseCustomAuth;
            textBoxAuth.Text = options.CustomAuth;
            checkBoxModel.Checked = options.UseCustomModel;
            textBoxModel.Text = options.CustomModel;
            textBoxGlossary.Text = options.Glossary;

        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            string name;
            if (comboBoxTestName.SelectedIndex == -1)
                name = comboBoxTestName.Text;
            else
                name = (string)comboBoxTestName.SelectedItem;
            SaveSettings(name);
            FillTestNames();
        }

        private void buttonTestReadData_Click(object sender, EventArgs e)
        {
            ReadData();
        }

        void ReadData()
        {
            string name;
            if (comboBoxTestName.SelectedIndex == -1)
                name = comboBoxTestName.Text;
            else
                name = (string)comboBoxTestName.SelectedItem;
            ReadSettings(name);
        }

        private void buttonTranslatePlain_Click(object sender, EventArgs e)
        {
            textBoxExpected.Enabled = false;
            buttonTranslatePlain.Enabled = false;

            IntentoSDK.IntentoAiTextTranslate translate = Fabric(textBoxApiKey.Text, "TestForm");
            dynamic res = translate.Fulfill(textBoxText.Text, textBoxTo.Text, textBoxFrom.Text, provider: textBoxProviderId.Text, 
                async: true, wait_async: true, format: null,
                auth: checkBoxAuth.Checked ? string.Format("{{\"{0}\": [{1}]}}", textBoxProviderId.Text, textBoxAuth.Text) : null,
                custom_model: checkBoxModel.Checked ? textBoxModel.Text : null,
                glossary: !string.IsNullOrEmpty(textBoxGlossary.Text) ? textBoxGlossary.Text : null
                );
            string id = res.id;
            if (res.error == null)
            {
                string result = res.response[0].results[0];
                textBoxResult.Text = result;
            }
            else
            {
                textBoxResult.Text = string.Format("error: {0}", id);
            }

            textBoxExpected.Enabled = true;
            buttonTranslatePlain.Enabled = true;
        }

        private void comboBoxTestName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadData();
        }

        private void textBoxResult_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxExpected.Text) && textBoxExpected.Text != textBoxResult.Text)
                textBoxExpected.BackColor = Color.LightPink;
            else
                textBoxExpected.BackColor = Color.White;
        }
    }
}
