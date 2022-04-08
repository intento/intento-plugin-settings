using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.SDK.DI;
using Intento.SDK.Translate;
using Intento.SDK.Translate.Options;
using static Intento.MT.Plugin.PropertiesForm.WinForms.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    /// <inheritdoc />
    // ReSharper disable once InconsistentNaming
    public partial class IntentoFormOptionsMT : Form
    {
        private Dictionary<string, string> routingTable;

        public Dictionary<string, string> RoutingTable
        {
            set
            {
                if (routingTable == value)
                {
                    return;
                }

                routingTable = value;
                comboBoxRouting.DataSource = new BindingSource(value, null);
                comboBoxRouting.ValueMember = "Key";
                comboBoxRouting.DisplayMember = "Value";
            }
        }

        private readonly IntentoTranslationProviderOptionsForm parent;
        private const string TestString = "14";

        readonly IList<string> testResultString = new ReadOnlyCollection<string>
            (new List<string> { "14", "14.", "Catorce" });

        public int cursorCountMt = 0;

        private delegate void TestResultsDelegate(ErrorInfo errorInfo);

        //cursor while the asynchronous request is running
        private CursorFormMT testAndSaveCursor;

        // frozen controls while the asynchronous request is running
        private readonly List<Control> disabledControls = new();
        private CancellationTokenSource cts;

        public IntentoFormOptionsMT(IntentoTranslationProviderOptionsForm form)
        {
            InitializeComponent();
            SuspendLayout();
            LocalizeContent();
            ResumeLayout();
            parent = form;
            comboBoxProviders.SelectedIndexChanged += parent.comboBoxProviders_SelectedIndexChanged;
            checkBoxUseCustomModel.CheckedChanged += parent.checkBoxUseCustomModel_CheckedChanged;
            comboBoxModels.SelectedIndexChanged += parent.modelControls_ValueChanged;
            comboBoxCredentialId.SelectedIndexChanged += parent.comboBoxCredentialId_SelectedIndexChanged;
            textBoxModel.TextChanged += parent.modelControls_ValueChanged;
            comboBoxRouting.SelectedIndexChanged += parent.checkBoxSmartRouting_CheckedChanged;
            textBoxGlossary.TextChanged += parent.glossaryControls_ValueChanged;
            comboBoxGlossaries.TextChanged += parent.glossaryControls_ValueChanged;
            listOfIntentoGlossaries.MouseUp += parent.agnosticGlossaryControls_ValueChanged;
            textBoxLabelURL.Click += parent.linkLabel_LinkClicked;
            textBoxLabelConnectAccount.Click += parent.linkLabel_LinkClicked;
            buttonRefresh.Click += parent.comboBoxProviders_SelectedIndexChanged;
            comboBoxRouting.Select();
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var smartRoutingState = parent.ApiKeyState?.SmartRoutingState;
            if (smartRoutingState is not { SmartRouting: true } && sender != null)
            {
                FreezeForm(true);

                var testOptions = new IntentoMTFormOptions();
                if (parent.ApiKeyState != null)
                {
                    parent.ApiKeyState.FillOptions(testOptions);
                    var to = testOptions.ToLanguage;
                    var from = testOptions.FromLanguage;
                    if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
                    {
                        var providerState = parent.ApiKeyState.SmartRoutingState.ProviderState;
                        if (string.IsNullOrWhiteSpace(from))
                        {
                            var fromLanguages =
                                providerState.FromLanguages.ToDictionary(x => x.Value, x => x.Key);
                            if (comboBoxFrom.SelectedIndex != -1 && fromLanguages.ContainsKey(comboBoxFrom.Text))
                                testOptions.FromLanguage = fromLanguages[comboBoxFrom.Text];
                            else if (fromLanguages.ContainsValue("en"))
                                testOptions.FromLanguage = "en";
                            else
                                testOptions.FromLanguage = fromLanguages.First().Value;
                        }

                        if (string.IsNullOrWhiteSpace(to))
                        {
                            var toLanguages =
                                providerState.ToLanguages.ToDictionary(x => x.Value, x => x.Key);
                            if (comboBoxTo.SelectedIndex != -1 && toLanguages.ContainsKey(comboBoxTo.Text))
                                testOptions.ToLanguage = toLanguages[comboBoxTo.Text];
                            else if (toLanguages.ContainsValue("es"))
                                testOptions.ToLanguage = "es";
                            else
                                testOptions.ToLanguage = toLanguages.First(x => x.Value != from).Value;
                        }
                    }
                }

                testOptions.ProxySettings = parent.CurrentOptions.ProxySettings;
                cts = new CancellationTokenSource();
                var ct = cts.Token;
                var testTask = new Task<ErrorInfo>(() => TestTranslationTask(testOptions));
                testTask.ContinueWith((x) => DoInvokeTestResult(x.Result, ct));
                testTask.Start();
            }
            else
                this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// callback for test asynchronous request
        /// </summary>
        /// <param name="errorInfo"></param>
        private void TestResults(ErrorInfo errorInfo)
        {
            var msg = errorInfo.VisibleErrorText;
            var res = errorInfo.IsError;
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

                errorForm.SetAdditionalErrorInfo(errorInfo.ClipBoardContent);
                errorForm.ShowDialog(parent);
                if (errorForm.DialogResult == DialogResult.OK)
                    msg = null;
            }

            FreezeForm(false);
            if (msg == null)
                DialogResult = DialogResult.OK;
        }

        private ErrorInfo TestTranslationTask(IntentoMTFormOptions testOptions)
        {
            var remoteLogService = parent.Locator.Resolve<IRemoteLogService>();
            remoteLogService.Write('F', "Test Translate start");
            try
            {
                var locator = parent.ApiKeyState.CreateIntentoConnection(testOptions.ProxySettings, testOptions.UserAgent );
                var translateService = locator.Resolve<ITranslateService>();
                
                var translateOptions = new TranslateOptions
                {
                    Text = TestString,
                    To = testOptions.ToLanguage,
                    From = testOptions.FromLanguage,
                    Provider = testOptions.ProviderId,
                    Async = true,
                    Auth = testOptions.UseCustomAuth
                        ? new[]
                        {
                            new AuthProviderInfo
                            {
                                Provider = testOptions.ProviderId,
                                Key = new []
                                {
                                    new KeyInfo
                                    {
                                        CredentialId = testOptions.AuthDict()["credential_id"]
                                    }
                                }
                            }
                        }
                        : null,
                    CustomModel = testOptions.UseCustomModel ? testOptions.CustomModel : null,
                    Glossary = testOptions.Glossary,
                    IntentoGlossary = testOptions.IntentoGlossaries,
                    WaitAsync = true,
                    Trace = remoteLogService.IsTrace()
                };
                // Call test translate intent
                
                var result = translateService.Fulfill(translateOptions);

                if (result.Error != null)
                {
                    return new ErrorInfo(true, result.Error.Message, result.Error.Message);
                }


                // Ordinary response of operations call (result of async request)
                if (result.Results.Any(str => str == null || testResultString.All(x => x != str)))
                {
                    return new ErrorInfo(true,
                        string.Format(Resource.TestTranslationWarning, TestString, result.Results.First()), null);
                }


                remoteLogService.Write('F', "Test Translate finish");
                return new ErrorInfo(true, null, null);
            }
            catch (AggregateException ex2)
            {
                remoteLogService.Write('F', "Test Translate error", ex: ex2);
                return new ErrorInfo(false, Resource.ErrorTestTranslation, ex2.Message);
            }
        }

        private void DoInvokeTestResult(ErrorInfo errorInfo, CancellationToken ct)
        {
            try
            {
                if (!ct.IsCancellationRequested)
                {
                    BeginInvoke(new TestResultsDelegate(TestResults), errorInfo);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void helpLink_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var tag = (sender as Control)?.Tag.ToString();
            switch (tag)
            {
                case "account":
                    labelHelpBillingAccount.Visible = !labelHelpBillingAccount.Visible;
                    break;
                case "model":
                    labelHelpModel.Visible = !labelHelpModel.Visible;
                    break;
                case "glossary":
                    labelHelpGlossary.Visible = !labelHelpGlossary.Visible;
                    break;
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
                foreach (Control ctrl in Controls)
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
                foreach (var ctrl in disabledControls)
                {
                    ctrl.Enabled = true;
                }

                disabledControls.Clear();
                testAndSaveCursor?.Dispose();
                cts?.Cancel();
            }
        }

        private void checkAllGlossaries_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (var i = 0; i < listOfIntentoGlossaries.Items.Count; i++)
            {
                listOfIntentoGlossaries.SetItemChecked(i, true);
            }
        }

        private void uncheckAllGlossaries_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (var i = 0; i < listOfIntentoGlossaries.Items.Count; i++)
            {
                listOfIntentoGlossaries.SetItemChecked(i, false);
            }
        }
    }
}