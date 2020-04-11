using Intento.MT.Plugin.PropertiesForm;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using IntentoSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Intento.MT.Plugin.PropertiesForm.IntentoMTFormOptions;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class AuthState : BaseState
    {
        // Parent component
        public ProviderState providerState;

        // Controlled components
        private ModelState modelState;
        private GlossaryState glossaryState;

        private List<string> _delegatedCredentials;
        string error_message;

        // credentials (in json format) choosen by user, both based on credential_id and on auth. 
        string credentials;

        bool firstTimeDraw = true;

        // Indicator of change of control status from the inside
        // in this case no change event call is required
        public static bool internalControlChange = false;

        public StateModeEnum mode;

        // current credentials StateModeEnum
        public Dictionary<string, string> providerDataAuthDict;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_options"></param>
        /// <param name="fromForm">Call from event, change of form check box may result recourcing</param>
        public AuthState(ProviderState providerState, IntentoMTFormOptions _options, bool fromForm = false) : base(providerState, _options)
        {
            this.providerState = providerState;
            formMT = providerState.form.formMT;
            options = _options;

            // set mode of checkBoxUseOwnCred
            formMT.checkBoxUseOwnCred.Enabled = true;
            if (!providerState.billable || !providerState.stock_model)  // Auth Required
            {
                formMT.groupBoxBillingAccount.Enabled = true;
                if (!fromForm)
                {
                    Internal_Change_checkBoxUseOwnCred_Checked(formMT, true);
                    formMT.checkBoxUseOwnCred.Enabled = false;
                }
                mode = StateModeEnum.required;
            }
            else if (!providerState.own_auth) // Auth Prohibited 
            {
                formMT.groupBoxBillingAccount.Enabled = false;
                if (!fromForm)
                    Internal_Change_checkBoxUseOwnCred_Checked(formMT, false);
                mode = StateModeEnum.prohibited;
            }
            else
            {   // Auth optional
                formMT.groupBoxBillingAccount.Enabled = true;
                if (!fromForm)
                    Internal_Change_checkBoxUseOwnCred_Checked(formMT, options.UseCustomAuth);
                mode = StateModeEnum.optional;
            }

            if (!providerState.IsOK)
                // smart routing 
                return;

            // Get credentials from Options
            providerDataAuthDict = new Dictionary<string, string>();
            Dictionary<string, string> auth = options.authDict();
            if (providerState.providerAuthList != null)
            {
                foreach (string key in providerState.providerAuthList)
                {
                    if (auth != null && auth.ContainsKey(key))
                        providerDataAuthDict[key] = auth[key];
                    else
                        providerDataAuthDict[key] = "";
                }
            }
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, AuthState state)
        {
            if (state == null)
            {
                Auth_GroupBox_Enabled(form.formMT, false);
                Auth_Control_BackColor_State(form.formMT, false);

                ModelState.Draw(form, null);
                GlossaryState.Draw(form, null);

                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            if (firstTimeDraw)
            {   // Need to fill all items - first tiem drawing after making AuthState
                FillCredentials();
                firstTimeDraw = false;
            }

            error_message = null;

            formMT.checkBoxUseOwnCred.Visible = true;
            if (formMT.checkBoxUseOwnCred.Checked)
            {
                // Credentials group boxes 
                if (IsDelegatedCredentials)
                {   // Delegated creds - use combo box 
                    formMT.comboBoxCredentialId.Visible = true;
                    formMT.textBoxCredentials.Visible = false;
                    formMT.buttonWizard.Visible = false;
                }
                else
                {   // Direct auth
                    formMT.comboBoxCredentialId.Visible = false;
                    formMT.textBoxCredentials.Visible = true;
                    formMT.buttonWizard.Visible = true;
                }
            }
            else
            {
                formMT.comboBoxCredentialId.Visible = false;
                formMT.textBoxCredentials.Visible = false;
                formMT.buttonWizard.Visible = false;
                formMT.panelCreateDelegatedCredential.Visible = false;
            }
            // checkBoxUseOwnCred
            if (formMT.checkBoxUseOwnCred.Checked && (providerDataAuthDict == null || providerDataAuthDict.Count == 0 || providerDataAuthDict.Any(i => string.IsNullOrEmpty(i.Value))))
            //if (formMT.checkBoxUseOwnCred.Checked && (providerDataAuthDict == null || providerDataAuthDict.Count == 0 || String.IsNullOrWhiteSpace(formMT.comboBoxCredentialId.Text)))
            {   // Credentials required but not filled in full
                Auth_Control_BackColor_State(formMT, true);
                error_message = Resource.OwnCredentialsNeededErrorMessage;
                formMT.groupBoxOptional.Enabled = false;
            }
            else
            {   // All fields in credentals are filled
                Auth_Control_BackColor_State(formMT, false);
                formMT.textBoxCredentials.Text = string.Join(", ", providerDataAuthDict.Select(i => string.Format("{0}:{1}", i.Key, i.Value)));
            }
            //formMT.buttonWizard.Visible = formMT.textBoxCredentials.Visible;
            if (!string.IsNullOrEmpty(error_message))
            {
                ModelState.Draw(form, null);
                GlossaryState.Draw(form, null);
                return error_message;
            }

            var modelSt = GetModelState();
            error_message = ModelState.Draw(form, modelSt);
            var glossarySt = GetGlossaryState();
            string error_message2 = GlossaryState.Draw(form, glossarySt);
            if (string.IsNullOrEmpty(error_message))
                error_message = error_message2;

            if (modelSt != null && modelSt.UseModel)
                formMT.groupBoxOptional.Enabled = true;
            else if (glossarySt != null && glossarySt.currentGlossary != null)
                formMT.groupBoxOptional.Enabled = true;
            else formMT.groupBoxOptional.Enabled = false;

            return error_message;
        }

        private void FillCredentials()
        {
            // Read and fill a list of delegated credentials for this provider
            if (IsDelegatedCredentials)
                FillDelegatedCredentials();
            else
                FillTextBoxCredentials();
        }

        public void Clear()
        {
            formMT.comboBoxCredentialId.Items.Clear();
            formMT.textBoxCredentials.Text = "";
        }

        private void FillDelegatedCredentials()
        {
            Clear();
            if (formMT.checkBoxUseOwnCred.Checked)
            {
                IList<dynamic> credentials = form.testAuthData != null ? form.testAuthData : form._translate.DelegatedCredentials();
                if (providerState.currentProviderId.StartsWith("ai.text.translate.google.")
                    && providerState.currentProviderId != "ai.text.translate.google.translate_api.2-0")
                {
                    credentials = credentials.Where(q =>
                        q.temporary_credentials != null
                        && q.temporary_credentials_created_at != null
                        && q.temporary_credentials_expiry_at != null
                        && q.credential_type == "google_service_account").ToList();
                }
                else
                    credentials = credentials.Where(q => q.credential_type == providerState.currentProviderId).ToList();
                _delegatedCredentials = credentials.Select(q => (string)q.credential_id).ToList();
                formMT.comboBoxCredentialId.Items.AddRange(_delegatedCredentials.ToArray());
                if (_delegatedCredentials.Count == 0)
                {
                    formMT.comboBoxCredentialId.Visible = false;
                }
                else //if (_delegatedCredentials.Count > 1)     - nned more testing to hide combo box selection in case only 1 credential is available
                {
                    formMT.comboBoxCredentialId.Items.Insert(0, "");
                    string credential_id = providerDataAuthDict["credential_id"];
                    if (formMT.comboBoxCredentialId.Items.Contains(credential_id))
                    {
                        formMT.comboBoxCredentialId.SelectedItem = credential_id;
                    }
                    //formMT.comboBoxCredentialId.Enabled = true;
                    formMT.comboBoxCredentialId.Visible = true;
                }
                //else if (form.AuthCombo_ComboBox_Count == 1)
                //{
                //    form.AuthCombo_ComboBox_SelectedIndex = 0;
                //    form.AuthCombo_ComboBox_Enabled = false;
                //    providerDataAuthDict["credential_id"] = _delegatedCredentials[0];
                // }
                formMT.panelCreateDelegatedCredential.Visible = !formMT.comboBoxCredentialId.Visible;
            }
        }

        private void FillTextBoxCredentials()
        {
            var str = string.Empty;
            if (providerDataAuthDict != null)
            {
                foreach (KeyValuePair<string, string> val in providerDataAuthDict)
                {
                    if (val.Value != null && !string.IsNullOrWhiteSpace(val.Value))
                        str += String.Format("{0}:{1} ", val.Key, val.Value);
                }
            }
            if (credentials != str.TrimEnd(' '))
                credentials = str;
            formMT.textBoxCredentials.Text = str;
        }

        // Something changed in auth settings on form. Need to clear model and glossary settings. 
        private void ClearCredentials()
        {
            if (IsDelegatedCredentials)
            {
                // For stored/delegated creds providerDataAuthDict has simple and standard structure
                providerDataAuthDict["credential_id"] = formMT.comboBoxCredentialId.Text;
                credentials = "credential_id:" + formMT.comboBoxCredentialId.Text;
            }
            else
            {
                credentials = "";
                foreach (string key in providerDataAuthDict.Keys.ToList())
                    providerDataAuthDict[key] = null;
            }
            if (!firstTimeDraw)
                ResetChildrensState();
            //ModelState.Draw(form, null);
            //GlossaryState.Draw(form, null);
        }

        private void ResetChildrensState()
        {
            modelState = new ModelState(this, options);
            modelState.ClearOptions(options);
            modelState = null;
            glossaryState = new GlossaryState(this, options);
            glossaryState.ClearOptions(options);
            glossaryState = null;

        }

        public bool UseCustomAuth
        { get { return formMT.checkBoxUseOwnCred.Checked; } }

        public static void FillOptions(AuthState state, IntentoMTFormOptions options)
        {
            if (state == null)
            {
                options.UseCustomAuth = false;
                options.CustomAuth = null;
                ModelState.FillOptions(null, options);
                GlossaryState.FillOptions(null, options);
            }
            else
            {
                options.IsAuthDelegated = state.IsDelegatedCredentials;
                options.AuthMode = state.mode;
                
                string id;
                if (state.IsDelegatedCredentials && state.providerDataAuthDict.TryGetValue("credential_id", out id))
                    options.AuthDelegatedCredentialId = id;
                else
                    options.AuthDelegatedCredentialId = null;

                options.UseCustomAuth = state.UseCustomAuth;
                if (options.UseCustomAuth)
                    options.SetAuthDict(state.providerDataAuthDict);
                else
                    options.CustomAuth = null;

                ModelState.FillOptions(state.GetModelState(), options);
                GlossaryState.FillOptions(state.GetGlossaryState(), options);
            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.UseCustomAuth = false;
            options.CustomAuth = null;
            formMT.textBoxCredentials.Visible = false;
            //formMT.buttonWizard.Visible = false;
            formMT.comboBoxCredentialId.Visible = true;
            Internal_Change_checkBoxUseOwnCred_Checked(formMT, false);
            formMT.comboBoxCredentialId.Items.Clear();
            formMT.textBoxCredentials.Text = "";
            ResetChildrensState();
        }

        #region Events

        public void buttonWizard_Click()
        {
            var dialog = new IntentoFormProviderAuthWizard(providerDataAuthDict, form.formApi.checkBoxShowHidden.Checked, options.HideHiddenTextButton);
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.OK)
            {
                providerDataAuthDict = dialog.authParam;
                modelState = null;
                glossaryState = null;
                FillTextBoxCredentials();
            }
            EnableDisable();
        }

        public void comboBoxCredentialId_SelectedIndexChanged()
        {
            ClearCredentials();
            //ClearOptions(options);
            EnableDisable();
        }

        public void checkBoxUseOwnCred_CheckedChanged()
        {
            if (!firstTimeDraw && !internalControlChange)
            {
                //ClearCredentials();
                GetModelState()?.ClearOptions(options);
                GetGlossaryState()?.ClearOptions(options);

                //ModelState.Draw(form, null);
                //GlossaryState.Draw(form, null);


                formMT.comboBoxCredentialId.Items.Clear();
                formMT.textBoxCredentials.Clear();
                if (formMT.checkBoxUseOwnCred.Checked)
                    FillCredentials();
                firstTimeDraw = true;
                EnableDisable();
                internalControlChange = false;
            }
        }

        #endregion Events

        #region Properties

        public bool IsDelegatedCredentials
        { get { return providerState.delegated_credentials; } }

        // True in case auth is entered or selected
        public bool IsSelected
        {
            get
            {
                if (IsDelegatedCredentials)
                    return !string.IsNullOrEmpty((string)formMT.comboBoxCredentialId.SelectedItem);
                else
                    return !providerDataAuthDict.Values.Any(i => string.IsNullOrEmpty(i));
            }
        }

        public bool IsOK
        {
            get
            {
                switch (mode)
                {
                    case StateModeEnum.required:
                        return IsSelected;
                    case StateModeEnum.optional:
                        if (UseCustomAuth)
                            return IsSelected;
                        return true;
                    case StateModeEnum.prohibited:
                        return true;
                    default:
                        throw new Exception(string.Format("Invalid mode {0}", mode));
                }
            }
        }

        public ModelState GetModelState()
        {
            if (IsOK)
            {
                if (modelState == null)
                    modelState = new ModelState(this, options);
            }
            else
                modelState = null;
            return modelState;
        }

        public GlossaryState GetGlossaryState()
        {
            if (IsOK)
            {
                if (glossaryState == null)
                    glossaryState = new GlossaryState(this, options);
            }
            else
                glossaryState = null;
            return glossaryState;
        }

        #endregion Properties

        #region methods for managing a group of controls

        static void Auth_GroupBox_Enabled(IntentoFormOptionsMT formMT, bool enabled)
        {
            if (enabled)
                formMT.groupBoxBillingAccount.Enabled = true;
            else
            {
                formMT.groupBoxBillingAccount.Enabled = false;
                formMT.textBoxCredentials.Visible = false;
                //formMT.buttonWizard.Visible = false;
                formMT.comboBoxCredentialId.Visible = false;
                Internal_Change_checkBoxUseOwnCred_Checked(formMT, false);
                formMT.comboBoxCredentialId.Items.Clear();
                formMT.textBoxCredentials.Text = "";
                formMT.panelCreateDelegatedCredential.Visible = false;

            }
        }

        static void Internal_Change_checkBoxUseOwnCred_Checked(IntentoFormOptionsMT formMT, bool value)
        {
            internalControlChange = formMT.checkBoxUseOwnCred.Checked != value;
            formMT.checkBoxUseOwnCred.Checked = value;
        }

        static void Auth_Control_BackColor_State(IntentoFormOptionsMT formMT, bool hasErrors)
        {
            formMT.comboBoxCredentialId.BackColor = hasErrors ? Color.LightPink : SystemColors.Window;
            formMT.textBoxCredentials.BackColor = hasErrors ? Color.LightPink : SystemColors.Window;
            //if (hasErrors)
            //{
            //    formMT.comboBoxCredentialId.BackColor = Color.LightPink;
            //    formMT.textBoxCredentials.BackColor = Color.LightPink;
            //}
            //else
            //{
            //    formMT.comboBoxCredentialId.BackColor = formMT.checkBoxUseOwnCred.Checked ? SystemColors.Window : SystemColors.Control;
            //    formMT.textBoxCredentials.BackColor = formMT.checkBoxUseOwnCred.Checked ? SystemColors.Window : SystemColors.Control;
            //}
        }

        #endregion methods for managing a group of controls

    }
}
