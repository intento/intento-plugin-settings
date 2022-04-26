using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Intento.MT.Plugin.PropertiesForm.Models;
using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.MT.Plugin.PropertiesForm.States;
using Intento.SDK;
using Intento.SDK.DI;
using Intento.SDK.Settings;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Intento.MT.Plugin.PropertiesForm.IntentoMTFormOptions;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    public partial class IntentoTranslationProviderOptionsForm : Form
    {
        #region vars

        public IntentoMTFormOptions OriginalOptions { get; }
        public IntentoMTFormOptions CurrentOptions { get; set; }

        public ILocatorImpl Locator { get; set; }

        public ApiKeyState ApiKeyState { get; }

        public List<string> Errors { get; set; }

        public string Version { get; }

        public IntentoFormOptionsAPI FormApi { get; }

        public IntentoFormOptionsMT FormMt { get; }
        public IntentoFormAdvanced FormAdvanced { get; }

        private int cursorCount;
        private bool settingsIsSet;
        public bool InsideEnableDisable { get; set; }
        
        private IRemoteLogService RemoteLogService => this.Locator?.Resolve<IRemoteLogService>();

        // flags from app
        public bool IsTrados { get; }
        
        private readonly string appName;
        public bool MemoqPublic { get; }

        #endregion vars

        public IntentoTranslationProviderOptionsForm(
            IntentoMTFormOptions options,
            LangPair[] languagePairs,
            ILocatorImpl locator)
        {
            Locator = locator;
            var hidden = options.Hidden;
            var splashForm = new IntentoFormSplash();
            if (!hidden)
            {
                splashForm.Show();
            }
            Visible = false;
            InitializeComponent();
            LocalizeContent();

            if (!string.IsNullOrWhiteSpace(options.ConsoleUrl))
            {
                linkLabel1.Tag = options.ConsoleUrl;
                linkLabel1.Text = options.ConsoleUrl;
            }

            buttonHelp.Visible = options.СallHelpAction != null;

            var currentAssem = typeof(IntentoTranslationProviderOptionsForm).Assembly;
            Version = $"{IntentoHelpers.GetVersion(currentAssem)}";

            LanguagePairs = languagePairs;
            
             // Determining the parent program that caused the plugin setting
            appName = options.AppName;
            IsTrados = appName == "SdlTradosStudioPlugin";
            MemoqPublic = !IsTrados && !appName.EndsWith("Private");


            var locallyOptions = GetOptionsSavedLocally();
            OriginalOptions = options;
            if (locallyOptions != null && string.IsNullOrEmpty(options.ApiKey))
                if (!string.IsNullOrEmpty(locallyOptions.ApiKey))
                {
                    OriginalOptions.ApiKey = locallyOptions.ApiKey;
                    OriginalOptions.SmartRouting = locallyOptions.SmartRouting;
                    OriginalOptions.Routing = locallyOptions.Routing;
                    OriginalOptions.RoutingDisplayName = locallyOptions.RoutingDisplayName;
                    OriginalOptions.ProviderId = locallyOptions.ProviderId;
                    OriginalOptions.CustomAuth = locallyOptions.CustomAuth;
                    OriginalOptions.CustomModel = locallyOptions.CustomModel;
                    OriginalOptions.UseCustomModel = locallyOptions.UseCustomModel;
                    OriginalOptions.UseCustomAuth = locallyOptions.UseCustomAuth;
                    OriginalOptions.Glossary = locallyOptions.Glossary;
                    OriginalOptions.IntentoGlossaries = locallyOptions.IntentoGlossaries;
                    OriginalOptions.SaveLocally = locallyOptions.SaveLocally;
                }

            CurrentOptions = OriginalOptions.Duplicate();

            //smart routing by default
            if (string.IsNullOrEmpty(CurrentOptions.ProviderId))
            {
                CurrentOptions.SmartRouting = true;
                if (string.IsNullOrEmpty(CurrentOptions.Routing))
                {
                    CurrentOptions.Routing = "best";
                    CurrentOptions.RoutingDisplayName = Resource.BestSmartRouteDescription;
                }
            }

            RemoteLogService.SetTraceEndTime(OriginalOptions.TraceEndTime);
            FormAdvanced = new IntentoFormAdvanced(this);
            FormApi = new IntentoFormOptionsAPI(this);
            FormMt = new IntentoFormOptionsMT(this);
            ApiKeyState = new ApiKeyState(this, CurrentOptions);
            if (string.IsNullOrEmpty(options.ApiKey) && !string.IsNullOrEmpty(ApiKeyState.ApiKey))
            {
                options.ApiKey = ApiKeyState.ApiKey;
            }

            if (GetValueFromRegistry("ProxyEnabled") != null && GetValueFromRegistry("ProxyEnabled") == "1")
            {
                CurrentOptions.ProxySettings = new ProxySettings()
                {
                    ProxyAddress = GetValueFromRegistry("ProxyAddress"),
                    ProxyPort = GetValueFromRegistry("ProxyPort"),
                    ProxyUserName = GetValueFromRegistry("ProxyUserName"),
                    ProxyPassword = GetValueFromRegistry("ProxyPassw"),
                    ProxyEnabled = true
                };
            }

            DialogResult = DialogResult.None;
            var arr = OriginalOptions.Signature.Split('/');
            FormAdvanced.toolStripStatusLabel1.Text =
                arr.Count() == 3 ? $"{arr[0]}/{arr[2]}" : OriginalOptions.Signature;

            if (!string.IsNullOrWhiteSpace(ApiKeyState.ApiKey))
            {
                ApiKeyState.ReadProvidersAndRouting();
            }

            if (ApiKeyState.IsOk)
            {
                buttonMTSetting.Select();
                ApiKeyState.EnableDisable();
                FillOptions(CurrentOptions);
            }
            else
                buttonSetApi.Select();

            ApiKeyState.EnableDisable();
            RefreshFormInfo();
            if (!hidden)
            {
                splashForm.Close();
                Visible = true;
            }

            var txt = $@"IntentoTranslationProviderOptionsForm ctor
				ProviderName:{CurrentOptions.ProviderId}
				ProviderId:{CurrentOptions.ProviderName}
				FromLanguage:{CurrentOptions.FromLanguage}
				ToLanguage:{CurrentOptions.ToLanguage}
				SmartRouting:{CurrentOptions.SmartRouting}
				UseCustomAuth:{CurrentOptions.UseCustomAuth}
				CustomAuth:{CurrentOptions.CustomAuth}
				AuthDelegatedCredentialId:{CurrentOptions.AuthDelegatedCredentialId}
				UseCustomModel:{CurrentOptions.UseCustomModel}
				CustomModel:{CurrentOptions.CustomModel}
				CustomModelName:{CurrentOptions.CustomModelName}
				Glossary:{CurrentOptions.Glossary}
				GlossaryName:{CurrentOptions.GlossaryName}
				CustomTagParser:{CurrentOptions.CustomTagParser}
				SaveLocally:{CurrentOptions.SaveLocally}
				Version:{CurrentOptions.UserAgent}
				UserAgent:{Version}
				Hidden:{hidden}";
            RemoteLogService.Write('F', txt);
        }

        public IntentoTranslationProviderOptionsForm(
            IntentoMTFormOptions options,
            LangPair[] languagePairs,
            bool insideEnableDisable,
            ILocatorImpl locator) : this(options, languagePairs, locator)
        {
            InsideEnableDisable = insideEnableDisable;
        }
        
        public IntentoMTFormOptions GetOptions()
        {
            return CurrentOptions;
        }

        public LangPair[] LanguagePairs { get; }

        #region Work with local registry

        private IntentoMTFormOptions GetOptionsSavedLocally()
        {
            if (MemoqPublic)
                return null;
            var ret = new IntentoMTFormOptions();
            GetValueFromRegistry("ApiKey");
            ret.SaveLocally = GetValueFromRegistry("SaveLocally") != null && GetValueFromRegistry("SaveLocally") == "1";
            var path = GetRegistryPath();
            if (ret.SaveLocally)
            {
                ret.ApiKey = GetValueFromRegistry("ApiKey", path);
                if (!string.IsNullOrWhiteSpace(ret.ApiKey))
                {
                    ret.SmartRouting = GetValueFromRegistry("SmartRouting", path) != null
                                       && GetValueFromRegistry("SmartRouting", path) == "1";
                    ret.Routing = GetValueFromRegistry("Routing", path);
                    ret.RoutingDisplayName = GetValueFromRegistry("RoutingDisplayName", path);
                    ret.ProviderId = GetValueFromRegistry("ProviderId", path);
                    ret.CustomAuth = GetValueFromRegistry("CustomAuth", path);
                    ret.CustomModel = GetValueFromRegistry("CustomModel", path);
                    ret.Glossary = GetValueFromRegistry("Glossary", path);
                    ret.UseCustomModel = GetValueFromRegistry("UseCustomModel", path) != null
                                         && GetValueFromRegistry("UseCustomModel", path) == "1";
                    ret.UseCustomAuth = GetValueFromRegistry("UseCustomAuth", path) != null
                                        && GetValueFromRegistry("UseCustomAuth", path) == "1";
                    var glossariesVal = GetValueFromRegistry("IntentoGlossaries", path);
                    if (glossariesVal != null)
                    {
                        try
                        {
                            var glossariesArray = JArray.Parse(glossariesVal);
                            // ReSharper disable once SuspiciousTypeConversion.Global
                            ret.IntentoGlossaries = glossariesArray.Cast<int>().ToArray();
                        }
                        catch (Exception e)
                        {
                            RemoteLogService.Write('F', e.Message, ex: e);
                        }
                    }
                }
            }

            return ret;
        }

        private void SaveOptionsToRegistry(IntentoMTFormOptions options)
        {
            var path = GetRegistryPath();
            SaveValueToRegistry("ApiKey", options.ApiKey); // for logging
            SaveValueToRegistry("ApiKey", options.ApiKey, path);
            SaveValueToRegistry("SmartRouting", options.SmartRouting, path);
            SaveValueToRegistry("Routing", options.Routing, path);
            SaveValueToRegistry("RoutingDisplayName", options.RoutingDisplayName, path);
            SaveValueToRegistry("ProviderId", options.ProviderId, path);
            SaveValueToRegistry("CustomAuth", options.CustomAuth, path);
            SaveValueToRegistry("CustomModel", options.CustomModel, path);
            SaveValueToRegistry("Glossary", options.Glossary, path);
            SaveValueToRegistry("UseCustomModel", options.UseCustomModel, path);
            SaveValueToRegistry("UseCustomAuth", options.UseCustomAuth, path);
            if (options.IntentoGlossaries != null)
            {
                SaveValueToRegistry("IntentoGlossaries", JsonConvert.SerializeObject(options.IntentoGlossaries));
            }
        }

        private string GetRegistryPath()
        {
            string path;
            if (LanguagePairs == null || LanguagePairs.Count() != 1)
                path = "all";
            else
                path = $"{LanguagePairs[0].From}-{LanguagePairs[0].To}";
            return "\\" + path;
        }


        public string GetValueFromRegistry(string name, string path = "")
        {
            try
            {
                var key = Registry.CurrentUser.CreateSubKey($"Software\\Intento\\{appName}{path}");
                if (key != null)
                {
                    return (string)key.GetValue(name, null);
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        private void SaveValueToRegistry(string name, bool value, string path = "")
        {
            SaveValueToRegistry(name, value ? "1" : "0", path);
        }

        private void SaveValueToRegistry(string name, string value, string path = "")
        {
            try
            {
                if (value == null)
                {
                    value = string.Empty;
                }

                var key = Registry.CurrentUser.CreateSubKey($"Software\\Intento\\{appName}{path}");
                if (key != null)
                {
                    key.SetValue(name, value);
                }
            }
            catch
            {
                // ignored
            }
        }

        #endregion Work with local registry

        #region events

        public void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(FormMt))
            {
                if (ApiKeyState is { SmartRoutingState: { ProviderState: { } } })
                    // Can happen during loading data from Options - constructor of ProviderState change settings in a list of providers
                    ApiKeyState.SmartRoutingState.ProviderState.SelectedIndexChanged();
            }
        }

        public void checkBoxUseCustomModel_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(FormMt))
            {
                ApiKeyState?.SmartRoutingState?.ProviderState?.GetAuthState()?.GetModelState()
                    ?.checkBoxUseCustomModel_CheckedChanged();
                ModelState.InternalControlChange = false;
            }
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            using (new CursorForm(this))
            {
                SaveValueToRegistry("ProxyEnabled", false);
                if (CurrentOptions.ProxySettings != null)
                {
                    if (CurrentOptions.ProxySettings.ProxyEnabled)
                    {
                        SaveValueToRegistry("ProxyAddress", CurrentOptions.ProxySettings.ProxyAddress);
                        SaveValueToRegistry("ProxyPort", CurrentOptions.ProxySettings.ProxyPort);
                        SaveValueToRegistry("ProxyUserName", CurrentOptions.ProxySettings.ProxyUserName);
                        SaveValueToRegistry("ProxyPassw", CurrentOptions.ProxySettings.ProxyPassword);
                        SaveValueToRegistry("ProxyEnabled", true);
                    }
                }

                CurrentOptions.Fill(OriginalOptions);

                if (!CurrentOptions.ForbidSaveApikey)
                {
                    SaveValueToRegistry("ApiKey", OriginalOptions.ApiKey);
                }

                SaveValueToRegistry("SaveLocally", OriginalOptions.SaveLocally);
                if (OriginalOptions.SaveLocally)
                    SaveOptionsToRegistry(OriginalOptions);

                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        public void linkLabel_LinkClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(((Control)sender).Tag.ToString());
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            GetOptions().СallHelpAction?.Invoke();
        }

        public void modelControls_ValueChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(FormMt))
            {
                ApiKeyState?.SmartRoutingState?.ProviderState?.GetAuthState()?.GetModelState()
                    ?.modelControls_ValueChanged();
            }
        }

        public void comboBoxCredentialId_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(FormMt))
                ApiKeyState?.SmartRoutingState?.ProviderState?.GetAuthState()
                    ?.comboBoxCredentialId_SelectedIndexChanged();
        }

        public void checkBoxSmartRouting_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(FormMt))
                if (ApiKeyState != null)
                    ApiKeyState?.SmartRoutingState?.CheckedChanged();
        }

        public void glossaryControls_ValueChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(FormMt))
                ApiKeyState?.SmartRoutingState?.ProviderState?.GetAuthState()?.GetGlossaryState()
                    ?.glossaryControls_ValueChanged();
        }

        public void agnosticGlossaryControls_ValueChanged(object sender, EventArgs e)
        {
            using (new CursorFormMT(FormMt))
            {
                ApiKeyState?.SmartRoutingState?.ProviderState?.GetAuthState()?.GetProviderGlossaryState()
                    ?.glossaryControls_ValueChanged();
            }
        }

        private void buttonSetApi_Click(object sender, EventArgs e)
        {
            settingsIsSet = false;
            var apiKey = ApiKeyState?.ApiKey;
            FormApi.CurrentOptions = CurrentOptions;
            FormApi.ShowDialog();
            if (ApiKeyState != null && FormApi.DialogResult == DialogResult.OK && (ApiKeyState.IsOk || string.IsNullOrWhiteSpace(ApiKeyState.ApiKey)) &&
                ApiKeyState.ApiKey != apiKey)
            {
                settingsIsSet = true;
                CurrentOptions.ApiKey = ApiKeyState.ApiKey;
                RefreshFormInfo();
            }
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {
            FormAdvanced.ShowDialog();
        }

        private void buttonMTSetting_Click(object sender, EventArgs e)
        {
            var smartRoutingState = ApiKeyState.SmartRoutingState;
            var bufferOptions = CurrentOptions.Duplicate();
            FormMt.ShowDialog();
            using (new CursorForm(this))
            {
                if (FormMt.DialogResult == DialogResult.OK)
                {
                    FillOptions(CurrentOptions);
                    settingsIsSet = false;
                    RefreshFormInfo();
                }
                else
                {
                    ApiKeyState.SmartRoutingState = smartRoutingState;
                    CurrentOptions = bufferOptions;
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

        /// <inheritdoc />
        // ReSharper disable once InconsistentNaming
        public class CursorFormMT : IDisposable
        {
            private readonly IntentoFormOptionsMT form;

            public CursorFormMT(IntentoFormOptionsMT form)
            {
                this.form = form;
                if (form.CursorCountMt == 0)
                    form.Cursor = Cursors.WaitCursor;
                form.CursorCountMt++;
            }

            public void Dispose()
            {
                form.CursorCountMt--;
                if (form.CursorCountMt == 0)
                    form.Cursor = Cursors.Default;
            }
        }

        private void FillOptions(IntentoMTFormOptions options)
        {
            options.ForbidSaveApikey = CurrentOptions.ForbidSaveApikey;
            options.HideHiddenTextButton = CurrentOptions.HideHiddenTextButton;
            options.CustomSettingsName = CurrentOptions.CustomSettingsName;
            options.CustomTagParser = CurrentOptions.CustomTagParser;
            options.CutTag = CurrentOptions.CutTag;
            options.SaveLocally = CurrentOptions.SaveLocally;

            ApiKeyState.FillOptions(options);
        }

        private void RefreshFormInfo()
        {
            var smartRoutingState = ApiKeyState?.SmartRoutingState;
            buttonContinue.Enabled = false;
            labelApiKeyIsChanged.Visible = false;
            var tmpOptions = new IntentoMTFormOptions();
            if (ApiKeyState == null)
            {
                return;
            }

            ApiKeyState.FillOptions(tmpOptions);
            if (smartRoutingState is { SmartRouting: true })
            {
                textBoxAccount.UseSystemPasswordChar = false;
                textBoxProviderName.Text =
                    string.Format(Resource.MFSmartRoutingText, smartRoutingState.RoutingDescription);
                textBoxAccount.Text = Resource.MFNa;
                textBoxModel.Text = Resource.MFNa;
                textBoxGlossary.Text = Resource.MFNa;
                buttonContinue.Enabled = true;
                if (ApiKeyState.IsOk)
                {
                    apiKey_tb.Text = ApiKeyState.ApiKey;
                }
            }
            else
            {
                if (ApiKeyState.IsOk)
                {
                    apiKey_tb.Text = ApiKeyState.ApiKey;
                    if (string.IsNullOrEmpty(tmpOptions.ProviderId))
                    {
                        textBoxProviderName.Text = Resource.NeedAChoise;
                        buttonContinue.Enabled = string.IsNullOrWhiteSpace(ApiKeyState.ApiKey);
                    }
                    else
                    {
                        textBoxProviderName.Text = tmpOptions.ProviderName;
                        buttonContinue.Enabled = true;
                    }
                }
                else
                {
                    buttonContinue.Enabled = string.IsNullOrWhiteSpace(ApiKeyState.ApiKey);
                    textBoxProviderName.Text = Resource.MFNa;
                }

                if (tmpOptions.AuthMode == StateModeEnum.Prohibited || tmpOptions.AuthMode == StateModeEnum.Unknown)
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
                    textBoxAccount.Text = tmpOptions.IsAuthDelegated
                        ? tmpOptions.AuthDelegatedCredentialId
                        : tmpOptions.CustomAuth;
                    labelApiKeyIsChanged.Visible = settingsIsSet;
                }

                if (tmpOptions.CustomModelMode == StateModeEnum.Prohibited ||
                    tmpOptions.CustomModelMode == StateModeEnum.Unknown)
                    textBoxModel.Text = Resource.MFNa;
                else if (tmpOptions.CustomModelMode == StateModeEnum.Optional && !tmpOptions.UseCustomModel)
                    textBoxModel.Text = Resource.Empty;
                else
                {
                    textBoxModel.Text = tmpOptions.CustomModelName;
                    labelApiKeyIsChanged.Visible = settingsIsSet;
                }

                if (tmpOptions.GlossaryMode == StateModeEnum.Prohibited ||
                    tmpOptions.GlossaryMode == StateModeEnum.Unknown)
                    textBoxGlossary.Text = Resource.MFNa;
                else
                {
                    textBoxGlossary.Text = string.IsNullOrEmpty(tmpOptions.GlossaryName)
                        ? Resource.Empty
                        : tmpOptions.GlossaryName;
                    labelApiKeyIsChanged.Visible = settingsIsSet;
                }

                if (tmpOptions.IntentoGlossaries is { Length: > 0 })
                {
                    var glossariesInState = ApiKeyState?.SmartRoutingState?.ProviderState?.GetAuthState()?
                        .GetProviderGlossaryState()?.GetGlossariesDetailed(tmpOptions.IntentoGlossaries);
                    textBoxProviderAgnosticGloss.Text = glossariesInState != null
                        ? string.Join(Environment.NewLine, glossariesInState.Select(g => g.Name))
                        : Resource.Empty;
                }
                else
                {
                    textBoxProviderAgnosticGloss.Text = Resource.Empty;
                }
            }
        }

        private void IntentoTranslationProviderOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OriginalOptions.TraceEndTime = RemoteLogService.GetTraceEndTime();
        }
    }
}