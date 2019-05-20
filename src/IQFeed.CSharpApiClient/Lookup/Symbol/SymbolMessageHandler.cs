using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public class SymbolMessageHandler : BaseLookupMessageHandler
    {
        public MessageContainer<ListedMarketMessage> GetListedMarketMessages(byte[] message, int count)
        {
            return ProcessMessages(ListedMarketMessage.Parse, message, count);
        }

        public MessageContainer<ListedMarketMessage> GetListedMarketMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(ListedMarketMessage.ParseWithRequestId, message, count);
        }

        public MessageContainer<TradeConditionMessage> GetTradeConditionMessages(byte[] message, int count)
        {
            return ProcessMessages(TradeConditionMessage.Parse, message, count);
        }

        public MessageContainer<TradeConditionMessage> GetTradeConditionMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TradeConditionMessage.ParseWithRequestId, message, count);
        }
    }
}