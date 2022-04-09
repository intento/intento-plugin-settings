using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Intento.MT.Plugin.PropertiesForm.Models;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using Intento.SDK.DI;
using Intento.SDK.Translate;
using static Intento.MT.Plugin.PropertiesForm.IntentoMTFormOptions;

namespace Intento.MT.Plugin.PropertiesForm.States
{
    public class AuthState : BaseState
    {
        // Parent component
        public ProviderState ProviderState { get; }

        // Controlled components
        private ModelState modelState;
        private GlossaryState glossaryState;
        private ProvideAgnosticGlossaryState providerAgnosticGlossary;

        private object[] сonnectedAccounts;
        private string errorMessage;

        private bool firstTimeDraw = true;

        private const string DefaultAccountName = "via Intento";

        private readonly StateModeEnum mode;

        // current credentials StateModeEnum
        public Dictionary<string, string> ProviderDataAuthDict { get; }

        private ITranslateService TranslateService => Form.Locator.Resolve<ITranslateService>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerState"></param>
        /// <param name="options"></param>
        public AuthState(ProviderState providerState, IntentoMTFormOptions options) : base(
            providerState, options)
        {
            ProviderState = providerState;
            FormMt = providerState.Form.FormMt;
            Options = options;

            // set mode of checkBoxUseOwnCred
            if (!providerState.Billable || !providerState.StockModel) // Auth Required
            {
                FormMt.groupBoxBillingAccount.Enabled = true;
                mode = StateModeEnum.Required;
            }
            else if (!providerState.OwnAuth) // Auth Prohibited 
            {
                FormMt.groupBoxBillingAccount.Enabled = false;
                mode = StateModeEnum.Prohibited;
            }
            else
            {
                // Auth optional
                FormMt.groupBoxBillingAccount.Enabled = true;
                mode = StateModeEnum.Optional;
            }

            if (!providerState.IsOk)
            {
                // smart routing 
                return;
            }

            // Get credentials from Options
            ProviderDataAuthDict = new Dictionary<string, string>();
            var auth = Options.AuthDict();
            if (providerState.ProviderAuthList == null)
            {
                return;
            }

            foreach (var key in providerState.ProviderAuthList)
            {
                ProviderDataAuthDict[key] = auth != null && auth.ContainsKey(key) ? auth[key] : string.Empty;
            }
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, AuthState state)
        {
            if (state != null)
            {
                return state.Draw();
            }

            AuthGroupBoxEnabled(form.FormMt, false);
            AuthControlBackColorState(form.FormMt, false);

            ModelState.Draw(form, null);
            GlossaryState.Draw(form, null);
            ProvideAgnosticGlossaryState.Draw(form, null);

            return null;
        }

        private string Draw()
        {
            if (firstTimeDraw && FormMt.comboBoxCredentialId.SelectedIndex < 0)
            {
                // Need to fill all items - first tiem drawing after making AuthState
                FillCredentials();
                firstTimeDraw = false;
            }

            errorMessage = null;

            if (FormMt.groupBoxBillingAccount.Enabled)
            {
                FormMt.comboBoxCredentialId.Visible = true;
                if (mode == StateModeEnum.Required)
                {
                    FormMt.comboBoxCredentialId.Items.Remove(DefaultAccountName);
                }
                else if (FormMt.comboBoxCredentialId.SelectedIndex < 0)
                {
                    FormMt.comboBoxCredentialId.SelectedIndexChanged -=
                        ProviderState.Form.comboBoxCredentialId_SelectedIndexChanged;
                    var index = FormMt.comboBoxCredentialId.Items.IndexOf(new ListItem { Value = DefaultAccountName });
                    if (index >= 0)
                    {
                        FormMt.comboBoxCredentialId.SelectedIndex = index;
                    }

                    FormMt.comboBoxCredentialId.SelectedIndexChanged +=
                        ProviderState.Form.comboBoxCredentialId_SelectedIndexChanged;
                }

                if (mode == StateModeEnum.Required && FormMt.comboBoxCredentialId.SelectedIndex == -1)
                {
                    // Credentials required but not filled in full
                    AuthControlBackColorState(FormMt, true);
                    errorMessage = Resource.OwnCredentialsNeededErrorMessage;
                    // temporary! formMT.groupBoxOptional.Enabled = false;
                }
                else
                {
                    // All fields in credentals are filled
                    AuthControlBackColorState(FormMt, false);
                }
            }
            else
            {
                FormMt.comboBoxCredentialId.Visible = false;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.Draw(Form, null);
                GlossaryState.Draw(Form, null);
                ProvideAgnosticGlossaryState.Draw(Form, null);
                return errorMessage;
            }

            var modelSt = GetModelState();
            errorMessage = ModelState.Draw(Form, modelSt);
            var glossarySt = GetGlossaryState();
            var newErrorMessage = GlossaryState.Draw(Form, glossarySt);
            var providerGlossary = GetProviderGlossaryState();
            var errorGlossary = ProvideAgnosticGlossaryState.Draw(Form, providerGlossary);
            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = newErrorMessage;
            }

