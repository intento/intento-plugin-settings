using System.Collections.Generic;
using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.SDK.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Intento.MT.Plugin.PropertiesForm.DI
{
    [RegisterExtension]
    internal sealed class PropertiesFormContainerRegisterExtension: IContainerRegisterExtension
    {
        public IEnumerable<ServiceDescriptor> GetServices()
        {
            yield return new ServiceDescriptor(typeof(IRemoteLogService), typeof(RemoteLogService),
                ServiceLifetime.Singleton);
        }
    }
}