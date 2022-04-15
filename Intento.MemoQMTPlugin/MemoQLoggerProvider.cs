using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Logging;

namespace IntentoMemoQMTPlugin
{
    /// <summary>
    /// Implementation of logger provider
    /// </summary>
    internal class MemoQLoggerProvider: ILoggerProvider
    {
        internal static readonly string LogFilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MemoQ", "Log", "IntentoMTPlugin.log");
        
        private readonly ConcurrentDictionary<string, MemoQLogger> loggers =
            new(StringComparer.OrdinalIgnoreCase);

        public ILogger CreateLogger(string categoryName) =>
            loggers.GetOrAdd(categoryName, name => new MemoQLogger(name));
       

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}