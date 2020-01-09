using IntentoSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Resources;
using Intento.MT.Plugin.PropertiesForm.WinForms;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class ApiKeyState : BaseState
    {
        public string apiKey;

        // Controlled components
        public SmartRoutingState smartRoutingState;

        public enum EApiKeyStatus
        {
            start,          // just after start of plugin
            download,       // during download list of providers
            changed,        // apikey was changed recently and was not checked
            ok,             // apikey checked
            error           // apikey check error
        };
        public EApiKeyStatus apiKeyStatus = EApiKeyStatus.start;

        // Result of request to read a list of providers
        public List<dynamic> providers;

        string error_reason = null;

        public delegate void ApiKeyChanged(bool isOK);
        public event ApiKeyChanged apiKeyChangedEvent;

        //IntentoFormOptionsMT formMT;

        public ApiKeyState(IntentoTranslationProviderOptionsForm _form, IntentoMTFormOptions options) : base(_form, options)
        {
            apiKey = options.ApiKey;

            if (!options.ForbidSaveApikey)
            {
                string apiKey2 = GetValueFromRegistry("ApiKey");
                if (string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(options.AppName))
                {   // read ApiKey from registry
                    apiKey = apiKey2;
                }
                if (!string.IsNullOrEmpty(apiKey2))
                    form.formAdvanced.checkBoxSaveApiKeyInRegistry.Checked = true;
            }
        }

        public void SetValue(string _apiKey)
        {
            if (apiKey == _apiKey)
                return;
            apiKey = _apiKey;
            ChangeStatus(EApiKeyStatus.changed);
        }

        public string Draw()
        {
            //form.ApiKey_TextBox_Text = apiKey;
            //form.ApiKeyCheck_Button_Enabled = CheckPossible;

            form.formApi.apiKey_tb.Text = apiKey;
            form.formApi.buttonSave.Enabled = CheckPossible;

            switch (apiKeyStatus)
            {
                case EApiKeyStatus.start:
                    if (string.IsNullOrEmpty(apiKey))
                    {
                        //form.ApiKey_TextBox_Enabled = true;
                        //form.ApiKey_TextBox_BackColor = Color.LightPink;
                        form.formApi.apiKey_tb.Enabled = true;
                        form.formApi.apiKey_tb.BackColor = Color.LightPink;

                        // "Enter your API key and press \"Check\" button."
                        error_reason = Resource.ApiKeyNeededErrorMessage;
                    }
                    else
                    {
                        //form.ApiKey_TextBox_Enabled = false;
                        //form.ApiKey_TextBox_BackColor = Color.White;
                        form.formApi.apiKey_tb.Enabled = false;
                        form.formApi.apiKey_tb.BackColor = Color.White;
                        // "API key verification in progress ...."
                        error_reason = Resource.ApiKeyVerificationInProgressMessage;
                    }
                    break;

                case EApiKeyStatus.download:
                    //form.ApiKey_TextBox_Enabled = false;
                    //form.ApiKey_TextBox_BackColor = Color.White;
                    form.formApi.apiKey_tb.Enabled = false;
                    form.formApi.apiKey_tb.BackColor = Color.White;
                    // "API key verification in progress ...."
                    error_reason = Resource.ApiKeyVerificationInProgressMessage;
                    break;

                case EApiKeyStatus.ok:
                    //form.ApiKey_TextBox_Enabled = true;
                    //form.ApiKey_TextBox_BackColor = Color.White;
                    form.formApi.apiKey_tb.Enabled = true;
                    form.formApi.apiKey_tb.BackColor = Color.White;
                    //form.MainForm_ApiKey_TextBox_Visible = true;
                    error_reason = null;
                    break;

                case EApiKeyStatus.error:
                    //form.ApiKey_TextBox_Enabled = true;
                    //form.ApiKey_TextBox_BackColor = Color.LightPink;
                    form.formApi.apiKey_tb.Enabled = true;
                    form.formApi.apiKey_tb.BackColor = Color.LightPink;
                    break;

                case EApiKeyStatus.changed:
                    //form.ApiKey_TextBox_BackColor = Color.LightPink;
                    //form.ApiKey_TextBox_Enabled = true;
                    form.formApi.apiKey_tb.Enabled = true;
                    form.formApi.apiKey_tb.BackColor = Color.LightPink;
                    if (string.IsNullOrEmpty(apiKey))
                        // Enter your API key and press \"Check\" button.
                        error_reason = Resource.ApiKeyNeededErrorMessage; 
                    else
                        // "API key verification in progress ...."
                        error_reason = Resource.ApiKeyVerificationInProgressMessage;
                    break;
            }
            ApiKey_Set_Panel(IsOK);
            if (!IsOK)
            {
                SmartRoutingState.Draw(form, null);
                return error_reason;
            }

            return SmartRoutingState.Draw(form, smartRoutingState);
        }

        public string Error()
        {
            return error_reason;
        }

        public void ReadProviders()
        {
            using (new IntentoTranslationProviderOptionsForm.CursorFormMT(form.formMT))
            {
                if (string.IsNullOrEmpty(apiKey))
                {
                    ChangeStatus(EApiKeyStatus.changed);
                    EnableDisable();
                    return;
                }

                try
                {
                    providers = null;
                    error_reason = null;

                    ChangeStatus(EApiKeyStatus.download);

                    //providers = form.Providers(filter: new Dictionary<string, string> { { "integrated", "true" }, { "mode", "async" } }).ToList();
                    providers = form._translate.Providers(filter: new Dictionary<string, string> { { "integrated", "true" }, { "mode", "async" } }).ToList();

                    // SmartRoutingState created inside
                    ChangeStatus(EApiKeyStatus.ok);
                }
                catch (AggregateException ex2)
                {
                    Exception ex = ex2.InnerExceptions[0];
                    if (ex is IntentoInvalidApiKeyException)
                    {
                        // Invalid API key
                        error_reason = Resource.InvalidApiKeyMessage;
                    }
                    else
                    {
                        if (ex is IntentoInvalidApiKeyException)
                            error_reason = string.Format("[F] {0}", ((IntentoSDK.IntentoApiException)ex).Content);
                        else if (ex is IntentoApiException)
                            error_reason = string.Format("[Api] {2}: {0}: {1}", ex.Message, ((IntentoApiException)ex).Content, ex.GetType().Name);
                        else if (ex is IntentoSdkException)
                            error_reason = string.Format("[Sdk] {1}: {0}", ex.Message, ex.GetType().Name);
                        else if (ex is HttpRequestException)
                        {
                            if (ex.InnerException != null)
                                error_reason = string.Format("[R] {0}: {1}", ex.InnerException.GetType().Name, ex.InnerException.Message);
                            else
                                error_reason = string.Format("[R] {0}: {1}", ex.GetType().Name, ex.Message);
                        }
                        else
                            error_reason = string.Format("[U] {0}: {1}", ex.GetType().Name, ex.Message);
                    }

                    // SmartRoutingState not created inside because status is not ok
                    ChangeStatus(EApiKeyStatus.error);
                }
                finally
                {
                    EnableDisable();
                }
            }
        }

        private void CreateChildStates()
        {
            if (IsOK)
                smartRoutingState = new SmartRoutingState(this, options);
            else
                smartRoutingState = null;
        }

        private void ChangeStatus(EApiKeyStatus status)
        {
            apiKeyStatus = status;
            ApiKeyChanged handler = apiKeyChangedEvent;
            handler(status == EApiKeyStatus.ok);

            CreateChildStates();
        }

        public List<dynamic> Providers
        { get { return providers; } }

        public bool CheckPossible
        { get { return apiKeyStatus == EApiKeyStatus.changed && !string.IsNullOrEmpty(apiKey); } }

        public bool IsOK
        { get { return apiKeyStatus == EApiKeyStatus.ok; } }

        public void FillOptions(IntentoMTFormOptions options)
        {
            options.ApiKey = apiKey;
            SmartRoutingState.FillOptions(smartRoutingState, options);
        }

        #region methods for managing a group of controls

        void ApiKey_Set_Panel(bool apiKeyStateIsOK)
        {
            form.groupBoxMTConnect.Visible = !apiKeyStateIsOK;
            form.groupBoxMTConnect2.Visible = apiKeyStateIsOK;
            form.buttonMTSetting.Enabled = apiKeyStateIsOK;
        }

        #endregion methods for managing a group of controls

    }
}
