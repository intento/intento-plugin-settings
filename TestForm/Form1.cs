﻿using Intento.MT.Plugin.PropertiesForm;
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
		DateTime TraceEndDT;

        public Form1()
        {
            InitializeComponent();
            FillTestNames();
        }

        private void FillTestNames()
        {
            comboBoxTestName.Items.Clear();
            comboBoxTestName.Items.AddRange(GetSettingNames());
			Dictionary<int, string> versionDct = new Dictionary<int, string>();
			versionDct.Add(0, "Default (installed)");
			versionDct.Add(7, "7");
			versionDct.Add(8, "8");
			versionDct.Add(9, "9");
			versionDct.Add(10, "10");
			versionDct.Add(11, "11 and above");
			comboBoxIEVersion.DataSource = new BindingSource(versionDct, null);
			comboBoxIEVersion.DisplayMember = "Value";
			comboBoxIEVersion.ValueMember = "Key";
			comboBoxIEVersion.SelectedValue = 0;
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
			textBoxRouting.Text = (string)key.GetValue("Routing", null);

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

            checkBoxProxy.Checked = (string)key.GetValue("ProxyEnabled", "0") != "0";
            textBoxAddress.Text = (string)key.GetValue("ProxyAddress", null);
            textBoxPort.Text = (string)key.GetValue("ProxyPort", null);
            textBoxUserName.Text = (string)key.GetValue("ProxyUserName", null);
            textBoxPassword.Text = (string)key.GetValue("ProxyPassw", null);


        }

        private void SaveSettings(string name)
        {
            var key = GetKey(name);

            key.SetValue("Stage", checkBoxStage.Checked);
            key.SetValue("ApiKey", textBoxApiKey.Text);

            key.SetValue("SmartRouting", checkBoxSmartRouting.Checked);
			key.SetValue("Routing", textBoxRouting.Text);

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

            key.SetValue("ProxyEnabled", checkBoxProxy.Checked ? "1" : "0");
            key.SetValue("ProxyAddress", textBoxAddress.Text);
            key.SetValue("ProxyPort", textBoxPort.Text);
            key.SetValue("ProxyUserName", textBoxUserName.Text);
            key.SetValue("ProxyPassw", textBoxPassword.Text);

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
			options.Routing = textBoxRouting.Text;
            options.ProviderId = textBoxProviderId.Text;
            options.Format = textBoxFormat.Text;
            options.UseCustomAuth = checkBoxAuth.Checked;
            options.CustomAuth = textBoxAuth.Text;
            options.UseCustomModel = checkBoxModel.Checked;
            options.CustomModel = textBoxModel.Text;
            options.Glossary = checkBoxSmartRouting.Checked ? string.Empty : textBoxGlossary.Text;
            options.ForbidSaveApikey = checkBoxForbidSaveApikey.Checked;
            options.FromLanguage = textBoxFrom.Text;
            options.ToLanguage = textBoxTo.Text;
			options.UserAgent = "TestForm/1.0.0";
			options.Signature = "TestForm";
			options.AppName = checkBoxTradosApp.Checked ? "SdlTradosStudioPlugin" : "TestForm";
			options.CustomTagParser = checkBoxCustomTagParser.Checked;
			options.CutTag = CheckBoxCutTag.Checked;
			options.proxySettings = new IntentoSDK.ProxySettings()
            {
                ProxyAddress = textBoxAddress.Text,
                ProxyPort = textBoxPort.Text,
                ProxyUserName = textBoxUserName.Text,
                ProxyPassword = textBoxPassword.Text,
                ProxyEnabled = checkBoxProxy.Checked
            };
            if (checkBoxForbidSaveApikey.Checked)
                options.ForbidSaveApikey = true;
            if (checkBoxHideHiddenTextButton.Checked)
                options.HideHiddenTextButton = true;
			options.TraceEndTime = DateTime.Now.AddMinutes(checkBox1.Checked ? 30 : -40);



			IntentoTranslationProviderOptionsForm.LangPair[] languagePair = new IntentoTranslationProviderOptionsForm.LangPair[1] 
                { new IntentoTranslationProviderOptionsForm.LangPair(
					String.IsNullOrWhiteSpace(textBoxFrom.Text) ? "en" : textBoxFrom.Text,
					String.IsNullOrWhiteSpace(textBoxTo.Text) ? "de" : textBoxTo.Text
					) };

            IntentoTranslationProviderOptionsForm form = new IntentoTranslationProviderOptionsForm(options, languagePair, Fabric);
            form.FormClosed += Form_Closed;
            form.Show(); 
        }

        private IntentoSDK.IntentoAiTextTranslate Fabric(string apiKey, string userAgent, IntentoSDK.ProxySettings proxySettings)
        {
            var _intento = IntentoSDK.Intento.Create(apiKey, null,
                path: checkBoxStage.Checked ? "https://api2.inten.to/" : "https://api.inten.to/", 
                userAgent: String.Format("{0} {1}", userAgent, "TestForm"),
                loggingCallback: Logs.Logging,
                proxySet: proxySettings
            );
            var _translate = _intento.Ai.Text.Translate;
            return _translate;
        }

        private void Form_Closed(Object sender, FormClosedEventArgs e)
        {
            textBoxApiKey.Text = options.ApiKey;
            checkBoxSmartRouting.Checked = options.SmartRouting;
			textBoxRouting.Text = options.Routing;
            textBoxProviderId.Text = options.ProviderId;
            textBoxFormat.Text = options.Format;
            checkBoxAuth.Checked = options.UseCustomAuth;
            textBoxAuth.Text = options.CustomAuth;
            checkBoxModel.Checked = options.UseCustomModel;
            textBoxModel.Text = options.CustomModel;
            textBoxGlossary.Text = options.Glossary;
            textBoxFrom.Text = options.FromLanguage;
            textBoxTo.Text = options.ToLanguage;
			checkBoxCustomTagParser.Checked = options.CustomTagParser;
			CheckBoxCutTag.Checked = options.CutTag;
			var key = GetKey(null);
            checkBoxProxy.Checked = (string)key.GetValue("ProxyEnabled", "0") != "0";
            textBoxAddress.Text = (string)key.GetValue("ProxyAddress", null);
            textBoxPort.Text = (string)key.GetValue("ProxyPort", null);
            textBoxUserName.Text = (string)key.GetValue("ProxyUserName", null);
            textBoxPassword.Text = (string)key.GetValue("ProxyPassw", null);
			if (options.TraceEndTime > DateTime.Now)
			{
				checkBox1.Checked = true;
				textBoxDTLog.Text = TraceEndDT.ToString("HH:MM:ss yy.mm.yyyy");
			}
			else
				checkBox1.Checked = false;

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

            IntentoSDK.IntentoAiTextTranslate translate = Fabric(textBoxApiKey.Text, "TestForm", options.proxySettings);
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

		private void checkBoxTradosApp_CheckedChanged(object sender, EventArgs e)
		{
			checkBoxCustomTagParser.Enabled = !checkBoxTradosApp.Checked;
			CheckBoxCutTag.Enabled = checkBoxTradosApp.Checked;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			BrowserForm BFrom = new BrowserForm((int)comboBoxIEVersion.SelectedValue);
			if (!BFrom.IsDisposed)
				BFrom.ShowDialog();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked)
			{
				TraceEndDT = DateTime.Now.AddMinutes(30);
				textBoxDTLog.Text = TraceEndDT.ToString("HH:MM:ss yy.mm.yyyy");
			}
			else
			{
				TraceEndDT = DateTime.Now.AddMinutes(-30);
				textBoxDTLog.Text = "";
			}

		}

		private void checkBoxSmartRouting_CheckedChanged(object sender, EventArgs e)
		{
			textBoxRouting.Text = checkBoxSmartRouting.Checked ? "best" : "";
		}
	}
}
