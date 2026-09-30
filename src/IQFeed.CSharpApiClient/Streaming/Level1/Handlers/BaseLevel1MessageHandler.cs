using System;
using System.Text;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public abstract class BaseLevel1MessageHandler
    {
        public event Action<FundamentalMessage> Fundamental;
        public event Action<SystemMessage> System;
        public event Action<SymbolNotFoundMessage> SymbolNotFound;
        public event Action<ErrorMessage> Error;
        public event Action<TimestampMessage> Timestamp;
        public event Action<NewsMessage> News;
        public event Action<RegionalUpdateMessage> Regional;
        public event Action<TradeCorrectionMessage> TradeCorrection;
        public event Action<UnhandledMessage> UnhandledMessage;

        public void ProcessMessages(byte[] messageBytes, int count)
        {
            string[] messages = Encoding.ASCII.GetString(messageBytes, 0, count - 1).Split(IQFeedDefault.ProtocolLineFeedCharacter);

            // each line is processed on its own, so a line that can't be processed is reported and skipped without losing the lines after it;
            // this runs on the socket's receive callback, where an escaping exception would terminate the process
            for (int i = 0; i < messages.Length; i++)
            {
                var message = messages[i];
                if (message.Length == 0)
                {
                    UnhandledMessage?.Invoke(new UnhandledMessage(message, UnhandledMessageReason.Empty, null));
                    continue;
                }

                bool processed;
                try
                {
                    processed = ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    UnhandledMessage?.Invoke(new UnhandledMessage(message, UnhandledMessageReason.ProcessingFailed, ex));
                    continue;
                }

                if (!processed)
                {
                    UnhandledMessage?.Invoke(new UnhandledMessage(message, UnhandledMessageReason.UnknownType, null));
                }
            }
        }

        private bool ProcessMessage(string message)
        {
            switch (message[0])
            {
                case 'F': // A fundamental message
                    ProcessFundamentalMessage(message);
                    return true;
                case 'P': // A summary message
                    ProcessSummaryMessage(message);
                    return true;
                case 'Q': // An update message
                    ProcessUpdateMessage(message);
                    return true;
                case 'R': // A regional update message
                    ProcessRegionalUpdateMessage(message);
                    return true;
                case 'N': // A news headline message
                    ProcessNewsMessage(message);
                    return true;
                case 'S': // A system message
                    ProcessSystemMessage(message);
                    return true;
                case 'T': // A timestamp message
                    ProcessTimestampMessage(message);
                    return true;
                case 'n': // Symbol not found message
                    ProcessSymbolNotFoundMessage(message);
                    return true;
                case 'E': // An error message
                    ProcessErrorMessage(message);
                    return true;
                case 'C': // A trade correction message
                    ProcessTradeCorrectionMessage(message);
                    return true;
                default:
                    return false;
            }
        }

        protected abstract void ProcessSummaryMessage(string msg);

        protected abstract void ProcessUpdateMessage(string msg);

        private void ProcessFundamentalMessage(string msg)
        {
            var fundamentalMessage = FundamentalMessage.Parse(msg);
            Fundamental?.Invoke(fundamentalMessage);
        }

        private void ProcessRegionalUpdateMessage(string msg)
        {
            var regionUpdateMessage = RegionalUpdateMessage.Parse(msg);
            Regional?.Invoke(regionUpdateMessage);
        }

        private void ProcessNewsMessage(string msg)
        {
            var newsMessage = NewsMessage.Parse(msg);
            News?.Invoke(newsMessage);
        }

        private void ProcessSystemMessage(string msg)
        {
            var systemMessage = SystemMessage.Parse(msg);
            System?.Invoke(systemMessage);
        }

        private void ProcessTimestampMessage(string msg)
        {
            var timestampMessage = TimestampMessage.Parse(msg);
            Timestamp?.Invoke(timestampMessage);
        }

        private void ProcessSymbolNotFoundMessage(string msg)
        {
            var symbolNotFoundMessage = SymbolNotFoundMessage.Parse(msg);
            SymbolNotFound?.Invoke(symbolNotFoundMessage);
        }

        private void ProcessErrorMessage(string msg)
        {
            var errorMessage = ErrorMessage.Parse(msg);
            Error?.Invoke(errorMessage);
        }

        private void ProcessTradeCorrectionMessage(string msg)
        {
            var tradeCorrectionMessage = TradeCorrectionMessage.Parse(msg);
            TradeCorrection?.Invoke(tradeCorrectionMessage);
        }
    }
}