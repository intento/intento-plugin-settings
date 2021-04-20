using Intento.MT.Plugin.PropertiesForm.WinForms;
using IntentoSDK;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static Intento.MT.Plugin.PropertiesForm.IntentoMTFormOptions;

namespace Intento.MT.Plugin.PropertiesForm
{
	public partial class IntentoTranslationProviderOptionsForm : Form //, IForm
	{
		public class LangPair
		{
			string _from;
			string _to;

			public LangPair(string from, string to)
			{
				if (from.Contains("-"))
					from = from.Substring(0, from.IndexOf('-'));
				if (to.Contains("-"))
					to = to.Substring(0, to.IndexOf('-'));
				_from = from;
				_to = to;
			}

			public string from { get => _from; set => _from = value; }
			public string to { get => _to; set => _to = value; }
		}

		#region vars
		public IntentoMTFormOptions originalOptions;
		public IntentoMTFormOptions currentOptions;
		public IntentoAiTextTranslate _translate;

		// Languages filter 
		public IList<dynamic> languages;
		private LangPair[] _languagePairs;

		public static DateTime TraceEndTime;

		//private int numberOfFlashes;

		public ApiKeyState apiKeyState;

		public List<string> errors;

		// Fabric to create intento connection. Parameters: apiKey and UserAgent for Settings Form 
		public Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric;

		public string version;

		public IntentoFormOptionsAPI formApi;
		public IntentoFormOptionsMT formMT;
		public IntentoFormAdvanced formAdvanced;
		private int cursorCount = 0;
		public IntentoMTFormOptions locallyOptions;
		public bool settingsIsSet;
		public bool insideEnableDisable = false;


		// Glossary data was obtained directly, without a request to the Intento service
		public List<dynamic> testListProvidersData;
		// Provider data was obtained directly, without a request to the Intento service
		public dynamic testOneProviderData;
		// Model data was obtained directly, without a request to the Intento service
		public IList<dynamic> testModelData;
		// Credentional data was obtained directly, without a request to the Intento service
		public List<dynamic> testAuthData;
		// Glossary data was obtained directly, without a request to the Intento service
		public IList<dynamic> testGlossaryData;

		// flas from app
		public readonly bool isTrados;
		public readonly string appName;
		public readonly bool memoqPublic;

		// Data from registry. For possible logging
		public string savedApiKey;



		#endregion vars

