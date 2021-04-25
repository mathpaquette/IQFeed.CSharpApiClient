using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IQFeed.CSharpApiClient.Lookup.Historical;
using IQFeed.CSharpApiClient.Tests.Common;
using NSubstitute;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Common
{
    public class BaseLookupMessageHandlerTests
    {
        private BaseLookupMessageHandlerTestClass _baseLookupMessageHandlerTestClass;
        private Func<string, string> _parserFunc;
        private Func<string[], string> _errorParserFunc;

        [SetUp]
        public void SetUp()
        {
            _baseLookupMessageHandlerTestClass = new BaseLookupMessageHandlerTestClass();
            _parserFunc = Substitute.For<Func<string, string>>();
            _errorParserFunc = Substitute.For<Func<string[], string>>();
        }

        [Test]
        public void Should_Return_Container_End_When_EndMsg_Received()
        {
            // Arrange
            var messages = new List<string>()
            {
                "EBAY1901K31,EBAY1901K32,EBAY1901K32.5,EBAY1901K33,EBAY1901K33.5,EBAY1901K34,EBAY1901K34.5,EBAY1901K35,EBAY1901K35.5,EBAY1901K36,EBAY1901K36.5,EBAY1901K37, \r\n",
                "!ENDMSG!,"
            };
            var messagesBytes = TestHelper.GetMessageBytes(messages);

            // Act
            var container = _baseLookupMessageHandlerTestClass.ProcessMessages(_parserFunc, _errorParserFunc, messagesBytes, messagesBytes.Length);

            // Assert
            Assert.True(container.End);
        }

        [Test]
        public void Should_Return_Container_End_When_Error_Received()
        {
            // Arrange
            var messages = new List<string>()
            {
                "E,!NO_DATA!,,\r\n",
                "!ENDMSG!,"
            };
            var messagesBytes = TestHelper.GetMessageBytes(messages);

            // Act
            var container = _baseLookupMessageHandlerTestClass.ProcessMessages(_parserFunc, _errorParserFunc, messagesBytes, messagesBytes.Length);

            // Assert
            Assert.True(container.End);
        }

        [Test]
        public void Should_Call_Parser_When_No_Error_Received()
        {
            // Arrange
            var messages = new List<string>()
            {
                "EBAY1901K31,EBAY1901K32,EBAY1901K32.5,EBAY1901K33,EBAY1901K33.5,EBAY1901K34,EBAY1901K34.5,EBAY1901K35,EBAY1901K35.5,EBAY1901K36,EBAY1901K36.5,EBAY1901K37, \r\n",
                "!ENDMSG!,"
            };
            var messagesBytes = TestHelper.GetMessageBytes(messages);

            // Act
            _baseLookupMessageHandlerTestClass.ProcessMessages(_parserFunc, _errorParserFunc, messagesBytes, messagesBytes.Length);

            // Assert
            _parserFunc.Received(1).Invoke(Arg.Any<string>());
        }

        [Test]
        public void Should_Not_Call_Parser_When_Error_Received()
        {
            // Arrange
            var messages = new List<string>()
            {
                "E,!NO_DATA!,,\r\n",
                "!ENDMSG!,"
            };
            var messagesBytes = TestHelper.GetMessageBytes(messages);
            _errorParserFunc(Arg.Any<string[]>()).Returns("!ENDMSG!");

            // Act
            _baseLookupMessageHandlerTestClass.ProcessMessages(_parserFunc, _errorParserFunc, messagesBytes, messagesBytes.Length);

            // Assert
            _parserFunc.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Test]
        public void Should_Return_Invalid_Data_Container_When_Tick_Overflow()
        {
            // Arrange
            var message = $"2018-04-17 17:51:22.123456,96.0700,{long.MaxValue},0,0.0000,0.0000,4145784264,O,19,143A,";
            var messagesBytes = TestHelper.GetMessageBytes(new List<string>{ message + IQFeedDefault.ProtocolTerminatingCharacters });

            // Act
            var container = _baseLookupMessageHandlerTestClass.ProcessMessages(HistoricalMessageHandler.TryParseTick, _errorParserFunc, messagesBytes, messagesBytes.Length);

            // Assert
            Assert.GreaterOrEqual(container.InvalidMessages.Count(), 1);
        }

        private class NoErrorMessageTestDataSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new[] { "E,7,1,ENI,", "!ENDMSG!," };
                yield return new[] { "E,,,,", "!ENDMSG!," };
                yield return new[] { "E,,,", "!ENDMSG!," };
                yield return new[] { "E,,", "!ENDMSG!," };
                yield return new[] { "E,!NO_DATA!,,", "E,,,", "!ENDMSG!," }; // batch with 3 messages
                yield return new[] { "X,,", "!ENDMSG!," };
            }
        }

        [TestCaseSource(typeof(NoErrorMessageTestDataSource))]
        public void Should_Return_Empty_Error_Message_When_Parsing_Messages(string[] messages)
        {
            // Arrange
            // Act
            var errorMessage = _baseLookupMessageHandlerTestClass.ParseErrorMessage(messages);

            // Assert
            Assert.IsEmpty(errorMessage);
        }

        private class NoErrorMessageWithRequestIdTestDataSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new[] { "RequestId,E,7,1,ENI,", "!ENDMSG!," };
                yield return new[] { "RequestId,E,,,,", "!ENDMSG!," };
                yield return new[] { "RequestId,E,,,", "!ENDMSG!," };
                yield return new[] { "RequestId,E,,", "!ENDMSG!," };
                yield return new[] { "RequestId,E,!NO_DATA!,,", "E,,,", "!ENDMSG!," }; // batch with 3 messages
                yield return new[] { "RequestId,X,,", "!ENDMSG!," };
            }
        }
        [TestCaseSource(typeof(NoErrorMessageWithRequestIdTestDataSource))]
        public void Should_Return_Empty_Error_Message_When_Parsing_Messages_With_RequestId(string[] messages)
        {
            // Arrange
            // Act
            var errorMessage = _baseLookupMessageHandlerTestClass.ParseErrorMessageWithRequestId(messages);

            // Assert
            Assert.IsEmpty(errorMessage);
        }

        private class ErrorMessageTestDataSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new[] { "E,Invalid number of parameters.,", "!ENDMSG!," };
                yield return new[] { "E,!NO_DATA!,,", "!ENDMSG!," };
                yield return new[] { "E,!NO_DATA!,", "!ENDMSG!," };
            }
        }
        [TestCaseSource(typeof(ErrorMessageTestDataSource))]
        public void Should_Return_Error_Message_When_Parsing_Messages(string[] messages)
        {
            // Arrange
            // Act
            var errorMessage = _baseLookupMessageHandlerTestClass.ParseErrorMessage(messages);

            // Assert
            Assert.IsNotEmpty(errorMessage);
        }

        private class ErrorMessageWithRequestIdTestDataSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new[] { "RequestId,E,Invalid number of parameters.,", "!ENDMSG!," };
                yield return new[] { "RequestId,E,!NO_DATA!,,", "!ENDMSG!," };
                yield return new[] { "RequestId,E,!NO_DATA!,", "!ENDMSG!," };
            }
        }
        [TestCaseSource(typeof(ErrorMessageWithRequestIdTestDataSource))]
        public void Should_Return_Error_Message_When_Parsing_Messages_With_RequestId(string[] messages)
        {
            // Arrange
            // Act
            var errorMessage = _baseLookupMessageHandlerTestClass.ParseErrorMessageWithRequestId(messages);

            // Assert
            Assert.IsNotEmpty(errorMessage);
        }
    }
}