using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public class SymbolMessageHandler : BaseLookupMessageHandler
    {
        public MessageContainer<SymbolByFilterMessage> GetSymbolByFilterMessages(byte[] message, int count)
        {
            return ProcessMessages(SymbolByFilterMessage.Parse, message, count, false);
        }

        public MessageContainer<SymbolByFilterMessage> GetSymbolByFilterMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SymbolByFilterMessage.ParseWithRequestId, message, count, true);
        }

        public MessageContainer<ListedMarketMessage> GetListedMarketMessages(byte[] message, int count)
        {
            return ProcessMessages(ListedMarketMessage.Parse, message, count, false);
        }

        public MessageContainer<ListedMarketMessage> GetListedMarketMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(ListedMarketMessage.ParseWithRequestId, message, count, true);
        }

        public MessageContainer<SecurityTypeMessage> GetSecurityTypeMessages(byte[] message, int count)
        {
            return ProcessMessages(SecurityTypeMessage.Parse, message, count, false);
        }

        public MessageContainer<SecurityTypeMessage> GetSecurityTypeMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SecurityTypeMessage.ParseWithRequestId, message, count, true);
        }

        public MessageContainer<TradeConditionMessage> GetTradeConditionMessages(byte[] message, int count)
        {
            return ProcessMessages(TradeConditionMessage.Parse, message, count, false);
        }

        public MessageContainer<TradeConditionMessage> GetTradeConditionMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TradeConditionMessage.ParseWithRequestId, message, count, true);
        }
    }
}