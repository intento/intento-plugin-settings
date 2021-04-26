using Intento.MT.Plugin.PropertiesForm.WinForms.API.Settings;
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
    /// <summary>
    /// Base control for CLient API Settings
    /// </summary>
    public partial class BaseClientApiSettings : UserControl
    {        

        public event EventHandler ChangeSettings;
        
        protected BaseSettings settings;

        public virtual void SetSettings(IntentoMTFormOptions options)
        {
            
        }

        public virtual void ApplySettings()
        {

        }

        protected virtual void OnChangeSettings()
        { 
        }

        protected void InvokeChangeSettings(BaseSettings settings)
        {
            ChangeSettings?.Invoke(settings, EventArgs.Empty);
        }
        

        public BaseClientApiSettings()
        {
            InitializeComponent();
        }
    }
}
