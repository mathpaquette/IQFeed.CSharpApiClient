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
            var delimitedString = "one,two,three,four,";

            int splitSize = 3;

            //Act
            string[] splitMessage = delimitedString.SplitFeedMessage(splitSize);

            // Assert
            Assert.AreEqual(splitMessage.Length, splitSize);

            Assert.AreEqual(splitMessage[splitSize - 1], "three,four");

            Assert.AreEqual(splitMessage[splitSize - 2], "two");

            Assert.AreEqual(splitMessage[splitSize - splitSize], "one");

        }


        [Test]
        public void SplitFeedMessage_LastPartReturnsAllCharactersOfLastSplitPart_IfSplitSizeEqualToDelimitedMessage()
        {
            // Arrange
            var delimitedString = "get,all,chars";

            int splitSize = 3;

            //Act
            string[] splitMessage = delimitedString.SplitFeedMessage(splitSize);

            // Assert
            Assert.AreEqual(splitMessage.Length, splitSize);

            Assert.AreEqual(splitMessage[splitSize - 1], "chars");

            Assert.AreEqual(splitMessage[splitSize - 2], "all");

            Assert.AreEqual(splitMessage[splitSize - splitSize], "get");

        }
    }
}