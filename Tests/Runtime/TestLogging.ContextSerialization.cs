using System;
using EasyToolkit.Logging.Core;
using NUnit.Framework;

namespace EasyToolkit.Logging.Tests
{
    /// <summary>
    /// Unit tests for <see cref="LoggingUtility.ConvertContextToJson"/> method.
    /// </summary>
    public class TestLoggingContextSerialization
    {
        #region Anonymous Type Serialization Tests

        /// <summary>
        /// Verifies that anonymous type with primitive values serializes to valid JSON.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithPrimitives_ReturnsValidJson()
        {
            // Arrange
            var context = new { IntValue = 42, StringValue = "test", BoolValue = true };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            Assert.IsNotEmpty(json);
            StringAssert.Contains("IntValue", json);
            StringAssert.Contains("42", json);
            StringAssert.Contains("StringValue", json);
            StringAssert.Contains("test", json);
            StringAssert.Contains("BoolValue", json);
            StringAssert.Contains("true", json);
        }

        /// <summary>
        /// Verifies that anonymous type with floating point values serializes correctly.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithFloats_ReturnsValidJson()
        {
            // Arrange
            var context = new { FloatValue = 3.14f, DoubleValue = 2.718281828, DecimalValue = 1.414m };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("FloatValue", json);
            StringAssert.Contains("3.14", json);
            StringAssert.Contains("DoubleValue", json);
            StringAssert.Contains("2.718281828", json);
        }

        /// <summary>
        /// Verifies that anonymous type with null values serializes correctly.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithNulls_ReturnsValidJson()
        {
            // Arrange
            var context = new { NullString = (string)null, NullInt = (int?)null };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("NullString", json);
            StringAssert.Contains("NullInt", json);
        }

        /// <summary>
        /// Verifies that empty anonymous type serializes to valid JSON.
        /// </summary>
        [Test]
        public void ConvertContextToJson_EmptyAnonymousType_ReturnsValidJson()
        {
            // Arrange
            var context = new { };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            // Empty object should contain braces
            Assert.IsTrue(json.Contains("{") && json.Contains("}"));
        }

        #endregion

        #region Nested Object Serialization Tests

        /// <summary>
        /// Verifies that nested anonymous type serializes to valid JSON.
        /// </summary>
        [Test]
        public void ConvertContextToJson_NestedAnonymousType_ReturnsValidJson()
        {
            // Arrange
            var context = new
            {
                Player = new
                {
                    Id = 123,
                    Name = "Hero",
                    Position = new { X = 10.5f, Y = 20.3f }
                }
            };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Player", json);
            StringAssert.Contains("123", json);
            StringAssert.Contains("Hero", json);
            StringAssert.Contains("Position", json);
            StringAssert.Contains("X", json);
            StringAssert.Contains("10.5", json);
            StringAssert.Contains("Y", json);
            StringAssert.Contains("20.3", json);
        }

        /// <summary>
        /// Verifies that deeply nested anonymous type serializes correctly.
        /// </summary>
        [Test]
        public void ConvertContextToJson_DeeplyNestedAnonymousType_ReturnsValidJson()
        {
            // Arrange
            var context = new
            {
                Level1 = new
                {
                    Level2 = new
                    {
                        Level3 = new
                        {
                            Value = "deep"
                        }
                    }
                }
            };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Level1", json);
            StringAssert.Contains("Level2", json);
            StringAssert.Contains("Level3", json);
            StringAssert.Contains("deep", json);
        }

        #endregion

        #region Array and Collection Serialization Tests

        /// <summary>
        /// Verifies that anonymous type with array serializes to valid JSON.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithArray_ReturnsValidJson()
        {
            // Arrange
            var context = new
            {
                Scores = new[] { 100, 200, 300 },
                Names = new[] { "Alice", "Bob", "Charlie" }
            };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Scores", json);
            StringAssert.Contains("100", json);
            StringAssert.Contains("200", json);
            StringAssert.Contains("300", json);
            StringAssert.Contains("Names", json);
            StringAssert.Contains("Alice", json);
            StringAssert.Contains("Bob", json);
            StringAssert.Contains("Charlie", json);
        }

