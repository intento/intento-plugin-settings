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
using Intento.MT.Plugin.PropertiesForm.Models;
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
        private ProvideAgnosticGlossaryState providerAgnosticGlossary;

        private object[] сonnectedAccounts;
        string error_message;

        bool firstTimeDraw = true;

        // Indicator of change of control status from the inside
        // in this case no change event call is required
        public static bool internalControlChange = false;

		private const string DefaultAccountName = "via Intento";

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
            if (!providerState.billable || !providerState.stock_model)  // Auth Required
            {
                formMT.groupBoxBillingAccount.Enabled = true;
                //if (!fromForm)
                //{
                //    Internal_Change_checkBoxUseOwnCred_Checked(formMT, true);
                //    formMT.checkBoxUseOwnCred.Enabled = false;
                //}
                mode = StateModeEnum.required;
            }
            else if (!providerState.own_auth) // Auth Prohibited 
            {
                formMT.groupBoxBillingAccount.Enabled = false;
                //if (!fromForm)
                //    Internal_Change_checkBoxUseOwnCred_Checked(formMT, false);
                mode = StateModeEnum.prohibited;
            }
            else
            {   // Auth optional
                formMT.groupBoxBillingAccount.Enabled = true;
                //if (!fromForm)
                //    Internal_Change_checkBoxUseOwnCred_Checked(formMT, options.UseCustomAuth);
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
                ProvideAgnosticGlossaryState.Draw(form, null);

                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            if (firstTimeDraw && formMT.comboBoxCredentialId.SelectedIndex < 0)
            {   // Need to fill all items - first tiem drawing after making AuthState
                FillCredentials();
                firstTimeDraw = false;
            }

            error_message = null;

            if (formMT.groupBoxBillingAccount.Enabled)
            {
                formMT.comboBoxCredentialId.Visible = true;
				if (mode == StateModeEnum.required)
					formMT.comboBoxCredentialId.Items.Remove(DefaultAccountName);
				else if (formMT.comboBoxCredentialId.SelectedIndex < 0)
				{
					formMT.comboBoxCredentialId.SelectedIndexChanged -= providerState.form.comboBoxCredentialId_SelectedIndexChanged;
                    var index = formMT.comboBoxCredentialId.Items.IndexOf(new ListItem { Value = DefaultAccountName });
                    if (index >= 0)
                    {
                        formMT.comboBoxCredentialId.SelectedIndex = index;
                    }
                    formMT.comboBoxCredentialId.SelectedIndexChanged += providerState.form.comboBoxCredentialId_SelectedIndexChanged;
				}
				if (mode == StateModeEnum.required && formMT.comboBoxCredentialId.SelectedIndex==-1)
				{// Credentials required but not filled in full
					Auth_Control_BackColor_State(formMT, true);
					error_message = Resource.OwnCredentialsNeededErrorMessage;
					// temporary! formMT.groupBoxOptional.Enabled = false;
				}
				else
				{// All fields in credentals are filled
					Auth_Control_BackColor_State(formMT, false);
				}
			}
            else
            {
                formMT.comboBoxCredentialId.Visible = false;
            }
            if (!string.IsNullOrEmpty(error_message))
            {
                ModelState.Draw(form, null);
                GlossaryState.Draw(form, null);
                ProvideAgnosticGlossaryState.Draw(form, null);
                return error_message;
            }

            var modelSt = GetModelState();
            error_message = ModelState.Draw(form, modelSt);
            var glossarySt = GetGlossaryState();
            string error_message2 = GlossaryState.Draw(form, glossarySt);
            var providerGlossary = GetProviderGlossaryState();
            var errorGlossary = ProvideAgnosticGlossaryState.Draw(form, providerGlossary);
            if (string.IsNullOrEmpty(error_message))
            {
                error_message = error_message2;
            }
            if (!string.IsNullOrEmpty(errorGlossary))
            {
                error_message = error_message2 + errorGlossary;
            }

			if (modelSt != null && modelSt.UseModel)
			{
				formMT.groupBoxOptional.Enabled = true;
				providerState.SetLanguageComboBoxes(modelSt.SelectedModelFrom, modelSt.SelectedModelTo);
			}
			else if (glossarySt != null && glossarySt.currentGlossary != null)
			{
				formMT.groupBoxOptional.Enabled = true;
				providerState.SetLanguageComboBoxes(glossarySt.SelectedGlossaryFrom, glossarySt.SelectedGlossaryTo);
			}
			else
			{   // No model or glossaqry, we do not need to show language selection
                // temporary! formMT.groupBoxOptional.Enabled = false;
                providerState.SetLanguageComboBoxes(null, null);
			}
			return error_message;
        }

        private void FillCredentials()
        {
			// Read and fill a list of delegated credentials for this provider
			Clear();
			if (formMT.groupBoxBillingAccount.Enabled)
			{
				formMT.comboBoxCredentialId.Visible = true;
				var accounts = form.testAuthData ?? form._translate.Accounts(providerState.currentProviderId);
                сonnectedAccounts = accounts.Select(q => new ListItem
                {
                    DisplayName = (string)q.credential_id, 
                    Value = (string)q.credential_id
                } ).ToArray();
				var defaultName = accounts.Where(x => (bool)x["default"]).Select(q => (string)q.credential_id).FirstOrDefault();
				if (defaultName != null)
                {
                    var defaultListItem = new ListItem
                    {
                        DisplayName = $"Default ({defaultName})",
                        Value = defaultName
                    };
                    formMT.comboBoxCredentialId.Items.Clear();
					formMT.comboBoxCredentialId.Items.Add(defaultListItem);
				}
				formMT.comboBoxCredentialId.Items.AddRange(сonnectedAccounts);
				if (сonnectedAccounts.Length != 0)
				{
					var credentialId = providerDataAuthDict["credential_id"];
                    var index = formMT.comboBoxCredentialId.Items.IndexOf(new ListItem { Value = credentialId});
					if (index >= 0)
                    {
                        formMT.comboBoxCredentialId.SelectedIndex = index;
					}
				}
			}
		}

        public void Clear()
        {
            formMT.comboBoxCredentialId.Items.Clear();
            formMT.comboBoxCredentialId.Items.Add(new ListItem
            {
                DisplayName = DefaultAccountName,
                Value = DefaultAccountName
            });
        }

        // Something changed in auth settings on form. Need to clear model and glossary settings. 
        private void ClearCredentials()
        {
            if (IsDelegatedCredentials)
            {
                // For stored/delegated creds providerDataAuthDict has simple and standard structure
                var id = formMT.comboBoxCredentialId.SelectedItem != null
                    ? ((ListItem)formMT.comboBoxCredentialId.SelectedItem).Value
                    : string.Empty;
                if (!string.IsNullOrEmpty(id))
                {
                    providerDataAuthDict["credential_id"] = id;
                }
            }
            else
            {
                foreach (string key in providerDataAuthDict.Keys.ToList())
                    providerDataAuthDict[key] = null;
            }
            if (!firstTimeDraw)
                ResetChildrensState();
        }

        private void ResetChildrensState()
        {
            modelState = new ModelState(this, options);
            modelState.ClearOptions(options);
            modelState = null;
            glossaryState = new GlossaryState(this, options);
            glossaryState.ClearOptions(options);
            glossaryState = null;
            providerAgnosticGlossary = new ProvideAgnosticGlossaryState(this, options);
            providerAgnosticGlossary.ClearOptions(options);
            providerAgnosticGlossary = null;
        }

        public bool UseCustomAuth
        { get
			{
				if (IsDelegatedCredentials && mode == StateModeEnum.required)
					return !string.IsNullOrEmpty(formMT.comboBoxCredentialId.Text);
                else
                {
                    var selectedItem = formMT.comboBoxCredentialId.SelectedItem as ListItem;
                    if (selectedItem == null)
                    {
                        return false;
                    }
                    return selectedItem.Value != DefaultAccountName;
                }
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
                ProvideAgnosticGlossaryState.FillOptions(null, options);
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
                ProvideAgnosticGlossaryState.FillOptions(state.GetProviderGlossaryState(), options);
            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.UseCustomAuth = false;
            options.CustomAuth = null;
            formMT.comboBoxCredentialId.Visible = true;
			Clear();
            ResetChildrensState();
        }

        #region Events

        public void comboBoxCredentialId_SelectedIndexChanged()
        {
            ClearCredentials();
			//ClearOptions(options);
			if (!firstTimeDraw && !internalControlChange)
			{
				GetModelState()?.ClearOptions(options);
                GetGlossaryState()?.ClearOptions(options);
                GetProviderGlossaryState()?.ClearOptions(options);
			}
			EnableDisable();
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
                {
                    if (!(formMT.comboBoxCredentialId.SelectedItem is ListItem item))
                        return false;
                    return !string.IsNullOrEmpty(item.Value);
                }
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

        public ProvideAgnosticGlossaryState GetProviderGlossaryState()
        {
            if (IsOK)
            {
                if (providerAgnosticGlossary == null)
                {
                    providerAgnosticGlossary = new ProvideAgnosticGlossaryState(this, options);
                }
            }
            else
            {
                providerAgnosticGlossary = null;
            }
            return providerAgnosticGlossary;
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
                formMT.comboBoxCredentialId.Visible = false;
                formMT.comboBoxCredentialId.Items.Clear();
				formMT.comboBoxCredentialId.Items.Add(new ListItem
                {
                    DisplayName = DefaultAccountName,
                    Value = DefaultAccountName
                });
            }
        }

        static void Auth_Control_BackColor_State(IntentoFormOptionsMT formMT, bool hasErrors)
        {
            formMT.comboBoxCredentialId.BackColor = hasErrors ? Color.LightPink : SystemColors.Window;
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
