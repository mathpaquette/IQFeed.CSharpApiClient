using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Common.Exceptions;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Common
{
    public class ExceptionFactoryTests
    {
        private readonly ExceptionFactory _exceptionFactory;

        public ExceptionFactoryTests()
        {
            _exceptionFactory = new ExceptionFactory();
        }

        [Test]
        public void Should_Return_InvalidData_Exception()
        {
            // Arrange
            var request = "request";
            var messageTrace = "message trace";

            // Act
            var exception = _exceptionFactory.CreateNew(request, InvalidDataIQFeedException.InvalidData, messageTrace);

            // Assert
            var expectedException = new InvalidDataIQFeedException(request, InvalidDataIQFeedException.InvalidData, messageTrace);
            Assert.AreEqual(exception, expectedException);
        }
    }
}