using System;
using System.Text;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Handlers
{
    public abstract class BaseDerivativeMessageHandler<T> : IDerivativeMessageHandler<T>
    {
        public event Action<SystemMessage> System;
        public event Action<ErrorMessage> Error;
        public event Action<IntervalBarMessage<T>> IntervalBar;
        public event Action<SymbolNotFoundMessage> SymbolNotFound;

        private readonly Func<string, IntervalBarMessage<T>> _intervalBarMessageParser;
        private readonly Func<string, IntervalBarMessage<T>> _intervalBarMessageWithRequestIdParser;

        protected BaseDerivativeMessageHandler(
            Func<string, IntervalBarMessage<T>> intervalBarMessageParser,
            Func<string, IntervalBarMessage<T>> intervalBarMessageWithRequestIdParser)
        {
            _intervalBarMessageWithRequestIdParser = intervalBarMessageWithRequestIdParser;
            _intervalBarMessageParser = intervalBarMessageParser;
        }

        public void ProcessMessages(byte[] messageBytes, int count)
        {
            var messages = Encoding.ASCII.GetString(messageBytes, 0, count).SplitFeedLine();

            for (var i = 0; i < messages.Length; i++)
            {
                var message = messages[i];

                // Try parsing the IntervalBarMessage pattern
                if (TryParse(message, out var intervalBarMessage))
                {
                    IntervalBar?.Invoke(intervalBarMessage);
                    continue;
                }

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

        private bool TryParse(string message, out IntervalBarMessage<T> intervalBarMessage)
        {
            intervalBarMessage = null;

            if (IntervalBarMessage.IntervalBarMessageWithoutRequestIdRegex.IsMatch(message))
                intervalBarMessage = _intervalBarMessageParser(message);

            else if (IntervalBarMessage.IntervalBarMessageWithRequestIdRegex.IsMatch(message))
                intervalBarMessage = _intervalBarMessageWithRequestIdParser(message);

            return intervalBarMessage != null;
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