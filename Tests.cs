using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using static Intento.MT.Plugin.PropertiesForm.ApiKeyState;
using static Intento.MT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm
{
	[TestClass]
	public sealed class Tests
	{
		private IntentoSDK.IntentoAiTextTranslate Fabric(string apiKey, string userAgent, IntentoSDK.ProxySettings proxySettings)
		{
			var _intento = IntentoSDK.Intento.Create(apiKey, null,
				path: "https://api.inten.to/",
				userAgent: String.Format("{0} {1}", userAgent, "TestForm"),
				loggingCallback: Logs.Logging,
				proxySet: proxySettings
			);
			var _translate = _intento.Ai.Text.Translate;
			return _translate;
		}

		IntentoMTFormOptions options;
		IntentoTranslationProviderOptionsForm form;

		IntentoTranslationProviderOptionsForm.LangPair[] langPair = new IntentoTranslationProviderOptionsForm.LangPair[1]
						{ new IntentoTranslationProviderOptionsForm.LangPair("en", "ru") };

		#region initional paremeters

		static string provider_google = "ai.text.translate.google.abc";
		static string provider_other = "ai.text.translate.def";
		dynamic testProviderData = JObject.Parse(
					"{'billing': 'true', 'description': 'Translation test prov'," +
					"'production': 'false', 'integrated': 'true', 'billable': 'true', 'own_auth': 'true', 'stock_model': 'true'," +
					"'custom_model': 'true', 'delegated_credentials': 'false', 'auth': " +
					"{'credential_id':'xxx', 'temporary_credentials':'1', 'temporary_credentials_created_at':'2'}," +
					"'languages': {'symmetric': ['gu','gd','ga','de','lb','la','lo','tr','lv','lt','th','tg','te','haw'," +
					"'yi','ceb','yo','de','da','el','eo','en','zh','eu','et','es','ru'], 'pairs': []}," +
					"'format': ['text','html'],'lang_detect': 'true', 'bulk': 'true', 'custom_glossary': 'true'}".Replace('\'', '"')
				);
		List<dynamic> testProvidersData = new List<dynamic>() {
				JObject.Parse(("{'name':'abc', 'id':'" + provider_google + "', 'pairs':[{'from':'de', 'to':'ru'},{'from':'en', 'to':'es'},{'from':'de', 'to':'es'},{'from':'en', 'to':'ru'}], 'symmetric':[] }").Replace('\'', '"')),
				JObject.Parse(("{'name':'def', 'id':'" + provider_other + "', 'pairs':[{'from':'de', 'to':'ru'},{'from':'en', 'to':'es'},{'from':'en', 'to':'ru'}], 'symmetric':[] }").Replace('\'', '"')),
			};
		List<dynamic> testAuthDataGoogle = new List<dynamic>() {
				JObject.Parse("{'credential_id': 'test'," +
				"'credential_type': 'google_service_account'," +
				"'created_at': '2019-10-17 21:18:38.999582+00:00'," +
				"'temporary_credentials': {'access_token': 'ya29.c.Kl65B_'}," +
				"'temporary_credentials_created_at': '2020-01-13 10:43:33.279703+00:00'," +
				"'temporary_credentials_expiry_at': '2020-01-13 11:43:33.278188+00:00'," +
				"'error_message': null}".Replace('\'', '"')),
				JObject.Parse("{'credential_id': 'test1'," +
				"'credential_type': 'google_service_account'," +
				"'created_at': '2019-10-17 21:18:38.999582+00:00'," +
				"'temporary_credentials': {'access_token': 'c.Kl65B_'}," +
				"'temporary_credentials_created_at': '2020-01-13 10:43:33.279703+00:00'," +
				"'temporary_credentials_expiry_at': '2020-01-13 11:43:33.278188+00:00'," +
				"'error_message': null}".Replace('\'', '"'))
			};
		List<dynamic> testAuthDataOther = new List<dynamic>() {
				JObject.Parse("{'credential_id': 'test'," +
				"'credential_type': '" + provider_other + "'," +
				"'error_message': null}".Replace('\'', '"')),
				JObject.Parse("{'credential_id': 'test1'," +
				"'credential_type': '" + provider_other + "'," +
				"'error_message': null}".Replace('\'', '"'))
			};
		List<dynamic> testAuthDataEmpty = new List<dynamic>() {
				JObject.Parse("{'credential_id': 'test'," +
				"'credential_type': 'empty'," +
				"'error_message': null}".Replace('\'', '"')),
				JObject.Parse("{'credential_id': 'test1'," +
				"'credential_type': 'empty'," +
				"'error_message': null}".Replace('\'', '"'))
			};

		IList<dynamic> testModelData = new List<dynamic>() {
				JObject.Parse("{ 'id': 'model1', 'name': 'Stock', 'from': 'zh', 'to': 'en', 'stock': true }".Replace('\'', '"')),
				JObject.Parse("{ 'id': 'model2', 'name': 'notStock', 'from': 'ru', 'to': 'de', 'stock': false }".Replace('\'', '"')),
			};
		IList<dynamic> testGlossaryData = new List<dynamic>() {
				JObject.Parse("{ 'id': 'glossaries/rws_zh_en', 'name': 'gloss_zh_en', 'from': 'zh', 'to': 'en', 'entry_count': 101, 'internal_id': '171572280795.rws_zh_en' }".Replace('\'', '"')),
				JObject.Parse("{ 'id': 'glossaries/rws_ru_en', 'name': 'gloss_ru_en', 'from': 'ru', 'to': 'en', 'entry_count': 101, 'internal_id': '171572280795.rws_ru_en' }".Replace('\'', '"')),
			};
		#endregion initional paremeters

		Form InitializeForm(Form form)
		{
			//form.WindowState = FormWindowState.Minimized;
			form.Show();
			form.Refresh();
			return form;

		}

		//void CheckControlsByOptions(IntentoTranslationProviderOptionsForm form)
		//{
		//          ApiKeyState apiKeyState = form.apiKeyState = new ApiKeyState(form, options);
		//          apiKeyState.ReadProviders();
		//          apiKeyState.EnableDisable();

		//          var qqq = (IntentoFormOptionsMT)InitializeForm(form.formMT);

		//          Assert.AreEqual(options.SmartRouting, qqq.checkBoxSmartRouting.Checked);
		//          if (options.SmartRouting)
		//          {
		//              Assert.AreEqual(false, qqq.groupBoxProvider.Enabled);
		//              Assert.AreEqual(false, qqq.groupBoxBillingAccount.Enabled);
		//              Assert.AreEqual(false, qqq.textBoxCredentials.Visible);
		//              Assert.AreEqual(false, qqq.comboBoxCredentialId.Visible);
		//              Assert.AreEqual(false, qqq.buttonWizard.Visible);
		//              Assert.AreEqual(false, qqq.groupBoxModel.Enabled);
		//              Assert.AreEqual(false, qqq.textBoxModel.Visible);
		//              Assert.AreEqual(false, qqq.comboBoxModels.Visible);
		//              Assert.AreEqual(false, qqq.groupBoxGlossary.Enabled);
		//              Assert.AreEqual(false, qqq.textBoxGlossary.Visible);
		//              Assert.AreEqual(false, qqq.comboBoxGlossaries.Visible);
		//          }
		//          else
		//          {
		//              Assert.AreEqual(string.IsNullOrWhiteSpace(options.ProviderName), string.IsNullOrWhiteSpace(qqq.comboBoxProviders.Text));

		//              #region тест контролов ввода credential
		//              if (string.IsNullOrWhiteSpace(options.ProviderName))
		//              {
		//                  Assert.AreEqual(false, qqq.groupBoxBillingAccount.Enabled);
		//                  Assert.AreEqual(false, qqq.checkBoxUseOwnCred.Checked);
		//                  Assert.AreEqual(false, qqq.textBoxCredentials.Visible);
		//                  Assert.AreEqual(false, qqq.comboBoxCredentialId.Visible);
		//                  Assert.AreEqual(qqq.textBoxCredentials.Visible, qqq.buttonWizard.Visible);
		//              }
		//              else
		//              {
		//                  if (!(bool)testProviderData.billable && !(bool)testProviderData.stock_model)
		//                  {
		//                      //StateModeEnum.required
		//                      Assert.AreEqual(true, qqq.groupBoxBillingAccount.Enabled);
		//                      Assert.AreEqual(false, qqq.checkBoxUseOwnCred.Enabled);
		//                      Assert.AreEqual(true, qqq.checkBoxUseOwnCred.Visible);
		//                      Assert.AreEqual(true, qqq.checkBoxUseOwnCred.Checked);
		//                  }
		//                  else if (!(bool)testProviderData.own_auth)
		//                  {
		//                      //StateModeEnum.prohibited
		//                      Assert.AreEqual(false, qqq.groupBoxBillingAccount.Enabled);
		//                      Assert.AreEqual(false, qqq.checkBoxUseOwnCred.Enabled);
		//                      Assert.AreEqual(false, qqq.checkBoxUseOwnCred.Visible);
		//                      Assert.AreEqual(false, qqq.checkBoxUseOwnCred.Checked);
		//                  }
		//                  else
		//                  {
		//                      //StateModeEnum.optional
		//                      Assert.AreEqual(true, qqq.groupBoxBillingAccount.Enabled);
		//                      Assert.AreEqual(true, qqq.checkBoxUseOwnCred.Enabled);
		//                      Assert.AreEqual(true, qqq.checkBoxUseOwnCred.Visible);
		//                      Assert.AreEqual(options.UseCustomAuth, qqq.checkBoxUseOwnCred.Checked);
		//                  }
		//                  #region both auth - delegated and custom enabled
		//                  //Assert.AreEqual(qqq.checkBoxUseOwnCred.Checked && !(bool)testProviderData.delegated_credentials, qqq.textBoxCredentials.Visible);
		//                  //Assert.AreEqual(qqq.checkBoxUseOwnCred.Checked && (bool)testProviderData.delegated_credentials, qqq.comboBoxCredentialId.Visible);
		//                  //Assert.AreEqual(qqq.textBoxCredentials.Visible, qqq.buttonWizard.Visible);
		//                  #endregion both auth - delegated and custom enabled
		//                  #region only delegated auth enabled
		//                  Assert.AreEqual(false, qqq.textBoxCredentials.Visible);
		//                  Assert.AreEqual(true, qqq.comboBoxCredentialId.Visible);
		//                  // если данных нет, то показываем панель со ссылкой
		//                  Assert.AreEqual(form.testAuthData.Equals(testAuthDataEmpty), qqq.panelCreateDelegatedCredential.Visible);
		//                  Assert.AreEqual(qqq.textBoxCredentials.Visible, qqq.buttonWizard.Visible);
		//                  #endregion only delegated auth enabled
		//                  if (qqq.checkBoxUseOwnCred.Enabled)
		//                  {
		//                      if ((bool)testProviderData.delegated_credentials)
		//                      {
		//                          dynamic credential = JToken.Parse(options.CustomAuth);
		//                          Assert.AreEqual((string)credential.credential_id, qqq.comboBoxCredentialId.Text);
		//                          Assert.AreEqual(SystemColors.Window, qqq.comboBoxCredentialId.BackColor);
		//                      }
		//                      else
		//                      {
		//                          string tbText = string.Empty;
		//                          var dct = apiKeyState.smartRoutingState.providerState.GetAuthState().providerDataAuthDict;
		//                          foreach (string item in apiKeyState.smartRoutingState.providerState.providerAuthList)
		//                          {
		//                              // словарь контроллера содержит ключ полученный от провайдера
		//                              Assert.IsTrue(dct.ContainsKey(item));

		//                          }
		//                          var arr = dct.Select(z => String.Format("{0}:{1}", z.Key, z.Value)).ToArray();
		//                          tbText = string.Join(", ", arr);
		//                          Assert.AreEqual((tbText).Trim(), qqq.textBoxCredentials.Text);
		//                          Assert.IsTrue(qqq.textBoxCredentials.Enabled);
		//                          Assert.AreEqual(SystemColors.Window, qqq.textBoxCredentials.BackColor);
		//                      }
		//                  }
		//              }
		//              #endregion тест контролов ввода credential

		//              #region тест контролов ввода models
		//              if (string.IsNullOrWhiteSpace(options.ProviderName))
		//              {
		//                  Assert.AreEqual(false, qqq.groupBoxModel.Enabled);
		//                  Assert.AreEqual(false, qqq.checkBoxUseCustomModel.Enabled);
		//                  Assert.AreEqual(false, qqq.checkBoxUseCustomModel.Checked);
		//                  Assert.AreEqual(false, qqq.textBoxModel.Visible);
		//                  Assert.AreEqual(false, qqq.comboBoxModels.Visible);
		//                  Assert.AreEqual(false, qqq.groupBoxOptional.Enabled);
		//              }
		//              else
		//              {
		//                  Assert.AreEqual((bool)testProviderData.custom_model, qqq.groupBoxModel.Enabled);
		//                  if (!(bool)testProviderData.stock_model && (bool)testProviderData.custom_model)
		//                  {
		//                      // StateModeEnum.required;
		//                      Assert.AreEqual(true, qqq.groupBoxModel.Enabled);
		//                      Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Visible);
		//                      Assert.AreEqual(false, qqq.checkBoxUseCustomModel.Enabled);
		//                      Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Checked);
		//                      Assert.AreEqual(true, qqq.groupBoxOptional.Enabled);
		//                  }
		//                  else if (!(bool)testProviderData.custom_model)
		//                  {
		//                      // StateModeEnum.prohibited;
		//                      Assert.AreEqual(false, qqq.groupBoxModel.Enabled);
		//                      Assert.AreEqual(false, qqq.checkBoxUseCustomModel.Visible);
		//                      Assert.AreEqual(false, qqq.textBoxModel.Visible);
		//                      Assert.AreEqual(false, qqq.comboBoxModels.Visible);
		//                      Assert.AreEqual(false, qqq.checkBoxUseCustomModel.Checked);
		//                      Assert.AreEqual(false, qqq.groupBoxOptional.Enabled);
		//                  }
		//                  else
		//                  {
		//                      // StateModeEnum.optional;
		//                      Assert.AreEqual(true, qqq.groupBoxModel.Enabled);
		//                      Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Visible);
		//                      Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Enabled);
		//                      Assert.AreEqual(true, qqq.groupBoxOptional.Enabled);
		//                  }
		//                  if (options.UseCustomModel)
		//                  {
		//                      Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Checked);
		//                      Assert.AreEqual(form.testModelData == null || !form.testModelData.Any(), qqq.textBoxModel.Visible);
		//                      Assert.AreEqual(!qqq.textBoxModel.Visible, qqq.comboBoxModels.Visible);
		//                      if (qqq.comboBoxModels.Visible)
		//                      {
		//                          string modelName = string.Empty;
		//                          foreach (dynamic item in form.testModelData)
		//                          {
		//                              if ((string)item.id == options.CustomModel)
		//                              {
		//                                  modelName = (string)item.name;
		//                                  break;
		//                              }
		//                          }
		//                          Assert.AreEqual(modelName, qqq.comboBoxModels.Text);
		//                          Assert.AreEqual(SystemColors.Window, qqq.comboBoxModels.BackColor);
		//                      }
		//                      else
		//                      {
		//                          Assert.AreEqual(options.CustomModel, qqq.textBoxModel.Text);
		//                          Assert.AreEqual(SystemColors.Window, qqq.textBoxModel.BackColor);
		//                      }
		//                      if (options.FromLanguage!=null)
		//                          Assert.AreEqual(true, qqq.comboBoxFrom.Text.Contains(options.FromLanguage));
		//                      else
		//                          Assert.AreEqual(true, qqq.comboBoxFrom.Text.Contains("en"));
		//                      if (options.ToLanguage != null)
		//                          Assert.AreEqual(true, qqq.comboBoxTo.Text.Contains(options.ToLanguage));
		//                      else
		//                          Assert.AreEqual(true, qqq.comboBoxTo.Text.Contains("es"));
		//                  }
		//                  else
		//                  {
		//                      Assert.AreEqual(false, qqq.checkBoxUseCustomModel.Checked);
		//                      Assert.AreEqual(false, qqq.textBoxModel.Visible);
		//                      Assert.AreEqual(false, qqq.comboBoxModels.Visible);
		//                  }
		//              }
		//              #endregion тест контролов ввода models

		//              #region тест контролов ввода glossary
		//              if (string.IsNullOrWhiteSpace(options.ProviderName))
		//              {
		//                  Assert.AreEqual(false, qqq.groupBoxGlossary.Enabled);
		//                  Assert.AreEqual(false, qqq.checkBoxUseGlossary.Visible);
		//                  Assert.AreEqual(false, qqq.textBoxGlossary.Visible);
		//                  Assert.AreEqual(false, qqq.comboBoxGlossaries.Visible);
		//              }
		//              else
		//              {
		//                  Assert.AreEqual((bool)testProviderData.custom_glossary, qqq.groupBoxModel.Enabled);
		//                  if (!(bool)testProviderData.custom_glossary)
		//                  {
		//                      // StateModeEnum.prohibited;
		//                      Assert.AreEqual(false, qqq.groupBoxGlossary.Enabled);
		//                      Assert.AreEqual(false, qqq.checkBoxUseGlossary.Visible);
		//                      Assert.AreEqual(false, qqq.textBoxGlossary.Visible);
		//                      Assert.AreEqual(false, qqq.comboBoxGlossaries.Visible);
		//                  }
		//                  else
		//                  {
		//                      // StateModeEnum.optional;
		//                      Assert.AreEqual(true, qqq.groupBoxGlossary.Enabled);
		//                      Assert.AreEqual(form.testGlossaryData == null || !form.testGlossaryData.Any(), qqq.textBoxGlossary.Visible);
		//                      Assert.AreEqual(!qqq.textBoxGlossary.Visible, qqq.comboBoxGlossaries.Visible);
		//                      if (!string.IsNullOrWhiteSpace(options.Glossary))
		//                      {
		//                          if (qqq.comboBoxGlossaries.Visible)
		//                          {
		//                              string gName = string.Empty;
		//                              foreach (dynamic item in form.testGlossaryData)
		//                              {
		//                                  if ((string)item.id == options.Glossary)
		//                                  {
		//                                      gName = (string)item.name;
		//                                      break;
		//                                  }
		//                              }
		//                              Assert.AreEqual(gName, qqq.comboBoxGlossaries.Text);
		//                              Assert.AreEqual(SystemColors.Window, qqq.comboBoxGlossaries.BackColor);
		//                          }
		//                          else
		//                          {
		//                              Assert.AreEqual(options.Glossary, qqq.textBoxGlossary.Text);
		//                              Assert.AreEqual(SystemColors.Window, qqq.textBoxGlossary.BackColor);
		//                          }

		//                      }
		//                  }
		//              }

		//              #endregion тест контролов ввода glossary

		//              #region тест контролов Optional
		//              if (options.UseCustomModel || options.Glossary != null)
		//              {
		//                  Assert.AreEqual(true, qqq.groupBoxOptional.Enabled);
		//                  Assert.AreEqual(false, string.IsNullOrWhiteSpace(qqq.comboBoxFrom.Text));
		//                  Assert.AreEqual(false, string.IsNullOrWhiteSpace(qqq.comboBoxTo.Text));
		//              }
		//              else
		//                  Assert.AreEqual(false, qqq.groupBoxOptional.Enabled);
		//              #endregion тест контролов Optional
		//          }
		//          form.apiKeyState.EnableDisable();
		//          form.FillOptions(form.currentOptions);
		//          form.apiKeyState.EnableDisable();
		//          form.RefreshFormInfo();
		//          //form.Visible = true;
		//          InitializeForm(form);
		//          //form.WindowState = FormWindowState.Minimized;
		//          //form.Show();
		//          //form.Refresh();
		//      }


		#region all parametries filled, auth - delegated, models & glossary - delegated
		//[TestMethod]
		//public void InitionalFillOptionsForm1()
		//{
		//    options = new IntentoMTFormOptions()
		//    {
		//        ApiKey = "def",
		//        SmartRouting = false,
		//        ProviderId = provider_other,
		//        ProviderName = "def",
		//        UseCustomAuth = true,
		//        CustomAuth = "{'credential_id':'test', 'temporary_credentials':'1', 'temporary_credentials_created_at':'2', 'temporary_credentials_expiry_at':'3'}".Replace('\'', '"'),
		//        UseCustomModel = true,
		//        CustomModel = "model2",
		//        Glossary = "glossaries/rws_zh_en",
		//        Format = null,
		//        FromLanguage = "de",
		//        ToLanguage = "es",
		//        ForbidSaveApikey = true,
		//    };
		//    using (form = new IntentoTranslationProviderOptionsForm(options, langPair, Fabric, true))
		//    {
		//        form.testListProvidersData = testProvidersData;
		//        form.testOneProviderData = testProviderData;
		//        form.testAuthData = testAuthDataOther;
		//        form.testModelData = testModelData;
		//        form.testGlossaryData = testGlossaryData;

		//        // all parametries filled, auth - not delegated, models & glossary - delegated
		//        CheckControlsByOptions(form);
		//        Assert.AreEqual(false, form.groupBoxMTConnect.Visible);
		//        Assert.AreEqual(true, form.groupBoxMTConnect2.Visible);
		//        Assert.AreEqual(options.ApiKey, form.apiKey_tb.Text);
		//        Assert.AreEqual(options.ProviderName, form.textBoxProviderName.Text);
		//        Assert.AreEqual(false, string.IsNullOrWhiteSpace(form.textBoxAccount.Text));
		//        Assert.AreEqual(false, string.IsNullOrWhiteSpace(form.textBoxModel.Text));
		//        Assert.AreEqual(false, string.IsNullOrWhiteSpace(form.textBoxGlossary.Text));
		//        Assert.AreEqual(true, form.buttonContinue.Enabled);
		//        form.formMT.Close();
		//    }
		//}
		#endregion all parametries filled, auth - delegated, models & glossary - delegated

		#region auth - not delegated (the provider has no predefined), models & glossary - empty
		[TestMethod]
		public void InitionalFillOptionsForm2()
		{
			IntentoTranslationProviderOptionsForm form;

			options = new IntentoMTFormOptions()
			{
				ApiKey = "def",
				SmartRouting = false,
				ProviderId = provider_other,
				ProviderName = "def",
				UseCustomAuth = true,
				CustomAuth = "{'credential_id':'xxx', 'temporary_credentials':'1', 'temporary_credentials_created_at':'2', 'temporary_credentials_expiry_at':'3'}".Replace('\'', '"'),
				UseCustomModel = false,
				CustomModel = "model2",
				Glossary = "",
				Format = null,
				FromLanguage = "en",
				ToLanguage = "es",
				ForbidSaveApikey = true,
			};
			using (form = new IntentoTranslationProviderOptionsForm(options, langPair, Fabric, true))
			{
				form.testListProvidersData = testProvidersData;
				form.testOneProviderData = testProviderData;
				form.testAuthData = testAuthDataEmpty;
				form.testModelData = testModelData;
				form.testGlossaryData = new List<dynamic>(); // testGlossaryData;

				ApiKeyState apiKeyState = form.apiKeyState = new ApiKeyState(form, options);
				apiKeyState.ReadProviders();
				apiKeyState.EnableDisable();
				var qqq = (IntentoFormOptionsMT)InitializeForm(form.formMT);

				Assert.AreEqual(false, string.IsNullOrWhiteSpace(qqq.comboBoxProviders.Text), "Empty provider");
				Assert.AreEqual(false, qqq.checkBoxSmartRouting.Checked, "SmartRouting.Checked");

				Assert.AreEqual(true, qqq.groupBoxProvider.Enabled, "groupBoxProvider.Enabled");
				Assert.AreEqual(true, qqq.groupBoxBillingAccount.Enabled, "groupBoxBillingAccount.Enabled");
				Assert.AreEqual(true, qqq.comboBoxCredentialId.Visible, "comboBoxCredentialId.Visible");
				Assert.AreEqual("via Intento", qqq.comboBoxCredentialId.Text, "comboBoxCredentialId.Text");
				Assert.AreEqual(SystemColors.Window, qqq.comboBoxCredentialId.BackColor, "comboBoxCredentialId.BackColor");
				//Assert.AreEqual((string)credential.credential_id, qqq.comboBoxCredentialId.Text);
				Assert.AreEqual(true, qqq.panelConnectAccount.Visible, "panelConnectAccount.Visible");
				Assert.AreEqual(true, qqq.buttonRefresh.Visible, "buttonRefresh.Visible");

				Assert.AreEqual(true, qqq.groupBoxModel.Enabled, "groupBoxModel.Enabled");
				Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Visible, "checkBoxUseCustomModel.Visible");
				Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Enabled, "checkBoxUseCustomModel.Enabled");
				Assert.AreEqual(false, qqq.checkBoxUseCustomModel.Checked, "checkBoxUseCustomModel.Checked");
				Assert.AreEqual(false, qqq.textBoxModel.Visible, "textBoxModel.Visible");
				Assert.AreEqual(false, qqq.comboBoxModels.Visible, "comboBoxModels.Visible");
				//Assert.AreEqual("", qqq.comboBoxModels.Text);
				//Assert.AreEqual(SystemColors.Window, qqq.comboBoxModels.BackColor);

				Assert.AreEqual(false, qqq.groupBoxOptional.Enabled);
				//Assert.AreEqual(true, qqq.comboBoxFrom.Text.Contains("en"));
				//Assert.AreEqual(true, qqq.comboBoxTo.Text.Contains(options.ToLanguage));

				Assert.AreEqual(true, qqq.groupBoxGlossary.Enabled);
				// потому что на запрос глоссариев ничего не возвратилось 
				Assert.AreEqual(true, qqq.textBoxGlossary.Visible);
				Assert.AreEqual(false, qqq.comboBoxGlossaries.Visible);

				Assert.AreEqual(true, string.IsNullOrEmpty(qqq.comboBoxGlossaries.Text));
				Assert.AreEqual(true, string.IsNullOrEmpty(qqq.textBoxGlossary.Text));
				Assert.AreEqual(SystemColors.Window, qqq.comboBoxGlossaries.BackColor);
				Assert.AreEqual(SystemColors.Window, qqq.textBoxGlossary.BackColor);


				//смотрим как заполнилась основная форма
				form.apiKeyState.EnableDisable();
				form.FillOptions(form.currentOptions);
				form.apiKeyState.EnableDisable();
				form.RefreshFormInfo();
				// form.Visible = true;
				InitializeForm(form);

				Assert.AreEqual(false, form.groupBoxMTConnect.Visible);
				Assert.AreEqual(true, form.groupBoxMTConnect2.Visible);
				Assert.AreEqual(options.ApiKey, form.apiKey_tb.Text);
				Assert.AreEqual(options.ProviderName, form.textBoxProviderName.Text);
				Assert.AreEqual(Resource.Empty, form.textBoxAccount.Text);
				Assert.AreEqual(Resource.Empty, form.textBoxModel.Text);
				Assert.AreEqual(Resource.Empty, form.textBoxGlossary.Text);
				Assert.AreEqual(true, form.buttonContinue.Enabled);
				form.formMT.Close();
				qqq.Refresh();
			}
		}
		#endregion auth - not delegated (the provider has no predefined), models & glossary - empty

		#region all parametries filled, auth - delegated, models & glossary - not delegated
		[TestMethod]
		public void InitionalFillOptionsForm3()
		{
			options = new IntentoMTFormOptions()
			{
				ApiKey = "def",
				SmartRouting = false,
				ProviderId = provider_google,
				ProviderName = "abc",
				UseCustomAuth = true,
				CustomAuth = "{'credential_id': 'test1'}".Replace('\'', '"'),
				UseCustomModel = true,
				CustomModel = "model2",
				Glossary = "glossaries/rws_zh_en",
				Format = null,
				ForbidSaveApikey = true,
				FromLanguage = "de",
				ToLanguage = "ru"

			};
			using (form = new IntentoTranslationProviderOptionsForm(options, langPair, Fabric, true))
			{
				testProviderData.delegated_credentials = true;
				form.testListProvidersData = testProvidersData;
				form.testOneProviderData = testProviderData;
				form.testAuthData = testAuthDataGoogle;
				form.testModelData = null;
				form.testGlossaryData = null;
				ApiKeyState apiKeyState = form.apiKeyState = new ApiKeyState(form, options);
				apiKeyState.ReadProviders();
				apiKeyState.EnableDisable();
				var qqq = (IntentoFormOptionsMT)InitializeForm(form.formMT);

				Assert.AreEqual(false, string.IsNullOrWhiteSpace(qqq.comboBoxProviders.Text), "Empty provider");
				Assert.AreEqual(false, qqq.checkBoxSmartRouting.Checked, "SmartRouting.Checked");

				Assert.AreEqual(true, qqq.groupBoxProvider.Enabled, "groupBoxProvider.Enabled");
				Assert.AreEqual(true, qqq.groupBoxBillingAccount.Enabled, "groupBoxBillingAccount.Enabled");
				Assert.AreEqual(true, qqq.comboBoxCredentialId.Visible, "comboBoxCredentialId.Visible");
				Assert.AreEqual("test1", qqq.comboBoxCredentialId.Text, "comboBoxCredentialId.Text");
				Assert.AreEqual(SystemColors.Window, qqq.comboBoxCredentialId.BackColor, "comboBoxCredentialId.BackColor");
				//Assert.AreEqual((string)credential.credential_id, qqq.comboBoxCredentialId.Text);
				Assert.AreEqual(true, qqq.panelConnectAccount.Visible, "panelConnectAccount.Visible");
				Assert.AreEqual(true, qqq.buttonRefresh.Visible, "buttonRefresh.Visible");

				Assert.AreEqual(true, qqq.groupBoxModel.Enabled, "groupBoxModel.Enabled");
				Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Visible, "checkBoxUseCustomModel.Visible");
				Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Enabled, "checkBoxUseCustomModel.Enabled");
				Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Checked, "checkBoxUseCustomModel.Checked");
				Assert.AreEqual(true, qqq.textBoxModel.Visible, "textBoxModel.Visible");
				Assert.AreEqual(false, qqq.comboBoxModels.Visible, "comboBoxModels.Visible");
				Assert.AreEqual("", qqq.comboBoxModels.Text);
				Assert.AreEqual("model2", qqq.textBoxModel.Text);
				Assert.AreEqual(SystemColors.Window, qqq.textBoxModel.BackColor);

				Assert.AreEqual(true, qqq.groupBoxOptional.Enabled);
				Assert.AreEqual(true, qqq.comboBoxFrom.Text.Contains("de"));
				Assert.AreEqual(true, qqq.comboBoxTo.Text.Contains(options.ToLanguage));

				Assert.AreEqual(true, qqq.groupBoxGlossary.Enabled);
				// потому что на запрос глоссариев ничего не возвратилось 
				Assert.AreEqual(true, qqq.textBoxGlossary.Visible);
				Assert.AreEqual(false, qqq.comboBoxGlossaries.Visible);

				Assert.AreEqual(true, string.IsNullOrEmpty(qqq.comboBoxGlossaries.Text));
				Assert.AreEqual("glossaries/rws_zh_en", qqq.textBoxGlossary.Text);
				Assert.AreEqual(SystemColors.Window, qqq.comboBoxGlossaries.BackColor);
				Assert.AreEqual(SystemColors.Window, qqq.textBoxGlossary.BackColor);


				//смотрим как заполнилась основная форма
				form.apiKeyState.EnableDisable();
				form.FillOptions(form.currentOptions);
				form.apiKeyState.EnableDisable();
				form.RefreshFormInfo();
				// form.Visible = true;
				InitializeForm(form);

				Assert.AreEqual(false, form.groupBoxMTConnect.Visible);
				Assert.AreEqual(true, form.groupBoxMTConnect2.Visible);
				Assert.AreEqual(options.ApiKey, form.apiKey_tb.Text);
				Assert.AreEqual(options.ProviderName, form.textBoxProviderName.Text);
				Assert.AreEqual("test1", form.textBoxAccount.Text);
				Assert.AreEqual("model2", form.textBoxModel.Text);
				Assert.AreEqual("glossaries/rws_zh_en", form.textBoxGlossary.Text);
				Assert.AreEqual(true, form.buttonContinue.Enabled);
				form.formMT.Close();
				qqq.Refresh();
			}
		}
		#endregion all parametries filled, auth - delegated, models & glossary - not delegated

		#region only provider filled
		//[TestMethod]
		//public void InitionalFillOptionsForm4()
		//{
		//    options = new IntentoMTFormOptions()
		//    {
		//        ApiKey = "def",
		//        SmartRouting = false,
		//        ProviderId = provider_other,
		//        ProviderName = "def",
		//        UseCustomAuth = false,
		//        CustomAuth = "{'credential_id': 'test1'}".Replace('\'', '"'),
		//        UseCustomModel = false,
		//        CustomModel = "model2",
		//        Glossary = null,
		//        Format = null,
		//        ForbidSaveApikey = true,
		//    };
		//    using (form = new IntentoTranslationProviderOptionsForm(options, langPair, Fabric, true))
		//    {
		//        testProviderData.delegated_credentials = false;
		//        //testProviderData.stock_model = false;
		//        form.testListProvidersData = testProvidersData;
		//        form.testOneProviderData = testProviderData;
		//        form.testAuthData = null;
		//        form.testModelData = null;
		//        form.testGlossaryData = null;
		//        CheckControlsByOptions(form);
		//        Assert.AreEqual(false, form.groupBoxMTConnect.Visible);
		//        Assert.AreEqual(true, form.groupBoxMTConnect2.Visible);
		//        Assert.AreEqual(options.ApiKey, form.apiKey_tb.Text);
		//        Assert.AreEqual(options.ProviderName, form.textBoxProviderName.Text);
		//        Assert.AreEqual(Resource.Empty, form.textBoxAccount.Text);
		//        Assert.AreEqual(Resource.Empty, form.textBoxModel.Text);
		//        Assert.AreEqual(Resource.Empty, form.textBoxGlossary.Text);
		//        Assert.AreEqual(true, form.buttonContinue.Enabled);
		//        form.formMT.Close();
		//    }
		//}
		#endregion only provider filled

		#region only API key filled, smartRouting - false
		//[TestMethod]
		//public void InitionalFillOptionsForm5()
		//{
		//    options = new IntentoMTFormOptions()
		//    {
		//        ApiKey = "def",
		//        SmartRouting = false,
		//        ProviderId = null,
		//        ProviderName = null,
		//        UseCustomAuth = false,
		//        CustomAuth = "{'credential_id': 'test1'}".Replace('\'', '"'),
		//        UseCustomModel = false,
		//        CustomModel = "model2",
		//        Glossary = null,
		//        Format = null,
		//        ForbidSaveApikey = true,
		//    };
		//    using (form = new IntentoTranslationProviderOptionsForm(options, langPair, Fabric, true))
		//    {
		//        form.testListProvidersData = testProvidersData;
		//        form.testOneProviderData = null;
		//        form.testAuthData = null;
		//        form.testModelData = null;
		//        form.testGlossaryData = null;
		//        CheckControlsByOptions(form);
		//        Assert.AreEqual(false, form.groupBoxMTConnect.Visible);
		//        Assert.AreEqual(true, form.groupBoxMTConnect2.Visible);
		//        Assert.AreEqual(options.ApiKey, form.apiKey_tb.Text);
		//        Assert.AreEqual(Resource.NeedAChoise, form.textBoxProviderName.Text);
		//        Assert.AreEqual(Resource.MFNa, form.textBoxAccount.Text);
		//        Assert.AreEqual(Resource.MFNa, form.textBoxModel.Text);
		//        Assert.AreEqual(Resource.MFNa, form.textBoxGlossary.Text);
		//        Assert.AreEqual(false, form.buttonContinue.Enabled);
		//        form.formMT.Close();
		//    }
		//}
		#endregion only provider filled

		#region only API key filled, smartRouting - true
		//[TestMethod]
		//public void InitionalFillOptionsForm6()
		//{
		//    options = new IntentoMTFormOptions()
		//    {
		//        ApiKey = "def",
		//        SmartRouting = true,
		//        ProviderId = provider_google,
		//        ProviderName = "abc",
		//        UseCustomAuth = true,
		//        CustomAuth = "{'credential_id': 'test1'}".Replace('\'', '"'),
		//        UseCustomModel = true,
		//        CustomModel = "model2",
		//        Glossary = "glossaries/rws_zh_en",
		//        Format = null,
		//        ForbidSaveApikey = true,
		//    };
		//    using (form = new IntentoTranslationProviderOptionsForm(options, langPair, Fabric, true))
		//    {
		//        form.testListProvidersData = testProvidersData;
		//        form.testOneProviderData = null;
		//        form.testAuthData = null;
		//        form.testModelData = null;
		//        form.testGlossaryData = null;
		//        CheckControlsByOptions(form);
		//        Assert.AreEqual(false, form.groupBoxMTConnect.Visible);
		//        Assert.AreEqual(true, form.groupBoxMTConnect2.Visible);
		//        Assert.AreEqual(options.ApiKey, form.apiKey_tb.Text);
		//        Assert.AreEqual(Resource.MFSmartRoutingText, form.textBoxProviderName.Text);
		//        Assert.AreEqual(Resource.MFNa, form.textBoxAccount.Text);
		//        Assert.AreEqual(Resource.MFNa, form.textBoxModel.Text);
		//        Assert.AreEqual(Resource.MFNa, form.textBoxGlossary.Text);
		//        Assert.AreEqual(true, form.buttonContinue.Enabled);
		//    }
		//}
		#endregion only API key filled, smartRouting - true

		[TestMethod]
		public void CleanOptionsOnChangeProvider()
		{
			#region all parametries filled, auth - not delegated, models & glossary - delegated
			options = new IntentoMTFormOptions()
			{
				ApiKey = "def",
				SmartRouting = false,
				ProviderId = provider_google,
				ProviderName = "abc",
				UseCustomAuth = true,
				CustomAuth = "{'credential_id':'xxx', 'temporary_credentials':'1', 'temporary_credentials_created_at':'2', 'temporary_credentials_expiry_at':'3'}".Replace('\'', '"'),
				UseCustomModel = true,
				CustomModel = "model2",
				Glossary = "glossaries/rws_zh_en",
				Format = null,
				ForbidSaveApikey = true,
			};
			IntentoTranslationProviderOptionsForm form =
				new IntentoTranslationProviderOptionsForm(options, langPair, Fabric, true);

			form.testListProvidersData = testProvidersData;
			form.testOneProviderData = testProviderData;
			form.testAuthData = testAuthDataGoogle;
			form.testModelData = testModelData;
			form.testGlossaryData = testGlossaryData;

			ApiKeyState apiKeyState = form.apiKeyState = new ApiKeyState(form, options);
			apiKeyState.ReadProviders();
			apiKeyState.EnableDisable();

			var qqq = (IntentoFormOptionsMT)InitializeForm(form.formMT);
			#endregion all parametries filled, auth - not delegated, models & glossary - delegated

			qqq.comboBoxProviders.SelectedIndex = 1;
			qqq.Refresh();

			Assert.AreEqual(true, qqq.groupBoxBillingAccount.Enabled);
			Assert.AreEqual(true, qqq.panelConnectAccount.Visible);
			Assert.AreEqual(true, qqq.buttonRefresh.Visible);
			Assert.AreEqual(true, qqq.comboBoxCredentialId.Visible);
			Assert.AreEqual(false, qqq.checkBoxUseCustomModel.Checked);
			Assert.AreEqual(false, qqq.textBoxModel.Visible);
			Assert.AreEqual(false, qqq.comboBoxModels.Visible);
			Assert.AreEqual(false, qqq.textBoxGlossary.Visible);
			Assert.AreEqual(true, qqq.comboBoxGlossaries.Visible);
			Assert.AreEqual(true, string.IsNullOrEmpty(qqq.comboBoxGlossaries.Text));
			Assert.AreEqual(false, qqq.groupBoxOptional.Enabled);

			form.apiKeyState.FillOptions(options);

			Assert.AreEqual(null, options.CustomAuth);
			Assert.AreEqual(false, options.UseCustomAuth);
			Assert.AreEqual(false, options.UseCustomModel);
			Assert.AreEqual(null, options.CustomModel);
			Assert.AreEqual(null, options.Glossary);

			qqq.Close();
		}

		[TestMethod]
		public void CleanOptionsOnChangeCreditionals()
		{
			// all parametries filled, auth, models & glossary - delegated
			options = new IntentoMTFormOptions()
			{
				ApiKey = "def",
				SmartRouting = false,
				ProviderId = provider_google,
				ProviderName = "abc",
				UseCustomAuth = true,
				CustomAuth = "{'credential_id': 'test1'}".Replace('\'', '"'),
				UseCustomModel = true,
				CustomModel = "model2",
				Glossary = "glossaries/rws_zh_en",
				Format = null,
				ForbidSaveApikey = true,
			};
			IntentoTranslationProviderOptionsForm form =
				new IntentoTranslationProviderOptionsForm(options, langPair, Fabric, true);
			testProviderData.delegated_credentials = true;
			form.testListProvidersData = testProvidersData;
			form.testOneProviderData = testProviderData;
			form.testAuthData = testAuthDataGoogle;
			form.testModelData = testModelData;
			form.testGlossaryData = new List<dynamic>(); //testGlossaryData;
			ApiKeyState apiKeyState = form.apiKeyState = new ApiKeyState(form, options);
			apiKeyState.ReadProviders();
			apiKeyState.EnableDisable();

			var qqq = (IntentoFormOptionsMT)InitializeForm(form.formMT);
			Assert.AreEqual("test1", qqq.comboBoxCredentialId.Text);

			//qqq.comboBoxCredentialId.SelectedIndex = 0;
			//qqq.Refresh();

			Assert.AreEqual(true, qqq.groupBoxBillingAccount.Enabled);
			Assert.AreEqual(true, qqq.panelConnectAccount.Visible);
			Assert.AreEqual(true, qqq.buttonRefresh.Visible);
			Assert.AreEqual(true, qqq.comboBoxCredentialId.Visible);
			Assert.AreEqual("test1", qqq.comboBoxCredentialId.Text);
			//Assert.AreEqual(Color.LightPink, qqq.comboBoxCredentialId.BackColor);
			Assert.AreEqual(SystemColors.Window, qqq.comboBoxCredentialId.BackColor);
			Assert.AreEqual(true, qqq.groupBoxModel.Enabled);
			Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Checked);
			Assert.AreEqual(false, qqq.textBoxModel.Visible);
			Assert.AreEqual(true, qqq.comboBoxModels.Visible);
			Assert.AreEqual(true, qqq.groupBoxGlossary.Enabled);
			Assert.AreEqual(true, qqq.textBoxGlossary.Visible);
			Assert.AreEqual(false, qqq.comboBoxGlossaries.Visible);
			Assert.AreEqual("glossaries/rws_zh_en", qqq.textBoxGlossary.Text);
			Assert.AreEqual(true, qqq.groupBoxOptional.Enabled);

			qqq.comboBoxCredentialId.SelectedIndex = 1;
			qqq.Refresh();

			Assert.AreEqual(true, qqq.groupBoxBillingAccount.Enabled);
			Assert.AreEqual(true, qqq.panelConnectAccount.Visible);
			Assert.AreEqual(true, qqq.buttonRefresh.Visible);
			Assert.AreEqual(true, qqq.comboBoxCredentialId.Visible);
			Assert.AreEqual(SystemColors.Window, qqq.comboBoxCredentialId.BackColor);
			Assert.AreEqual(true, qqq.groupBoxModel.Enabled);
			Assert.AreEqual(false, qqq.checkBoxUseCustomModel.Checked);
			Assert.AreEqual(false, qqq.textBoxModel.Visible);
			Assert.AreEqual(false, qqq.comboBoxModels.Visible);
			Assert.AreEqual(true, qqq.groupBoxGlossary.Enabled);
			Assert.AreEqual(true, qqq.textBoxGlossary.Visible);
			Assert.AreEqual(false, qqq.comboBoxGlossaries.Visible);
			Assert.AreEqual(true, string.IsNullOrEmpty(qqq.comboBoxGlossaries.Text));
			Assert.AreEqual(false, qqq.groupBoxOptional.Enabled);

			form.apiKeyState.FillOptions(options);

			Assert.AreEqual("{'credential_id': 'test'}".Replace('\'', '"'), options.CustomAuth);
			Assert.AreEqual(true, options.UseCustomAuth);
			Assert.AreEqual(false, options.UseCustomModel);
			Assert.AreEqual(null, options.CustomModel);
			Assert.AreEqual(null, options.Glossary);

			qqq.Close();

		}

		#region ChangeOfLanguageOnChangeModel: all parametries filled, auth - delegated, models & glossary - delegated
		[TestMethod]
		public void ChangeOfLanguageOnChangeModel()
		{
			options = new IntentoMTFormOptions()
			{
				ApiKey = "def",
				SmartRouting = false,
				ProviderId = provider_google,
				ProviderName = "abc",
				UseCustomAuth = true,
				CustomAuth = "{'credential_id': 'test1'}".Replace('\'', '"'),
				UseCustomModel = true,
				CustomModel = "model2",
				Glossary = "glossaries/rws_zh_en",
				Format = null,
				ForbidSaveApikey = true,
				FromLanguage = "de",
				ToLanguage = "ru"

			};
			using (form = new IntentoTranslationProviderOptionsForm(options, langPair, Fabric, true))
			{
				testProviderData.delegated_credentials = true;
				form.testListProvidersData = testProvidersData;
				form.testOneProviderData = testProviderData;
				form.testAuthData = testAuthDataGoogle;
				form.testModelData = testModelData;
				form.testGlossaryData = testGlossaryData;
				ApiKeyState apiKeyState = form.apiKeyState = new ApiKeyState(form, options);
				apiKeyState.ReadProviders();
				apiKeyState.EnableDisable();
				var qqq = (IntentoFormOptionsMT)InitializeForm(form.formMT);

				Assert.AreEqual(false, string.IsNullOrWhiteSpace(qqq.comboBoxProviders.Text), "Empty provider");
				Assert.AreEqual(false, qqq.checkBoxSmartRouting.Checked, "SmartRouting.Checked");

				Assert.AreEqual(true, qqq.groupBoxProvider.Enabled, "groupBoxProvider.Enabled");
				Assert.AreEqual(true, qqq.groupBoxBillingAccount.Enabled, "groupBoxBillingAccount.Enabled");
				Assert.AreEqual(true, qqq.comboBoxCredentialId.Visible, "comboBoxCredentialId.Visible");
				Assert.AreEqual("test1", qqq.comboBoxCredentialId.Text, "comboBoxCredentialId.Text");
				Assert.AreEqual(SystemColors.Window, qqq.comboBoxCredentialId.BackColor, "comboBoxCredentialId.BackColor");
				//Assert.AreEqual((string)credential.credential_id, qqq.comboBoxCredentialId.Text);
				Assert.AreEqual(true, qqq.panelConnectAccount.Visible, "panelConnectAccount.Visible");
				Assert.AreEqual(true, qqq.buttonRefresh.Visible, "buttonRefresh.Visible");

				Assert.AreEqual(true, qqq.groupBoxModel.Enabled, "groupBoxModel.Enabled");
				Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Visible, "checkBoxUseCustomModel.Visible");
				Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Enabled, "checkBoxUseCustomModel.Enabled");
				Assert.AreEqual(true, qqq.checkBoxUseCustomModel.Checked, "checkBoxUseCustomModel.Checked");
				Assert.AreEqual(false, qqq.textBoxModel.Visible, "textBoxModel.Visible");
				Assert.AreEqual(true, qqq.comboBoxModels.Visible, "comboBoxModels.Visible");
				Assert.AreEqual("notStock", qqq.comboBoxModels.Text);
				Assert.AreEqual("", qqq.textBoxModel.Text);
				Assert.AreEqual(SystemColors.Window, qqq.textBoxModel.BackColor);

				Assert.AreEqual(true, qqq.groupBoxOptional.Enabled);
				// ru-en из данных модели должно переписать de-ru пришедшие в options
				Assert.AreEqual(true, qqq.comboBoxFrom.Text.Contains("ru"));
				Assert.AreEqual(true, qqq.comboBoxTo.Text.Contains("de"));

				Assert.AreEqual(true, qqq.groupBoxGlossary.Enabled);
				// потому что на запрос глоссариев ничего не возвратилось 
				Assert.AreEqual(false, qqq.textBoxGlossary.Visible);
				Assert.AreEqual(true, qqq.comboBoxGlossaries.Visible);

				Assert.AreEqual("gloss_zh_en", qqq.comboBoxGlossaries.Text);
				Assert.AreEqual("", qqq.textBoxGlossary.Text);
				Assert.AreEqual(SystemColors.Window, qqq.comboBoxGlossaries.BackColor);
				Assert.AreEqual(SystemColors.Window, qqq.textBoxGlossary.BackColor);

				qqq.comboBoxModels.SelectedItem = "Stock";
				qqq.Refresh();
				Assert.AreEqual(true, qqq.comboBoxFrom.Text.Contains("zh"));
				Assert.AreEqual(true, qqq.comboBoxTo.Text.Contains("en"));

				qqq.checkBoxUseCustomModel.Checked = false;
				qqq.comboBoxGlossaries.SelectedItem = "gloss_ru_en";
				qqq.Refresh();
				Assert.AreEqual(true, qqq.comboBoxFrom.Text.Contains("ru"));
				Assert.AreEqual(true, qqq.comboBoxTo.Text.Contains("en"));

				//смотрим как заполнилась основная форма
				form.apiKeyState.EnableDisable();
				form.FillOptions(form.currentOptions);
				form.apiKeyState.EnableDisable();
				form.RefreshFormInfo();
				// form.Visible = true;
				InitializeForm(form);

				Assert.AreEqual(false, form.groupBoxMTConnect.Visible);
				Assert.AreEqual(true, form.groupBoxMTConnect2.Visible);
				Assert.AreEqual(options.ApiKey, form.apiKey_tb.Text);
				Assert.AreEqual(options.ProviderName, form.textBoxProviderName.Text);
				Assert.AreEqual("test1", form.textBoxAccount.Text);
				Assert.AreEqual(Resource.Empty, form.textBoxModel.Text);
				Assert.AreEqual("gloss_ru_en", form.textBoxGlossary.Text);
				Assert.AreEqual(true, form.buttonContinue.Enabled);
				form.formMT.Close();
				qqq.Refresh();
			}

		}
		#endregion ChangeOfLanguageOnChangeModel: all parametries filled, auth - delegated, models & glossary - delegated
	}
}
