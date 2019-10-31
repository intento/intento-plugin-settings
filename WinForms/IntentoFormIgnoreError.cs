using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    public partial class IntentoFormIgnoreError : Form
    {
        public IntentoFormIgnoreError()
        {
            InitializeComponent();
            buttonContinueEdit.Text = Resource.ButtonContinueEdit;
            buttonIgnoreAndSave.Text = Resource.ButtonIgnoreAndSave;
            buttonIgnoreAndSave.DialogResult = DialogResult.OK;
        }
    }
}
