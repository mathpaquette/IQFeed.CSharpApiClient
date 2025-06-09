using System;
using System.Text;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public class Level2MessageHandler : ILevel2MessageHandler
    {
        public event Action<UpdateSummaryMessage> Summary;
        public event Action<UpdateSummaryMessage> Update;
        public event Action<SymbolNotFoundMessage> SymbolNotFound;
        public event Action<SymbolHasNoDepthAvailableMessage> SymbolHasNoDepthAvailable;
        public event Action<MarketMakerNameMessage> Query;
        public event Action<ErrorMessage> Error;
        public event Action<TimestampMessage> Timestamp;
        public event Action<SystemMessage> System;

        // protocol 6.2 events
        public event Action<OrderAddUpdateSummaryMessage> OrderAdd;
        public event Action<OrderAddUpdateSummaryMessage> OrderUpdate;
        public event Action<OrderAddUpdateSummaryMessage> OrderSummary;
        public event Action<OrderDeleteMessage> OrderDelete;
        public event Action<PriceLevelOrderMessage> PriceLevelOrder;
        public event Action<PriceLevelUpdateSummaryMessage> PriceLevelSummary;
        public event Action<PriceLevelUpdateSummaryMessage> PriceLevelUpdate;
        public event Action<PriceLevelDeleteMessage> PriceLevelDelete;

        public void ProcessMessages(byte[] messageBytes, int count)
        {
            var messages = Encoding.ASCII.GetString(messageBytes, 0, count).SplitFeedLine();

            for (int i = 0; i < messages.Length; i++)
            {
                var message = messages[i];
                switch (message[0])
                {
                    case 'Z': // A summary message
                        ProcessSummaryMessage(message);
                        break;
                    case '2': // An update message (protocol 6.1 and below)
                        ProcessUpdateMessage(message);
                        break;
                    case '0': // A Price Level Order message (protocol 6.2 and above)
                        ProcessPriceLevelOrderMessage(message);
                        break;
                    case '3': // An Order/Level Add message (protocol 6.2 and above)
                        ProcessOrderAddMessage(message);
                        break;
                    case '4': // An Order/Level Update message (protocol 6.2 and above)
                        ProcessOrderUpdateMessage(message);
                        break;
                    case '5': // An Order/Level Delete message (protocol 6.2 and above)
                        ProcessOrderDeleteMessage(message);
                        break;
                    case '6': // An Order/Level Summary message (protocol 6.2 and above)
                        ProcessOrderSummaryMessage(message);
                        break;
                    case '7': // A Price Level Summary message (protocol 6.2 and above)
                        ProcessPriceLevelSummaryMessage(message);
                        break;
                    case '8': // A Price Level Update message (protocol 6.2 and above)
                        ProcessPriceLevelUpdateMessage(message);
                        break;
                    case '9': // A Price Level Delete message (protocol 6.2 and above)
                        ProcessPriceLevelDeleteMessage(message);
                        break;
                    case 'T': // A timestamp message
                        ProcessTimestampMessage(message);
                        break;
                    case 'M': // A Market Maker name OR order book level query response message.
                        ProcessMarketMakerNameMessage(message);
                        break;
                    case 'S': // A system message
                        ProcessSystemMessage(message);
                        break;
                    case 'n': // Symbol not found message
                        ProcessSymbolNotFoundMessage(message);
                        break;
                    case 'q': // No depth available message. (protocol 6.2 and above)
                              // This message is received when a symbol is valid but there is currently no depth available.  
                        ProcessSymbolHasNoDepthAvailableMessage(message);
                        break;
                    case 'E': // An error message
                        ProcessErrorMessage(message);
                        break;
                    case 'O': // A deprecated message included only for backward compability
                        break;
                    default:
                        throw new Exception("Unknown type of level 2 message received.");
                }
            }
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

        private void ProcessPriceLevelOrderMessage(string msg)
        {
            var priceLevelOrderMessage = PriceLevelOrderMessage.Parse(msg);
            PriceLevelOrder?.Invoke(priceLevelOrderMessage);
        }

        private void ProcessOrderAddMessage(string msg)
        {
            var orderAddUpdateSummaryMessage = OrderAddUpdateSummaryMessage.Parse(msg);
            OrderAdd?.Invoke(orderAddUpdateSummaryMessage);
        }

        private void ProcessOrderUpdateMessage(string msg)
        {
            var orderAddUpdateSummaryMessage = OrderAddUpdateSummaryMessage.Parse(msg);
            OrderUpdate?.Invoke(orderAddUpdateSummaryMessage);
        }

        private void ProcessOrderSummaryMessage(string msg)
        {
            var orderAddUpdateSummaryMessage = OrderAddUpdateSummaryMessage.Parse(msg);
            OrderSummary?.Invoke(orderAddUpdateSummaryMessage);
        }

        private void ProcessOrderDeleteMessage(string msg)
        {
            var orderDeleteMessage = OrderDeleteMessage.Parse(msg);
            OrderDelete?.Invoke(orderDeleteMessage);
        }

        private void ProcessPriceLevelSummaryMessage(string msg)
        {
            var priceLevelUpdateSummaryMessage = PriceLevelUpdateSummaryMessage.Parse(msg);
            PriceLevelSummary?.Invoke(priceLevelUpdateSummaryMessage);
        }

        private void ProcessPriceLevelUpdateMessage(string msg)
        {
            var priceLevelUpdateSummaryMessage = PriceLevelUpdateSummaryMessage.Parse(msg);
            PriceLevelUpdate?.Invoke(priceLevelUpdateSummaryMessage);
        }

        private void ProcessPriceLevelDeleteMessage(string msg)
        {
            var priceLevelDeleteMessage = PriceLevelDeleteMessage.Parse(msg);
            PriceLevelDelete?.Invoke(priceLevelDeleteMessage);
        }

        private void ProcessTimestampMessage(string msg)
        {
            var timestampMessage = TimestampMessage.Parse(msg);
            Timestamp?.Invoke(timestampMessage);
        }

        private void ProcessMarketMakerNameMessage(string msg)
        {
            var marketMakerNameMessage = MarketMakerNameMessage.Parse(msg);
            Query?.Invoke(marketMakerNameMessage);
        }

        private void ProcessSystemMessage(string msg)
        {
            var systemMessage = SystemMessage.Parse(msg);
            System?.Invoke(systemMessage);
        }

        private void ProcessSymbolNotFoundMessage(string msg)
        {
            var symbolNotFoundMessage = SymbolNotFoundMessage.Parse(msg);
            SymbolNotFound?.Invoke(symbolNotFoundMessage);
        }

        private void ProcessSymbolHasNoDepthAvailableMessage(string msg)
        {
            var symbolHasNoDepthAvailableMessage = SymbolHasNoDepthAvailableMessage.Parse(msg);
            SymbolHasNoDepthAvailable?.Invoke(symbolHasNoDepthAvailableMessage);
        }

        private void ProcessErrorMessage(string msg)
        {
            var errorMessage = ErrorMessage.Parse(msg);
            Error?.Invoke(errorMessage);
        }
    }
}