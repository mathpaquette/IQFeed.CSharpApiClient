using System;
using System.Collections.Generic;
using System.Text;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public abstract class BaseLookupMessageHandler
    {
        private static readonly string EndMessagePattern = IQFeedDefault.ProtocolEndOfMessageCharacters + IQFeedDefault.ProtocolDelimiterCharacter;
        private static readonly char[] ValueDelimiter = { IQFeedDefault.ProtocolDelimiterCharacter };

        public delegate T3 TryParseDelegate<in T1, T2, out T3>(T1 input, out T2 output);

        // TODO(mathip): remove once fully migrated
        protected MessageContainer<T> ProcessMessages<T>(Func<string, T> parserFunc, Func<string[], string> errorParserFunc, byte[] message, int count)
        {
            var messages = Encoding.ASCII.GetString(message, 0, count).SplitFeedLine();

            var parsedMessages = new List<T>();
            var endMsg = false;
            var lastMsgIdx = messages.Length - 1;
            var errorMsg = errorParserFunc(messages);

            if (!string.IsNullOrEmpty(errorMsg))
            {
                var messageTrace = Encoding.ASCII.GetString(message, 0, count);
                return new MessageContainer<T>(errorMsg, messageTrace);
            }

            for (var i = 0; i < messages.Length; i++)
            {
                // check for ending message (last message only)
                if (i == lastMsgIdx && messages[i].EndsWith(EndMessagePattern))
                {
                    endMsg = true;
                    break;
                }

                // Market Summary Messages come with a header line, so returns a null parsed message, to be ignored
                var parsedMessage = parserFunc(messages[i]);
                if (parsedMessage != null)
                {
                    parsedMessages.Add(parsedMessage);
                }
            }

            return new MessageContainer<T>(parsedMessages, endMsg);
        }

        protected MessageContainer<T> ProcessMessages<T>(TryParseDelegate<string, T, bool> tryParseDelegate, Func<string[], string> errorParserFunc, byte[] message, int count)
        {
            var messages = Encoding.ASCII.GetString(message, 0, count).SplitFeedLine();

            var parsedMessages = new List<T>();
            var invalidMessages = new List<InvalidMessage<T>>();
            var endMsg = false;
            var lastMsgIdx = messages.Length - 1;
            var errorMsg = errorParserFunc(messages);

            if (!string.IsNullOrEmpty(errorMsg))
            {
                var messageTrace = Encoding.ASCII.GetString(message, 0, count);
                return new MessageContainer<T>(errorMsg, messageTrace);
            }

            for (var i = 0; i < messages.Length; i++)
            {
                // check for ending message (last message only)
                if (i == lastMsgIdx && messages[i].EndsWith(EndMessagePattern))
                {
                    endMsg = true;
                    break;
                }

                if (!tryParseDelegate(messages[i], out var item))
                    invalidMessages.Add(new InvalidMessage<T>(item, messages[i]));

                parsedMessages.Add(item);
            }

            return new MessageContainer<T>(parsedMessages, invalidMessages, endMsg);
        }

        // TODO(mathip): extract common code
        protected string ParseErrorMessage(string[] messages)
        {
            // errors will always happen with the error message + the end message
            if (messages.Length != 2)
                return string.Empty;

            var possibleErrorValues = messages[0].Split(ValueDelimiter, StringSplitOptions.RemoveEmptyEntries);

            // check for error character
            if (possibleErrorValues[0][0] != IQFeedDefault.PrototolErrorCharacter)
                return string.Empty;

            // error message will be composed of two values (E and error)
            if (possibleErrorValues.Length != 2)
                return string.Empty;

            return possibleErrorValues[1];
        }

        protected string ParseErrorMessageWithRequestId(string[] messages)
        {
            // errors will always happen with the error message + the end message
            if (messages.Length != 2)
                return string.Empty;

            var possibleErrorValues = messages[0].Split(ValueDelimiter, StringSplitOptions.RemoveEmptyEntries);

            // check for error character
            if (possibleErrorValues[1][0] != IQFeedDefault.PrototolErrorCharacter)
                return string.Empty;

            // error message will be composed of three values (requestId and E and error)
            if (possibleErrorValues.Length != 3)
                return string.Empty;

            return possibleErrorValues[2];
        }
    }
}