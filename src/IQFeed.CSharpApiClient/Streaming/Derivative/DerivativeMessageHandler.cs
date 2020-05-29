using System;
using System.Text;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public class DerivativeMessageHandler : IDerivativeMessageHandler
    {
        public event Action<IntervalBarMessage> IntervalBar;
        public event Action<SystemMessage> System;
        public event Action<ErrorMessage> Error;
        public event Action<SymbolNotFoundMessage> SymbolNotFound;

        public void ProcessMessages(byte[] messageBytes, int count)
        {
            var messages = Encoding.ASCII.GetString(messageBytes, 0, count).SplitFeedLine();

            for (var i = 0; i < messages.Length; i++)
            {
                var message = messages[i];

                if (HasIntervalBar(message))
                    continue;

                // Check with other pattern possible
                switch (message[0])
                {
                    case 'S': // A system message
                        ProcessSystemMessage(message);
                        break;
                    case 'E': // An error message
                        ProcessErrorMessage(message);
                        break;
                    case 'n': // Symbol not found message
                        ProcessSymbolNotFoundMessage(message);
                        break;
                    default:
                        throw new Exception("Unknown type of derivative message received.");
                }
            }
        }

        private bool HasIntervalBar(string msg)
        {
            if (IntervalBarMessage.TryParse(msg, out IntervalBarMessage intervalBarMessage))
            {
                IntervalBar?.Invoke(intervalBarMessage);
                return true;
            }

            return false;
        }

        private void ProcessSystemMessage(string msg)
        {
            var systemMessage = SystemMessage.Parse(msg);
            System?.Invoke(systemMessage);
        }

        private void ProcessErrorMessage(string msg)
        {
            var errorMessage = ErrorMessage.Parse(msg);
            Error?.Invoke(errorMessage);
        }

        private void ProcessSymbolNotFoundMessage(string msg)
        {
            var symbolNotFoundMessage = SymbolNotFoundMessage.Parse(msg);
            SymbolNotFound?.Invoke(symbolNotFoundMessage);
        }
    }
}