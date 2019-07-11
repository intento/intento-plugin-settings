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
    public class AuthState
    {
        public IntentoTranslationProviderOptionsForm form;
        public ProviderState providerState;
        public ModelState modelState;
        public GlossaryState glossaryState;

        System.Windows.Forms.GroupBox groupBoxAuthCredentialId;
        System.Windows.Forms.CheckBox checkBoxUseOwnCred;
        System.Windows.Forms.ComboBox comboBoxCredentialId;
        System.Windows.Forms.GroupBox groupBoxAuth;
        System.Windows.Forms.TextBox textBoxCredentials;
        private IntentoAiTextTranslate _translate;
        private List<string> _delegatedCredentials;
        string error_message;
        IntentoMTFormOptions options;
        bool useComboBox;

        // credentials (in json format) choosen by user, both based on credential_id and on auth. 
        string credentials;

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
        public AuthState(ProviderState providerState, IntentoMTFormOptions _options, bool fromForm = false)
        {
            this.providerState = providerState;
            form = providerState.form;
            options = _options;
            providerState = form.providerState;
            groupBoxAuthCredentialId = form.groupBoxAuthCredentialId;
            checkBoxUseOwnCred = form.checkBoxUseOwnCred;
            comboBoxCredentialId = form.comboBoxCredentialId;
            groupBoxAuth = form.groupBoxAuth;
            textBoxCredentials = form.textBoxCredentials;
            _translate = form._translate;

            // set mode of checkBoxUseOwnCred
            if (!providerState.IsOK)
            {   // smart routing
                mode = EnumMode.hidden;
                checkBoxUseOwnCred.Visible = false;
                if (!fromForm)
                    checkBoxUseOwnCred.Checked = false;
            }
            if (!providerState.billable || !providerState.stock_model)  //Required
            {
                checkBoxUseOwnCred.Visible = true;
                checkBoxUseOwnCred.Enabled = false;
                if (!fromForm)
                    checkBoxUseOwnCred.Checked = true;
                mode = EnumMode.required;
            }
            else if (!providerState.own_auth) //Prohibited 
            {
                checkBoxUseOwnCred.Visible = true;
                checkBoxUseOwnCred.Enabled = false;
                if (!fromForm)
                    checkBoxUseOwnCred.Checked = false;
                mode = EnumMode.prohibited;
            }
            else
            {   // optional
                checkBoxUseOwnCred.Visible = true;
                checkBoxUseOwnCred.Enabled = true;
                if (!fromForm)
                    checkBoxUseOwnCred.Checked = options.UseCustomAuth;
                mode = EnumMode.optional;
            }

            if (!providerState.IsOK)
                // smart routing 
                return;

            GetOptionsCredentials();

            useComboBox = providerState.delegated_credentials;

            // Read and fill a list of delegated credentials for this provider
            if (providerState.delegated_credentials)
            {
                comboBoxCredentialId.Items.Clear();
                var credentials = _translate.DelegatedCredentials();
                var cred = credentials.Where(q => q.temporary_credentials != null
                    && q.temporary_credentials_created_at != null
                    && q.temporary_credentials_expiry_at != null);
                _delegatedCredentials = cred.Select(q => (string)q.credential_id).ToList();
                comboBoxCredentialId.Items.AddRange(_delegatedCredentials.ToArray());
                if (_delegatedCredentials.Count > 1)
                {
                    comboBoxCredentialId.Items.Insert(0, "");
                    string credential_id = providerDataAuthDict["credential_id"];
                    if (comboBoxCredentialId.Items.Contains(credential_id))
                        comboBoxCredentialId.SelectedItem = credential_id;
                    comboBoxCredentialId.Enabled = true;
                }
                else if (comboBoxCredentialId.Items.Count == 1)
                {
                    comboBoxCredentialId.SelectedIndex = 0;
                    comboBoxCredentialId.Enabled = false;
                }
                else
                    comboBoxCredentialId.Enabled = false;
            }
            else
                filltextBoxCredentials();

            modelState = new ModelState(this, form.GetOptions());
            glossaryState = new GlossaryState(this, form.GetOptions());
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, AuthState state)
        {
            if (state == null)
            {
                form.checkBoxUseOwnCred.Visible = false;
                form.groupBoxAuthCredentialId.Visible = false;
                form.groupBoxAuth.Visible = false;

                ModelState.Draw(form, null);
                GlossaryState.Draw(form, null);

                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            ModelState.Draw(form, modelState);
            GlossaryState.Draw(form, glossaryState);

            error_message = null;
            if (!providerState.IsOK || mode == EnumMode.hidden)
            {   // smart routing
                checkBoxUseOwnCred.Visible = false;
                groupBoxAuthCredentialId.Visible = false;
                groupBoxAuth.Visible = false;
                return null;
            }

            // Credentials group boxes 
            if (useComboBox)
            {   // Delegated creds - use combo box 
                groupBoxAuthCredentialId.Visible = true;
                groupBoxAuthCredentialId.Enabled = true;
                groupBoxAuthCredentialId.BringToFront();
                groupBoxAuth.Visible = false;
                comboBoxCredentialId.Visible = true;
            }
            else
            {
                groupBoxAuthCredentialId.Visible = false;
                groupBoxAuth.Visible = true;
                groupBoxAuth.Enabled = true;
                groupBoxAuth.BringToFront();
                textBoxCredentials.Visible = true;
            }

            comboBoxCredentialId.Enabled = checkBoxUseOwnCred.Checked;
            textBoxCredentials.Enabled = checkBoxUseOwnCred.Checked;

            // checkBoxUseOwnCred
            if (checkBoxUseOwnCred.Checked && (providerDataAuthDict == null || providerDataAuthDict.Any(x => string.IsNullOrWhiteSpace(x.Value))))
            {
                textBoxCredentials.BackColor = Color.LightPink;
                comboBoxCredentialId.BackColor = Color.LightPink;
                error_message = "You must provide your own credentials for this provider. ";
            }
            else
            {
                textBoxCredentials.BackColor = SystemColors.Window;
                comboBoxCredentialId.BackColor = SystemColors.Window;
            }

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

        private void filltextBoxCredentials()
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

        public static void checkBoxUseOwnCred_CheckedChanged(ProviderState providerState, IntentoMTFormOptions options)
        {
            providerState.form.Cursor = Cursors.WaitCursor;

            options.UseCustomAuth = providerState.form.checkBoxUseOwnCred.Checked;
            providerState.authState = new AuthState(providerState, options, true);

            providerState.form.Cursor = Cursors.Default;
            providerState.form.EnableDisable();
        }

        public void buttonWizard_Click()
        {
            var dialog = new IntentoTranslationProviderAuthWizardForm(providerDataAuthDict, form.checkBoxShowHidden.Checked);
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.OK)
            {
                providerDataAuthDict = dialog.authParam;
                filltextBoxCredentials();
            }
        }

        public void comboBoxCredentialId_SelectedIndexChanged()
        {
            if (string.IsNullOrEmpty(comboBoxCredentialId.Text))
            {
                credentials = "";
                providerDataAuthDict = new Dictionary<string, string>();
            }
            else
            {
                providerDataAuthDict["credential_id"] = comboBoxCredentialId.Text;
                credentials = "credential_id:" + comboBoxCredentialId.Text;
            }
        }

        public bool UseCustomAuth
        { get { return checkBoxUseOwnCred.Checked; } }

        // True in case auth is entered or selected
        public bool IsSelected
        {
            get
            {
                if (useComboBox)
                    return !string.IsNullOrEmpty((string)comboBoxCredentialId.SelectedItem);
                return !string.IsNullOrEmpty(textBoxCredentials.Text) && providerDataAuthDict.Values.Any(i => string.IsNullOrEmpty(i));
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

        public void FillOptions(IntentoMTFormOptions options)
        {
            if (options.SmartRouting)
            {
                options.UseCustomAuth = false;
                options.CustomAuth = null;
            }
            else
            {
                options.UseCustomAuth = UseCustomAuth;
                if (options.UseCustomAuth)
                    options.SetAuthDict(providerDataAuthDict);
                else
                    options.CustomAuth = null;
            }

            if (modelState != null)
                modelState.FillOptions(options);
            if (glossaryState != null)
                glossaryState.FillOptions(options);
        }
    }
}
