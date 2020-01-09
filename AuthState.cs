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
            //form = providerState.form;
            formMT = providerState.form.formMT;
            options = _options;

            // set mode of checkBoxUseOwnCred
            //form.Auth_CheckBox_Enabled = true;
            formMT.checkBoxUseOwnCred.Enabled = true;
            if (!providerState.billable || !providerState.stock_model)  // Auth Required
            {
                //form.Auth_GroupBox_Enabled = false;
                formMT.groupBoxBillingAccount.Enabled = false;
                if (!fromForm)
                {
                    //form.Auth_CheckBox_Checked = true;
                    //form.Auth_CheckBox_Enabled = false;
                    formMT.checkBoxUseOwnCred.Checked = true;
                    formMT.checkBoxUseOwnCred.Enabled = false;
                }
                mode = StateModeEnum.required;
            }
            else if (!providerState.own_auth) // Auth Prohibited 
            {
                //form.Auth_GroupBox_Enabled = false;
                formMT.groupBoxBillingAccount.Enabled = false;
                if (!fromForm)
                    formMT.checkBoxUseOwnCred.Checked = false;
                    //form.Auth_CheckBox_Checked = false;
                mode = StateModeEnum.prohibited;
            }
            else
            {   // Auth optional
                //form.Auth_GroupBox_Enabled = true;
                formMT.groupBoxBillingAccount.Enabled = true;
                if (!fromForm)
                    formMT.checkBoxUseOwnCred.Checked = options.UseCustomAuth;
                    //form.Auth_CheckBox_Checked = options.UseCustomAuth;
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
                //form.Auth_CheckBox_Visible = false;
                //form.AuthText_Group_Visible = false;
                //form.AuthCombo_Group_Visible = false;
                Auth_GroupBox_Enabled(form.formMT, false);
                Auth_Control_BackColor_State(form.formMT, false);

                ModelState.Draw(form, null);
                GlossaryState.Draw(form, null);

                return null;
            }
            return state.Draw();
        }

        public bool IsDelegatedCredentials
        { get { return providerState.delegated_credentials; } }

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
            Auth_Control_Clear(formMT);
        }

        private void FillDelegatedCredentials()
        {
            Clear();
            IList<dynamic> credentials = form._translate.DelegatedCredentials();
            IEnumerable<dynamic> cred = credentials.Where(q => q.temporary_credentials != null
                && q.temporary_credentials_created_at != null
                && q.temporary_credentials_expiry_at != null);
            _delegatedCredentials = cred.Select(q => (string)q.credential_id).ToList();
            //form.AuthCombo_ComboBox_AddRange(_delegatedCredentials.ToArray());
            formMT.comboBoxCredentialId.Items.AddRange(_delegatedCredentials.ToArray());
            if (_delegatedCredentials.Count == 0)
                formMT.comboBoxCredentialId.Enabled = false;
                //form.AuthCombo_ComboBox_Enabled = false;
            else //if (_delegatedCredentials.Count > 1)     - nned more testing to hide combo box selection in case only 1 credential is available
            {
                //form.AuthCombo_ComboBox_Insert(0, "");
                formMT.comboBoxCredentialId.Items.Insert(0, "");
                string credential_id = providerDataAuthDict["credential_id"];
                //if (form.AuthCombo_ComboBox_Contains(credential_id))
                if (formMT.comboBoxCredentialId.Items.Contains(credential_id))
                {
                    //form.AuthCombo_ComboBox_SelectedItem = credential_id;
                    formMT.comboBoxCredentialId.SelectedItem = credential_id;
                }
                //form.AuthCombo_ComboBox_Enabled = true;
                formMT.comboBoxCredentialId.Enabled = true;
            }
            //else if (form.AuthCombo_ComboBox_Count == 1)
            //{
            //    form.AuthCombo_ComboBox_SelectedIndex = 0;
            //    form.AuthCombo_ComboBox_Enabled = false;
            //    providerDataAuthDict["credential_id"] = _delegatedCredentials[0];
            // }
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
        }

        public string Draw()
        {
            if (firstTimeDraw)
            {   // Need to fill all items - first tiem drawing after making AuthState
                firstTimeDraw = false;
                FillCredentials();
            }

            error_message = null;

            //form.Auth_CheckBox_Visible = true;
            formMT.checkBoxUseOwnCred.Visible = true;
            //if (form.Auth_CheckBox_Checked)
            if (formMT.checkBoxUseOwnCred.Checked)
            {
                // Credentials group boxes 
                if (IsDelegatedCredentials)
                {   // Delegated creds - use combo box 
                    //form.AuthCombo_Group_Visible = true;
                    //form.AuthText_Group_Visible = false;
                    formMT.comboBoxCredentialId.Visible = true;
                    formMT.textBoxCredentials.Visible = false;
                    // groupBoxAuthCredentialId.BringToFront();
                }
                else
                {   // Direct auth
                    //form.AuthCombo_Group_Visible = false;
                    //form.AuthText_Group_Visible = true;
                    formMT.comboBoxCredentialId.Visible = false;
                    formMT.textBoxCredentials.Visible = true;
                    // groupBoxAuth.BringToFront();
                }
            }
            else
            {
                //form.AuthCombo_Group_Visible = false;
                //form.AuthText_Group_Visible = false;
                formMT.comboBoxCredentialId.Visible = false;
                formMT.textBoxCredentials.Visible = false;
            }
            // checkBoxUseOwnCred
            //if (form.Auth_CheckBox_Checked && (providerDataAuthDict == null || providerDataAuthDict.Count == 0 || providerDataAuthDict.Any(i => string.IsNullOrEmpty(i.Value))))
            if (formMT.checkBoxUseOwnCred.Checked && (providerDataAuthDict == null || providerDataAuthDict.Count == 0 || providerDataAuthDict.Any(i => string.IsNullOrEmpty(i.Value))))
            {   // Credentials required but not filled in full
                Auth_Control_BackColor_State(formMT, true);
                Auth_Control_BackColor_State(formMT, true);
                error_message = Resource.OwnCredentialsNeededErrorMessage;
            }
            else
            {   // All fields in credentals are filled
                Auth_Control_BackColor_State(formMT, false);
                //form.AuthText_TextBox_Text = string.Join(", ", providerDataAuthDict.Select(i => string.Format("{0}:{1}", i.Key, i.Value)));
                formMT.textBoxCredentials.Text = string.Join(", ", providerDataAuthDict.Select(i => string.Format("{0}:{1}", i.Key, i.Value)));
            }
            if (!string.IsNullOrEmpty(error_message))
            {
                ModelState.Draw(form, null);
                GlossaryState.Draw(form, null);
                return error_message;
            }

            error_message = ModelState.Draw(form, GetModelState());
            string error_message2 = GlossaryState.Draw(form, GetGlossaryState());
            if (string.IsNullOrEmpty(error_message))
                error_message = error_message2;

            return error_message;
        }

        public void checkBoxUseOwnCred_CheckedChanged()
        {
            // options.UseCustomAuth = form.Auth_CheckBox_Checked;

            ClearCredentials();
            ClearOptions(options);
            //if (form.Auth_CheckBox_Checked)
            if (formMT.checkBoxUseOwnCred.Checked)
                FillCredentials();
            firstTimeDraw = true;
            EnableDisable();
        }

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
            ClearOptions(options);
            EnableDisable();
        }

        // Something changed in auth settings on form. Need to clear model and glossary settings. 
        private void ClearCredentials()
        {
            if (IsDelegatedCredentials)
            {
                // For stored/delegated creds providerDataAuthDict has simple and standard structure
                //providerDataAuthDict["credential_id"] = form.AuthCombo_ComboBox_Text;
                //credentials = "credential_id:" + form.AuthCombo_ComboBox_Text;
                providerDataAuthDict["credential_id"] = formMT.comboBoxCredentialId.Text;
                credentials = "credential_id:" + formMT.comboBoxCredentialId.Text;
            }
            else
            {
                // 
                credentials = "";
                foreach (string key in providerDataAuthDict.Keys.ToList())
                    providerDataAuthDict[key] = null;
            }
            modelState = null;
            glossaryState = null;
        }

        public bool UseCustomAuth
        //        { get { return form.Auth_CheckBox_Checked; } }
        { get { return formMT.checkBoxUseOwnCred.Checked; } }

        // True in case auth is entered or selected
        public bool IsSelected
        {
            get
            {
                if (IsDelegatedCredentials)
                    return !string.IsNullOrEmpty((string)formMT.comboBoxCredentialId.SelectedItem);
                    //return !string.IsNullOrEmpty((string)form.AuthCombo_ComboBox_SelectedItem);
                else
                    return !providerDataAuthDict.Values.Any(i => string.IsNullOrEmpty(i));
            }
        }

        public bool IsOK
        {
            get
            {
                switch(mode)
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

/*
                // check if auth data is not filled in full
                if (providerDataAuthDict != null && providerDataAuthDict.Count != 0)
                {
                    if (providerDataAuthDict.Values.Any(i => string.IsNullOrEmpty(i)))
                        return false;
                }

                if (mode == StateModeEnum.required)
                    return IsSelected;

                if (mode == StateModeEnum.optional)
                {
                    if (UseCustomAuth && !IsSelected)
                        return false;
                    return true;
                }

                return true;
                */
            }
        }

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

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.UseCustomAuth = false;
            options.CustomAuth = null;
            //form.Auth_GroupBox_Disable();
            Auth_GroupBox_Enabled(formMT, false);

            if (modelState != null)
            {
                modelState.ClearOptions(options);
                modelState = null;
            }
            if (glossaryState != null)
            {
                glossaryState.ClearOptions(options);
                glossaryState = null;
            }
        }

        #region methods for managing a group of controls

        static void Auth_GroupBox_Enabled(IntentoFormOptionsMT formMT, bool enabled)
        {
            if (enabled)
                formMT.groupBoxBillingAccount.Enabled = true;
            else
            {
                formMT.groupBoxBillingAccount.Enabled = false;
                formMT.textBoxCredentials.Visible = false;
                formMT.buttonWizard.Visible = false;
                formMT.comboBoxCredentialId.Visible = true;
                formMT.checkBoxUseOwnCred.Checked = false;
                Auth_Control_Clear(formMT);
            }
        }

        static void Auth_Control_Clear(IntentoFormOptionsMT formMT)
        {
            formMT.comboBoxCredentialId.Items.Clear();
            formMT.textBoxCredentials.Text = "";
        }

        static void Auth_Control_BackColor_State(IntentoFormOptionsMT formMT, bool hasErrors)
        {
            if (hasErrors)
            {
                formMT.comboBoxCredentialId.BackColor = Color.LightPink;
                formMT.textBoxCredentials.BackColor = Color.LightPink;
            }
            else
            {
                formMT.comboBoxCredentialId.BackColor = formMT.checkBoxUseOwnCred.Checked ? Color.White : SystemColors.Control;
                formMT.textBoxCredentials.BackColor = formMT.checkBoxUseOwnCred.Checked ? Color.White : SystemColors.Control;
            }
        }

        #endregion methods for managing a group of controls

    }
}
