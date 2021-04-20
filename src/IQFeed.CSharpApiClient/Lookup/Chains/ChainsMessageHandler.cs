using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public class ChainsMessageHandler : BaseLookupMessageHandler
    {
        public MessageContainer<FutureMessage> GetFutureMessages(byte[] message, int count)
        {
            return ProcessMessages(FutureMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<FutureMessage> GetFutureMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(FutureMessage.ParseWithRequestId, ParseErrorMessage, message, count);
        }

        public MessageContainer<FutureSpreadMessage> GetFutureSpreadMessages(byte[] message, int count)
        {
            return ProcessMessages(FutureSpreadMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<FutureSpreadMessage> GetFutureSpreadMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(FutureSpreadMessage.ParseWithRequestId, ParseErrorMessage, message, count);
        }

        public MessageContainer<FutureOptionMessage> GetFutureOptionMessages(byte[] message, int count)
        {
            return ProcessMessages(FutureOptionMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<FutureOptionMessage> GetFutureOptionMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(FutureOptionMessage.ParseWithRequestId, ParseErrorMessage, message, count);
        }

        public MessageContainer<EquityOptionMessage> GetEquityOptionMessages(byte[] message, int count)
        {
            return ProcessMessages(EquityOptionMessage.Parse, ParseErrorMessage, message, count);
        }
        public MessageContainer<EquityOptionMessage> GetEquityOptionMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(EquityOptionMessage.ParseWithRequestId, ParseErrorMessage, message, count);
        }
    }
}