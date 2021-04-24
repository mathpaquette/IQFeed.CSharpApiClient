using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
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
            var intervalMessage = new IntervalMessage(new DateTime(2000, 01, 01, 9, 30, 00), 1, 1, 1, 1, 1, 1, 0);
            var invalidMessages = new List<InvalidMessage<IntervalMessage>>() { new InvalidMessage<IntervalMessage>(intervalMessage, intervalMessage.ToCsv()) };
            var messages = new List<IntervalMessage>() { intervalMessage };

            // Act
            var exception = _exceptionFactory.CreateNew(request, invalidMessages, messages);

            // Assert
            var expectedException = new InvalidDataIQFeedException<IntervalMessage>(request, invalidMessages, messages);
            Assert.AreEqual(exception, expectedException);
        }
    }
}