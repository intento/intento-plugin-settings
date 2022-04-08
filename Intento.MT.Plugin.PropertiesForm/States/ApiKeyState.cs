using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using Intento.SDK.Autofac;
using Intento.SDK.DI;
using Intento.SDK.Exceptions;
using Intento.SDK.Settings;
using Intento.SDK.Translate;
using Intento.SDK.Translate.DTO;

namespace Intento.MT.Plugin.PropertiesForm.States
{
    public partial class ApiKeyState : BaseState
    {
        private string errorReason;
        private IEnumerable<string> errorDetail;
        private readonly IntentoMTFormOptions options;

        private ITranslateService TranslateService => Form.Locator?.Resolve<ITranslateService>();

        public string ApiKey { get; private set; }

        // Controlled components
        public SmartRoutingState SmartRoutingState { get; set; }

        public EApiKeyStatus ApiKeyStatus { get; private set; } = EApiKeyStatus.Start;

        // Routing table query result
        public IList<Routing> Routings { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="form"></param>
        /// <param name="options"></param>
        public ApiKeyState(IntentoTranslationProviderOptionsForm form, IntentoMTFormOptions options) : base(form,
            options)
        {
            this.options = options;
            ApiKey = options.ApiKey;
            if (options.ForbidSaveApikey)
            {
                return;
            }

            var apiKey2 = form.GetValueFromRegistry("ApiKey");
            if (string.IsNullOrEmpty(ApiKey) && !string.IsNullOrEmpty(options.AppName))
            {
                // read ApiKey from registry
                ApiKey = apiKey2;
            }

            if (!string.IsNullOrEmpty(apiKey2))
            {
                Form.FormAdvanced.checkBoxSaveApiKeyInRegistry.Checked = true;
            }
        }

        public string Draw()
        {
            Form.FormApi.apiKey_tb.Text = ApiKey;
            Form.FormApi.buttonSave.Enabled = CheckPossible;

            switch (ApiKeyStatus)
            {
                case EApiKeyStatus.Start:
                    if (string.IsNullOrEmpty(ApiKey))
                    {
                        Form.FormApi.apiKey_tb.Enabled = true;
                        Form.FormApi.apiKey_tb.BackColor = Color.LightPink;
                        // "Enter your API key and press \"Check\" button."
                        errorReason = Resource.ApiKeyNeededErrorMessage;
                    }
                    else
                    {
                        Form.FormApi.apiKey_tb.Enabled = false;
                        Form.FormApi.apiKey_tb.BackColor = SystemColors.Window;
                        // "API key verification in progress ...."
                        errorReason = Resource.ApiKeyVerificationInProgressMessage;
                    }

                    break;

                case EApiKeyStatus.Download:
                    Form.FormApi.apiKey_tb.Enabled = false;
                    Form.FormApi.apiKey_tb.BackColor = SystemColors.Window;
                    // "API key verification in progress ...."
                    errorReason = Resource.ApiKeyVerificationInProgressMessage;
                    break;

                case EApiKeyStatus.Ok:
                    Form.FormApi.apiKey_tb.Enabled = true;
                    Form.FormApi.apiKey_tb.BackColor = SystemColors.Window;
                    errorReason = null;
                    break;

                case EApiKeyStatus.Error:
                    Form.FormApi.apiKey_tb.Enabled = true;
                    Form.FormApi.apiKey_tb.BackColor = Color.LightPink;
                    break;

                case EApiKeyStatus.Changed:
                    Form.FormApi.apiKey_tb.Enabled = true;
                    Form.FormApi.apiKey_tb.BackColor = Color.LightPink;
                    if (string.IsNullOrEmpty(ApiKey))
                        // Enter your API key and press \"Check\" button.
                        errorReason = Resource.ApiKeyNeededErrorMessage;
                    else
                        // "API key verification in progress ...."
                        errorReason = Resource.ApiKeyVerificationInProgressMessage;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ApiKey_Set_Panel(IsOk);
            if (IsOk)
            {
                return SmartRoutingState.Draw(Form, SmartRoutingState);
            }

            SmartRoutingState.Draw(Form, null);
            return errorReason;
        }

        public void SetValue(string apiKey)
        {
            if (ApiKey == apiKey)
            {
                return;
            }

            ApiKey = apiKey;
            ChangeStatus(EApiKeyStatus.Changed);
        }

        public string Error()
        {
            return errorReason;
        }

        public ILocatorImpl CreateIntentoConnection(ProxySettings proxySettings, string additionalUserAgent = null)
        {
            var impl = new DefaultLocatorImpl();
            impl.Init(new Options
            {
                ServerUrl = options.ApiPath,
                TmsServerUrl = options.TmsApiPath,
                ApiKey = ApiKey,
                ClientUserAgent = $"Intento.PluginSettingsForm/{Form.Version} {additionalUserAgent}",
                Proxy = proxySettings
            });
            return impl;
        }

        public IEnumerable<string> ErrorDetail()
        {
            return errorDetail;
        }

        public void ReadProvidersAndRouting()
        {
            using (new IntentoTranslationProviderOptionsForm.CursorFormMT(Form.FormMt))
            {
                if (string.IsNullOrEmpty(ApiKey))
                {
                    ChangeStatus(EApiKeyStatus.Changed);
                    EnableDisable();
                    return;
                }

                try
                {
                    Providers = null;
                    errorReason = null;

                    ChangeStatus(EApiKeyStatus.Download);

                    Providers = TranslateService.Providers(
                            filter: new Dictionary<string, string>
                            {
                                { "integrated", "true" },
                                { "mode", "async" }
                            })
                        .ToList();
                    var additionalParams = new Dictionary<string, string> { { "pairs", "true" } };
                    Routings = TranslateService.Routing(additionalParams).ToList();
                    // SmartRoutingState created inside
                    ChangeStatus(EApiKeyStatus.Ok);
                }
                catch (AggregateException ex2)
                {
                    var ex = ex2.InnerExceptions[0];
                    errorReason = ex switch
                    {
                        IntentoInvalidApiKeyException => Resource.InvalidApiKeyMessage,
                        IntentoApiException exception => $"[Api] {exception.Message}: {exception.GetType().Name}",
                        IntentoSdkException => string.Format("[Sdk] {1}: {0}", ex.Message, ex.GetType().Name),
                        HttpRequestException => ex.InnerException != null
                            ? $"[R] {ex.InnerException.GetType().Name}: {ex.InnerException.Message}"
                            : $"[R] {ex.GetType().Name}: {ex.Message}",
                        _ => $"[U] {ex.GetType().Name}: {ex.Message}"
                    };

                    // SmartRoutingState not created inside because status is not ok
                    ChangeStatus(EApiKeyStatus.Error);
                    errorDetail = RemoteLogService.LoggingEx(ex2);
                }
                finally
                {
                    EnableDisable();
                }
            }
        }

        private void CreateChildStates()
        {
            SmartRoutingState = IsOk ? new SmartRoutingState(this, Options) : null;
        }

        private void ChangeStatus(EApiKeyStatus status)
        {
            ApiKeyStatus = status;
            if (ApiKeyStatus == EApiKeyStatus.Download)
            {
                Form.Locator = CreateIntentoConnection(Options.ProxySettings, Options.UserAgent);
            }
            else
            {
                CreateChildStates();
            }
        }

        public IList<Provider> Providers { get; private set; }

        private bool CheckPossible => ApiKeyStatus == EApiKeyStatus.Changed && !string.IsNullOrEmpty(ApiKey);

        public bool IsOk => ApiKeyStatus == EApiKeyStatus.Ok;

        // ReSharper disable once ParameterHidesMember
        public void FillOptions(IntentoMTFormOptions options)
        {
            options.ApiKey = ApiKey;
            SmartRoutingState.FillOptions(SmartRoutingState, options);
        }

        #region methods for managing a group of controls

        private void ApiKey_Set_Panel(bool apiKeyStateIsOk)
        {
            Form.groupBoxMTConnect.Visible = !apiKeyStateIsOk;
            Form.groupBoxMTConnect2.Visible = apiKeyStateIsOk;
            Form.buttonMTSetting.Enabled = apiKeyStateIsOk;
        }

        #endregion methods for managing a group of controls
    }
}