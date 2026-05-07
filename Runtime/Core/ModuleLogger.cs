using System;
using JetBrains.Annotations;
using UnityEngine;

namespace EasyToolkit.Logging.Core.Implementations
{
    /// <summary>
    /// Decorator that adds module name prefixing to log messages.
    /// </summary>
    /// <remarks>
    /// This implementation wraps an <see cref="ILogger"/> and prepends the module name
    /// to all log messages, making it easier to identify which module generated each log entry.
    /// It also maintains a default Unity sender that can be used when no explicit sender is provided.
    /// </remarks>
    internal sealed class ModuleLogger : ILogger
    {
        private readonly ILogger _logger;
        private readonly string _moduleName;
        [CanBeNull] private readonly UnityEngine.Object _sender;

        /// <summary>
        /// Initializes a new instance with the specified logger, module name, and default sender.
        /// </summary>
        /// <param name="logger">The underlying logger to which log events are dispatched.</param>
        /// <param name="moduleName">The module name to prefix to all log messages.</param>
        /// <param name="sender">The default Unity object that originated log events for this module.</param>
        public ModuleLogger(ILogger logger, string moduleName, [CanBeNull] UnityEngine.Object sender)
        {
            _logger = logger;
            _moduleName = moduleName;
            _sender = sender;
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Debug(string message, object context = null, UnityEngine.Object sender = null)
        {
            sender ??= _sender;
            _logger.Debug($"[{_moduleName}] {message}", context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Info(string message, object context = null, UnityEngine.Object sender = null)
        {
            sender ??= _sender;
            _logger.Info($"[{_moduleName}] {message}", context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Warn(string message, object context = null, UnityEngine.Object sender = null)
        {
            sender ??= _sender;
            _logger.Warn($"[{_moduleName}] {message}", context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Error(string message, object context = null, UnityEngine.Object sender = null)
        {
            sender ??= _sender;
            _logger.Error($"[{_moduleName}] {message}", context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Error(string message, Exception exception, object context = null, UnityEngine.Object sender = null)
        {
            sender ??= _sender;
            _logger.Error($"[{_moduleName}] {message}", exception, context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Fatal(string message, object context = null, UnityEngine.Object sender = null)
        {
            sender ??= _sender;
            _logger.Fatal($"[{_moduleName}] {message}", context, sender);
        }

        /// <inheritdoc/>
        [HideInCallstack]
        public void Fatal(string message, Exception exception, object context = null, UnityEngine.Object sender = null)
        {
            sender ??= _sender;
            _logger.Fatal($"[{_moduleName}] {message}", exception, context, sender);
        }
    }
}
