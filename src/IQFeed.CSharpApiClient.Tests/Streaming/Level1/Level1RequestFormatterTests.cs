using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Streaming.Level1;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1
{
    public class Level1RequestFormatterTests
    {
        private Level1RequestFormatter _level1RequestFormatter;

        [SetUp]
        public void SetUp()
        {
            _level1RequestFormatter = new Level1RequestFormatter();
        }

        [Test]
        public void SetLogLevels()
        {
            // Arrange
            var logLevels = new[] { LoggingLevel.Admin, LoggingLevel.Debug };

            // Act
            var formatted =_level1RequestFormatter.SetLogLevels(logLevels);

            // Assert
            Assert.AreEqual(formatted, "S,SET LOG LEVELS,Admin,Debug\r\n");
        }
    }
}