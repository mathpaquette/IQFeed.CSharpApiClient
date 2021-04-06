using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class FutureSpreadMessage : ChainsMessage<FutureSpread>
    {
        public FutureSpreadMessage(string message, bool hasRequestId)
        {
            ParseInner(message, hasRequestId);
        }

        public static FutureSpreadMessage Parse(string message)
        {
            return new FutureSpreadMessage(message, false);
        }

        public static FutureSpreadMessage ParseWithRequestId(string message)
        {
            return new FutureSpreadMessage(message, true);
        }

        public void ParseInner(string message, bool hasRequestId)
        {
            foreach (var symbol in GetSymbols(message, hasRequestId))
            {
                Chains.Add(FutureSpread.Parse(symbol));
            }
        }
    }
}