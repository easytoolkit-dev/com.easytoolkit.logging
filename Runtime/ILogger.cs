using System;

namespace EasyToolkit.Logging
{
    /// <summary>
    /// Defines a logger that records log events at various severity levels.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for logging messages at different severity levels.
    /// Log events are dispatched to configured sinks for output to destinations such as console, files, or external services.
    /// The minimum log level can be configured to filter out messages below a certain severity.
    /// </remarks>
    public interface ILogger
    {
        /// <summary>
        /// Logs a debug-level message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Debug messages are typically used for detailed information during development
        /// and troubleshooting. They are usually disabled in production builds.
        /// </remarks>
        void Debug(string message, object context = null, UnityEngine.Object sender = null);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Informational messages track the general flow of the application.
        /// </remarks>
        void Info(string message, object context = null, UnityEngine.Object sender = null);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Warning messages indicate potentially harmful situations or important events
        /// that do not prevent the application from continuing.
        /// </remarks>
        void Warn(string message, object context = null, UnityEngine.Object sender = null);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Error messages indicate error events that might still allow the application to continue running.
        /// </remarks>
        void Error(string message, object context = null, UnityEngine.Object sender = null);

        /// <summary>
        /// Logs an error message with an associated exception.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Error messages indicate error events that might still allow the application to continue running.
        /// </remarks>
        void Error(string message, Exception exception, object context = null, UnityEngine.Object sender = null);

        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Fatal messages indicate critical errors that may cause the application to terminate.
        /// </remarks>
        void Fatal(string message, object context = null, UnityEngine.Object sender = null);

        /// <summary>
        /// Logs a fatal error message with an associated exception.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Fatal messages indicate critical errors that may cause the application to terminate.
        /// </remarks>
        void Fatal(string message, Exception exception, object context = null, UnityEngine.Object sender = null);
    }
}
