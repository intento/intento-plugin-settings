using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm.WinForms.API
{
    //Factory for select client API
    public class DisplayClientAPISettingsFactory
    {
        private readonly IDisplayClientAPISettings[] displayClientAPISettings;

        public DisplayClientAPISettingsFactory()
        {
            displayClientAPISettings = new IDisplayClientAPISettings[] { 
                new DisplayClientAPILocalSettings()
            };
        }

        public IDisplayClientAPISettings Get(Guid uid)
        {
            return displayClientAPISettings.FirstOrDefault(c => c.ClientAPIProvider == uid);
        }

        public static DisplayClientAPISettingsFactory Current => displayClientAPISettingsFactory;


        private static readonly DisplayClientAPISettingsFactory displayClientAPISettingsFactory = new DisplayClientAPISettingsFactory();
    }
}
