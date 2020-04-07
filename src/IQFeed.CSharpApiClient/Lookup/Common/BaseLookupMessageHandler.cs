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
                return new MessageContainer<T>(parsedMessages, true, errorMsg, messageTrace);
            }

            for (var i = 0; i < messages.Length; i++)
            {
                // check for ending message (last message only)
                if (i == lastMsgIdx && messages[i].EndsWith(EndMessagePattern))
                {
                    endMsg = true;
                    break;
                }

                var parsedMessage = parserFunc(messages[i]);
                if (parsedMessage != null)
                { 
                    parsedMessages.Add(parsedMessage); 
                }
            }

            return new MessageContainer<T>(parsedMessages, endMsg);
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
            //  as of 6.1 this seems to be no longer true - an error message can be three values
            //    E, ErrorCode, Error
            if (possibleErrorValues.Length != 2 && possibleErrorValues.Length != 3)
                return string.Empty;

            return possibleErrorValues.Length == 2 ? possibleErrorValues[1] : $"{possibleErrorValues[1]}: {possibleErrorValues[2]}";
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