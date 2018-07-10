using System;
using System.Collections.Generic;
using System.Text;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public abstract class BaseLookupMessageHandler
    {
        protected static MessageContainer<T> ProcessMessages<T>(Func<string, T> parser, byte[] message, int count)
        {
            var messages = Encoding.ASCII.GetString(message, 0, count).SplitFeedLine();

            var parsedMessages = new List<T>();
            var endMsg = false;
            var lastMsgIdx = messages.Length - 1;

            for (var i = 0; i < messages.Length; i++)
            {
                // check for error
                if (messages[i][0] == IQFeedDefault.PrototolErrorCharacter && messages[i][1] == IQFeedDefault.ProtocolDelimiterCharacter)
                    return new MessageContainer<T>(parsedMessages, true, messages[i]);

                // check for end (only last message)
                if (i == lastMsgIdx && messages[i].StartsWith(IQFeedDefault.ProtocolEndOfMessageCharacters))
                {
                    endMsg = true;
                    break;
                }

                parsedMessages.Add(parser(messages[i]));
            }

            return new MessageContainer<T>(parsedMessages, endMsg);
        }
    }
}