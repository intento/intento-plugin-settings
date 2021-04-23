using IntentoSDK.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm.WinForms.API
{
    internal class DisplayClientAPILocalSettings : IDisplayClientAPISettings
    {
        public Guid ClientAPIProvider => LocalAPIClient.uid;

        public BaseClientApiSettings GetSettingsControl()
        {
            return new LocalClientAPISettingsControl();
        }
    }
}
