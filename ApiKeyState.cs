using IntentoMT.Plugin.PropertiesForm;
using IntentoSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntentoMT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace IntentoMT.Plugin.PropertiesForm
{
    public class ApiKeyState
    {
        public string apiKey;

        System.Windows.Forms.TextBox apiKey_tb;
        IntentoTranslationProviderOptionsForm form;
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
        private List<dynamic> providers;

        private IntentoAiTextTranslate _translate;

        string error_reason = null;

        public delegate void ApiKeyChanged(bool isOK);
        public event ApiKeyChanged apiKeyChangedEvent;

        public ApiKeyState(IntentoTranslationProviderOptionsForm _form, System.Windows.Forms.TextBox _apiKey_tb, 
            IntentoMTFormOptions options)
        {
            apiKey = options.ApiKey;
            apiKey_tb = _apiKey_tb;
            form = _form;
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
            apiKey_tb.Text = apiKey;
            apiKey_tb.Visible = true;

            switch (apiKeyStatus)
            {
                case EApiKeyStatus.start:
                    if (string.IsNullOrEmpty(apiKey))
                    {
                        apiKey_tb.Enabled = true;
                        apiKey_tb.BackColor = Color.LightPink;
                        error_reason = "Enter your API key and press \"Check\" button.";
                    }
                    else
                    {
                        apiKey_tb.Enabled = false;
                        apiKey_tb.BackColor = Color.White;
                        error_reason = "API key verification in progress ....";
                    }
                    break;

                case EApiKeyStatus.download:
                    apiKey_tb.Enabled = false;
                    apiKey_tb.BackColor = Color.White;
                    error_reason = "API key verification in progress ....";
                    break;

                case EApiKeyStatus.ok:
                    apiKey_tb.Enabled = true;
                    apiKey_tb.BackColor = Color.White;
                    error_reason = null;
                    break;

                case EApiKeyStatus.error:
                    apiKey_tb.Enabled = true;
                    apiKey_tb.BackColor = Color.LightPink;
                    break;

                case EApiKeyStatus.changed:
                    apiKey_tb.BackColor = Color.LightPink;
                    apiKey_tb.Enabled = true;
                    if (string.IsNullOrEmpty(apiKey))
                        error_reason = "Enter your API key and press \"Check\" button.";
                    else
                        error_reason = "API key verification required. Press \"Check\" button.";
                    break;
            }
            return error_reason;
        }

        public string Error()
        {
            return error_reason;
        }

        public void Validate()
        {
            ReadProviders();
        }

        private void ReadProviders()
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                ChangeStatus(EApiKeyStatus.changed);
                return;
            }

            try
            {
                providers = null;
                error_reason = null;

                ChangeStatus(EApiKeyStatus.download);
                _translate = form._translate;

                providers = _translate.Providers(filter: new Dictionary<string, string> { { "integrated", "true" }, { "mode", "async" } }).ToList();

                ChangeStatus(EApiKeyStatus.ok);
            }
            catch (AggregateException ex2)
            {
                Exception ex = ex2.InnerExceptions[0];
                if (ex is IntentoInvalidApiKeyException)
                {
                    error_reason = "Invalid API key";
                }
                else
                {
                    if (ex is IntentoInvalidApiKeyException)
                        error_reason = string.Format("Forbitten. {0}", ((IntentoSDK.IntentoApiException)ex).Content);
                    else if (ex is IntentoApiException)
                        error_reason = string.Format("Api Exception {2}: {0}: {1}", ex.Message, ((IntentoApiException)ex).Content, ex.GetType().Name);
                    else if (ex is IntentoSdkException)
                        error_reason = string.Format("Sdk Exception {1}: {0}", ex.Message, ex.GetType().Name);
                    else
                        error_reason = string.Format("Unexpected exception {0}: {1}", ex.GetType().Name, ex.Message);
                }
                ChangeStatus(EApiKeyStatus.error);
            }
        }

        private void ChangeStatus(EApiKeyStatus status)
        {
            apiKeyStatus = status;
            ApiKeyChanged handler = apiKeyChangedEvent;
            if (handler != null)
                handler(status == EApiKeyStatus.ok);
        }

        public List<dynamic> Providers
        { get { return providers; } }

        public bool CheckPossible
        { get { return apiKeyStatus == EApiKeyStatus.changed && !string.IsNullOrEmpty(apiKey); } }

        public bool IsOK
        { get { return apiKeyStatus == EApiKeyStatus.ok; } }

    }
}
