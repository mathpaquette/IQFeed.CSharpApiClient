using System.Text;
using IQFeed.CSharpApiClient.Extensions;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Extensions
{
    public class ByteExtensionsTests
    {
        private const string EndMessage = IQFeedDefault.ProtocolEndOfMessageCharacters;

        [Test]
        public void Should_Return_False_When_Count_Is_Smaller_Then_Pattern_Length()
        {
            // Arrange
            var buffer = new byte[10];
            var pattern = new byte[5];

            // Act
            var result = buffer.EndsWith(4, pattern);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void Should_Return_False_When_Buffer_Size_Is_Smaller_Then_Pattern_Size()
        {
            // Arrange
            var buffer = new byte[5];
            var pattern = new byte[10];

            // Act
            var result = buffer.EndsWith(buffer.Length, pattern);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void Should_Return_True_When_Buffer_Ends_With_Pattern()
        {
            // Arrange
            var buffer = Encoding.ASCII.GetBytes("0123456789" + EndMessage);
            var pattern = Encoding.ASCII.GetBytes(EndMessage);

            // Act
            var result = buffer.EndsWith(buffer.Length, pattern);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void Should_Return_False_When_Buffer_Doesnt_Match_Pattern()
        {
            // Arrange
            var altMsgEnd = EndMessage.Replace("E", "X");
            var buffer = Encoding.ASCII.GetBytes("0123456789" + altMsgEnd);
            var pattern = Encoding.ASCII.GetBytes(EndMessage);

            // Act
            var result = buffer.EndsWith(buffer.Length, pattern);

            // Assert
            Assert.False(result);
        }
    }
}