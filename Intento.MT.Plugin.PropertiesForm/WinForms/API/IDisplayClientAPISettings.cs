using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm.WinForms.API
{
    /// <summary>
    /// Extension point for control of settings of Client API
    /// </summary>
    internal interface IDisplayClientAPISettings
    {
        /// <summary>
        /// Uid of provider
        /// </summary>
        Guid ClientAPIProvider { get; }

        /// <summary>
        /// Control for settings
        /// </summary>
        /// <returns></returns>
        BaseClientApiSettings GetSettingsControl();
    }
}
