using System;
using System.Collections.Generic;
using EasyToolkit.Logging.Core;
using EasyToolkit.Logging.Sinks;

namespace EasyToolkit.Logging.Configuration
{
    /// <summary>
    /// Provides fluent settings for building configured logger instances.
    /// </summary>
    /// <remarks>
    /// This class collects sink configurations and minimum level settings through a fluent interface,
    /// then creates a <see cref="Logger"/> instance with those settings. The sinks are aggregated
    /// into a single composite sink that broadcasts events to all registered sinks.
    /// </remarks>
    /// <example>
    /// <code>
    /// var logger = new LoggerConfiguration()
    ///     .MinimumLevel.Debug()
    ///     .WriteTo.UnityConsole()
    ///     .CreateLogger();
    /// </code>
    /// </example>
    public sealed class LoggerConfiguration
    {
        private readonly List<ILogEventSink> _sinks = new();
        private bool _loggerCreated;
        private LogEventLevel _minimumLevel = LogEventLevel.Info;

        /// <summary>
        /// Gets the sink configuration for adding log event sinks.
        /// </summary>
        /// <remarks>
        /// Use this property to add sinks that will receive log events. Multiple sinks can be
        /// chained to output to different destinations simultaneously.
        /// </remarks>
        public LoggerSinkConfiguration WriteTo { get; }

        /// <summary>
        /// Gets the minimum level configuration for setting the log level threshold.
        /// </summary>
        /// <remarks>
        /// Use this property to set the minimum level at which log events should be processed.
        /// Events below this level will be filtered out.
        /// </remarks>
        public LoggerMinimumLevelConfiguration MinimumLevel { get; }

        /// <summary>
        /// Initializes a new instance with default configuration.
        /// </summary>
        /// <remarks>
        /// Initializes the <see cref="WriteTo"/> and <see cref="MinimumLevel"/> configuration objects
        /// with delegates that modify this configuration instance. The default minimum level is Info.
        /// </remarks>
        public LoggerConfiguration()
        {
            WriteTo = new LoggerSinkConfiguration(this, sink => _sinks.Add(sink));
            MinimumLevel = new LoggerMinimumLevelConfiguration(this, level => _minimumLevel = level);
        }

        /// <summary>
        /// Creates a new logger instance with the configured settings.
        /// </summary>
        /// <returns>A new <see cref="ILogger"/> instance.</returns>
        /// <remarks>
        /// This method can only be called once per configuration instance. Subsequent calls
        /// will throw an exception.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <see cref="CreateLogger"/> has been called previously on this instance.
        /// </exception>
        public ILogger CreateLogger()
        {
            if (_loggerCreated)
                throw new InvalidOperationException("CreateLogger() was previously called and can only be called once.");
            _loggerCreated = true;

            var sink = new Sinks.Implementations.AggregateLogEventSink(_sinks);
            return new Core.Implementations.Logger(_minimumLevel, sink);
        }
    }
}
