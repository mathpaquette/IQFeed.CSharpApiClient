using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public class SymbolMessageHandler : BaseLookupMessageHandler
    {
        public MessageContainer<SymbolByFilterMessage> GetSymbolByFilterMessages(byte[] message, int count)
        {
            return ProcessMessages(SymbolByFilterMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<SymbolByFilterMessage> GetSymbolByFilterMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SymbolByFilterMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<SymbolBySicCodeMessage> GetSymbolBySicCodeMessages(byte[] message, int count)
        {
            return ProcessMessages(SymbolBySicCodeMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<SymbolBySicCodeMessage> GetSymbolBySicCodeMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SymbolBySicCodeMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<SymbolByNaicsCodeMessage> GetSymbolByNaicsCodeMessages(byte[] message, int count)
        {
            return ProcessMessages(SymbolByNaicsCodeMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<SymbolByNaicsCodeMessage> GetSymbolByNaicsCodeMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SymbolByNaicsCodeMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<ListedMarketMessage> GetListedMarketMessages(byte[] message, int count)
        {
            return ProcessMessages(ListedMarketMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<ListedMarketMessage> GetListedMarketMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(ListedMarketMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<SecurityTypeMessage> GetSecurityTypeMessages(byte[] message, int count)
        {
            return ProcessMessages(SecurityTypeMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<SecurityTypeMessage> GetSecurityTypeMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SecurityTypeMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<TradeConditionMessage> GetTradeConditionMessages(byte[] message, int count)
        {
            return ProcessMessages(TradeConditionMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<TradeConditionMessage> GetTradeConditionMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TradeConditionMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<SicCodeInfoMessage> GetSicCodeInfoMessages(byte[] message, int count)
        {
            return ProcessMessages(SicCodeInfoMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<SicCodeInfoMessage> GetSicCodeInfoMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(SicCodeInfoMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<NaicsCodeInfoMessage> GetNaicsCodeInfoMessages(byte[] message, int count)
        {
            return ProcessMessages(NaicsCodeInfoMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<NaicsCodeInfoMessage> GetNaicsCodeInfoMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(NaicsCodeInfoMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }
    }
}