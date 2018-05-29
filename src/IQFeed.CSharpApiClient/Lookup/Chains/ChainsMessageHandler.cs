using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public class ChainsMessageHandler
    {
        private readonly char[] _lineSplitDelimiter;
        private readonly char[] _symbolSplitDelimiter;

        public ChainsMessageHandler()
        {
            _lineSplitDelimiter = IQFeedDefault.ProtocolTerminatingCharacters.ToCharArray();
            _symbolSplitDelimiter = new[] { IQFeedDefault.ProtocolDelimiterCharacter };
        }

        public ChainsMessageContainer<EquityOptionMessage> GetEquityOptionMessages(byte[] message, int count)
        {
            return ProcessMessages(message, count, EquityOptionMessage.CreateEquityIndexOptionMessage);
        }

        public ChainsMessageContainer<string> GetStringMessages(byte[] message, int count)
        {
            return ProcessMessages(message, count, s => s);
        }

        // TODO: this method can be combined with Historical
        private ChainsMessageContainer<T> ProcessMessages<T>(byte[] message, int count, Func<string, T> converter)
        {
            var lines = Encoding.ASCII.GetString(message, 0, count)
                .Split(_lineSplitDelimiter, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim()).ToArray();

            var convertedMessages = new List<T>();
            var endMsg = false;
            var lastMsgIdx = lines.Length - 1;

            // check for errors
            if (lines[0][0] == 'E')
                return new ChainsMessageContainer<T>(convertedMessages, true, lines[0]);

            for (var i = 0; i < lines.Length; i++)
            {
                var symbols = lines[i].Split(_symbolSplitDelimiter, StringSplitOptions.RemoveEmptyEntries);

                foreach (var symbol in symbols)
                {
                    // skip characters
                    if (symbol == ":")
                        continue;

                    // check for last message
                    if (i == lastMsgIdx && symbol.StartsWith(IQFeedDefault.ProtocolEndOfMessageCharacters))
                    {
                        endMsg = true;
                        break;
                    }

                    convertedMessages.Add(converter(symbol));
                }
            }

            return new ChainsMessageContainer<T>(convertedMessages, endMsg);
        }
    }
}