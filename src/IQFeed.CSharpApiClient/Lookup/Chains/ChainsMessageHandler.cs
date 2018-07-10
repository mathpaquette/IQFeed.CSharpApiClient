using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public class ChainsMessageHandler : BaseLookupMessageHandler
    {
        public MessageContainer<FutureMessage> GetFutureMessages(byte[] message, int count)
        {
            return ProcessMessages(FutureMessage.Parse, message, count);
        }

        public MessageContainer<FutureSpreadMessage> GetFutureSpreadMessages(byte[] message, int count)
        {
            return ProcessMessages(FutureSpreadMessage.Parse, message, count);
        }

        public MessageContainer<FutureOptionMessage> GetFutureOptionMessages(byte[] message, int count)
        {
            return ProcessMessages(FutureOptionMessage.Parse, message, count);
        }

        public MessageContainer<EquityOptionMessage> GetEquityOptionMessages(byte[] message, int count)
        {
            return ProcessMessages(EquityOptionMessage.Parse, message, count);
        }
    }
}