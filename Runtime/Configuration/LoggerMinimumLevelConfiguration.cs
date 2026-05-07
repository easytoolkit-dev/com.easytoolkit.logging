using System;
using EasyToolkit.Logging.Core;

namespace EasyToolkit.Logging.Configuration
{
    /// <summary>
    /// Provides fluent configuration for setting the minimum log level.
    /// </summary>
    /// <remarks>
    /// This class provides the minimum level configuration functionality for the fluent builder pattern.
    /// It maintains a reference to the parent configuration and uses a delegate to set the
    /// minimum level in the configuration.
    /// </remarks>
    public sealed class LoggerMinimumLevelConfiguration
    {
        private readonly LoggerConfiguration _loggerConfiguration;
        private readonly Action<LogEventLevel> _setMinimum;

        /// <summary>
        /// Initializes a new instance with the specified parent configuration and level setting delegate.
        /// </summary>
        /// <param name="loggerConfiguration">The parent logger configuration for fluent chaining.</param>
        /// <param name="setMinimum">The delegate that sets the minimum level in the configuration.</param>
        internal LoggerMinimumLevelConfiguration(LoggerConfiguration loggerConfiguration, Action<LogEventLevel> setMinimum)
        {
            _loggerConfiguration = loggerConfiguration;
            _setMinimum = setMinimum;
        }

        /// <summary>
        /// Sets the minimum log level to the specified value.
        /// </summary>
        /// <param name="minimumLevel">The minimum level at which log events should be processed.</param>
        /// <returns>The logger configuration for chaining additional configuration calls.</returns>
        /// <remarks>
        /// Only log events at or above the specified level will be processed.
        /// For example, setting to <see cref="LogEventLevel.Warn"/> will filter out
        /// Debug and Info messages but process Warn, Error, and Fatal messages.
        /// </remarks>
        public LoggerConfiguration Is(LogEventLevel minimumLevel)
        {
            _setMinimum(minimumLevel);
            return _loggerConfiguration;
        }
    }
}
