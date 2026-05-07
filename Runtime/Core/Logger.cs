using System;
using EasyToolkit.Core.Pooling;
using EasyToolkit.Logging.Sinks;
using JetBrains.Annotations;
using UnityEngine;

namespace EasyToolkit.Logging.Core.Implementations
{
    /// <summary>
    /// Default implementation of <see cref="ILogger"/> that filters and dispatches log events to sinks.
    /// </summary>
    /// <remarks>
    /// This implementation filters log events based on a minimum level threshold and dispatches
    /// enabled events to a configured <see cref="ILogEventSink"/>. The sink can be a simple
    /// implementation or an aggregate that broadcasts to multiple sinks.
    /// </remarks>
    internal sealed class Logger : ILogger
    {
        private readonly LogEventLevel _minimumLevel;
        private readonly ILogEventSink _sink;

        /// <summary>
        /// Initializes a new instance with the specified minimum level and sink.
        /// </summary>
        /// <param name="minimumLevel">The minimum level at which log events should be processed.</param>
        /// <param name="sink">The sink to which log events are dispatched.</param>
        public Logger(LogEventLevel minimumLevel, ILogEventSink sink)
        {
            _minimumLevel = minimumLevel;
            _sink = sink;
        }

        /// <summary>
        /// Determines whether the specified log level is enabled for logging.
        /// </summary>
        /// <param name="level">The log level to check.</param>
        /// <returns>True if the level is greater than or equal to the minimum level; otherwise, false.</returns>
        private bool IsEnabled(LogEventLevel level)
        {
            return level >= _minimumLevel;
        }

        /// <summary>
        /// Dispatches the log event to the configured sink.
        /// </summary>
        /// <param name="logEvent">The log event to dispatch.</param>
        [HideInCallstack]
        private void Dispatch(LogEvent logEvent)
        {
            _sink.Emit(logEvent);
            PoolUtility.ReleaseObject(logEvent);
        }

        /// <summary>
        /// Writes a log event if the level is enabled.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="exception">The optional exception associated with the log event.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        [HideInCallstack]
        private void Write(LogEventLevel level, [CanBeNull] Exception exception, string message, object context = null, UnityEngine.Object sender = null)
        {
            if (!IsEnabled(level))
                return;
            var logEvent = LogEvent.Create(DateTime.Now, level, exception, message, context, sender);
            Dispatch(logEvent);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Debug(string message, object context = null, UnityEngine.Object sender = null)
        {
            Write(LogEventLevel.Debug, null, message, context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Info(string message, object context = null, UnityEngine.Object sender = null)
        {
            Write(LogEventLevel.Info, null, message, context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Warn(string message, object context = null, UnityEngine.Object sender = null)
        {
            Write(LogEventLevel.Warn, null, message, context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Error(string message, object context = null, UnityEngine.Object sender = null)
        {
            Write(LogEventLevel.Error, null, message, context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Error(string message, Exception exception, object context = null, UnityEngine.Object sender = null)
        {
            Write(LogEventLevel.Error, exception, message, context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Fatal(string message, object context = null, UnityEngine.Object sender = null)
        {
            Write(LogEventLevel.Fatal, null, message, context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Fatal(string message, Exception exception, object context = null, UnityEngine.Object sender = null)
        {
            Write(LogEventLevel.Fatal, exception, message, context, sender);
        }
    }
}
