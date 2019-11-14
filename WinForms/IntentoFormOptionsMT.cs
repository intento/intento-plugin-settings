using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using static Intento.MT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm
{
    public partial class IntentoFormOptionsMT : Form
    {
        IntentoTranslationProviderOptionsForm parent;
        const string testString = "1";
        readonly IList<string> testResultString = new ReadOnlyCollection<string> 
            (new List<string> {"1", "1.", "Uno", "Uno." });
        public int cursorCountMT = 0;

        public IntentoFormOptionsMT(IntentoTranslationProviderOptionsForm form)
        {
            InitializeComponent();
            LocalizeContent();
            parent = form;
            comboBoxProviders.SelectedIndexChanged += parent.comboBoxProviders_SelectedIndexChanged;
            //this.Shown += parent.IntentoTranslationProviderOptionsForm_Shown;
            checkBoxUseOwnCred.CheckedChanged += parent.checkBoxUseOwnCred_CheckedChanged;
            checkBoxUseCustomModel.CheckedChanged += parent.checkBoxUseCustomModel_CheckedChanged;
            buttonWizard.Click += parent.buttonWizard_Click;
            comboBoxModels.SelectedIndexChanged += parent.comboBoxModels_SelectedIndexChanged;
            comboBoxCredentialId.SelectedIndexChanged += parent.comboBoxCredentialId_SelectedIndexChanged;
            textBoxModel.TextChanged += parent.textBoxModel_TextChanged;
            checkBoxSmartRouting.CheckedChanged += parent.checkBoxSmartRouting_CheckedChanged;
            textBoxCredentials.Enter += parent.textBoxCredentials_Enter;
            textBoxGlossary.TextChanged += parent.glossaryControls_ValueChanged;
            comboBoxGlossaries.TextChanged += parent.glossaryControls_ValueChanged;
            checkBoxSmartRouting.Select();

            textBoxModel.Location = comboBoxModels.Location;
            textBoxGlossary.Location = comboBoxGlossaries.Location;

            move_Controls("account", false);
            move_Controls("model", false);
            move_Controls("glossary", false);
        }

        private void LocalizeContent()
        {
            Text = Resource.MTcaption;
            labelHelpBillingAccount.Text = Resource.MTlabelHelpBillingAccount;
            checkBoxUseOwnCred.Text = Resource.MTcheckBoxUseOwnCred;
            labelHelpModel.Text = Resource.MTlabelHelpModel;
            checkBoxUseCustomModel.Text = Resource.MTcheckBoxUseCustomModel;
            labelHelpGlossary.Text = Resource.MTlabelHelpGlossary;
            checkBoxUseGlossary.Text = Resource.MTcheckBoxUseGlossary;
            groupBoxOptional.Text = Resource.MTgroupBoxOptional;
            labelHelpOptional.Text = Resource.MTlabelHelpOptional;
            buttonSave.Text = Resource.TestAndSave;
            buttonCancel.Text = Resource.Cancel;
            textBoxLabelURL.Text = Resource.MTtextBoxLabelURL;
            groupBoxBillingAccount.Text = Resource.BillingAccount;
            groupBoxModel.Text = Resource.Model;
            groupBoxGlossary.Text = Resource.Glossary;
            checkBoxSmartRouting.Text = Resource.MTcheckBoxSmartRouting;
            groupBoxProvider.Text = Resource.Provider;
        }

        //private void linkLabelHelp_Click(object sender, EventArgs e)
        //{
        //    toolTipHelp.SetToolTip((Control)sender, "Name should start with Capital letter");
        //}

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string msg = null;
            SmartRoutingState smartRoutingState = parent.apiKeyState?.smartRoutingState;
            if (smartRoutingState == null || !smartRoutingState.SmartRouting)
            {
                msg = TestTranslationIsSuccessful();
                if (msg != null)
                {
                    var errorForm = new IntentoFormIgnoreError();
                    errorForm.labelError.Text = string.Format("{0} {1}", Resource.Error, msg);
                    errorForm.ShowDialog();
                    if (errorForm.DialogResult == DialogResult.OK)
                        msg = null;
                }
            }
            if (msg == null)
                this.DialogResult = DialogResult.OK;
        }
        private string TestTranslationIsSuccessful()
        {
            IntentoTranslationProviderOptionsForm.Logging("Trados Translate: start");
            try
            {
                using (new CursorFormMT(this))
                {
                    IntentoMTFormOptions testOptions = new IntentoMTFormOptions();
                    parent.apiKeyState.FillOptions(testOptions);

                    // Call test translate intent 
                    dynamic result = parent._translate.Fulfill(
                        testString,
                        to: string.IsNullOrWhiteSpace(comboBoxTo.Text) ? "es" : comboBoxTo.Text,
                        from: string.IsNullOrWhiteSpace(comboBoxFrom.Text) ? "en" : comboBoxFrom.Text,
                        provider: testOptions.ProviderId,
                        format: null,
                        async: true,
                        auth: testOptions.UseCustomAuth ? string.Format("{{'{0}':[{1}]}}", testOptions.ProviderId, testOptions.CustomAuth).Replace('\'', '"') : null,
                        routing: null,
                        pre_processing: null,
                        post_processing: null,
                        custom_model: testOptions.UseCustomModel ? testOptions.CustomModel : null,
                        glossary: testOptions.Glossary,
                        wait_async: true,
                        trace: IntentoTranslationProviderOptionsForm.IsTrace()
                        );
                    dynamic response = result.response;
                    if (response != null && response.First != null)
                    {   // Ordinary response of operations call (result of async request)
                        foreach (dynamic str in response.First.results)
                        {
                            string res = (string)str;
                            if (str == null || !testResultString.Any(x => x==res))
                                return Resource.ErrorTestTranslation;
                        }
                    }
                    else
                        return Resource.ErrorTestTranslation;

                    IntentoTranslationProviderOptionsForm.Logging("Trados Translate: finish");
                }
                return null;
            }
            catch (AggregateException ex2)
            {
                IntentoTranslationProviderOptionsForm.Logging("Trados Translate: error", ex: ex2);
                return ex2.Message;
            }
        }



        private void checkBoxUseOwnCred_CheckedChanged(object sender, EventArgs e)
        {
            var value = checkBoxUseOwnCred.Checked;
            comboBoxCredentialId.Enabled = value;
            textBoxCredentials.Enabled = value;
            buttonWizard.Enabled = value;
        }

        private void helpLink_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var tag = (sender as Control).Tag.ToString();
            switch (tag)
            {
                case "account":
                    labelHelpBillingAccount.Visible = !labelHelpBillingAccount.Visible;
                    move_Controls(tag, labelHelpBillingAccount.Visible);
                    break;
                case "model":
                    labelHelpModel.Visible = !labelHelpModel.Visible;
                    move_Controls(tag, labelHelpModel.Visible);
                    break;
                case "glossary":
                    labelHelpGlossary.Visible = !labelHelpGlossary.Visible;
                    move_Controls(tag, labelHelpGlossary.Visible);
                    break;
            }
        }

        private void move_Controls(string tag, bool up)
        {
            int val = 23 * (up ? 1 : -1);
            foreach (Control ctr in this.Controls)
            {
                if (ctr is GroupBox)
                    foreach (Control ctrl in ctr.Controls)
                    {
                        if (ctrl.Tag != null)
                            if (ctrl.Tag.ToString() == tag + "Control")
                                ctrl.Top = ctrl.Top + val;
                    }
            }

        }
    }
}
