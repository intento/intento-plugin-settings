using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace IntentoMemoQMTPlugin
{
    /// <summary>
    /// Logger for MemoQ
    /// </summary>
    internal class MemoQLogger : ILogger
    {
        private readonly string categoryName;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="categoryName"></param>
        public MemoQLogger(string categoryName)
        {
            this.categoryName = categoryName;
        }

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            
            Kilgray.Utils.Log.WriteInfo( $"[{eventId.Id,2}: {logLevel,-12}] {categoryName} {formatter(state, exception)}");
        }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        /// <inheritdoc />
        public IDisposable BeginScope<TState>(TState state) => default!;
    }
}