using System;
using IQFeed.CSharpApiClient.Extensions;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Extensions
{
    public class StringExtensionTests
    {
        [Test]
        public void SplitFeedMessage_ThrowsArgumentException_WhenNoProtocolDelimiterCharacterFoundInString()
        {
            // Arrange
            var nonDelimitedString = "nonDelimiters";

            // Assert
            Assert.Throws<ArgumentException>(() => nonDelimitedString.SplitFeedMessage(2));
        }

        [Test]
        public void SplitFeedMessage_ThrowsArgumentException_ExpectedPartsGreaterThanDelimitedStrings()
        {
            // Arrange
            var delimitedString = "not,enough";

            // Assert
            Assert.Throws<ArgumentException>(() => delimitedString.SplitFeedMessage(3));
        }

        [Test]
        public void SplitFeedMessage_ThrowsArgumentException_WithInvalidDelimiters()
        {
            // Arrange
            var delimitedString = "wrong.delimiter";

            // Assert
            Assert.Throws<ArgumentException>(() => delimitedString.SplitFeedMessage(2));
        }

        [Test]
        public void SplitFeedMessage_LastPartReturnsAllCharactersUpToTheLastDelimiter_IfSplitSizeLessThanSizeOfDelimitedMessage()
        {
            // Arrange
            var delimitedString = "one,two,my, company name,";
            var splitSize = 3;

            // Act
            var values = delimitedString.SplitFeedMessage(splitSize);

            // Assert
            var expectedValues = new[] { "one", "two", "my, company name" };
            Assert.AreEqual(values, expectedValues);
        }
    }
}