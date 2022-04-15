using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Logging;

namespace IntentoMemoQMTPlugin
{
    /// <summary>
    /// Logger factory for MemoQ
    /// </summary>
    internal class MemoQLoggerFactory : ILoggerFactory
    {
        private readonly ILoggerProvider provider;
        public MemoQLoggerFactory(ILoggerProvider provider)
        {
            this.provider = provider;
        }

        public void Dispose()
        {
            provider.Dispose();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return provider.CreateLogger(categoryName);
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }
    }
}