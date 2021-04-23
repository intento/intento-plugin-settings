using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm.WinForms.API
{
    public partial class LocalClientAPISettingsControl : BaseClientApiSettings
    {
        public LocalClientAPISettingsControl()
        {
            InitializeComponent();
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                filePathBox.Text = fileSelector.FileName;
            }
        }
    }
}
