using System;

namespace Intento.MT.Plugin.PropertiesForm.Services
{
    /// <summary>
    /// Service for writing logs
    /// </summary>
    public interface IRemoteLogService
    {
        /// <summary>
        /// Init service
        /// </summary>
        /// <param name="pluginName"></param>
        void Init(string pluginName);
        
        /// <summary>
        /// Is trace enabled
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns></returns>
        bool IsTrace(string pluginId = null);
        
        /// <summary>
        /// Is logging enabled
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns></returns>
        bool IsLogging(string pluginId = null);

        /// <summary>
        /// Set TraceEndDate
        /// </summary>
        /// <param name="traceEndTime"></param>
        void SetTraceEndTime(DateTime traceEndTime);

        /// <summary>
        /// Get TraceEndDate
        /// </summary>
        /// <returns></returns>
        DateTime GetTraceEndTime();
        
        /// <summary>
        /// Write logs to telemetry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subject"></param>
        /// <param name="comment"></param>
        /// <param name="ex"></param>
        void Write(char id, string subject, string comment = null, Exception ex = null);
    }
}