using IntentoSDK;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm
{
    public partial class IntentoFormOptonsMain : Form, IForm
    {
        public class LangPair
        {
            public string _from;
            string _to;

            public LangPair(string from, string to)
            {
                this.from = from;
                this.to = to;
            }

            public string from { get => _from; set => _from = value; }
            public string to { get => _to; set => _to = value; }
        }

        #region vars
        public IntentoMTFormOptions originalOptions;
        public IntentoMTFormOptions currentOptions;
        public IntentoAiTextTranslate _translate;

        // Languages filter 
        public IList<dynamic> languages;
        private LangPair[] _languagePairs;

        public static DateTime TraceEndTime;

        private int numberOfFlashes;

        public ApiKeyState apiKeyState;

        List<string> errors;

        // Fabric to create intento connection. Parameters: apiKey and UserAgent for Settings Form 
        Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric;

        string version;

        #endregion vars


        public IntentoFormOptonsMain(
            IntentoMTFormOptions options, 
            LangPair[] languagePairs,
            Func<string, string, ProxySettings, IntentoAiTextTranslate> fabric
            )
        {
            this.fabric = fabric;

                InitializeComponent();

                if (options.HideHiddenTextButton)
                    checkBoxShowHidden.Visible = false;
                if (options.ForbidSaveApikey)
                    checkBoxSaveApiKeyInRegistry.Visible = false;

                Assembly currentAssem = typeof(IntentoTranslationProviderOptionsForm).Assembly;
                version = String.Format("{0}-{1}",
                    IntentoHelpers.GetVersion(currentAssem),
                    IntentoHelpers.GetGitCommitHash(currentAssem));

                originalOptions = options;
                currentOptions = originalOptions.Duplicate();
                apiKeyState = new ApiKeyState(this, currentOptions);
                if (apiKeyState.GetValueFromRegistry("ProxyEnabled") != null && apiKeyState.GetValueFromRegistry("ProxyEnabled") == "1")
                {
                    currentOptions.proxySettings = new ProxySettings()
                    {
                        ProxyAddress = apiKeyState.GetValueFromRegistry("ProxyAddress"),
                        ProxyPort = apiKeyState.GetValueFromRegistry("ProxyPort"),
                        ProxyUserName = apiKeyState.GetValueFromRegistry("ProxyUserName"),
                        ProxyPassword = apiKeyState.GetValueFromRegistry("ProxyPassw"),
                        ProxyEnabled = true
                    };
                }
                else
                    checkBoxProxy.Checked = false;

                checkBoxProxy.CheckedChanged += checkBoxProxy_CheckedChanged;

                _languagePairs = languagePairs;
                DialogResult = DialogResult.None;

                var tmp = TraceEndTime;
                checkBoxTrace.Checked = (TraceEndTime - DateTime.Now).Minutes > 0;
                TraceEndTime = tmp;
                // string pluginFor = string.IsNullOrEmpty(Options.PluginFor) ? "" : Options.PluginFor + '/';
                // toolStripStatusLabel2.Text = String.Format("{0} {1}{2}", Options.PluginName, pluginFor, Options.AssemblyVersion);
                var arr = originalOptions.Signature.Split('/');
                toolStripStatusLabel2.Text = arr.Count() == 3 ? String.Format("{0}/{1}", arr[0], arr[2]) : originalOptions.Signature;
                textBoxModel.Location = comboBoxModels.Location; // new Point(comboBoxModels.Location.X, comboBoxModels.Location.Y);
                textBoxGlossary.Location = comboBoxGlossaries.Location; // new Point(comboBoxGlossaries.Location.X, comboBoxGlossaries.Location.Y);
                groupBoxAuthCredentialId.Location = groupBoxAuth.Location; // new Point(groupBoxAuth.Location.X, groupBoxAuth.Location.Y)

                apiKeyState.apiKeyChangedEvent += ChangeApiKeyStatusDelegate;
                apiKey_tb.Select();

                apiKeyState.EnableDisable();
            }
        }

        
}
