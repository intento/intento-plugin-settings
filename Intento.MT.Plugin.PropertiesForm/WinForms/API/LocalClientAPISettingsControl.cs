using Intento.MT.Plugin.PropertiesForm.WinForms.API.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm.WinForms.API
{
    public partial class LocalClientAPISettingsControl : BaseClientApiSettings
    {
        private string filepath;
        private IntentoMTFormOptions options;

        public LocalClientAPISettingsControl()
        {
            InitializeComponent();
        }

        private LocalFileSettings Settings => (LocalFileSettings)settings;
        
        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                filePathBox.Text = filepath = fileSelector.FileName;
                Settings.FilePath = filepath;
                InvokeChangeSettings(Settings);
                ApplySettings();
            }
        }

        public override void SetSettings(IntentoMTFormOptions options)
        {
            this.options = options;
            this.settings = new LocalFileSettings {
                FilePath = options.FilePath,
                ClientApiId = options.ClientApiId
            };
            filePathBox.Text = filepath = Settings.FilePath;
            InvokeChangeSettings(settings);
        }

        public override void ApplySettings()
        {
            options.FilePath = filepath;
        }
    }
}
