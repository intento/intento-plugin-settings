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
    public class AuthState
    {
        IntentoTranslationProviderOptionsForm form;
        ProviderState providerState;
        System.Windows.Forms.GroupBox groupBoxAuthCredentialId;
        System.Windows.Forms.CheckBox checkBoxUseOwnCred;
        System.Windows.Forms.ComboBox comboBoxCredentialId;
        System.Windows.Forms.GroupBox groupBoxAuth;
        System.Windows.Forms.TextBox textBoxCredentials;
        private IntentoAiTextTranslate _translate;
        private List<string> _delegatedCredentials;
        string mode; // optional, required, prohibited, hidden
        string error_message;
        IntentoMTFormOptions options;
        bool useComboBox;

        // current credentials 
        public Dictionary<string, string> providerDataAuthDict;

        public AuthState(IntentoTranslationProviderOptionsForm _form, System.Windows.Forms.CheckBox _checkBoxUseOwnCred, 
            System.Windows.Forms.GroupBox _groupBoxAuthCredentialId, System.Windows.Forms.ComboBox _comboBoxCredentialId,
            System.Windows.Forms.GroupBox _groupBoxAuth, System.Windows.Forms.TextBox _textBoxCredentials, IntentoMTFormOptions _options)
        {
            form = _form;
            options = _options;
            providerState = _form.providerState;
            groupBoxAuthCredentialId = _groupBoxAuthCredentialId;
            checkBoxUseOwnCred = _checkBoxUseOwnCred;
            comboBoxCredentialId = _comboBoxCredentialId;
            groupBoxAuth = _groupBoxAuth;
            textBoxCredentials = _textBoxCredentials;
            _translate = form._translate;

            // set mode of checkBoxUseOwnCred
            if (!providerState.IsOK)
            {   // smart routing
                mode = "hidden";
                checkBoxUseOwnCred.Visible = false;
                checkBoxUseOwnCred.Checked = false;
            }
            if (!providerState.billable || !providerState.stock_model)  //Required
            {
                checkBoxUseOwnCred.Visible = true;
                checkBoxUseOwnCred.Enabled = false;
                checkBoxUseOwnCred.Checked = true;
                mode = "required";
            }
            else if (!providerState.own_auth) //Prohibited 
            {
                checkBoxUseOwnCred.Visible = true;
                checkBoxUseOwnCred.Enabled = false;
                checkBoxUseOwnCred.Checked = false;
                mode = "prohibited";
            }
            else
            {   // optional
                checkBoxUseOwnCred.Visible = true;
                checkBoxUseOwnCred.Enabled = true;
                checkBoxUseOwnCred.Checked = options.UseCustomAuth;
                mode = "optional";
            }
        }

        public void Fill()
        {
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
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, AuthState state)
        {
            if (state == null)
            {
                form.checkBoxUseOwnCred.Visible = false;
                form.groupBoxAuthCredentialId.Visible = false;
                form.groupBoxAuth.Visible = false;
                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            error_message = null;
            if (!providerState.IsOK || mode == "hidden")
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
                foreach(string key in providerState.providerAuthList)
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
            if (textBoxCredentials.Text != str.TrimEnd(' '))
                textBoxCredentials.Text = str;
        }

        public void checkBoxUseOwnCred_CheckedChanged()
        {
            groupBoxAuth.Enabled = checkBoxUseOwnCred.Checked;
            textBoxCredentials.Clear();
            //if (!checkBoxUseOwnCred.Checked)
            //    Options.SetAuthDict(null);
            groupBoxAuthCredentialId.Enabled = checkBoxUseOwnCred.Checked;
            comboBoxCredentialId.Items.Clear();
            if (checkBoxUseOwnCred.Checked && providerState.delegated_credentials)
            {
                comboBoxCredentialId.Items.AddRange(_delegatedCredentials.ToArray());
                if (_delegatedCredentials.Count > 1) comboBoxCredentialId.Items.Insert(0, "");
                providerDataAuthDict = new Dictionary<string, string>();
                providerDataAuthDict.Add("credential_id", "");
            }

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
            providerDataAuthDict["credential_id"] = comboBoxCredentialId.Text;
            textBoxCredentials.Text = string.IsNullOrEmpty(comboBoxCredentialId.Text) ? string.Empty : "credential_id:" + comboBoxCredentialId.Text;
        }

        public bool UseCustomAuth
        { get { return checkBoxUseOwnCred.Checked; } }

        // True in case auth is entered or selected
        public bool IsSelected
        {
            get
            {
                if (!UseCustomAuth)
                    return false;
                if (useComboBox)
                    return !string.IsNullOrEmpty((string)comboBoxCredentialId.SelectedItem);
                return !string.IsNullOrEmpty(textBoxCredentials.Text);
            }
        }

    }
}
