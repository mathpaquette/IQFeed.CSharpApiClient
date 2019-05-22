using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public abstract class BaseLookupMessageHandler
    {
        private static readonly string EndMessagePattern = IQFeedDefault.ProtocolEndOfMessageCharacters + IQFeedDefault.ProtocolDelimiterCharacter;
        private static readonly string NoDataIndicator = "E,!NO_DATA!";
        private static readonly Regex ErrorPattern = new Regex(@"^E,.*,$");
        private static readonly Regex ErrorPatternWithRequestId = new Regex(@"^.*,E,.*,$");        

        protected static MessageContainer<T> ProcessMessages<T>(Func<string, T> parser, byte[] message, int count, bool hasRequestId)
        {
            var messages = Encoding.ASCII.GetString(message, 0, count).SplitFeedLine();

            var parsedMessages = new List<T>();
            var endMsg = false;
            var lastMsgIdx = messages.Length - 1;

            if (messages.Length == 1)
            {
                if(IsErrorMessage(messages[0], hasRequestId))
                    return new MessageContainer<T>(parsedMessages, true, messages[0]);
            }

            for (var i = 0; i < messages.Length; i++)
            {
                if(IsNoDataMessage(messages[0], hasRequestId))
                    return new MessageContainer<T>(parsedMessages, true);

                // check for error
                if(IsErrorMessage(messages[i], hasRequestId))
                    return new MessageContainer<T>(parsedMessages, true, messages[i]);                

                // check for end (only last message)
                if (i == lastMsgIdx && messages[i].EndsWith(EndMessagePattern))
                {
                    endMsg = true;
                    break;
                }

                parsedMessages.Add(parser(messages[i]));
            }

            return new MessageContainer<T>(parsedMessages, endMsg);
        }

        private static bool IsErrorMessage(string message, bool hasRequestId)
        {
            // note regex pattern matching is needed to avoid problems like detecting a valid message like
            // E,7,1,ENI, from search symbol request as an error. But we leave that check at the end to avoid heavy checks for every message
            if(hasRequestId)
            {
                int firstDelimiterIndex = message.IndexOf(IQFeedDefault.ProtocolDelimiterCharacter);
                return firstDelimiterIndex != -1 // there is a delimiter
                    && firstDelimiterIndex + 2 < message.Length // there are 2 more characters after that
                    && message[firstDelimiterIndex + 1] == IQFeedDefault.PrototolErrorCharacter // next is the error marker
                    && message[firstDelimiterIndex + 2] == IQFeedDefault.ProtocolDelimiterCharacter // and delimiter after that
                    && ErrorPatternWithRequestId.IsMatch(message); // matches the error pattern
            }
            else
            {
                return message.Length > 2 // there are 2 or more characters
                    && message[0] == IQFeedDefault.PrototolErrorCharacter // first one is the error marker
                    && message[1] == IQFeedDefault.ProtocolDelimiterCharacter // the next one is a delimiter
                    && ErrorPatternWithRequestId.IsMatch(message); // matches the error pattern
            }
        }

        private static bool IsNoDataMessage(string message, bool hasRequestId)
        {
            if(hasRequestId)
            {
                int firstDelimiterIndex = message.IndexOf(IQFeedDefault.ProtocolDelimiterCharacter);
                return firstDelimiterIndex != -1 // there is a delimiter
                    && firstDelimiterIndex + 2 < message.Length // there are 2 more characters after that
                    && message[firstDelimiterIndex + 1] == IQFeedDefault.PrototolErrorCharacter // next is the error marker
                    && message[firstDelimiterIndex + 2] == IQFeedDefault.ProtocolDelimiterCharacter // and delimiter after that
                    && message.Contains(NoDataIndicator);
            }
            else
            {
                return message.Length > 2 // there are 2 or more characters
                    && message[0] == IQFeedDefault.PrototolErrorCharacter // first one is the error marker
                    && message[1] == IQFeedDefault.ProtocolDelimiterCharacter // the next one is a delimiter
                    && message.StartsWith(NoDataIndicator);
            }
        }
    }
}