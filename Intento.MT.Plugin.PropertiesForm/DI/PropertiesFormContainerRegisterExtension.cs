using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.SDK.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Intento.MT.Plugin.PropertiesForm.DI
{
    [RegisterExtension]
    internal sealed class PropertiesFormContainerRegisterExtension: IContainerRegisterExtension
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IRemoteLogService, RemoteLogService>();
        }
    }
}