		public IntentoTranslationProviderOptionsForm(
		IntentoMTFormOptions options,
		LangPair[] languagePairs,
		Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric
		)
		{
			// Logs.Write2("Test", "test content");
			bool hidden = options.Hidden;
			var splashForm = new IntentoFormSplash();
			if (!hidden)
				splashForm.Show();

			this.Visible = false;
			this.fabric = fabric;

			InitializeComponent();
			LocalizeContent();


			buttonHelp.Visible = options.сallHelpAction != null;

			Assembly currentAssem = typeof(IntentoTranslationProviderOptionsForm).Assembly;
			version = String.Format("{0}-{1}",
				IntentoHelpers.GetVersion(currentAssem),
				IntentoHelpers.GetGitCommitHash(currentAssem));

			_languagePairs = languagePairs;

			// Determining the parent program that caused the plugin setting
			appName = options.AppName;
			isTrados = appName == "SdlTradosStudioPlugin";
			memoqPublic = !isTrados && !appName.EndsWith("Private");


			locallyOptions = GetOptionsSavedLocally();
			originalOptions = options;
			if (locallyOptions != null && string.IsNullOrEmpty(options.ApiKey))
				if (!string.IsNullOrEmpty(locallyOptions.ApiKey))
				{
					originalOptions.ApiKey = locallyOptions.ApiKey;
					originalOptions.SmartRouting = locallyOptions.SmartRouting;
					originalOptions.Routing = locallyOptions.Routing;
					originalOptions.ProviderId = locallyOptions.ProviderId;
					originalOptions.CustomAuth = locallyOptions.CustomAuth;
					originalOptions.CustomModel = locallyOptions.CustomModel;
					originalOptions.UseCustomModel = locallyOptions.UseCustomModel;
					originalOptions.UseCustomAuth = locallyOptions.UseCustomAuth;
					originalOptions.Glossary = locallyOptions.Glossary;
					originalOptions.SaveLocally = locallyOptions.SaveLocally;
				}

			currentOptions = originalOptions.Duplicate();

			//smart routing by default
			if (string.IsNullOrEmpty(currentOptions.ProviderId))
			{
				currentOptions.SmartRouting = true;
				if (string.IsNullOrEmpty(currentOptions.Routing))
				{
					currentOptions.Routing = "best";
					currentOptions.RoutingDisplayName = Resource.BestSmartRouteDescription;
				}
			}

			TraceEndTime = originalOptions.TraceEndTime;
			formAdvanced = new IntentoFormAdvanced(this);
			formApi = new IntentoFormOptionsAPI(this);
			formMT = new IntentoFormOptionsMT(this);
			apiKeyState = new ApiKeyState(this, currentOptions);
			if (GetValueFromRegistry("ProxyEnabled") != null && GetValueFromRegistry("ProxyEnabled") == "1")
			{
				currentOptions.proxySettings = new ProxySettings()
				{
					ProxyAddress = GetValueFromRegistry("ProxyAddress"),
					ProxyPort = GetValueFromRegistry("ProxyPort"),
					ProxyUserName = GetValueFromRegistry("ProxyUserName"),
					ProxyPassword = GetValueFromRegistry("ProxyPassw"),
					ProxyEnabled = true
				};
			}

			DialogResult = DialogResult.None;
			var arr = originalOptions.Signature.Split('/');
			formAdvanced.toolStripStatusLabel1.Text = arr.Count() == 3 ? String.Format("{0}/{1}", arr[0], arr[2]) : originalOptions.Signature;
			groupBoxMTConnect2.Location = groupBoxMTConnect.Location;

			if (!string.IsNullOrWhiteSpace(apiKeyState.apiKey))
			{
				apiKeyState.ReadProvidersAndRouting();
			}
			if (apiKeyState.IsOK)
			{
				buttonMTSetting.Select();
				apiKeyState.EnableDisable();
				FillOptions(currentOptions);
			}
			else
				buttonSetApi.Select();

			apiKeyState.EnableDisable();
			RefreshFormInfo();
			if (!hidden)
			{
				splashForm.Close();
				this.Visible = true;
			}
			string txt = string.Format(
				@"IntentoTranslationProviderOptionsForm ctor
				ProviderName:{0}
				ProviderId:{1}
				FromLanguage:{2}
				ToLanguage:{3}
				SmartRouting:{4}
				UseCustomAuth:{5}
				CustomAuth:{6}
				AuthDelegatedCredentialId:{7}
				UseCustomModel:{8}
				CustomModel:{9}
				CustomModelName:{10}
				Glossary:{11}
				GlossaryName:{12}
				CustomTagParser:{13}
				SaveLocally:{14}
				Version:{15}
				UserAgent:{16}
				Hidden:{17}",
				currentOptions.ProviderId,
				currentOptions.ProviderName,
				currentOptions.FromLanguage,
				currentOptions.ToLanguage,
				currentOptions.SmartRouting,
				currentOptions.UseCustomAuth,
				currentOptions.CustomAuth,
				currentOptions.AuthDelegatedCredentialId,
				currentOptions.UseCustomModel,
				currentOptions.CustomModel,
				currentOptions.CustomModelName,
				currentOptions.Glossary,
				currentOptions.GlossaryName,
				currentOptions.CustomTagParser,
				currentOptions.SaveLocally,
				currentOptions.UserAgent,
				version,
				hidden
				);
			Logs.Write('F', txt);
		}

		public IntentoTranslationProviderOptionsForm(
			IntentoMTFormOptions options,
			LangPair[] languagePairs,
			Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric,
			bool testings
			)
		{
			this.fabric = fabric;

			InitializeComponent();
			LocalizeContent();

			originalOptions = options;
			currentOptions = originalOptions.Duplicate();

			formAdvanced = new IntentoFormAdvanced(this);
			formApi = new IntentoFormOptionsAPI(this);
			formMT = new IntentoFormOptionsMT(this);

			_languagePairs = languagePairs;
			DialogResult = DialogResult.None;

		}

