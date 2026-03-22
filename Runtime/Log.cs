using System;
using UnityEngine;

namespace EasyToolkit.Logging
{
    /// <summary>
    /// Provides static access to logging functionality.
    /// </summary>
    /// <remarks>
    /// This class provides a convenient static accessor for logging throughout the application.
    /// Set the <see cref="Logger"/> property with a configured logger instance before use.
    /// All methods delegate to the configured logger.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Initialize during application startup
    /// Log.Logger = LoggerFactory.Configure()
    ///     .MinimumLevel.Info()
    ///     .WriteTo.UnityConsole()
    ///     .CreateLogger();
    ///
    /// // Use anywhere in the application
    /// Log.Info("Application started");
    /// Log.Error(ex, "An error occurred");
    /// </code>
    /// </example>
    public static class Log
    {
        /// <summary>
        /// Gets or sets the logger instance used for all static logging methods.
        /// </summary>
        /// <remarks>
        /// This property must be set before calling any logging methods. Typically configured
        /// during application initialization.
        /// </remarks>
        public static ILogger Logger { get; set; }

        /// <summary>
        /// Creates a module-scoped logger that prefixes all log messages with the specified module name.
        /// </summary>
        /// <param name="moduleName">The module name to prefix to all log messages.</param>
        /// <param name="sender">The default Unity object that originates log events for this module.</param>
        /// <returns>A module-scoped <see cref="ILogger"/> that prepends the module name to all messages.</returns>
        /// <remarks>
        /// Module loggers are useful for organizing logs from different subsystems or components.
        /// Each log message will be automatically prefixed with "[ModuleName]" for easy identification.
        /// The provided sender is used as the default context when no explicit sender is specified.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Create a module logger for a network subsystem
        /// var networkLog = Log.Module("Network", this);
        /// networkLog.Info("Connected to server");  // Output: "[Network] Connected to server"
        /// networkLog.Error("Connection lost");     // Output: "[Network] Connection lost"
        /// </code>
        /// </example>
        public static ILogger Module(string moduleName, UnityEngine.Object sender = null)
        {
            return new Core.Implementations.ModuleLogger(Logger, moduleName, sender);
        }

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
        [HideInCallstack]
        public static void Debug(string message, object context = null, UnityEngine.Object sender = null)
        {
            Logger.Debug(message, context, sender);
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Informational messages track the general flow of the application.
        /// </remarks>
        [HideInCallstack]
        public static void Info(string message, object context = null, UnityEngine.Object sender = null)
        {
            Logger.Info(message, context, sender);
        }

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
        [HideInCallstack]
        public static void Warn(string message, object context = null, UnityEngine.Object sender = null)
        {
            Logger.Warn(message, context, sender);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Error messages indicate error events that might still allow the application to continue running.
        /// </remarks>
        [HideInCallstack]
        public static void Error(string message, object context = null, UnityEngine.Object sender = null)
        {
            Logger.Error(message, context, sender);
        }

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
        [HideInCallstack]
        public static void Error(string message, Exception exception, object context = null,
            UnityEngine.Object sender = null)
        {
            Logger.Error(message, exception, context, sender);
        }

        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="context">Optional context data to be serialized into the log.</param>
        /// <param name="sender">The Unity object that originated this log event.</param>
        /// <remarks>
        /// Fatal messages indicate critical errors that may cause the application to terminate.
        /// </remarks>
        [HideInCallstack]
        public static void Fatal(string message, object context = null, UnityEngine.Object sender = null)
        {
            Logger.Fatal(message, context, sender);
        }

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
        [HideInCallstack]
        public static void Fatal(string message, Exception exception, object context = null,
            UnityEngine.Object sender = null)
        {
            Logger.Fatal(message, exception, context, sender);
        }
    }
}
