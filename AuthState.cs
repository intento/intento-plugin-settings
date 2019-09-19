using Intento.MT.Plugin.PropertiesForm;
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
    public class AuthState : BaseState
    {
        // Parent component
        public ProviderState providerState;

        // Controlled components
        private ModelState modelState;
        private GlossaryState glossaryState;

        private List<string> _delegatedCredentials;
        string error_message;
        bool useComboBox;

        // credentials (in json format) choosen by user, both based on credential_id and on auth. 
        string credentials;

        bool firstTime = true;

        public enum EnumMode
        {
            optional, required, prohibited, hidden
        }
        EnumMode mode;

        // current credentials 
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
            form = providerState.form;
            options = _options;

            // set mode of checkBoxUseOwnCred
            if (!providerState.billable || !providerState.stock_model)  //Required
            {
                form.Auth_CheckBox_Enabled = false;
                if (!fromForm)
                    form.Auth_CheckBox_Checked = true;
                mode = EnumMode.required;
            }
            else if (!providerState.own_auth) //Prohibited 
            {
                form.Auth_CheckBox_Enabled = false;
                if (!fromForm)
                    form.Auth_CheckBox_Checked = false;
                mode = EnumMode.prohibited;
            }
            else
            {   // optional
                form.Auth_CheckBox_Enabled = true;
                if (!fromForm)
                    form.Auth_CheckBox_Checked = options.UseCustomAuth;
                mode = EnumMode.optional;
            }

            if (!providerState.IsOK)
                // smart routing 
                return;

            GetOptionsCredentials();

            useComboBox = providerState.delegated_credentials;
        }

        public static string Draw(IForm form, AuthState state)
        {
            if (state == null)
            {
                form.Auth_CheckBox_Visible = false;
                form.AuthText_Group_Visible = false;
                form.AuthCombo_Group_Visible = false;

                ModelState.Draw(form, null);
                GlossaryState.Draw(form, null);

                return null;
            }
            return state.Draw();
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

        public string Draw()
        {
            if (firstTime)
            {   // Need to fill all items - first tiem drawing after making AuthState
                firstTime = false;

                // Read and fill a list of delegated credentials for this provider
                if (providerState.delegated_credentials)
                {
                    form.AuthCombo_ComboBox_Clear();
                    IList<dynamic> credentials = form.DelegatedCredentials();
                    IEnumerable<dynamic> cred = credentials.Where(q => q.temporary_credentials != null
                        && q.temporary_credentials_created_at != null
                        && q.temporary_credentials_expiry_at != null);
                    _delegatedCredentials = cred.Select(q => (string)q.credential_id).ToList();
                    form.AuthCombo_ComboBox_AddRange(_delegatedCredentials.ToArray());
                    if (_delegatedCredentials.Count > 1)
                    {
                        form.AuthCombo_ComboBox_Insert(0, "");
                        string credential_id = providerDataAuthDict["credential_id"];
                        if (form.AuthCombo_ComboBox_Contains(credential_id))
                            form.AuthCombo_ComboBox_SelectedItem = credential_id;
                        form.AuthCombo_ComboBox_Enabled = true;
                    }
                    else if (form.AuthCombo_ComboBox_Count == 1)
                    {
                        form.AuthCombo_ComboBox_SelectedIndex = 0;
                        form.AuthCombo_ComboBox_Enabled = false;
                        providerDataAuthDict["credential_id"] = _delegatedCredentials[0];
                    }
                    else
                        form.AuthCombo_ComboBox_Enabled = false;
                }
                else
                    FilltextBoxCredentials();
            }

            error_message = null;

            form.Auth_CheckBox_Visible = true;
            if (form.Auth_CheckBox_Checked)
            {
                // Credentials group boxes 
                if (useComboBox)
                {   // Delegated creds - use combo box 
                    form.AuthCombo_Group_Visible = true;
                    form.AuthText_Group_Visible = false;
                    // groupBoxAuthCredentialId.BringToFront();
                }
                else
                {   // Direct auth
                    form.AuthCombo_Group_Visible = false;
                    form.AuthText_Group_Visible = true;
                    // groupBoxAuth.BringToFront();
                }
            }
            else
            {
                form.AuthCombo_Group_Visible = false;
                form.AuthText_Group_Visible = false;
            }

            // comboBoxCredentialId.Enabled = checkBoxUseOwnCred.Checked;
            // textBoxCredentials.Enabled = checkBoxUseOwnCred.Checked;

            // checkBoxUseOwnCred
            if (form.Auth_CheckBox_Checked && (providerDataAuthDict == null || providerDataAuthDict.Count == 0 || providerDataAuthDict.Any(i => string.IsNullOrEmpty(i.Value))))
            {   // Credentials required but not filled in full
                form.AuthText_TextBox_BackColor = Color.LightPink;
                form.AuthCombo_ComboBox_BackColor = Color.LightPink;
                error_message = "You must provide your own credentials for this provider. ";
            }
            else
            {   // All fields in credentals are filled
                form.AuthText_TextBox_BackColor = SystemColors.Window;
                form.AuthCombo_ComboBox_BackColor = SystemColors.Window;
                form.AuthText_TextBox_Text = string.Join(", ", providerDataAuthDict.Select(i => string.Format("{0}:{1}", i.Key, i.Value)));
            }
            if (!string.IsNullOrEmpty(error_message))
                return error_message;

            error_message = ModelState.Draw(form, GetModelState());
            string error_message2 = GlossaryState.Draw(form, GetGlossaryState());
            if (string.IsNullOrEmpty(error_message))
                error_message = error_message2;

            return error_message;
        }

        private void GetOptionsCredentials()
        {
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

        private void FilltextBoxCredentials()
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

        public void checkBoxUseOwnCred_CheckedChanged()
        {
            options.UseCustomAuth = form.Auth_CheckBox_Checked;

            EnableDisable();
        }

        public void buttonWizard_Click()
        {
            var dialog = new IntentoTranslationProviderAuthWizardForm(providerDataAuthDict, form.ShowHidden_CheckBox_Checked);
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.OK)
            {
                providerDataAuthDict = dialog.authParam;
                FilltextBoxCredentials();
            }
            EnableDisable();
        }

        public void comboBoxCredentialId_SelectedIndexChanged()
        {
            if (string.IsNullOrEmpty(form.AuthCombo_ComboBox_Text))
            {
                credentials = "";
                providerDataAuthDict = new Dictionary<string, string>();
            }
            else
            {
                providerDataAuthDict["credential_id"] = form.AuthCombo_ComboBox_Text;
                credentials = "credential_id:" + form.AuthCombo_ComboBox_Text;
            }
            modelState = null;
            glossaryState = null;

            EnableDisable();
        }

        public bool UseCustomAuth
        { get { return form.Auth_CheckBox_Checked; } }

        // True in case auth is entered or selected
        public bool IsSelected
        {
            get
            {
                if (useComboBox)
                    return !string.IsNullOrEmpty((string)form.AuthCombo_ComboBox_SelectedItem);
                return !providerDataAuthDict.Values.Any(i => string.IsNullOrEmpty(i));
            }
        }

        public bool IsOK
        {
            get
            {
                if (mode == EnumMode.hidden)
                    return false;

                // check if auth data is not filled in full
                if (providerDataAuthDict != null && providerDataAuthDict.Count != 0)
                {
                    if (providerDataAuthDict.Values.Any(i => string.IsNullOrEmpty(i)))
                        return false;
                }

                if (mode == EnumMode.required)
                    return IsSelected;

                if (mode == EnumMode.optional)
                {
                    if (UseCustomAuth && !IsSelected)
                        return false;
                    return true;
                }

                return true;
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
            form.AuthCombo_ComboBox_Clear();

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
    }
}
