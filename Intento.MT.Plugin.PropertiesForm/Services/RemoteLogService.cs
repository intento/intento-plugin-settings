using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Timers;
using Intento.SDK.Logging;
using Intento.SDK.Logging.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace Intento.MT.Plugin.PropertiesForm.Services
{
    /// <summary>
    /// Telemetry service implementation
    /// </summary>
    internal class RemoteLogService : IRemoteLogService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="telemetryService"></param>
        public RemoteLogService(ILogger<RemoteLogService> logger, ITelemetryService telemetryService)
        {
            this.logger = logger;
            this.telemetryService = telemetryService;
            sessionId = Guid.NewGuid().ToString();
            sender = new Timer();
            sender.Interval = SleepTime;
            sender.Elapsed += OnTimedEvent;
            sender.Start();
        }

        private readonly ITelemetryService telemetryService;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Url to post logs
        /// </summary>
        private const string Url = "https://api.inten.to/telemetry/upload_json";

        /// <summary>
        /// Limiting the request rate, ms
        /// </summary>
        const int SleepTime = 5000;

        /// <summary>
        /// Limit of entries from the queue in one request
        /// </summary>
        const int MaxEntries = 300;

        /// <summary>
        /// A stream instance that checks the data queue and sends it to the cloud
        /// </summary>
        private readonly Timer sender;

        /// <summary>
        /// Trace end time from interface
        /// </summary>
        private DateTime traceEndTime;

        private string pluginName;

        private readonly string sessionId;

        /// <summary>
        /// Object for sync
        /// </summary>
        private readonly object syncObject = new();

        /// <summary>
        /// Data queue
        /// </summary>
        private ConcurrentQueue<KeyValuePair<char, string>> queue = new();

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public void SetTraceEndTime(DateTime traceEndTime)
        {
            lock (syncObject)
            {
                this.traceEndTime = traceEndTime;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public DateTime GetTraceEndTime()
        {
            lock (syncObject)
            {
                return traceEndTime;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public void Init(string pluginName)
        {
            lock (syncObject)
            {
                this.pluginName = pluginName;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public bool IsTrace(string pluginId = null)
        {
            var key = Registry.CurrentUser.CreateSubKey(pluginId != null
                ? $"Software\\Intento\\{pluginId}"
                : "Software\\Intento");

            if (key != null)
            {
                var loggingReg = (string)key.GetValue("Logging", null);
                if (loggingReg != null)
                {
                    loggingReg = loggingReg.ToLower();
                    if (loggingReg == "1" || loggingReg == "true")
                        return true;
                }
            }

            var loggingEnv = Environment.GetEnvironmentVariable("intento_plugin_logging");
            if (loggingEnv != null)
            {
                loggingEnv = loggingEnv.ToLower();
                if (loggingEnv == "1" || loggingEnv == "true")
                    return true;
            }

            return (traceEndTime - DateTime.Now).Minutes > 0;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public bool IsLogging(string pluginId = null)
        {
            string env = Environment.GetEnvironmentVariable("intento_plugin_logging");
            if (env != null)
            {
                env = env.ToLower();
                if (env == "1" || env == "true")
                    return true;
            }

            return IsTrace(pluginId);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public void Write(char id, string subject, string comment = null, Exception ex = null)
        {
            if (!IsTrace())
            {
                return;
            }

            var now = DateTime.UtcNow;
            var content = new StringBuilder();
            content.AppendLine($"{now:yyyy-MM-dd HH:mm:ss.fffff} {subject}");
            if (comment != null)
            {
                content.AppendLine(comment);
            }

            if (ex != null)
            {
                var errors = LoggingEx(ex);
                foreach (var error in errors)
                {
                    content.AppendLine(error);
                }
            }

            WriteLogInner(id, content.ToString());
        }

        private void WriteLogInner(char id, string text)
        {
            if (!IsLogging())
            {
                return;
            }

            text ??= "";


            queue.Enqueue(new KeyValuePair<char, string>(id, text));
        }

        public static IEnumerable<string> LoggingEx(Exception ex)
        {
            var items = new List<string>
            {
                $"Exception {ex.Message}"
            };
            if (ex.StackTrace != null)
            {
                items.Add("Stack Trace:");
                items.Add(ex.StackTrace);
            }

            if (ex.InnerException != null)
            {
                items.AddRange(LoggingEx(ex.InnerException));
            }

            return items;
        }

        private async void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var inprogress = new Dictionary<char, string>();
            var data = new TelemetryLogItem
            {
                SessionId = sessionId
            };
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var entries = 0;
            while (queue.TryDequeue(out var item) && entries < MaxEntries)
            {
                if (inprogress.ContainsKey(item.Key))
                {
                    inprogress[item.Key] += "\n" + item.Value;
                }
                else
                {
                    inprogress.Add(item.Key, item.Value);
                }

                entries++;
            }

            foreach (var kp in inprogress)
            {
                data.PluginName = $"{pluginName}-{kp.Key}";
                data.Logs = kp.Value;
                try
                {
                    await telemetryService.Send(data);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Can't send log to telemetry API: {0}", JsonConvert.SerializeObject(data));
                }
            }
        }
    }
}