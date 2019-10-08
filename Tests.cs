using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace Intento.MT.Plugin.PropertiesForm
{
    [TestClass]
    public sealed class Tests
    {
        public class FakeForm : IForm
        {
            bool continue_Button_Enabled;

            public ApiKeyState apiKeyState;
            string apiKeyText;
            bool apiKey_TextBox_Enabled;
            bool apiKeyCheck_Button_Enabled;
            Color apiKey_TextBox_BackColor;

            bool smartRouting_CheckBox_Visible;
            bool smartRouting_CheckBox_Enabled;
            public bool smartRouting_CheckBox_Checked = false;
            IntentoTranslationProviderOptionsForm.LangPair[] languagePairs = 
                new IntentoTranslationProviderOptionsForm.LangPair[] { new IntentoTranslationProviderOptionsForm.LangPair("en", "ru") };

            bool providers_Group_Visible;
            bool providers_Group_Enabled;

            List<string> providers_ComboBox_List = new List<string>();
            public int providers_ComboBox_Index = -1;

            bool authText_Group_Visible;
            bool authCombo_Group_Visible;

            bool auth_CheckBox_Visible;
            bool auth_CheckBox_Enabled;
            bool auth_CheckBox_Checked;

            bool authCombo_ComboBox_Enabled;
            List<string> authCombo_ComboBox_List;
            int authCombo_ComboBox_Index = -1;

            bool model_Group_Visible;
            bool model_Group_Enabled;

            bool model_CheckBox_Visible;
            bool model_CheckBox_Enabled;
            bool model_CheckBox_Checked;
            bool model_ComboBox_Visible;
            bool model_TextBox_Visible;
            List<string> model_ComboBox_List;
            int model_ComboBox_Index = -1;

            bool glossary_Group_Visible;
            bool glossary_TextBox_Visible;
            string glossary_TextBox_Text;

            bool glossary_ComboBox_Visible;
            List<string> glossary_ComboBox_List;
            int glossary_ComboBox_Index = -1;

            public List<dynamic> delegatedCredentials;
            public List<dynamic> models;
            public List<dynamic> glossaries;

            List<string> errors;
            string errorMessage_TextBox_Text;
            Color errorMessage_TextBox_BackColor;



            public ApiKeyState ApiKeyState => apiKeyState;

            public string ApiKey_TextBox_Text { get => apiKeyText; set => apiKeyText = value; }
            public bool ApiKey_TextBox_Enabled { set => apiKey_TextBox_Enabled = value; }
            public Color ApiKey_TextBox_BackColor { set => apiKey_TextBox_BackColor = value; }
            public bool ApiKeyCheck_Button_Enabled { get => apiKeyCheck_Button_Enabled; set => apiKeyCheck_Button_Enabled = value; }
            public bool Continue_Button_Enabled { get => continue_Button_Enabled; set => continue_Button_Enabled = value; }
            public bool SmartRouting_CheckBox_Checked { get => smartRouting_CheckBox_Checked; set => smartRouting_CheckBox_Checked = value; }
            public bool SmartRouting_CheckBox_Visible { set => smartRouting_CheckBox_Visible = value; }
            public bool SmartRouting_CheckBox_Enabled { set => smartRouting_CheckBox_Enabled = value; }

            public string Providers_ComboBox_SelectedItem { set => providers_ComboBox_Index = providers_ComboBox_List.IndexOf(value); }
            public string Providers_ComboBox_Text => providers_ComboBox_Index == -1 ? null : providers_ComboBox_List[providers_ComboBox_Index];
            public Color Providers_ComboBox_BackColor { set => throw new NotImplementedException(); }

            public bool Providers_Group_Visible { set => providers_Group_Visible = value; }
            public bool Providers_Group_Enabled { set => providers_Group_Enabled = value; }

            public bool Auth_CheckBox_Visible { set => auth_CheckBox_Visible = value; }
            public bool Auth_CheckBox_Enabled { set => auth_CheckBox_Enabled = value; }
            public bool Auth_CheckBox_Checked { get => auth_CheckBox_Checked; set => auth_CheckBox_Checked = value; }

            public bool AuthText_Group_Visible { set => authText_Group_Visible = value; }
            public Color AuthText_TextBox_BackColor { set => throw new NotImplementedException(); }
            public string AuthText_TextBox_Text { set => throw new NotImplementedException(); }
            public bool AuthCombo_Group_Visible { set => authCombo_Group_Visible = value; }
            public object AuthCombo_ComboBox_SelectedItem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public bool AuthCombo_ComboBox_Enabled { set => authCombo_ComboBox_Enabled = value; }

            public int AuthCombo_ComboBox_Count => authCombo_ComboBox_List.Count;

            public int AuthCombo_ComboBox_SelectedIndex { set => authCombo_ComboBox_Index = value; }
            public Color AuthCombo_ComboBox_BackColor { set => throw new NotImplementedException(); }

            public string AuthCombo_ComboBox_Text => throw new NotImplementedException();

            public bool Model_CheckBox_Checked { get => model_CheckBox_Checked; set => model_CheckBox_Checked = value; }
            public bool Model_CheckBox_Visible { set => model_CheckBox_Visible = value; }
            public bool Model_CheckBox_Enabled { set => model_CheckBox_Enabled = value; }
            public bool Model_Group_Visible { set => model_Group_Visible = value; }
            public bool Model_Group_Enabled { set => model_Group_Enabled = value; }
            public int Model_ComboBox_SelectedIndex { set => model_ComboBox_Index = value; }
                        public int Model_ComboBox_Count => model_ComboBox_List.Count;
                        public bool Model_ComboBox_Visible { set => model_ComboBox_Visible = value; }
            public Color Model_ComboBox_BackColor { set => throw new NotImplementedException(); }
            public string Model_ComboBox_Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public bool Model_TextBox_Visible { set => model_TextBox_Visible = value; }
            public string Model_TextBox_Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Color Model_TextBox_BackColor { set => throw new NotImplementedException(); }

            public bool Glossary_Group_Visible { get => glossary_Group_Visible; set => glossary_Group_Visible = value; }
            public string Glossary_TextBox_Text { get => glossary_TextBox_Text; set => glossary_TextBox_Text = value; }
            public bool Glossary_TextBox_Visible { set => glossary_TextBox_Visible = value; }
            public int Glossary_ComboBox_SelectedIndex { set => glossary_ComboBox_Index = value; }
            public bool Glossary_ComboBox_Visible { set => glossary_ComboBox_Visible = value; }
            public string Glossary_ComboBox_Text => glossary_ComboBox_Index == -1 ? null : glossary_ComboBox_List[glossary_ComboBox_Index];

            public string ErrorMessage_TextBox_Text { get => errorMessage_TextBox_Text; set => errorMessage_TextBox_Text = value; }
            public Color ErrorMessage_TextBox_BackColor { set => errorMessage_TextBox_BackColor = value; }
            public bool SaveApiKeyInRegistry_CheckBox_Checked { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public bool ShowHidden_CheckBox_Checked => throw new NotImplementedException();

            public int CursorCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Cursor Cursor { set => throw new NotImplementedException(); }
            public List<string> Errors { get => errors; set => errors = new List<string>(); }

            public IntentoTranslationProviderOptionsForm.LangPair[] LanguagePairs => languagePairs;

            ApiKeyState IForm.ApiKeyState => throw new NotImplementedException();

            string IForm.ApiKey_TextBox_Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            bool IForm.ApiKey_TextBox_Enabled { set => throw new NotImplementedException(); }
            Color IForm.ApiKey_TextBox_BackColor { set => throw new NotImplementedException(); }
            bool IForm.ApiKeyCheck_Button_Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            bool IForm.Continue_Button_Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            bool IForm.SmartRouting_CheckBox_Checked { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            bool IForm.SmartRouting_CheckBox_Visible { set => throw new NotImplementedException(); }
            bool IForm.SmartRouting_CheckBox_Enabled { set => throw new NotImplementedException(); }
            string IForm.Providers_ComboBox_SelectedItem { set => throw new NotImplementedException(); }

            string IForm.Providers_ComboBox_Text => throw new NotImplementedException();

            Color IForm.Providers_ComboBox_BackColor { set => throw new NotImplementedException(); }
            bool IForm.Providers_Group_Visible { set => throw new NotImplementedException(); }
            bool IForm.Providers_Group_Enabled { set => throw new NotImplementedException(); }
            bool IForm.Auth_CheckBox_Visible { set => throw new NotImplementedException(); }
            bool IForm.Auth_CheckBox_Enabled { set => throw new NotImplementedException(); }
            bool IForm.Auth_CheckBox_Checked { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            bool IForm.AuthText_Group_Visible { set => throw new NotImplementedException(); }
            Color IForm.AuthText_TextBox_BackColor { set => throw new NotImplementedException(); }
            string IForm.AuthText_TextBox_Text { set => throw new NotImplementedException(); }
            bool IForm.AuthCombo_Group_Visible { set => throw new NotImplementedException(); }
            object IForm.AuthCombo_ComboBox_SelectedItem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            bool IForm.AuthCombo_ComboBox_Enabled { set => throw new NotImplementedException(); }

            int IForm.AuthCombo_ComboBox_Count => throw new NotImplementedException();

            int IForm.AuthCombo_ComboBox_SelectedIndex { set => throw new NotImplementedException(); }
            Color IForm.AuthCombo_ComboBox_BackColor { set => throw new NotImplementedException(); }

            string IForm.AuthCombo_ComboBox_Text => throw new NotImplementedException();

            bool IForm.Model_CheckBox_Checked { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            bool IForm.Model_CheckBox_Visible { set => throw new NotImplementedException(); }
            bool IForm.Model_CheckBox_Enabled { set => throw new NotImplementedException(); }
            bool IForm.Model_Group_Visible { set => throw new NotImplementedException(); }
            bool IForm.Model_Group_Enabled { set => throw new NotImplementedException(); }
            int IForm.Model_ComboBox_SelectedIndex { set => throw new NotImplementedException(); }

            int IForm.Model_ComboBox_Count => throw new NotImplementedException();

            bool IForm.Model_ComboBox_Visible { set => throw new NotImplementedException(); }
            Color IForm.Model_ComboBox_BackColor { set => throw new NotImplementedException(); }
            string IForm.Model_ComboBox_Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            bool IForm.Model_TextBox_Visible { set => throw new NotImplementedException(); }
            string IForm.Model_TextBox_Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            Color IForm.Model_TextBox_BackColor { set => throw new NotImplementedException(); }
            bool IForm.Glossary_Group_Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            string IForm.Glossary_TextBox_Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            bool IForm.Glossary_TextBox_Visible { set => throw new NotImplementedException(); }
            bool IForm.Glossary_TextBox_Enabled { set => throw new NotImplementedException(); }
            int IForm.Glossary_ComboBox_SelectedIndex { set => throw new NotImplementedException(); }
            bool IForm.Glossary_ComboBox_Visible { set => throw new NotImplementedException(); }
            bool IForm.Glossary_ComboBox_Enabled { set => throw new NotImplementedException(); }

            string IForm.Glossary_ComboBox_Text => throw new NotImplementedException();

            string IForm.ErrorMessage_TextBox_Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            Color IForm.ErrorMessage_TextBox_BackColor { set => throw new NotImplementedException(); }
            bool IForm.SaveApiKeyInRegistry_CheckBox_Checked { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            bool IForm.ShowHidden_CheckBox_Checked => throw new NotImplementedException();

            int IForm.CursorCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            Cursor IForm.Cursor { set => throw new NotImplementedException(); }
            List<string> IForm.Errors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            IntentoTranslationProviderOptionsForm.LangPair[] IForm.LanguagePairs => throw new NotImplementedException();

            public void AuthCombo_ComboBox_AddRange(object[] items)
            {
                authCombo_ComboBox_List.AddRange(items.Select(i => (string)i));
            }

            public void AuthCombo_ComboBox_Clear()
            {
                authCombo_ComboBox_List = new List<string>();
                authCombo_ComboBox_Index = -1;
            }

            public bool AuthCombo_ComboBox_Contains(string text)
            {
                throw new NotImplementedException();
            }

            public void AuthCombo_ComboBox_Insert(int n, string text)
            {
                throw new NotImplementedException();
            }

            public IList<dynamic> DelegatedCredentials()
            {
                return delegatedCredentials;
            }

            public IList<dynamic> Glossaries(string provider, Dictionary<string, string> credential_id)
            {
                return glossaries;
            }

            public int Glossary_ComboBoxAdd(string text)
            {
                glossary_ComboBox_List.Add(text);
                return 0;
            }

            public void Glossary_ComboBox_Clear()
            {
                glossary_ComboBox_List = new List<string>();
                glossary_ComboBox_Index = -1;
            }

            public void Glossary_ComboBox_Insert(int n, string text)
            {
                glossary_ComboBox_List.Insert(n, text);
            }

            public IList<dynamic> Models(string provider, Dictionary<string, string> credential_id)
            {
                return models;
            }

            public int Model_ComboBox_Add(string text)
            {
                model_ComboBox_List.Add(text);
                return 0;
            }

            public void Model_ComboBox_Clear()
            {
                model_ComboBox_List = new List<string>();
                model_ComboBox_Index = -1;

            }

            public dynamic Provider(string provider, string additionaParams)
            {
                switch (provider)
                {
                    case "ai.text.translate.abc":
                        return JObject.Parse("{'billable':true, 'stock_model':true, 'custom_model':true, 'custom_glossary':true, 'delegated_credentials':true }".Replace('\'', '"'));
                    case "ai.text.translate.def":
                        return JObject.Parse("{'billable':true, 'stock_model':true, 'custom_model':true, 'custom_glossary':true, 'delegated_credentials':true }".Replace('\'', '"'));
                }
                throw new NotImplementedException();
            }

            public IEnumerable<dynamic> Providers(Dictionary<string, string> filter)
            {
                throw new NotImplementedException();
            }

            public void Providers_ComboBox_AddRange(object[] items)
            {
                providers_ComboBox_List.AddRange(items.Select(i => (string)i));
            }

            public void Providers_ComboBox_Clear()
            {
                providers_ComboBox_List = new List<string>();
                providers_ComboBox_Index = -1;
            }

            public void ResumeLayout()
            {
                // throw new NotImplementedException();
            }

            public void SuspendLayout()
            {
                // throw new NotImplementedException();
            }

            void IForm.Providers_ComboBox_Clear()
            {
                throw new NotImplementedException();
            }

            void IForm.Providers_ComboBox_AddRange(object[] items)
            {
                throw new NotImplementedException();
            }

            void IForm.AuthCombo_ComboBox_Clear()
            {
                throw new NotImplementedException();
            }

            void IForm.AuthCombo_ComboBox_AddRange(object[] items)
            {
                throw new NotImplementedException();
            }

            void IForm.AuthCombo_ComboBox_Insert(int n, string text)
            {
                throw new NotImplementedException();
            }

            bool IForm.AuthCombo_ComboBox_Contains(string text)
            {
                throw new NotImplementedException();
            }

            void IForm.Model_ComboBox_Clear()
            {
                throw new NotImplementedException();
            }

            int IForm.Model_ComboBox_Add(string text)
            {
                throw new NotImplementedException();
            }

            void IForm.Glossary_ComboBox_Clear()
            {
                throw new NotImplementedException();
            }

            int IForm.Glossary_ComboBoxAdd(string text)
            {
                throw new NotImplementedException();
            }

            void IForm.Glossary_ComboBox_Insert(int n, string text)
            {
                throw new NotImplementedException();
            }

            IEnumerable<dynamic> IForm.Providers(Dictionary<string, string> filter)
            {
                throw new NotImplementedException();
            }

            dynamic IForm.Provider(string provider, string additionaParams)
            {
                throw new NotImplementedException();
            }

            IList<dynamic> IForm.DelegatedCredentials()
            {
                throw new NotImplementedException();
            }

            IList<dynamic> IForm.Models(string provider, Dictionary<string, string> credential_id)
            {
                throw new NotImplementedException();
            }

            IList<dynamic> IForm.Glossaries(string provider, Dictionary<string, string> credential_id)
            {
                throw new NotImplementedException();
            }

            void IForm.SuspendLayout()
            {
                throw new NotImplementedException();
            }

            void IForm.ResumeLayout()
            {
                throw new NotImplementedException();
            }

            bool insideEnableDisable = false;
            bool IForm.InsideEnableDisable { get { return insideEnableDisable; } set { insideEnableDisable = value; } }

            ResourceManager IForm.ResourceManager { get { return IntentoTranslationProviderOptionsForm.resourceManager; } }
        }

        [TestMethod]
        public void CleanOptionsOnChangeProvider()
        {
            FakeForm form = new FakeForm();
            IntentoMTFormOptions options = new IntentoMTFormOptions() {
                ApiKey = "abc",
                SmartRouting = false,
                ProviderId = "ai.text.translate.abc",
                ProviderName = "abc",
                UseCustomAuth = true,
                CustomAuth = "{'credential_id':'xxx'}".Replace('\'', '"'),
                UseCustomModel = true,
                CustomModel = "mmm",
                Glossary = "ggg",
                Format = null,
            };
            form.delegatedCredentials = new List<dynamic>() { JObject.Parse("{'credential_id':'xxx', 'temporary_credentials':'1', 'temporary_credentials_created_at':'2', 'temporary_credentials_expiry_at':'3'}".Replace('\'', '"')) };
            form.models = new List<dynamic>() { JObject.Parse("{'name':'mmm', 'id':'mmm-id'}".Replace('\'', '"')) };
            form.glossaries = new List<dynamic>() { JObject.Parse("{'name':'ggg', 'id':'ggg-id'}".Replace('\'', '"')) };

            ApiKeyState apiKeyState = form.apiKeyState = new ApiKeyState(form, options);
            apiKeyState.providers = new List<dynamic>() {
                JObject.Parse("{'name':'abc', 'id':'ai.text.translate.abc', 'pairs':[{'from':'en', 'to':'ru'}], 'symmetric':[] }".Replace('\'', '"')),
                JObject.Parse("{'name':'def', 'id':'ai.text.translate.def', 'pairs':[{'from':'en', 'to':'ru'}], 'symmetric':[] }".Replace('\'', '"')),
            };
            SmartRoutingState smartRoutingState = apiKeyState.smartRoutingState = new SmartRoutingState(apiKeyState, options);
            ProviderState providerState = smartRoutingState.providerState = new ProviderState(smartRoutingState, options);

            providerState.GetAuthState().GetModelState();
            providerState.GetAuthState().GetGlossaryState();

            form.providers_ComboBox_Index = 1;
            providerState.SelectedIndexChanged();

            Assert.AreEqual(false, options.UseCustomAuth);
            Assert.AreEqual(null, options.CustomAuth);
            Assert.AreEqual(false, options.UseCustomModel);
            Assert.AreEqual(null, options.CustomModel);
            Assert.AreEqual(null, options.Glossary);
        }
    }
}
