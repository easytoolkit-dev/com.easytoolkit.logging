using System;
using EasyToolkit.Logging.Configuration;
using EasyToolkit.Logging.Core;
using NUnit.Framework;

namespace EasyToolkit.Logging.Tests
{
    /// <summary>
    /// Unit tests for <see cref="Log"/> static class.
    /// </summary>
    public class TestLoggingLog
    {
        #region Setup and Teardown

        /// <summary>
        /// Stores the original logger to restore after each test.
        /// </summary>
        private ILogger _originalLogger;

        /// <summary>
        /// Stores the test sink for inspection.
        /// </summary>
        private LoggingTestHelperTypes.TestLogEventSink _testSink;

        /// <summary>
        /// Saves the original logger and sets up a test logger before each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _originalLogger = Log.Logger;
            _testSink = new LoggingTestHelperTypes.TestLogEventSink();
            Log.Logger = LoggerFactory.Configure()
                .MinimumLevel.Debug()
                .WriteTo.Sink(_testSink)
                .CreateLogger();
        }

        /// <summary>
        /// Restores the original logger after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Log.Logger = _originalLogger;
        }

        #endregion

        #region Debug Method Tests

        /// <summary>
        /// Verifies that Debug logs a message at Debug level.
        /// </summary>
        [Test]
        public void Debug_Message_EmitsDebugEvent()
        {
            // Arrange
            var message = "Debug message";

            // Act
            Log.Debug(message);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Debug, _testSink.LastEvent.Level);
            Assert.AreEqual(message, _testSink.LastEvent.Message);
        }

        #endregion

        #region Info Method Tests

        /// <summary>
        /// Verifies that Info logs a message at Info level.
        /// </summary>
        [Test]
        public void Info_Message_EmitsInfoEvent()
        {
            // Arrange
            var message = "Info message";

            // Act
            Log.Info(message);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Info, _testSink.LastEvent.Level);
            Assert.AreEqual(message, _testSink.LastEvent.Message);
        }

        #endregion

        #region Warn Method Tests

        /// <summary>
        /// Verifies that Warn logs a message at Warn level.
        /// </summary>
        [Test]
        public void Warn_Message_EmitsWarnEvent()
        {
            // Arrange
            var message = "Warning message";

            // Act
            Log.Warn(message);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Warn, _testSink.LastEvent.Level);
            Assert.AreEqual(message, _testSink.LastEvent.Message);
        }

        #endregion

        #region Error Method Tests

        /// <summary>
        /// Verifies that Error logs a message at Error level.
        /// </summary>
        [Test]
        public void Error_Message_EmitsErrorEvent()
        {
            // Arrange
            var message = "Error message";

            // Act
            Log.Error(message);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Error, _testSink.LastEvent.Level);
            Assert.AreEqual(message, _testSink.LastEvent.Message);
        }

        /// <summary>
        /// Verifies that Error with exception captures the exception.
        /// </summary>
        [Test]
        public void Error_ExceptionAndMessage_EmitsErrorEventWithException()
        {
            // Arrange
            var exception = new InvalidOperationException("Test exception");
            var message = "Error occurred";

            // Act
            Log.Error(message,exception);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Error, _testSink.LastEvent.Level);
            Assert.AreSame(exception, _testSink.LastEvent.Exception);
            Assert.AreEqual(message, _testSink.LastEvent.Message);
        }

        /// <summary>
        /// Verifies that Error with exception works without message.
        /// </summary>
        [Test]
        public void Error_ExceptionOnly_EmitsErrorEventWithException()
        {
            // Arrange
            var exception = new ArgumentException("Invalid argument");

            // Act
            Log.Error(null, exception);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Error, _testSink.LastEvent.Level);
            Assert.AreSame(exception, _testSink.LastEvent.Exception);
        }

        /// <summary>
        /// Verifies that Error with null exception and message works correctly.
        /// </summary>
        [Test]
        public void Error_NullException_EmitsErrorEventWithoutException()
        {
            // Arrange
            var message = "Error without exception";

            // Act
            Log.Error(message);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Error, _testSink.LastEvent.Level);
            Assert.IsNull(_testSink.LastEvent.Exception);
            Assert.AreEqual(message, _testSink.LastEvent.Message);
        }

        #endregion

        #region Fatal Method Tests

        /// <summary>
        /// Verifies that Fatal logs a message at Fatal level.
        /// </summary>
        [Test]
        public void Fatal_Message_EmitsFatalEvent()
        {
            // Arrange
            var message = "Fatal message";

            // Act
            Log.Fatal(message);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Fatal, _testSink.LastEvent.Level);
            Assert.AreEqual(message, _testSink.LastEvent.Message);
        }

        /// <summary>
        /// Verifies that Fatal with exception captures the exception.
        /// </summary>
        [Test]
        public void Fatal_ExceptionAndMessage_EmitsFatalEventWithException()
        {
            // Arrange
            var exception = new NotImplementedException("Not implemented");
            var message = "Fatal error occurred";

            // Act
            Log.Fatal(message, exception);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Fatal, _testSink.LastEvent.Level);
            Assert.AreSame(exception, _testSink.LastEvent.Exception);
            Assert.AreEqual(message, _testSink.LastEvent.Message);
        }

        /// <summary>
        /// Verifies that Fatal with exception works without message.
        /// </summary>
        [Test]
        public void Fatal_ExceptionOnly_EmitsFatalEventWithException()
        {
            // Arrange
            var exception = new OutOfMemoryException("Out of memory");

            // Act
            Log.Fatal(null, exception);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Fatal, _testSink.LastEvent.Level);
            Assert.AreSame(exception, _testSink.LastEvent.Exception);
        }

        /// <summary>
        /// Verifies that Fatal with null exception and message works correctly.
        /// </summary>
        [Test]
        public void Fatal_NullException_EmitsFatalEventWithoutException()
        {
            // Arrange
            var message = "Fatal without exception";

            // Act
            Log.Fatal(message);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Fatal, _testSink.LastEvent.Level);
            Assert.IsNull(_testSink.LastEvent.Exception);
            Assert.AreEqual(message, _testSink.LastEvent.Message);
        }

        #endregion

        #region Null Logger Tests

        /// <summary>
        /// Verifies that calling Debug with null logger throws NullReferenceException.
        /// </summary>
        [Test]
        public void Debug_NullLogger_ThrowsNullReferenceException()
        {
            // Arrange
            Log.Logger = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => Log.Debug("Test message"));
        }

        /// <summary>
        /// Verifies that calling Info with null logger throws NullReferenceException.
        /// </summary>
        [Test]
        public void Info_NullLogger_ThrowsNullReferenceException()
        {
            // Arrange
            Log.Logger = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => Log.Info("Test message"));
        }

        /// <summary>
        /// Verifies that calling Warn with null logger throws NullReferenceException.
        /// </summary>
        [Test]
        public void Warn_NullLogger_ThrowsNullReferenceException()
        {
            // Arrange
            Log.Logger = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => Log.Warn("Test message"));
        }

        /// <summary>
        /// Verifies that calling Error with null logger throws NullReferenceException.
        /// </summary>
        [Test]
        public void Error_NullLogger_ThrowsNullReferenceException()
        {
            // Arrange
            Log.Logger = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => Log.Error("Test message"));
        }

        /// <summary>
        /// Verifies that calling Fatal with null logger throws NullReferenceException.
        /// </summary>
        [Test]
        public void Fatal_NullLogger_ThrowsNullReferenceException()
        {
            // Arrange
            Log.Logger = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => Log.Fatal("Test message"));
        }

        #endregion

        #region Level Filtering Tests

        /// <summary>
        /// Verifies that Debug messages are filtered when minimum level is Info.
        /// </summary>
        [Test]
        public void Debug_MinimumLevelInfo_DoesNotEmit()
        {
            // Arrange
            _testSink.Clear();
            Log.Logger = LoggerFactory.Configure()
                .MinimumLevel.Info()
                .WriteTo.Sink(_testSink)
                .CreateLogger();

            // Act
            Log.Debug("Debug message");

            // Assert
            Assert.AreEqual(0, _testSink.Count);
        }

        /// <summary>
        /// Verifies that Info and above are emitted when minimum level is Info.
        /// </summary>
        [Test]
        public void InfoAndAbove_MinimumLevelInfo_EmitsAll()
        {
            // Arrange
            _testSink.Clear();
            Log.Logger = LoggerFactory.Configure()
                .MinimumLevel.Info()
                .WriteTo.Sink(_testSink)
                .CreateLogger();

            // Act
            Log.Info("Info message");
            Log.Warn("Warning message");
            Log.Error("Error message");
            Log.Fatal("Fatal message");

            // Assert
            Assert.AreEqual(4, _testSink.Count);
        }

        #endregion

        #region Multiple Log Calls Tests

        /// <summary>
        /// Verifies that multiple log calls are captured in order.
        /// </summary>
        [Test]
        public void MultipleLogCalls_CaptureInOrder()
        {
            // Act
            Log.Debug("Debug message");
            Log.Info("Info message");
            Log.Warn("Warning message");
            Log.Error("Error message");
            Log.Fatal("Fatal message");

            // Assert
            Assert.AreEqual(5, _testSink.Count);
            Assert.AreEqual(LogEventLevel.Debug, _testSink.LogEvents[0].Level);
            Assert.AreEqual(LogEventLevel.Info, _testSink.LogEvents[1].Level);
            Assert.AreEqual(LogEventLevel.Warn, _testSink.LogEvents[2].Level);
            Assert.AreEqual(LogEventLevel.Error, _testSink.LogEvents[3].Level);
            Assert.AreEqual(LogEventLevel.Fatal, _testSink.LogEvents[4].Level);
        }

        #endregion

        #region Logger Property Tests

        /// <summary>
        /// Verifies that Logger property can be set and retrieved.
        /// </summary>
        [Test]
        public void Logger_PropertyCanBeSetAndRetrieved()
        {
            // Arrange
            var testLogger = LoggerFactory.Configure()
                .MinimumLevel.Debug()
                .WriteTo.Sink(new LoggingTestHelperTypes.TestLogEventSink())
                .CreateLogger();

            // Act
            Log.Logger = testLogger;
            var retrieved = Log.Logger;

            // Assert
            Assert.AreSame(testLogger, retrieved);
        }

        #endregion

        #region Context Serialization Tests

        /// <summary>
        /// Verifies that Debug logs with context object.
        /// </summary>
        [Test]
        public void Debug_WithContext_CapturesContext()
        {
            // Arrange
            var testContext = new { PlayerId = 123, Health = 100 };

            // Act
            Log.Debug("Player spawned", testContext);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.IsNotNull(_testSink.LastEvent.Context);
            Assert.AreSame(testContext, _testSink.LastEvent.Context);
        }

        /// <summary>
        /// Verifies that Info logs with context and serializes to JSON.
        /// </summary>
        [Test]
        public void Info_WithAnonymousContext_SerializesToJson()
        {
            // Arrange
            var testContext = new { Level = 5, Score = 1250.5f };

            // Act
            Log.Info("Game state", testContext);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            var contextJson = LoggingUtility.ConvertContextToJson(_testSink.LastEvent.Context);
            Assert.IsNotNull(contextJson);
            StringAssert.Contains("Level", contextJson);
            StringAssert.Contains("5", contextJson);
            StringAssert.Contains("Score", contextJson);
            StringAssert.Contains("1250.5", contextJson);
        }

        /// <summary>
        /// Verifies that Warn logs with context object.
        /// </summary>
        [Test]
        public void Warn_WithContext_CapturesContext()
        {
            // Arrange
            var testContext = new { WarningCode = "LOW_HEALTH", Threshold = 10 };

            // Act
            Log.Warn("Health warning", testContext);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.IsNotNull(_testSink.LastEvent.Context);
            Assert.AreSame(testContext, _testSink.LastEvent.Context);
        }

        /// <summary>
        /// Verifies that Error logs with context and serializes correctly.
        /// </summary>
        [Test]
        public void Error_WithContext_SerializesToJson()
        {
            // Arrange
            var testContext = new { ErrorCode = 500, Details = "Internal error" };

            // Act
            Log.Error("Server error", testContext);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            var contextJson = LoggingUtility.ConvertContextToJson(_testSink.LastEvent.Context);
            Assert.IsNotNull(contextJson);
            StringAssert.Contains("ErrorCode", contextJson);
            StringAssert.Contains("500", contextJson);
            StringAssert.Contains("Details", contextJson);
            StringAssert.Contains("Internal error", contextJson);
        }

        /// <summary>
        /// Verifies that Error with exception and context captures both.
        /// </summary>
        [Test]
        public void Error_WithExceptionAndContext_CapturesBoth()
        {
            // Arrange
            var exception = new InvalidOperationException("Test exception");
            var testContext = new { RetryCount = 3, MaxRetries = 5 };

            // Act
            Log.Error("Operation failed", exception, testContext);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreSame(exception, _testSink.LastEvent.Exception);
            Assert.IsNotNull(_testSink.LastEvent.Context);
            Assert.AreSame(testContext, _testSink.LastEvent.Context);
        }

        /// <summary>
        /// Verifies that Fatal logs with context and serializes correctly.
        /// </summary>
        [Test]
        public void Fatal_WithContext_SerializesToJson()
        {
            // Arrange
            var testContext = new { FatalCode = "CRASH", StackTrace = "Main::Update" };

            // Act
            Log.Fatal("Application crashed", testContext);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            var contextJson = LoggingUtility.ConvertContextToJson(_testSink.LastEvent.Context);
            Assert.IsNotNull(contextJson);
            StringAssert.Contains("FatalCode", contextJson);
            StringAssert.Contains("CRASH", contextJson);
            StringAssert.Contains("StackTrace", contextJson);
            StringAssert.Contains("Main::Update", contextJson);
        }

        /// <summary>
        /// Verifies that Fatal with exception and context captures both.
        /// </summary>
        [Test]
        public void Fatal_WithExceptionAndContext_CapturesBoth()
        {
            // Arrange
            var exception = new OutOfMemoryException("Out of memory");
            var testContext = new { MemoryUsage = "95%", Available = "100MB" };

            // Act
            Log.Fatal("Memory exhausted", exception, testContext);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            Assert.AreSame(exception, _testSink.LastEvent.Exception);
            Assert.IsNotNull(_testSink.LastEvent.Context);
            Assert.AreSame(testContext, _testSink.LastEvent.Context);
        }

        /// <summary>
        /// Verifies that nested anonymous type context serializes correctly.
        /// </summary>
        [Test]
        public void Log_WithNestedContext_SerializesToJson()
        {
            // Arrange
            var nestedContext = new
            {
                Player = new
                {
                    Id = 123,
                    Name = "Hero",
                    Stats = new { Strength = 10, Dexterity = 15 }
                }
            };

            // Act
            Log.Info("Player created", nestedContext);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            var contextJson = LoggingUtility.ConvertContextToJson(_testSink.LastEvent.Context);
            Assert.IsNotNull(contextJson);
            StringAssert.Contains("Player", contextJson);
            StringAssert.Contains("123", contextJson);
            StringAssert.Contains("Strength", contextJson);
            StringAssert.Contains("10", contextJson);
        }

        /// <summary>
        /// Verifies that context with array serializes correctly.
        /// </summary>
        [Test]
        public void Log_WithArrayContext_SerializesToJson()
        {
            // Arrange
            var arrayContext = new
            {
                Scores = new[] { 100, 200, 300 },
                ActivePlayers = new[] { "Alice", "Bob" }
            };

            // Act
            Log.Info("Game statistics", arrayContext);

            // Assert
            Assert.AreEqual(1, _testSink.Count);
            var contextJson = LoggingUtility.ConvertContextToJson(_testSink.LastEvent.Context);
            Assert.IsNotNull(contextJson);
            StringAssert.Contains("Scores", contextJson);
            StringAssert.Contains("100", contextJson);
            StringAssert.Contains("ActivePlayers", contextJson);
            StringAssert.Contains("Alice", contextJson);
        }

        /// <summary>
        /// Verifies that null context does not cause error.
        /// </summary>
        [Test]
        public void Log_WithNullContext_DoesNotThrow()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => Log.Info("Message without context", null));
            Assert.AreEqual(1, _testSink.Count);
            Assert.IsNull(_testSink.LastEvent.Context);
        }

        #endregion
    }
}