        /// <summary>
        /// Verifies that anonymous type with empty array serializes correctly.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithEmptyArray_ReturnsValidJson()
        {
            // Arrange
            var context = new { Items = new int[0] };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Items", json);
        }

        /// <summary>
        /// Verifies that anonymous type with jagged array serializes correctly.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithJaggedArray_ReturnsValidJson()
        {
            // Arrange
            var context = new { Matrix = new[] { new[] { 1, 2 }, new[] { 3, 4 } } };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Matrix", json);
            StringAssert.Contains("1", json);
            StringAssert.Contains("4", json);
        }

        #endregion

        #region DateTime and TimeSpan Serialization Tests

        /// <summary>
        /// Verifies that DateTime value serializes to valid JSON.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithDateTime_ReturnsValidJson()
        {
            // Arrange
            var testDate = new DateTime(2026, 3, 16, 12, 30, 45);
            var context = new { Timestamp = testDate, Event = "Start" };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Timestamp", json);
            StringAssert.Contains("2026", json);
            StringAssert.Contains("Event", json);
            StringAssert.Contains("Start", json);
        }

        /// <summary>
        /// Verifies that TimeSpan value serializes to valid JSON.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithTimeSpan_ReturnsValidJson()
        {
            // Arrange
            var duration = TimeSpan.FromHours(2.5);
            var context = new { Duration = duration };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Duration", json);
        }

        #endregion

        #region Guid Serialization Tests

        /// <summary>
        /// Verifies that Guid value serializes to valid JSON.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithGuid_ReturnsValidJson()
        {
            // Arrange
            var testGuid = Guid.Parse("12345678-1234-1234-1234-123456789abc");
            var context = new { Id = testGuid, Name = "Test" };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Id", json);
            StringAssert.Contains("12345678", json);
            StringAssert.Contains("Name", json);
            StringAssert.Contains("Test", json);
        }

        #endregion

        #region Enum Serialization Tests

        /// <summary>
        /// Test enum for serialization testing.
        /// </summary>
        private enum TestStatus
        {
            Pending,
            InProgress,
            Completed
        }

        /// <summary>
        /// Verifies that enum value serializes to valid JSON.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithEnum_ReturnsValidJson()
        {
            // Arrange
            var context = new { Status = TestStatus.InProgress, Count = 5 };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Status", json);
            StringAssert.Contains("Count", json);
            StringAssert.Contains("5", json);
        }

        #endregion

        #region Special Character Serialization Tests

        /// <summary>
        /// Verifies that strings with special characters serialize correctly.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithSpecialCharacters_ReturnsValidJson()
        {
            // Arrange
            var context = new
            {
                Quote = "Text with \"quotes\"",
                NewLine = "Line1\nLine2",
                Tab = "Col1\tCol2",
                Unicode = "Unicode: \u4E2D\u6587"
            };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Quote", json);
            StringAssert.Contains("quotes", json);
            StringAssert.Contains("NewLine", json);
            StringAssert.Contains("Tab", json);
            StringAssert.Contains("Unicode", json);
        }

        #endregion

        #region Large Data Serialization Tests

        /// <summary>
        /// Verifies that anonymous type with many properties serializes correctly.
        /// </summary>
        [Test]
        public void ConvertContextToJson_AnonymousTypeWithManyProperties_ReturnsValidJson()
        {
            // Arrange
            var context = new
            {
                Prop1 = "Value1",
                Prop2 = "Value2",
                Prop3 = "Value3",
                Prop4 = "Value4",
                Prop5 = "Value5",
                Prop6 = "Value6",
                Prop7 = "Value7",
                Prop8 = "Value8",
                Prop9 = "Value9",
                Prop10 = "Value10"
            };

            // Act
            var json = LoggingUtility.ConvertContextToJson(context);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains("Prop1", json);
            StringAssert.Contains("Value1", json);
            StringAssert.Contains("Prop10", json);
            StringAssert.Contains("Value10", json);
        }

        #endregion
    }
}
