using EasyToolkit.Logging.Core;

namespace EasyToolkit.Logging.Configuration
{
    /// <summary>
    /// Provides extension methods for configuring the minimum log level.
    /// </summary>
    public static class LoggerMinimumLevelExtensions
    {
        /// <summary>
        /// Sets the minimum log level to <see cref="LogEventLevel.Debug"/>.
        /// </summary>
        /// <param name="minimumLevelConfiguration">The minimum level configuration.</param>
        /// <returns>The logger configuration for chaining.</returns>
        public static LoggerConfiguration Debug(this LoggerMinimumLevelConfiguration minimumLevelConfiguration)
        {
            return minimumLevelConfiguration.Is(LogEventLevel.Debug);
        }

        /// <summary>
        /// Sets the minimum log level to <see cref="LogEventLevel.Info"/>.
        /// </summary>
        /// <param name="minimumLevelConfiguration">The minimum level configuration.</param>
        /// <returns>The logger configuration for chaining.</returns>
        public static LoggerConfiguration Info(this LoggerMinimumLevelConfiguration minimumLevelConfiguration)
        {
            return minimumLevelConfiguration.Is(LogEventLevel.Info);
        }

        /// <summary>
        /// Sets the minimum log level to <see cref="LogEventLevel.Warn"/>.
        /// </summary>
        /// <param name="minimumLevelConfiguration">The minimum level configuration.</param>
        /// <returns>The logger configuration for chaining.</returns>
        public static LoggerConfiguration Warn(this LoggerMinimumLevelConfiguration minimumLevelConfiguration)
        {
            return minimumLevelConfiguration.Is(LogEventLevel.Warn);
        }

        /// <summary>
        /// Sets the minimum log level to <see cref="LogEventLevel.Error"/>.
        /// </summary>
        /// <param name="minimumLevelConfiguration">The minimum level configuration.</param>
        /// <returns>The logger configuration for chaining.</returns>
        public static LoggerConfiguration Error(this LoggerMinimumLevelConfiguration minimumLevelConfiguration)
        {
            return minimumLevelConfiguration.Is(LogEventLevel.Error);
        }

        /// <summary>
        /// Sets the minimum log level to <see cref="LogEventLevel.Fatal"/>.
        /// </summary>
        /// <param name="minimumLevelConfiguration">The minimum level configuration.</param>
        /// <returns>The logger configuration for chaining.</returns>
        public static LoggerConfiguration Fatal(this LoggerMinimumLevelConfiguration minimumLevelConfiguration)
        {
            return minimumLevelConfiguration.Is(LogEventLevel.Fatal);
        }
    }
}