		public IntentoMTFormOptions GetOptions()
		{
			return currentOptions;
		}

		public LangPair[] LanguagePairs
		{ get { return _languagePairs; } }

		public static bool IsTrace(string pluginID = null)
		{
			RegistryKey key;
			if (pluginID != null)
				key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento\\{0}", pluginID));
			else
				key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento", pluginID));

			string loggingReg = (string)key.GetValue("Logging", null);
			if (loggingReg != null)
			{
				loggingReg = loggingReg.ToLower();
				if (loggingReg == "1" || loggingReg == "true")
					return true;
			}

			string loggingEnv = Environment.GetEnvironmentVariable("intento_plugin_logging");
			if (loggingEnv != null)
			{
				loggingEnv = loggingEnv.ToLower();
				if (loggingEnv == "1" || loggingEnv == "true")
					return true;
			}

			return (TraceEndTime - DateTime.Now).Minutes > 0;
		}

		#region Work with local registry
		public IntentoMTFormOptions GetOptionsSavedLocally()
		{
			if (memoqPublic)
				return null;
			else
			{
				IntentoMTFormOptions ret = new IntentoMTFormOptions();
				ret = new IntentoMTFormOptions();
				savedApiKey = GetValueFromRegistry("ApiKey");
				ret.SaveLocally = GetValueFromRegistry("SaveLocally") != null && GetValueFromRegistry("SaveLocally") == "1";
				string path = GetRegistryPath();
				if (ret.SaveLocally)
				{
					ret.ApiKey = GetValueFromRegistry("ApiKey", path);
					if (!string.IsNullOrWhiteSpace(ret.ApiKey))
					{
						ret.SmartRouting = GetValueFromRegistry("SmartRouting", path) != null
							&& GetValueFromRegistry("SmartRouting", path) == "1";
						ret.Routing = GetValueFromRegistry("Routing", path);
						ret.ProviderId = GetValueFromRegistry("ProviderId", path);
						ret.CustomAuth = GetValueFromRegistry("CustomAuth", path);
						ret.CustomModel = GetValueFromRegistry("CustomModel", path);
						ret.Glossary = GetValueFromRegistry("Glossary", path);
						ret.UseCustomModel = GetValueFromRegistry("UseCustomModel", path) != null
							&& GetValueFromRegistry("UseCustomModel", path) == "1";
						ret.UseCustomAuth = GetValueFromRegistry("UseCustomAuth", path) != null
							&& GetValueFromRegistry("UseCustomAuth", path) == "1";
					}
				}
				return ret;
			}
		}

		public void SaveOptionsToRegistry(IntentoMTFormOptions options)
		{
			string path = GetRegistryPath();
			SaveValueToRegistry("ApiKey", options.ApiKey); // for logging
			SaveValueToRegistry("ApiKey", options.ApiKey, path);
			SaveValueToRegistry("SmartRouting", options.SmartRouting, path);
			SaveValueToRegistry("Routing", options.Routing, path);
			SaveValueToRegistry("ProviderId", options.ProviderId, path);
			SaveValueToRegistry("CustomAuth", options.CustomAuth, path);
			SaveValueToRegistry("CustomModel", options.CustomModel, path);
			SaveValueToRegistry("Glossary", options.Glossary, path);
			SaveValueToRegistry("UseCustomModel", options.UseCustomModel, path);
			SaveValueToRegistry("UseCustomAuth", options.UseCustomAuth, path);
		}

		public string GetRegistryPath()
		{
			string path = string.Empty;
			if (_languagePairs == null || _languagePairs.Count() != 1)
				path = "all";
			else
				path = string.Format("{0}-{1}", _languagePairs[0].from, _languagePairs[0].to);
			return "\\" + path;
		}