            if (!string.IsNullOrEmpty(errorGlossary))
            {
                errorMessage = newErrorMessage + errorGlossary;
            }

            if (modelSt is { UseModel: true })
            {
                FormMt.groupBoxOptional.Enabled = true;
                ProviderState.SetLanguageComboBoxes(modelSt.SelectedModelFrom, modelSt.SelectedModelTo);
            }
            else if (glossarySt is { CurrentGlossary: { } })
            {
                FormMt.groupBoxOptional.Enabled = true;
                ProviderState.SetLanguageComboBoxes(glossarySt.SelectedGlossaryFrom, glossarySt.SelectedGlossaryTo);
            }
            else
            {
                // No model or glossary, we do not need to show language selection
                // temporary! formMT.groupBoxOptional.Enabled = false;
                ProviderState.SetLanguageComboBoxes(null, null);
            }

            return errorMessage;
        }

        private void FillCredentials()
        {
            // Read and fill a list of delegated credentials for this provider
            Clear();
            if (!FormMt.groupBoxBillingAccount.Enabled)
            {
                return;
            }

            FormMt.comboBoxCredentialId.Visible = true;
            var accounts = TranslateService.Accounts(ProviderState.CurrentProviderId);
            // ReSharper disable once CoVariantArrayConversion
            сonnectedAccounts = accounts.Select(q => new ListItem
            {
                DisplayName = q.CredentialId,
                Value = q.CredentialId
            }).ToArray();
            var defaultName = accounts
                .Where(x => x.Default)
                .Select(q => q.CredentialId).FirstOrDefault();
            if (defaultName != null)
            {
                var defaultListItem = new ListItem
                {
                    DisplayName = $"Default ({defaultName})",
                    Value = defaultName
                };
                FormMt.comboBoxCredentialId.Items.Clear();
                FormMt.comboBoxCredentialId.Items.Add(defaultListItem);
            }

            FormMt.comboBoxCredentialId.Items.AddRange(сonnectedAccounts);
            if (сonnectedAccounts.Length == 0)
            {
                return;
            }

            var credentialId = ProviderDataAuthDict["credential_id"];
            var index = FormMt.comboBoxCredentialId.Items.IndexOf(new ListItem { Value = credentialId });
            if (index >= 0)
            {
                FormMt.comboBoxCredentialId.SelectedIndex = index;
            }
        }

        public void Clear()
        {
            FormMt.comboBoxCredentialId.Items.Clear();
            FormMt.comboBoxCredentialId.Items.Add(new ListItem
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
                var id = FormMt.comboBoxCredentialId.SelectedItem != null
                    ? ((ListItem)FormMt.comboBoxCredentialId.SelectedItem).Value
                    : string.Empty;
                if (!string.IsNullOrEmpty(id))
                {
                    ProviderDataAuthDict["credential_id"] = id;
                }
            }
            else
            {
                foreach (var key in ProviderDataAuthDict.Keys.ToList())
                {
                    ProviderDataAuthDict[key] = null;
                }
            }

            if (!firstTimeDraw)
            {
                ResetChildrenState();
            }
        }

        private void ResetChildrenState()
        {
            modelState = new ModelState(this, Options);
            modelState.ClearOptions(Options);
            modelState = null;
            glossaryState = new GlossaryState(this, Options);
            glossaryState.ClearOptions(Options);
            glossaryState = null;
            providerAgnosticGlossary = new ProvideAgnosticGlossaryState(this, Options);
            providerAgnosticGlossary.ClearOptions(Options);
            providerAgnosticGlossary = null;
        }

