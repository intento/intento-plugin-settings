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
    public class ModelState : BaseState
    {
        AuthState authState;

        // List of custom models obtained from provider
        Dictionary<string, dynamic> providerModels;

        public enum EnumModelMode
        {
            prohibited,
            required,
            optional
        }

        EnumModelMode modelMode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_options"></param>
        /// <param name="fromForm">Call from event, change of form check box may result recourcing</param>
        public ModelState(AuthState authState, IntentoMTFormOptions _options, bool fromForm = false) : base(authState, _options)
        {
            this.authState = authState;

            if (authState.providerState.smartRoutingState.SmartRouting)
                modelMode = EnumModelMode.prohibited;
            else if (!authState.providerState.stock_model && authState.providerState.custom_model)
                modelMode = EnumModelMode.required;
            else if (!authState.providerState.custom_model)
                modelMode = EnumModelMode.prohibited;
            else
                modelMode = EnumModelMode.optional;

            form.Model_ComboBox_Clear();

            form.Model_CheckBox_Visible = true;

            if (modelMode == EnumModelMode.prohibited)
            {
                if (!fromForm)
                    form.Model_CheckBox_Checked = false;
                form.Model_CheckBox_Enabled = false;
                form.Model_Group_Enabled = false;
            }
            else if (modelMode == EnumModelMode.required)
            {
                if (!fromForm)
                    form.Model_CheckBox_Checked = true;
                form.Model_CheckBox_Enabled = false;
                form.Model_Group_Visible = true;
                form.Model_Group_Enabled = true;
            }
            else
            {
                if (!fromForm)
                    form.Model_CheckBox_Checked = options.UseCustomModel;
                form.Model_CheckBox_Enabled = true;
                form.Model_Group_Visible = true;
                form.Model_Group_Enabled = true;
            }

            FillProviderModels();

            if (UseModel)
                FillProviderModels();
            else
                providerModels = null;
        }

        // Flag to provide possibility to enter own model
        bool UseEspecialModel
        { get { return providerModels == null; } }

        private void FillProviderModels()
        {   // Fill combo or text box depending on provider features
            if (providerModels != null)
                return;

            providerModels = authState.providerState.GetModels(authState.providerDataAuthDict);

            try
            {
                if (providerModels != null && providerModels.Any())
                {
                    if (providerModels.Count > 1 || modelMode == EnumModelMode.optional)
                        form.Model_ComboBox_Add("");

                    // Fill comboBoxModels and choose SelectedIndex{"dt": "2019-04-11 00:54:31.132266+00:00", "ts": 1554944071.132266, "kong_request_id": "172.18.0.4-8443-18-73563306-1-1554944070.920", "tornado_request_id": "1dc2a0a6-c447-48e7-a6b2-b99a1e4eb615", "client_key_id": "fb8fb351-4b64-47be-b79a-a2cf7fef706b", "client_key_name": "intento_integration", "request_mode": "production", "error_type": null, "code": null, "reason": null, "op": "async", "intent_id": "ai.text.translate", "instance_id": null, "instance_ids": [], "intent_dependent": null, "intent_request_data": {"request": {"method": "POST", "uri": "/ai/text/translate", "version": "HTTP/1.1", "headers": "Host: prod_intent_api_05.aws-usw2.inten.to:8844\nConnection: keep-alive\nX-Forwarded-For: 23.111.23.189\nX-Forwarded-Proto: https\nX-Forwarded-Host: api.inten.to\nX-Forwarded-Port: 8443\nX-Real-Ip: 23.111.23.189\nContent-Length: 483\nApikey: 1d2dfc89fad9403a8599af9372b0a909\nUser-Agent: Intento.CSharpSDK/1.2.0 Intento.MemoqPlugin/1.2.0 memoq/8.6.6.27345\nContent-Type: text/plain; charset=utf-8\nX-Consumer-Id: fb8fb351-4b64-47be-b79a-a2cf7fef706b\nX-Consumer-Username: intento_integration\nX-Consumer-Groups: integration\nKong-Request-Id: 172.18.0.4-8443-18-73563306-1-1554944070.920\n", "intento_user_agents": ["intento.csharpsdk", "intento.memoqplugin"], "remote_ip": "172.31.12.124", "arguments": "{}", "cookies": "", "body": {"service": {"provider": "ai.text.translate.google.translate_api.v3", "async": true, "auth": {"ai.text.translate.google.translate_api.v3": [{"credential_id": "testcred"}]}}, "context": {"text": "hash: 4e9601c3f20b43aa0b91f79bdf5c2131f805d265e2783efd65aaebe1b7d2e618", "to": "de", "from": "en", "category": "projects/1091715416487/locations/us-central1/models/TRL4634621755906527763", "glossary": "hash: b12b64fd1fe0843d6f55058885bb90013f20e8f1d19f0a77b7cf6d2995f851c4", "provider": "hash: bf2dbb74741c7ad4c16e0d92c7593011feee24fc457e548843ea0f93177f1261", "model": "hash: b417eadd75484331936326c6ce9d1afb28e09178384d70552a1165df6d618d1b"}}}, "intent": {"service": {"provider": "ai.text.translate.google.translate_api.v3", "async": true, "auth": {"ai.text.translate.google.translate_api.v3": [{"credential_id": "testcred"}]}}, "context": {"text": "hash: 4e9601c3f20b43aa0b91f79bdf5c2131f805d265e2783efd65aaebe1b7d2e618", "to": "de", "from": "en", "category": "projects/1091715416487/locations/us-central1/models/TRL4634621755906527763", "glossary": "hash: b12b64fd1fe0843d6f55058885bb90013f20e8f1d19f0a77b7cf6d2995f851c4", "provider": "hash: bf2dbb74741c7ad4c16e0d92c7593011feee24fc457e548843ea0f93177f1261", "model": "hash: b417eadd75484331936326c6ce9d1afb28e09178384d70552a1165df6d618d1b"}}, "metainfo": {"symbols": 40, "items": 1, "words": 7}}, "provider_request_data": {}, "provider_answer_data": {}, "intent_answer_data": {}, "timestamp": "2019-04-11 00:54:31.132266+00:00"}
                    foreach (string x in providerModels.Select(x => (string)x.Key).OrderBy(x => x))
                    {
                        int n = form.Model_ComboBox_Add(x);
                        if (providerModels[x].id == options.CustomModel)
                            form.Model_ComboBox_SelectedIndex = n;
                    }
                    form.Model_ComboBox_Visible = !(form.Model_TextBox_Visible = false);
                    if (form.Model_ComboBox_Count == 1)
                    {
                        form.Model_ComboBox_SelectedIndex = 0;
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
            {
                // If no list of models available we use text box to edit model id
                form.Model_TextBox_Text = options.CustomModel;
            }
        }

        public static string Draw(IForm form, ModelState state)
        {
            if (state == null)
            {
                // form.Model_TextBox_Visible = false;
                // form.Model_ComboBox_Visible = false;
                form.Model_CheckBox_Visible = false;
                form.Model_Group_Visible = false;
                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            string errorMessage = null;

            form.Model_CheckBox_Visible = true;
            form.Model_Group_Visible = true;

            // set state of checkBoxUseCustomModel
            switch (modelMode)
            {
                case EnumModelMode.required:
                    if (string.IsNullOrEmpty(ModelName))
                        errorMessage = "You must specify a model for this provider";
                    break;

                case EnumModelMode.optional:
                    if (form.Model_CheckBox_Checked && string.IsNullOrEmpty(ModelName))
                        errorMessage = "You must specify a model for this provider";
                    break;

            }

            if (!UseModel)
            {
                form.Model_Group_Visible = false;
                return errorMessage;
            }

            form.Model_Group_Visible = true;

            // choose between combo box and text exit controls to show a model
            form.Model_ComboBox_Visible = !(form.Model_TextBox_Visible = providerModels == null || providerModels.Count == 0);

            // set back color 
            if (string.IsNullOrEmpty(ModelName))
            {
                form.Model_ComboBox_BackColor = Color.LightPink;
                form.Model_TextBox_BackColor = Color.LightPink;
                errorMessage = "You must specify a custom model or uncheck \"use your custom model\"";
            }
            else
            {
                form.Model_ComboBox_BackColor = SystemColors.Window;
                form.Model_TextBox_BackColor = SystemColors.Window;
            }

            return errorMessage;
        }

        public void checkBoxUseCustomModel_CheckedChanged()
        {
            options.UseCustomModel = form.Model_CheckBox_Checked;
            EnableDisable();
        }

        public void comboBoxModels_SelectedIndexChanged()
        {

        }

        private string ModelName
        {
            get
            {
                if (!UseModel)
                    return null;

                if (UseEspecialModel)
                {   // Use model name form text box
                    if (string.IsNullOrEmpty(form.Model_TextBox_Text))
                        return null;
                    return form.Model_TextBox_Text;
                }

                // Use model name from combo box
                if (!string.IsNullOrEmpty(form.Model_ComboBox_Text) && providerModels != null && providerModels.Count != 0)
                {
                    dynamic d;
                    if (providerModels.TryGetValue(form.Model_ComboBox_Text, out d))
                    {
                        string modelName = (string)providerModels[form.Model_ComboBox_Text].id;
                        if (string.IsNullOrEmpty(modelName))
                            return null;
                        return modelName;
                    }
                }

                return null;
            }
        }

        public bool UseModel
        { get { return form.Model_CheckBox_Checked; } }

        public static void FillOptions(ModelState state, IntentoMTFormOptions options)
        {
            if (state == null)
            {
                options.UseCustomModel = false;
                options.CustomModel = null;
            }
            else
            {
                options.UseCustomModel = state.UseModel;
                options.CustomModel = state.ModelName;
            }
        }

    }
}