		public string GetValueFromRegistry(string name, string path = "")
		{
			try
			{
				RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento\\{0}{1}", appName, path));
				return (string)key.GetValue(name, null);
			}
			catch { }
			return null;
		}
		public void SaveValueToRegistry(string name, bool value, string path = "")
		{
			SaveValueToRegistry(name, value ? "1" : "0", path);
		}
		public void SaveValueToRegistry(string name, string value, string path = "")
		{
			try
			{
				if (value == null)
					value = string.Empty;
				RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format("Software\\Intento\\{0}{1}", appName, path));
				key.SetValue(name, value);
			}
			catch { }
		}
		#endregion Work with local registry

		private string customAuthJsonToString(string authJsonString)
		{
			string ret = String.Empty;
			if (IsValidJson(authJsonString) == null)
			{
				var js = JToken.Parse(authJsonString);
				foreach (dynamic val in js)
					ret += String.Format("{0}:{1} ", val.Name, val.Value);
			}
			return ret;
		}

		private string IsValidJson(string strInput)
		{
			strInput = String.IsNullOrEmpty(strInput) ? String.Empty : strInput.Trim();
			string result = "Invalid json for own authorization parameters";
			if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
				(strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
			{
				try
				{
					var obj = JToken.Parse(strInput);
					if (obj.HasValues)
						result = null;
				}
				catch { }
			}
			return result;
		}

		private bool filterBy(dynamic x, string lang)
		{
			if (x == null) return true;
			if (x.GetType().Name == "JArray")
				return ((JArray)x).Any(q => (string)q == lang);
			if (x.GetType().Name == "JValue")
				return (string)x == lang;
			return true;
		}

		#region events

		public void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
		{
			using (new CursorFormMT(formMT))
			{
				if (apiKeyState != null && apiKeyState.smartRoutingState != null && apiKeyState.smartRoutingState.providerState != null)
					// Can happen during loading data from Options - constructor of ProviderState change settings in a list of providers
					apiKeyState.smartRoutingState.providerState.SelectedIndexChanged();
			}
		}

		public void checkBoxUseCustomModel_CheckedChanged(object sender, EventArgs e)
		{
			using (new CursorFormMT(formMT))
			{
				apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetModelState()?.checkBoxUseCustomModel_CheckedChanged();
				ModelState.internalControlChange = false;
			}
		}

		private void buttonContinue_Click(object sender, EventArgs e)
		{
			using (new CursorForm(this))
			{
				SaveValueToRegistry("ProxyEnabled", false);
				if (currentOptions.proxySettings != null)
				{
					if (currentOptions.proxySettings.ProxyEnabled)
					{
						SaveValueToRegistry("ProxyAddress", currentOptions.proxySettings.ProxyAddress);
						SaveValueToRegistry("ProxyPort", currentOptions.proxySettings.ProxyPort);
						SaveValueToRegistry("ProxyUserName", currentOptions.proxySettings.ProxyUserName);
						SaveValueToRegistry("ProxyPassw", currentOptions.proxySettings.ProxyPassword);
						SaveValueToRegistry("ProxyEnabled", true);
					}
				}
				currentOptions.Fill(originalOptions);
				originalOptions.Translate = _translate;

				if (!currentOptions.ForbidSaveApikey)
				{
					if (!string.IsNullOrEmpty(originalOptions.ApiKey))
						SaveValueToRegistry("ApiKey", originalOptions.ApiKey);
				}

				SaveValueToRegistry("SaveLocally", originalOptions.SaveLocally);
				if (originalOptions.SaveLocally)
					SaveOptionsToRegistry(originalOptions);

				this.DialogResult = DialogResult.OK;
				Close();
			}
		}

		public void linkLabel_LinkClicked(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(((Control)sender).Tag.ToString());
		}

		//private void linkLabel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		//{
		//    System.Windows.Forms.LinkLabel control = (System.Windows.Forms.LinkLabel)sender;
		//    SizeF stringSize = e.Graphics.MeasureString(control.Text, control.Font);
		//    control.Font = new Font(FontFamily.GenericSansSerif, control.Font.Size);
		//}

		private void buttonHelp_Click(object sender, EventArgs e)
		{
			GetOptions().сallHelpAction?.Invoke();
		}

		public void modelControls_ValueChanged(object sender, EventArgs e)
		{
			using (new CursorFormMT(formMT))
			{
				apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetModelState()?.modelControls_ValueChanged();
			}
		}

		public void comboBoxCredentialId_SelectedIndexChanged(object sender, EventArgs e)
		{
			using (new CursorFormMT(formMT))
				apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.comboBoxCredentialId_SelectedIndexChanged();
		}

		public void checkBoxSmartRouting_CheckedChanged(object sender, EventArgs e)
		{
			using (new CursorFormMT(formMT))
				if (apiKeyState != null)
					apiKeyState?.smartRoutingState?.CheckedChanged();
		}


		public void glossaryControls_ValueChanged(object sender, EventArgs e)
		{
			using (new CursorFormMT(formMT))
				apiKeyState?.smartRoutingState?.providerState?.GetAuthState()?.GetGlossaryState()?.glossaryControls_ValueChanged();
		}

		private void buttonSetApi_Click(object sender, EventArgs e)
		{
			settingsIsSet = false;
			string apiKey = apiKeyState?.apiKey;
			formApi.currentOptions = currentOptions;
			formApi.ShowDialog();
			if (formApi.DialogResult == DialogResult.OK && apiKeyState.IsOK && apiKeyState.apiKey != apiKey)
			{
				settingsIsSet = true;
				currentOptions.ApiKey = apiKeyState.apiKey;
				RefreshFormInfo();
			}
		}

		private void buttonAdvanced_Click(object sender, EventArgs e)
		{
			formAdvanced.ShowDialog();
		}

		private void buttonMTSetting_Click(object sender, EventArgs e)
		{
			var smartRoutingState = apiKeyState.smartRoutingState;
			var bufferOptions = currentOptions.Duplicate();
			formMT.ShowDialog();
			using (new CursorForm(this))
			{
				if (formMT.DialogResult == DialogResult.OK)
				{
					FillOptions(currentOptions);
					settingsIsSet = false;
					RefreshFormInfo();
				}
				else
				{
					apiKeyState.smartRoutingState = smartRoutingState;
					currentOptions = bufferOptions;
				}
			}
		}

		#endregion events

		private void LocalizeContent()
		{
			groupBoxBillingAccount.Text = Resource.BillingAccount;
			groupBoxModel.Text = Resource.Model;
			groupBoxGlossary.Text = Resource.Glossary;
			buttonContinue.Text = Resource.MFbuttonClose;
			buttonMTSetting.Text = Resource.MFbuttonMTSetting;
			buttonSetApi.Text = Resource.EnterIntentoAPIKey;
			Text = Resource.MFcaption;
			groupBoxMTSettings.Text = Resource.MFgroupBoxMTSettings;
			labelRegister1.Text = Resource.MFlabelRegister1;
			labelRegister2.Text = Resource.MFlabelRegister2;
			buttonCheck.Text = Resource.MFbuttonCheck;
			labelIAK.Text = Resource.APIFlabelAPI;
			buttonAdvanced.Text = Resource.MFbuttonAdvanced;
			Icon = Resource.intento;
			labelApiKeyIsChanged.Text = Resource.MFlabelApiKeyIsChanged;
		}

		public class CursorForm : IDisposable
		{
			IntentoTranslationProviderOptionsForm form;
			public CursorForm(IntentoTranslationProviderOptionsForm form)
			{
				this.form = form;
				if (form.cursorCount == 0)
					form.Cursor = Cursors.WaitCursor;
				form.cursorCount++;
			}

			public void Dispose()
			{
				form.cursorCount--;
				if (form.cursorCount == 0)
					form.Cursor = Cursors.Default;
			}
		}

		public class CursorFormMT : IDisposable
		{
			IntentoFormOptionsMT form;
			public CursorFormMT(IntentoFormOptionsMT form)
			{
				this.form = form;
				if (form.cursorCountMT == 0)
					form.Cursor = Cursors.WaitCursor;
				form.cursorCountMT++;
			}

			public void Dispose()
			{
				form.cursorCountMT--;
				if (form.cursorCountMT == 0)
					form.Cursor = Cursors.Default;
			}
		}

		public void FillOptions(IntentoMTFormOptions options)
		{
			options.ForbidSaveApikey = currentOptions.ForbidSaveApikey;
			options.HideHiddenTextButton = currentOptions.HideHiddenTextButton;
			options.CustomSettingsName = currentOptions.CustomSettingsName;
			options.CustomTagParser = currentOptions.CustomTagParser;
			options.CutTag = currentOptions.CutTag;
			options.SaveLocally = currentOptions.SaveLocally;

			apiKeyState.FillOptions(options);
		}

		public void RefreshFormInfo()
		{
			SmartRoutingState smartRoutingState = apiKeyState?.smartRoutingState;
			buttonContinue.Enabled = false;
			labelApiKeyIsChanged.Visible = false;
			IntentoMTFormOptions tmpOptions = new IntentoMTFormOptions();
			apiKeyState.FillOptions(tmpOptions);
			if (smartRoutingState != null && smartRoutingState.SmartRouting)
			{
				textBoxAccount.UseSystemPasswordChar = false;
				textBoxProviderName.Text = String.Format(Resource.MFSmartRoutingText, smartRoutingState.routingDescription);
				textBoxAccount.Text = Resource.MFNa;
				textBoxModel.Text = Resource.MFNa;
				textBoxGlossary.Text = Resource.MFNa;
				buttonContinue.Enabled = true;
				if (apiKeyState.IsOK)
					apiKey_tb.Text = apiKeyState.apiKey;
			}
			else
			{
				if (apiKeyState.IsOK)
				{
					apiKey_tb.Text = apiKeyState.apiKey;
					if (String.IsNullOrEmpty(tmpOptions.ProviderId))
						textBoxProviderName.Text = Resource.NeedAChoise;
					else
					{
						textBoxProviderName.Text = tmpOptions.ProviderName;
						buttonContinue.Enabled = true;
					}
				}
				else
					textBoxProviderName.Text = Resource.MFNa;

				if (tmpOptions.AuthMode == StateModeEnum.prohibited || tmpOptions.AuthMode == StateModeEnum.unknown)
				{
					textBoxAccount.UseSystemPasswordChar = false;
					textBoxAccount.Text = Resource.MFNa;
				}
				else if (String.IsNullOrEmpty(tmpOptions.CustomAuth))
				{
					textBoxAccount.UseSystemPasswordChar = false;
					textBoxAccount.Text = Resource.Empty;
				}
				else
				{
					textBoxAccount.UseSystemPasswordChar = !tmpOptions.IsAuthDelegated;
					textBoxAccount.Text = tmpOptions.IsAuthDelegated ? tmpOptions.AuthDelegatedCredentialId : tmpOptions.CustomAuth;
					labelApiKeyIsChanged.Visible = settingsIsSet;
				}

				if (tmpOptions.CustomModelMode == StateModeEnum.prohibited || tmpOptions.CustomModelMode == StateModeEnum.unknown)
					textBoxModel.Text = Resource.MFNa;
				else if (tmpOptions.CustomModelMode == StateModeEnum.optional && !tmpOptions.UseCustomModel)
					textBoxModel.Text = Resource.Empty;
				else
				{
					textBoxModel.Text = tmpOptions.CustomModelName;
					labelApiKeyIsChanged.Visible = settingsIsSet;
				}
				if (tmpOptions.GlossaryMode == StateModeEnum.prohibited || tmpOptions.GlossaryMode == StateModeEnum.unknown)
					textBoxGlossary.Text = Resource.MFNa;
				else
				{
					textBoxGlossary.Text = String.IsNullOrEmpty(tmpOptions.GlossaryName) ? Resource.Empty : tmpOptions.GlossaryName;
					labelApiKeyIsChanged.Visible = settingsIsSet;
				}
			}

		}

		private void IntentoTranslationProviderOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			originalOptions.TraceEndTime = TraceEndTime;
		}
	}

}
