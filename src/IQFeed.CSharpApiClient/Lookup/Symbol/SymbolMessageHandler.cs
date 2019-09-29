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

        public MessageContainer<SymbolBySicCodeMessage> GetSymbolBySicCodeMessages(byte[] message, int count)
        {
            return ProcessMessages(SymbolBySicCodeMessage.Parse, message, count, false);
        }

        public MessageContainer<SymbolBySicCodeMessage> GetSymbolBySicCodeMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SymbolBySicCodeMessage.ParseWithRequestId, message, count, true);
        }

        public MessageContainer<SymbolByNiacCodeMessage> GetSymbolByNiacCodeMessages(byte[] message, int count)
        {
            return ProcessMessages(SymbolByNiacCodeMessage.Parse, message, count, false);
        }

        public MessageContainer<SymbolByNiacCodeMessage> GetSymbolByNiacCodeMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SymbolByNiacCodeMessage.ParseWithRequestId, message, count, true);
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

        public MessageContainer<SicCodeInfoMessage> GetSicCodeInfoMessages(byte[] message, int count)
        {
            return ProcessMessages(SicCodeInfoMessage.Parse, message, count, false);
        }

        public MessageContainer<SicCodeInfoMessage> GetSicCodeInfoMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SicCodeInfoMessage.ParseWithRequestId, message, count, true);
        }

        public MessageContainer<NiacCodeInfoMessage> GetNiacCodeInfoMessages(byte[] message, int count)
        {
            return ProcessMessages(NiacCodeInfoMessage.Parse, message, count, false);
        }

        public MessageContainer<NiacCodeInfoMessage> GetNiacCodeInfoMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(NiacCodeInfoMessage.ParseWithRequestId, message, count, true);
        }
    }
}