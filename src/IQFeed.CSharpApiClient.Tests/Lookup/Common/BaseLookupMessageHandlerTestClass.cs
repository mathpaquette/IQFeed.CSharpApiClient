using System;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Common
{
    /// <summary>
    /// Test class to access protected static method from BaseLookupMessageHandler
    /// </summary>
    internal class BaseLookupMessageHandlerTestClass : BaseLookupMessageHandler
    {
        public new MessageContainer<T> ProcessMessages<T>(Func<string, T> parserFunc, Func<string[], string> errorFunc, byte[] message, int count)
        {
            return base.ProcessMessages(parserFunc, errorFunc, message, count);
        }

        public new MessageContainer<T> ProcessMessages<T>(TryParseDelegate<string, T, bool> tryParseDelegate, Func<string[], string> errorFunc, byte[] message, int count)
        {
            return base.ProcessMessages(tryParseDelegate, errorFunc, message, count);
        }

        public new string ParseErrorMessage(string[] messages)
        {
            return base.ParseErrorMessage(messages);
        }

        public new string ParseErrorMessageWithRequestId(string[] messages)
        {
            return base.ParseErrorMessageWithRequestId(messages);
        }
    }
}