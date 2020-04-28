using Intento.MT.Plugin.PropertiesForm.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Intento.MT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm
{
	public partial class IntentoFormOptionsMT : Form
    {
        IntentoTranslationProviderOptionsForm parent;
        //const string testString = "1";
        //readonly IList<string> testResultString = new ReadOnlyCollection<string> 
        //    (new List<string> {"1", "1.", "Uno", "Uno." });
        const string testString = "14";
        readonly IList<string> testResultString = new ReadOnlyCollection<string>
            (new List<string> { "14", "14.", "Catorce" });
        public int cursorCountMT = 0;
        private delegate void TestResultsDelegate(bool res, string msg);
        //cursor while the asynchronous request is running
        private CursorFormMT testAndSaveCursor;
        // frozen controls while the asynchronous request is running
        private List<Control> disabledControls = new List<Control>();
        private CancellationTokenSource cts;

        public IntentoFormOptionsMT(IntentoTranslationProviderOptionsForm form)
        {
            InitializeComponent();
            LocalizeContent();
            parent = form;
            comboBoxProviders.SelectedIndexChanged += parent.comboBoxProviders_SelectedIndexChanged;
            //this.Shown += parent.IntentoTranslationProviderOptionsForm_Shown;
            //checkBoxUseOwnCred.CheckedChanged += parent.checkBoxUseOwnCred_CheckedChanged;
            checkBoxUseCustomModel.CheckedChanged += parent.checkBoxUseCustomModel_CheckedChanged;
            //buttonWizard.Click += parent.buttonWizard_Click;
            comboBoxModels.SelectedIndexChanged += parent.modelControls_ValueChanged;
            comboBoxCredentialId.SelectedIndexChanged += parent.comboBoxCredentialId_SelectedIndexChanged;
            textBoxModel.TextChanged += parent.modelControls_ValueChanged;
            checkBoxSmartRouting.CheckedChanged += parent.checkBoxSmartRouting_CheckedChanged;
            //textBoxCredentials.Enter += parent.textBoxCredentials_Enter;
            textBoxGlossary.TextChanged += parent.glossaryControls_ValueChanged;
            comboBoxGlossaries.TextChanged += parent.glossaryControls_ValueChanged;
            textBoxLabelURL.Click += parent.linkLabel_LinkClicked;
			textBoxLabelConnectAccount.Click += parent.linkLabel_LinkClicked;
//			buttonRefresh.Click += parent.buttonRefresh_Click;
			buttonRefresh.Click += parent.comboBoxProviders_SelectedIndexChanged;
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
            //checkBoxUseOwnCred.Text = Resource.MTcheckBoxUseOwnCred;
            labelHelpModel.Text = Resource.MTlabelHelpModel;
            checkBoxUseCustomModel.Text = Resource.MTcheckBoxUseCustomModel;
            labelHelpGlossary.Text = Resource.MTlabelHelpGlossary;
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
			textBoxLabelConnectAccount.Text = Resource.MTLinkConnectAccount;
			toolTipHelp.SetToolTip(buttonRefresh, Resource.MTbuttonRefreshToolTip);
		}

        public void buttonSave_Click(object sender, EventArgs e)
        {
            SmartRoutingState smartRoutingState = parent.apiKeyState?.smartRoutingState;
            if ((smartRoutingState == null || !smartRoutingState.SmartRouting) && sender != null)
            {
                FreezeForm(true);
                var providerState = parent.apiKeyState.smartRoutingState.providerState;
                //string to = comboBoxTo.SelectedIndex != -1 ?
                //    providerState.toLanguages.Where(x => x.Value == comboBoxTo.Text).First().Key : "es";
                //string from = comboBoxFrom.SelectedIndex != -1 ?
                //    providerState.fromLanguages.Where(x => x.Value == comboBoxFrom.Text).First().Key : "en";

                IntentoMTFormOptions testOptions = new IntentoMTFormOptions();
                parent.apiKeyState.FillOptions(testOptions);
                cts = new CancellationTokenSource();
                CancellationToken ct = cts.Token;
                Task<KeyValuePair<bool, string>> testTask = new Task<KeyValuePair<bool, string>>(() => TestTranslationTask(testOptions));
                testTask.ContinueWith((x) => DoInvokeTestResult(x.Result.Key, x.Result.Value, ct));
                testTask.Start();

            }
            else
                this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// callback for test asynchronous request
        /// </summary>
        /// <param name="res">true if there were no errors in the answer, else false</param>
        /// <param name="msg">message for user</param>
        private void TestResults(bool res, string msg)
        {
            if (msg != null)
            {
                var errorForm = new IntentoFormIgnoreError();
                errorForm.labelError.Text = msg;
                if (res)
                {
                    errorForm.labelError.ForeColor = Color.Blue;
                    errorForm.buttonIgnoreAndSave.Text = Resource.ButtonIgnoreAndSave_Ok;
                }
                else
                {
                    errorForm.labelError.ForeColor = Color.Red;
                    errorForm.buttonIgnoreAndSave.Text = Resource.ButtonIgnoreAndSave_Ignore;
                }
                errorForm.ShowDialog(parent);
                if (errorForm.DialogResult == DialogResult.OK)
                    msg = null;
            }

            FreezeForm(false);
            if (msg == null)
                this.DialogResult = DialogResult.OK;
        }

        private KeyValuePair<bool, string> TestTranslationTask(IntentoMTFormOptions testOptions)
        {
            IntentoTranslationProviderOptionsForm.Logging("Trados Translate: start");
            try
            {
                //IntentoMTFormOptions testOptions = new IntentoMTFormOptions();
                //parent.apiKeyState.FillOptions(testOptions);
                // Call test translate intent 
                dynamic result = parent._translate.Fulfill(
                        testString,
                        to: string.IsNullOrWhiteSpace(testOptions.ToLanguage) ? "es" : testOptions.ToLanguage,
                        from: string.IsNullOrWhiteSpace(testOptions.FromLanguage) ? "en" : testOptions.FromLanguage,
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
                if (result.error != null)
                {
                    return new KeyValuePair<bool, string>(false, Resource.ErrorTestTranslation);
                }
                else
                {
                    dynamic response = result.response;
                    if (response != null && response.First != null)
                    {   // Ordinary response of operations call (result of async request)
                        foreach (dynamic str in response.First.results)
                        {
                            string res = (string)str;
                            if (str == null || !testResultString.Any(x => x == res))
                            {
                                return new KeyValuePair<bool, string>(true, string.Format(Resource.TestTranslationWarning, testString, res));
                            }
                        }
                    }
                    else
                    {
                        return new KeyValuePair<bool, string>(true, Resource.ErrorTestTranslation);
                    }

                    IntentoTranslationProviderOptionsForm.Logging("Trados Translate: finish");
                }
                return new KeyValuePair<bool, string>(true, null);
            }
            catch (AggregateException ex2)
            {
                IntentoTranslationProviderOptionsForm.Logging("Trados Translate: error", ex: ex2);
                return new KeyValuePair<bool, string>(false, ex2.Message);
            }
        }

        private void DoInvokeTestResult(bool res, string msg, CancellationToken ct)
        {
            try
            {
                if (!ct.IsCancellationRequested)
                    BeginInvoke(new TestResultsDelegate(TestResults), res, msg);
            }
            catch { }
        }

        //private void checkBoxUseOwnCred_CheckedChanged(object sender, EventArgs e)
        //{
        //    var value = checkBoxUseOwnCred.Checked;
        //    comboBoxCredentialId.Enabled = value;
        //    textBoxCredentials.Enabled = value;
        //    buttonWizard.Enabled = value;
        //}

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

        private void comboBoxCredentialId_VisibleChanged(object sender, EventArgs e)
        {
			panelConnectAccount.Visible = comboBoxCredentialId.Visible;
			buttonRefresh.Visible = comboBoxCredentialId.Visible;
			//    if (comboBoxCredentialId.Visible)
			//        buttonWizard.Visible = false;
		}

		private void IntentoFormOptionsMT_FormClosing(object sender, FormClosingEventArgs e)
        {
            FreezeForm(false);
        }

        /// <summary>
        /// Freezing form controls during the asynchronous execution of a test translation
        /// </summary>
        /// <param name="freeze">true - locking, false - unlocking</param>
        private void FreezeForm(bool freeze)
        {
            if (freeze)
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl.Enabled)
                    {
                        disabledControls.Add(ctrl);
                        ctrl.Enabled = false;
                    }
                }
                testAndSaveCursor = new CursorFormMT(this);
            }
            else
            {
                foreach (Control ctrl in disabledControls)
                    ctrl.Enabled = true;
                disabledControls.Clear();
                if (testAndSaveCursor != null)
                    testAndSaveCursor.Dispose();
                if (cts != null)
                    cts.Cancel();
            }
        }

	}
}