        public bool UseCustomAuth
        {
            get
            {
                if (IsDelegatedCredentials && mode == StateModeEnum.Required)
                {
                    return !string.IsNullOrEmpty(FormMt.comboBoxCredentialId.Text);
                }

                if (FormMt.comboBoxCredentialId.SelectedItem is not ListItem selectedItem)
                {
                    return false;
                }

                return selectedItem.Value != DefaultAccountName;
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

                if (state.IsDelegatedCredentials && state.ProviderDataAuthDict.TryGetValue("credential_id", out var id))
                {
                    options.AuthDelegatedCredentialId = id;
                }
                else
                {
                    options.AuthDelegatedCredentialId = null;
                }

                options.UseCustomAuth = state.UseCustomAuth;
                if (options.UseCustomAuth)
                {
                    options.SetAuthDict(state.ProviderDataAuthDict);
                }
                else
                {
                    options.CustomAuth = null;
                }

                ModelState.FillOptions(state.GetModelState(), options);
                GlossaryState.FillOptions(state.GetGlossaryState(), options);
                ProvideAgnosticGlossaryState.FillOptions(state.GetProviderGlossaryState(), options);
            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.UseCustomAuth = false;
            options.CustomAuth = null;
            FormMt.comboBoxCredentialId.Visible = true;
            Clear();
            ResetChildrenState();
        }

        #region Events

        public void comboBoxCredentialId_SelectedIndexChanged()
        {
            ClearCredentials();
            if (!firstTimeDraw)
            {
                GetModelState()?.ClearOptions(Options);
                GetGlossaryState()?.ClearOptions(Options);
                GetProviderGlossaryState()?.ClearOptions(Options);
            }

            EnableDisable();
        }

        #endregion Events

        #region Properties

        private bool IsDelegatedCredentials => ProviderState.DelegatedCredentials;

        // True in case auth is entered or selected
        private bool IsSelected
        {
            get
            {
                if (!IsDelegatedCredentials)
                {
                    return !ProviderDataAuthDict.Values.Any(string.IsNullOrEmpty);
                }

                if (FormMt.comboBoxCredentialId.SelectedItem is not ListItem item)
                {
                    return false;
                }

                return !string.IsNullOrEmpty(item.Value);
            }
        }

        public bool IsOk
        {
            get
            {
                switch (mode)
                {
                    case StateModeEnum.Required:
                        return IsSelected;
                    case StateModeEnum.Optional:
                        return !UseCustomAuth || IsSelected;
                    case StateModeEnum.Prohibited:
                        return true;
                    case StateModeEnum.Unknown:
                    default:
                        throw new Exception($"Invalid mode {mode}");
                }
            }
        }

        public ModelState GetModelState()
        {
            if (IsOk)
            {
                modelState ??= new ModelState(this, Options);
            }
            else
            {
                modelState = null;
            }

            return modelState;
        }

        public GlossaryState GetGlossaryState()
        {
            if (IsOk)
            {
                glossaryState ??= new GlossaryState(this, Options);
            }
            else
            {
                glossaryState = null;
            }

            return glossaryState;
        }

        public ProvideAgnosticGlossaryState GetProviderGlossaryState()
        {
            if (IsOk)
            {
                providerAgnosticGlossary ??= new ProvideAgnosticGlossaryState(this, Options);
            }
            else
            {
                providerAgnosticGlossary = null;
            }

            return providerAgnosticGlossary;
        }

        #endregion Properties

        #region methods for managing a group of controls

        private static void AuthGroupBoxEnabled(IntentoFormOptionsMT formMt, bool enabled)
        {
            if (enabled)
            {
                formMt.groupBoxBillingAccount.Enabled = true;
            }
            else
            {
                formMt.groupBoxBillingAccount.Enabled = false;
                formMt.comboBoxCredentialId.Visible = false;
                formMt.comboBoxCredentialId.Items.Clear();
                formMt.comboBoxCredentialId.Items.Add(new ListItem
                {
                    DisplayName = DefaultAccountName,
                    Value = DefaultAccountName
                });
            }
        }

        private static void AuthControlBackColorState(IntentoFormOptionsMT formMt, bool hasErrors)
        {
            formMt.comboBoxCredentialId.BackColor = hasErrors ? Color.LightPink : SystemColors.Window;
        }

        #endregion methods for managing a group of controls
    }
}