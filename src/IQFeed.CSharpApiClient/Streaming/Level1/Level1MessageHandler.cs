using System;
using System.Text;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1MessageHandler : ILevel1Event
    {
        public event Action<FundamentalMessage> Fundamental;
        public event Action<UpdateSummaryMessage> Summary;
        public event Action<SystemMessage> System;
        public event Action<SymbolNotFoundMessage> SymbolNotFound;
        public event Action<ErrorMessage> Error;
        public event Action<TimestampMessage> Timestamp;
        public event Action<UpdateSummaryMessage> Update;
        public event Action<RegionalUpdateMessage> Regional;
        public event Action<NewsMessage> News;

        public void ProcessMessages(byte[] messageBytes, int count)
        {
            string[] messages = Encoding.ASCII.GetString(messageBytes, 0, count - 1).Split(IQFeedDefault.ProtocolLineFeedCharacter);

            for (int i = 0; i < messages.Length; i++)
            {
                var message = messages[i];
                switch (messages[i][0])
                {
                    case 'F': // A fundamental message
                        ProcessFundamentalMessage(message);
                        break;
                    case 'P': // A summary message
                        ProcessSummaryMessage(message);
                        break;
                    case 'Q': // An update message
                        ProcessUpdateMessage(message);
                        break;
                    case 'R': // A regional update message
                        ProcessRegionalUpdateMessage(message);
                        break;
                    case 'N': // A news headline message
                        ProcessNewsMessage(message);
                        break;
                    case 'S': // A system message
                        ProcessSystemMessage(message);
                        break;
                    case 'T': // A timestamp message
                        ProcessTimestampMessage(message);
                        break;
                    case 'n': // Symbol not found message
                        ProcessSymbolNotFoundMessage(message);
                        break;
                    case 'E': // An error message
                        ProcessErrorMessage(message);
                        break;
                    default:
                        throw new Exception("Unknown type of level 1 message received.");
                }
            }
        }
        
        private void ProcessFundamentalMessage(string msg)
        {
            var fundamentalMessage = FundamentalMessage.Parse(msg);
            Fundamental?.Invoke(fundamentalMessage);
        }

        private void ProcessSummaryMessage(string msg)
        {
            var updateSummaryMessage = UpdateSummaryMessage.Parse(msg);
            Summary?.Invoke(updateSummaryMessage);
        }

        private void ProcessUpdateMessage(string msg)
        {
            var updateSummaryMessage = UpdateSummaryMessage.Parse(msg);
            Update?.Invoke(updateSummaryMessage);
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
    }
}