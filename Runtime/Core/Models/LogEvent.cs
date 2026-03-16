using System;
using EasyToolkit.Core.Pooling;

namespace EasyToolkit.Logging.Core
{
    /// <summary>
    /// Represents a log event containing timestamp, level, message, and optional diagnostic information.
    /// </summary>
    /// <remarks>
    /// Log events are created by <see cref="ILogger"/> implementations and passed to
    /// <see cref="ILogEventSink"/> instances for output. They contain all the information
    /// needed to format and emit a log message, including stack trace for debugging.
    /// </remarks>
    public sealed class LogEvent : IPoolObject
    {
        /// <summary>
        /// Gets the timestamp when the log event occurred.
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Gets the severity level of the log event.
        /// </summary>
        public LogEventLevel Level { get; private set; }

        /// <summary>
        /// Gets the optional exception associated with the log event.
        /// </summary>
        /// <remarks>
        /// This property is null when logging a simple message without an exception.
        /// </remarks>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the log message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the optional context data associated with the log event.
        /// </summary>
        /// <remarks>
        /// This property provides additional contextual information that will be serialized
        /// into the log output. It is null when no context is provided.
        /// </remarks>
        public object Context { get; private set; }

        /// <summary>
        /// Gets the Unity object that originated this log event.
        /// </summary>
        /// <remarks>
        /// This property references the Unity object (such as a MonoBehaviour or ScriptableObject)
        /// that generated the log event. It is null when no sender is specified.
        /// </remarks>
        public UnityEngine.Object Sender { get; private set; }

        /// <summary>
        /// Initializes a new instance with the specified properties.
        /// </summary>
        /// <param name="timestamp">The timestamp when the log event occurred.</param>
        /// <param name="level">The severity level of the log event.</param>
        /// <param name="exception">The optional exception associated with the log event.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        public static LogEvent Create(DateTime timestamp, LogEventLevel level, Exception exception, string message, object context = null, UnityEngine.Object sender = null)
        {
            var logEvent = PoolUtility.RentObject<LogEvent>();
            logEvent.Timestamp = timestamp;
            logEvent.Level = level;
            logEvent.Exception = exception;
            logEvent.Message = message;
            logEvent.Context = context;
            logEvent.Sender = sender;
            return logEvent;
        }

        void IPoolObject.OnRent()
        {
        }

        void IPoolObject.OnRelease()
        {
            Timestamp = DateTime.MinValue;
            Level = LogEventLevel.Debug;
            Exception = null;
            Message = null;
            Context = null;
            Sender = null;
        }
    }
}
