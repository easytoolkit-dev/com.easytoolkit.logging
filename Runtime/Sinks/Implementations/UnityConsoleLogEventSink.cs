using System;
using EasyToolkit.Core.Textual;
using EasyToolkit.Logging.Core;
using EasyToolkit.Serialization;
using UnityEngine;

namespace EasyToolkit.Logging.Sinks.Implementations
{
    /// <summary>
    /// A log event sink that emits log events to the Unity console.
    /// </summary>
    /// <remarks>
    /// This sink maps <see cref="LogEventLevel"/> to Unity's <see cref="LogType"/> and outputs
    /// log messages using the Unity logger.
    /// </remarks>
    internal class UnityConsoleLogEventSink : ILogEventSink
    {
        private readonly UnityEngine.ILogger _unityLogger;

        /// <summary>
        /// Initializes a new instance with the specified Unity logger.
        /// </summary>
        /// <param name="unityLogger">The Unity logger to use for outputting log events.</param>
        public UnityConsoleLogEventSink(UnityEngine.ILogger unityLogger)
        {
            _unityLogger = unityLogger;
        }

        /// <summary>
        /// Emits the log event to the Unity console.
        /// </summary>
        /// <param name="logEvent">The log event to emit.</param>
        public void Emit(LogEvent logEvent)
        {
            LogType type;
            switch (logEvent.Level)
            {
                case LogEventLevel.Debug:
                case LogEventLevel.Info:
                    type = LogType.Log;
                    break;
                case LogEventLevel.Warn:
                    type = LogType.Warning;
                    break;
                case LogEventLevel.Error:
                case LogEventLevel.Fatal:
                    type = LogType.Error;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var message = logEvent.Message;
            if (logEvent.Context != null)
            {
                var contextText = LoggingUtility.ConvertContextToJson(logEvent.Context);
                message = $"{message} | {contextText}";
            }
            if (logEvent.Exception != null)
            {
                _unityLogger.Log(type, (object)message, logEvent.Sender);
                _unityLogger.LogException(logEvent.Exception, logEvent.Sender);
            }
            else
            {
                _unityLogger.Log(type, (object)message, logEvent.Sender);
            }
        }
    }
}
