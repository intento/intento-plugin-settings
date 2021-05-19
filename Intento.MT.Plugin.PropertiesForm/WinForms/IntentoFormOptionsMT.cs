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
		private Dictionary<string, string> _routingTable;
		public Dictionary<string, string> RoutingTable
		{
			get => _routingTable;
			set
			{
				if (_routingTable != value)
				{
					_routingTable = value;
					comboBoxRouting.DataSource = new BindingSource(value, null);
					comboBoxRouting.ValueMember = "Key";
					comboBoxRouting.DisplayMember = "Value";
				}
			}
		}
        IntentoTranslationProviderOptionsForm parent;
        const string testString = "14";
        readonly IList<string> testResultString = new ReadOnlyCollection<string>
            (new List<string> { "14", "14.", "Catorce" });
        public int cursorCountMT = 0;
		private delegate void TestResultsDelegate(PluginHelper.ErrorInfo errorInfo);
		//cursor while the asynchronous request is running
		private CursorFormMT testAndSaveCursor;
        // frozen controls while the asynchronous request is running
        private List<Control> disabledControls = new List<Control>();
        private CancellationTokenSource cts;

		public IntentoFormOptionsMT(IntentoTranslationProviderOptionsForm form)
        {
            InitializeComponent();
            this.SuspendLayout();
            LocalizeContent();
            this.ResumeLayout();
            parent = form;
            comboBoxProviders.SelectedIndexChanged += parent.comboBoxProviders_SelectedIndexChanged;
            checkBoxUseCustomModel.CheckedChanged += parent.checkBoxUseCustomModel_CheckedChanged;
            comboBoxModels.SelectedIndexChanged += parent.modelControls_ValueChanged;
            comboBoxCredentialId.SelectedIndexChanged += parent.comboBoxCredentialId_SelectedIndexChanged;
            textBoxModel.TextChanged += parent.modelControls_ValueChanged;
			comboBoxRouting.SelectedIndexChanged += parent.checkBoxSmartRouting_CheckedChanged;
			textBoxGlossary.TextChanged += parent.glossaryControls_ValueChanged;
            comboBoxGlossaries.TextChanged += parent.glossaryControls_ValueChanged;
            textBoxLabelURL.Click += parent.linkLabel_LinkClicked;
			textBoxLabelConnectAccount.Click += parent.linkLabel_LinkClicked;
			buttonRefresh.Click += parent.comboBoxProviders_SelectedIndexChanged;
			comboBoxRouting.Select();

			move_Controls("account", false);
            move_Controls("model", false);
            move_Controls("glossary", false);
        }

        private void LocalizeContent()
        {
            Text = Resource.MTcaption;
            labelHelpBillingAccount.Text = Resource.MTlabelHelpBillingAccount;            
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
            labelSmartRouting.Text = Resource.MTlabelSmartRouting;
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
                
                IntentoMTFormOptions testOptions = new IntentoMTFormOptions();
                parent.apiKeyState.FillOptions(testOptions);
				string to = testOptions.ToLanguage;
				string from = testOptions.FromLanguage;
				if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
				{
					var providerState = parent.apiKeyState.smartRoutingState.providerState;
					if (string.IsNullOrWhiteSpace(from))
					{
						Dictionary<string, string> fromLanguages = providerState.fromLanguages.ToDictionary(x => x.Value, x => x.Key);
						if (comboBoxFrom.SelectedIndex != -1 && fromLanguages.ContainsKey(comboBoxFrom.Text))
							testOptions.FromLanguage = fromLanguages[comboBoxFrom.Text];
						else if (fromLanguages.ContainsValue("en"))
							testOptions.FromLanguage = "en";
						else
							testOptions.FromLanguage = fromLanguages.First().Value;
					}
					if (string.IsNullOrWhiteSpace(to))
					{
						Dictionary<string, string> toLanguages = providerState.toLanguages.ToDictionary(x => x.Value, x => x.Key);
						if (comboBoxTo.SelectedIndex != -1 && toLanguages.ContainsKey(comboBoxTo.Text))
							testOptions.ToLanguage = toLanguages[comboBoxTo.Text];
						else if (toLanguages.ContainsValue("es"))
							testOptions.ToLanguage = "es";
						else
							testOptions.ToLanguage = toLanguages.Where(x => x.Value != from).First().Value;
					}
				}
				testOptions.proxySettings = parent.currentOptions.proxySettings;
                cts = new CancellationTokenSource();
                CancellationToken ct = cts.Token;
				Task<PluginHelper.ErrorInfo> testTask = new Task<PluginHelper.ErrorInfo>(() => TestTranslationTask(testOptions));
				testTask.ContinueWith((x) => DoInvokeTestResult(x.Result, ct));
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
		private void TestResults(PluginHelper.ErrorInfo errorInfo)
		{
			var msg = errorInfo.visibleErrorText;
			var res = errorInfo.isError;
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
				errorForm.setAdditionalErrorInfo(errorInfo.clipBoardContent);
				errorForm.ShowDialog(parent);
                if (errorForm.DialogResult == DialogResult.OK)
                    msg = null;
            }

            FreezeForm(false);
            if (msg == null)
                this.DialogResult = DialogResult.OK;
        }

		private PluginHelper.ErrorInfo TestTranslationTask(IntentoMTFormOptions testOptions)
		{
			Logs.Write('F', "Test Translate start");
            try
            {

				var testTranslate = parent.apiKeyState.CreateIntentoConnection(testOptions.proxySettings, "Intento.CheckSettings");
				// Call test translate intent 
				dynamic result = testTranslate.Fulfill(
                        testString,
                        to: testOptions.ToLanguage,
                        from: testOptions.FromLanguage,
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
					return new PluginHelper.ErrorInfo(false, Resource.ErrorTestTranslation, result.ToString());
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
								return new PluginHelper.ErrorInfo(true, string.Format(Resource.TestTranslationWarning, testString, res), null);
							}
                        }
                    }
                    else
                    {
						return new PluginHelper.ErrorInfo(true, Resource.ErrorTestTranslation, result.ToString());
					}

				}
				Logs.Write('F', "Test Translate finish");
				return new PluginHelper.ErrorInfo(true, null, null);
			}
            catch (AggregateException ex2)
            {
				Logs.Write('F', "Test Translate error", ex: ex2);
				return new PluginHelper.ErrorInfo(false, Resource.ErrorTestTranslation, ex2.Message);
			}
        }

		private void DoInvokeTestResult(PluginHelper.ErrorInfo errorInfo, CancellationToken ct)
		{
            try
            {
                if (!ct.IsCancellationRequested)
					BeginInvoke(new TestResultsDelegate(TestResults), errorInfo);
			}
            catch { }
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

        private void comboBoxCredentialId_VisibleChanged(object sender, EventArgs e)
        {
			panelConnectAccount.Visible = comboBoxCredentialId.Visible;
			buttonRefresh.Visible = comboBoxCredentialId.Visible;			
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
					if (ctrl.Enabled && ctrl.Name != "buttonCancel")
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
