using System.Collections.Generic;
using Intento.SDK.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace IntentoMemoQMTPlugin
{
    /// <summary>
    /// Register logger for plugin
    /// </summary>
    [RegisterExtension]
    internal class LoggerRegisterExtension : IContainerRegisterExtension
    {
        public IEnumerable<ServiceDescriptor> GetServices()
        {
#if WRITELOG
            yield return new ServiceDescriptor(typeof(ILoggerProvider), typeof(MemoQLoggerProvider),
                ServiceLifetime.Singleton);
            yield return new ServiceDescriptor(typeof(ILoggerFactory), typeof(MemoQLoggerFactory),
                ServiceLifetime.Singleton);
#else
            yield return new ServiceDescriptor(typeof(ILoggerFactory), typeof(NullLoggerFactory),
                ServiceLifetime.Singleton);
#endif
        }
    }
}