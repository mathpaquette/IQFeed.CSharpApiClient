using System;
using System.Runtime.ExceptionServices;
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

            // with an UnhandledMessage subscriber, each line is processed on its own: a line that can't be processed is reported and skipped
            // without losing the lines after it. Without one, such a line throws as it always has.
            for (int i = 0; i < messages.Length; i++)
            {
                var message = messages[i];

                // a CRLF-terminated empty line is left as a lone carriage return by the split on line feeds
                if (message.Length == 0 || message == "\r")
                {
                    ReportUnhandledMessage(
                        new UnhandledMessage(message, UnhandledMessageReason.Empty, null),
                        message.Length == 0 ? new IndexOutOfRangeException() : CreateUnknownTypeException());
                    continue;
                }

                bool processed;
                try
                {
                    processed = ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    ReportUnhandledMessage(new UnhandledMessage(message, UnhandledMessageReason.ProcessingFailed, ex), ex);
                    continue;
                }

                if (!processed)
                {
                    ReportUnhandledMessage(new UnhandledMessage(message, UnhandledMessageReason.UnknownType, null), CreateUnknownTypeException());
                }
            }
        }

        private static Exception CreateUnknownTypeException()
        {
            return new Exception("Unknown type of level 1 message received.");
        }

        private void ReportUnhandledMessage(UnhandledMessage unhandledMessage, Exception exceptionWithoutSubscribers)
        {
            var unhandledMessageHandler = UnhandledMessage;

            // subscribing is the opt-in: without a subscriber the line fails the way it did before UnhandledMessage existed
            if (unhandledMessageHandler == null)
            {
                ExceptionDispatchInfo.Capture(exceptionWithoutSubscribers).Throw();
            }

            try
            {
                unhandledMessageHandler(unhandledMessage);
            }
            catch (Exception)
            {
                // this runs on the socket's receive callback, where an escaping exception would terminate the process; a failing
                // subscriber can't be reported through the event it just failed in, so it is ignored and the batch goes on
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