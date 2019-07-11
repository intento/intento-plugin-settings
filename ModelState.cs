using IntentoMT.Plugin.PropertiesForm;
using IntentoSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class ModelState
    {
        IntentoTranslationProviderOptionsForm form;
        ProviderState providerState;
        AuthState authState;
        IntentoMTFormOptions options;
        private IntentoAiTextTranslate _translate;
        System.Windows.Forms.CheckBox checkBoxUseCustomModel;
        System.Windows.Forms.GroupBox groupBoxModel;
        System.Windows.Forms.ComboBox comboBoxModels;
        System.Windows.Forms.TextBox textBoxModel;

        // List of custom models obtained from provider
        Dictionary<string, dynamic> providerModels = new Dictionary<string, dynamic>();

        public enum EnumModelMode
        {
            prohibited,
            required,
            optional
        }

        EnumModelMode customModelMode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_options"></param>
        /// <param name="fromForm">Call from event, change of form check box may result recourcing</param>
        public ModelState(AuthState authState, IntentoMTFormOptions _options, bool fromForm = false)
        {
            this.authState = authState;
            providerState = authState.providerState;
            form = providerState.form;
            options = _options;
            checkBoxUseCustomModel = form.checkBoxUseCustomModel;
            groupBoxModel = form.groupBoxModel;
            comboBoxModels = form.comboBoxModels;
            textBoxModel = form.textBoxModel;
            _translate = form._translate;

            if (form.smartRoutingState.SmartRouting)
                customModelMode = EnumModelMode.prohibited;
            else if (!providerState.stock_model && providerState.custom_model)
                customModelMode = EnumModelMode.required;
            else if (!providerState.custom_model)
                customModelMode = EnumModelMode.prohibited;
            else 
                customModelMode = EnumModelMode.optional;

            if (!fromForm)
            {
                if (customModelMode == EnumModelMode.prohibited)
                    checkBoxUseCustomModel.Checked = false;
                else if (customModelMode == EnumModelMode.required)
                    checkBoxUseCustomModel.Checked = true;
                else
                    checkBoxUseCustomModel.Checked = options.UseCustomModel;
            }

            FillProviderModels();

            if (UseCustomModel)
                FillProviderModels();
            else
                providerModels = null;
        }

        // Flag to provide possibility to enter own model
        bool UseEspecialModel
        { get { return providerModels == null; } }

        private void FillProviderModels()
        {   // Fill combo or text box depending on provider features
            comboBoxModels.Items.Clear();
            providerModels = null;

            if (!providerState.IsOK)
                return;
            if (!authState.IsOK)
                return;
            if (!UseCustomModel)
                return;

            providerModels = providerState.GetModels(authState.providerDataAuthDict);

            try
            {
                if (providerModels != null && providerModels.Any())
                {
                    if (providerModels.Count > 1 || customModelMode == EnumModelMode.optional)
                        comboBoxModels.Items.Add("");

                    // Fill comboBoxModels and choose SelectedIndex{"dt": "2019-04-11 00:54:31.132266+00:00", "ts": 1554944071.132266, "kong_request_id": "172.18.0.4-8443-18-73563306-1-1554944070.920", "tornado_request_id": "1dc2a0a6-c447-48e7-a6b2-b99a1e4eb615", "client_key_id": "fb8fb351-4b64-47be-b79a-a2cf7fef706b", "client_key_name": "intento_integration", "request_mode": "production", "error_type": null, "code": null, "reason": null, "op": "async", "intent_id": "ai.text.translate", "instance_id": null, "instance_ids": [], "intent_dependent": null, "intent_request_data": {"request": {"method": "POST", "uri": "/ai/text/translate", "version": "HTTP/1.1", "headers": "Host: prod_intent_api_05.aws-usw2.inten.to:8844\nConnection: keep-alive\nX-Forwarded-For: 23.111.23.189\nX-Forwarded-Proto: https\nX-Forwarded-Host: api.inten.to\nX-Forwarded-Port: 8443\nX-Real-Ip: 23.111.23.189\nContent-Length: 483\nApikey: 1d2dfc89fad9403a8599af9372b0a909\nUser-Agent: Intento.CSharpSDK/1.2.0 Intento.MemoqPlugin/1.2.0 memoq/8.6.6.27345\nContent-Type: text/plain; charset=utf-8\nX-Consumer-Id: fb8fb351-4b64-47be-b79a-a2cf7fef706b\nX-Consumer-Username: intento_integration\nX-Consumer-Groups: integration\nKong-Request-Id: 172.18.0.4-8443-18-73563306-1-1554944070.920\n", "intento_user_agents": ["intento.csharpsdk", "intento.memoqplugin"], "remote_ip": "172.31.12.124", "arguments": "{}", "cookies": "", "body": {"service": {"provider": "ai.text.translate.google.translate_api.v3", "async": true, "auth": {"ai.text.translate.google.translate_api.v3": [{"credential_id": "testcred"}]}}, "context": {"text": "hash: 4e9601c3f20b43aa0b91f79bdf5c2131f805d265e2783efd65aaebe1b7d2e618", "to": "de", "from": "en", "category": "projects/1091715416487/locations/us-central1/models/TRL4634621755906527763", "glossary": "hash: b12b64fd1fe0843d6f55058885bb90013f20e8f1d19f0a77b7cf6d2995f851c4", "provider": "hash: bf2dbb74741c7ad4c16e0d92c7593011feee24fc457e548843ea0f93177f1261", "model": "hash: b417eadd75484331936326c6ce9d1afb28e09178384d70552a1165df6d618d1b"}}}, "intent": {"service": {"provider": "ai.text.translate.google.translate_api.v3", "async": true, "auth": {"ai.text.translate.google.translate_api.v3": [{"credential_id": "testcred"}]}}, "context": {"text": "hash: 4e9601c3f20b43aa0b91f79bdf5c2131f805d265e2783efd65aaebe1b7d2e618", "to": "de", "from": "en", "category": "projects/1091715416487/locations/us-central1/models/TRL4634621755906527763", "glossary": "hash: b12b64fd1fe0843d6f55058885bb90013f20e8f1d19f0a77b7cf6d2995f851c4", "provider": "hash: bf2dbb74741c7ad4c16e0d92c7593011feee24fc457e548843ea0f93177f1261", "model": "hash: b417eadd75484331936326c6ce9d1afb28e09178384d70552a1165df6d618d1b"}}, "metainfo": {"symbols": 40, "items": 1, "words": 7}}, "provider_request_data": {}, "provider_answer_data": {}, "intent_answer_data": {}, "timestamp": "2019-04-11 00:54:31.132266+00:00"}
                    foreach (string x in providerModels.Select(x => (string)x.Key).OrderBy(x => x))
                    {
                        int n = comboBoxModels.Items.Add(x);
                        if (providerModels[x].id == options.CustomModel)
                            comboBoxModels.SelectedIndex = n;
                    }
                    comboBoxModels.Visible = !(textBoxModel.Visible = false);
                    if (comboBoxModels.Items.Count == 1)
                    {
                        comboBoxModels.SelectedIndex = 0;
                    }
                }
                else
                    providerModels = null;
            }
            catch
            {
                providerModels = null;
            }

            if (providerModels == null)
                textBoxModel.Text = options.CustomModel;
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, ModelState state)
        {
            if (state == null)
            {
                form.checkBoxUseOwnCred.Visible = false;
                form.textBoxModel.Visible = false;
                form.comboBoxModels.Visible = false;
                form.groupBoxModel.Visible = false;
                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            string errorMessage = null;
            // If smart routing or provider is not initialized or no custom auth - no model selection
            if (!providerState.IsOK)
            {   // Model is not used
                checkBoxUseCustomModel.Visible = false;
                groupBoxModel.Visible = false;
                return null;
            }

            checkBoxUseCustomModel.Visible = true;
            // set state of checkBoxUseCustomModel
            switch (customModelMode)
            {
                case EnumModelMode.required:
                    checkBoxUseCustomModel.Enabled = false;
                    checkBoxUseCustomModel.Checked = true;
                    groupBoxModel.Enabled = true;
                    textBoxModel.Enabled = true;
                    comboBoxModels.Enabled = true;

                    if (string.IsNullOrEmpty(Model()))
                        errorMessage = "You must specify a model for this provider";
                    break;

                case EnumModelMode.prohibited:
                    checkBoxUseCustomModel.Enabled = false;
                    checkBoxUseCustomModel.Checked = false;
                    groupBoxModel.Enabled = false;
                    textBoxModel.Enabled = false;
                    comboBoxModels.Enabled = false;

                    break;

                case EnumModelMode.optional:
                    checkBoxUseCustomModel.Enabled = true;
                    groupBoxModel.Enabled = true;
                    textBoxModel.Enabled = true;
                    comboBoxModels.Enabled = true;

                    break;

                default:
                    errorMessage = "EnableDisable.customModelMode";
                    break;
            }

            if (!UseCustomModel)
            {
                groupBoxModel.Visible = false;
                return errorMessage;
            }

            groupBoxModel.Visible = true;

            // choose between combo box and text exit controls to show a model
            textBoxModel.Visible = providerModels == null || providerModels.Count == 0;
            comboBoxModels.Visible = !textBoxModel.Visible;

            // set back color 
            if (textBoxModel.Visible)
                textBoxModel.BackColor = string.IsNullOrEmpty(Model()) ? Color.LightPink : SystemColors.Window;
            else
                comboBoxModels.BackColor = string.IsNullOrEmpty(Model()) ? Color.LightPink : SystemColors.Window;

            if (UseCustomModel && string.IsNullOrEmpty(Model()))
                errorMessage = "You must specify a custom model or uncheck \"use your custom model\"";

            return errorMessage;
        }

        public static void checkBoxUseCustomModel_CheckedChanged(AuthState authState, IntentoMTFormOptions options)
        {
            authState.form.Cursor = Cursors.WaitCursor;

            options.UseCustomModel = authState.form.checkBoxUseCustomModel.Checked;
            authState.modelState = new ModelState(authState, options, true);

            authState.form.Cursor = Cursors.Default;
            authState.form.EnableDisable();
        }

        public void comboBoxModels_SelectedIndexChanged()
        {

        }

        private string Model()
        {
            if (!UseEspecialModel)
            {
                if (string.IsNullOrEmpty(textBoxModel.Text))
                    return null;
                return textBoxModel.Text;
            }

            if (!string.IsNullOrEmpty(comboBoxModels.Text) && providerModels != null && providerModels.Count != 0)
                return (string)providerModels[comboBoxModels.Text].id;

            return null;
        }

        public bool UseCustomModel
        { get { return checkBoxUseCustomModel.Checked; } }

        public string CustomModel
        {
            get
            {
                if (UseCustomModel)
                    return Model();
                return null;
            }
        }

        public void FillOptions(IntentoMTFormOptions options)
        {
            if (options.SmartRouting)
            {
                options.UseCustomModel = false;
                options.CustomModel = null;
            }
            else
            {
                options.UseCustomModel = UseCustomModel;
                options.CustomModel = CustomModel;
            }
        }

    }
}
