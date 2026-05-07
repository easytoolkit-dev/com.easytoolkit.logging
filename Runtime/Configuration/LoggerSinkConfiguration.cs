using System;
using EasyToolkit.Logging.Sinks;

namespace EasyToolkit.Logging.Configuration
{
    /// <summary>
    /// Provides fluent configuration for adding log event sinks.
    /// </summary>
    /// <remarks>
    /// This class provides the sink configuration functionality for the fluent builder pattern.
    /// It maintains a reference to the parent configuration and uses a delegate to add sinks
    /// to the configuration's internal collection.
    /// </remarks>
    public sealed class LoggerSinkConfiguration
    {
        private readonly LoggerConfiguration _loggerConfiguration;
        private readonly Action<ILogEventSink> _addSink;

        /// <summary>
        /// Initializes a new instance with the specified parent configuration and sink addition delegate.
        /// </summary>
        /// <param name="loggerConfiguration">The parent logger configuration for fluent chaining.</param>
        /// <param name="addSink">The delegate that adds sinks to the configuration's internal collection.</param>
        internal LoggerSinkConfiguration(LoggerConfiguration loggerConfiguration, Action<ILogEventSink> addSink)
        {
            _loggerConfiguration = loggerConfiguration;
            _addSink = addSink;
        }

        /// <summary>
        /// Adds a custom log event sink to the logger configuration.
        /// </summary>
        /// <param name="sink">The sink to add for receiving log events.</param>
        /// <returns>The logger configuration for chaining additional configuration calls.</returns>
        /// <remarks>
        /// The sink will receive all log events that pass the minimum level filter.
        /// Multiple sinks can be added to broadcast log events to different destinations.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="sink"/> is null.</exception>
        public LoggerConfiguration Sink(ILogEventSink sink)
        {
            if (sink == null)
                throw new ArgumentNullException(nameof(sink));

            _addSink(sink);
            return _loggerConfiguration;
        }
    }
}
