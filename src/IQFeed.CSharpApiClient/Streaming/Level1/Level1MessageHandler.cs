using System;
using System.Text;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level1.EventArgs;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1MessageHandler : ILevel1EventHandler
    {
        public event EventHandler<FundamentalEventArgs> Fundamental;
        public event EventHandler<UpdateSummaryEventArgs> Summary;
        public event EventHandler<SystemEventArgs> System;
        public event EventHandler<SymbolNotFoundEventArgs> SymbolNotFound;
        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<TimestampEventArgs> Timestamp;
        public event EventHandler<UpdateSummaryEventArgs> Update;
        public event EventHandler<RegionalUpdateEventArgs> Regional;
        public event EventHandler<NewsEventArgs> News;

        public void ProcessMessages(byte[] messageBytes, int count)
        {
            string[] messages = Encoding.ASCII.GetString(messageBytes, 0, count - 1).Split(IQFeedDefault.ProtocolLineFeedCharacter);

            for (int i = 0; i < messages.Length; i++)
            {
                var message = messages[i];
                var values = GetValuesFromMessage(message);
                switch (messages[i][0])
                {
                    case 'F': // A fundamental message
                        ProcessFundamentalMessage(message, values);
                        break;
                    case 'P': // A summary message
                        ProcessSummaryMessage(message, values);
                        break;
                    case 'Q': // An update message
                        ProcessUpdateMessage(message, values);
                        break;
                    case 'R': // A regional update message
                        ProcessRegionalUpdateMessage(message, values);
                        break;
                    case 'N': // A news headline message
                        ProcessNewsMessage(message, values);
                        break;
                    case 'S': // A system message
                        ProcessSystemMessage(message, values);
                        break;
                    case 'T': // A timestamp message
                        ProcessTimestampMessage(message, values);
                        break;
                    case 'n': // Symbol not found message
                        ProcessSymbolNotFoundMessage(message, values);
                        break;
                    case 'E': // An error message
                        ProcessErrorMessage(message, values);
                        break;
                    default:
                        throw new Exception("Unknown type of level 1 message received.");
                }
            }
        }

        public void ProcessFundamentalMessage(string msg, string[] values)
        {
            var fundamentalMessage = FundamentalMessage.CreateFundamentalMessage(values);
            Fundamental.RaiseEvent(this, new FundamentalEventArgs(fundamentalMessage));
        }

        public void ProcessSummaryMessage(string msg, string[] values)
        {
            var updateSummaryMessage = UpdateSummaryMessage.CreateUpdateSummaryMessage(values);
            Summary.RaiseEvent(this, new UpdateSummaryEventArgs(updateSummaryMessage));
        }

        public void ProcessUpdateMessage(string msg, string[] values)
        {
            var updateSummaryMessage = UpdateSummaryMessage.CreateUpdateSummaryMessage(values);
            Update.RaiseEvent(this, new UpdateSummaryEventArgs(updateSummaryMessage));
        }

        public void ProcessRegionalUpdateMessage(string msg, string[] values)
        {
            var regionUpdateMessage = RegionalUpdateMessage.CreateRegionalUpdateMessage(values);
            Regional.RaiseEvent(this, new RegionalUpdateEventArgs(regionUpdateMessage));
        }

        public void ProcessNewsMessage(string msg, string[] values)
        {
            var newsMessage = NewsMessage.CreateNewsMessage(values);
            News.RaiseEvent(this, new NewsEventArgs(newsMessage));
        }

        public void ProcessSystemMessage(string msg, string[] values)
        {
            var systemMessage = new SystemMessage(values[0], msg);
            System.RaiseEvent(this, new SystemEventArgs(systemMessage));
        }

        public void ProcessTimestampMessage(string msg, string[] values)
        {
            var timestampMessage = TimestampMessage.CreateTimestampMessage(values[0]);
            Timestamp.RaiseEvent(this, new TimestampEventArgs(timestampMessage));
        }

        public void ProcessSymbolNotFoundMessage(string msg, string[] values)
        {
            var symbolNotFoundMessage = new SymbolNotFoundMessage(values[0]);
            SymbolNotFound.RaiseEvent(this, new SymbolNotFoundEventArgs(symbolNotFoundMessage));
        }

        public void ProcessErrorMessage(string msg, string[] values)
        {
            var errorMessage = new ErrorMessage(values[0]);
            Error.RaiseEvent(this, new ErrorEventArgs(errorMessage));
        }

        public static string[] GetValuesFromMessage(string message)
        {
            return message.Substring(2).Split(IQFeedDefault.ProtocolDelimiterCharacter);
        }
    }
}