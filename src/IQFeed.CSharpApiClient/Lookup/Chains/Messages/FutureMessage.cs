using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class FutureMessage : ChainsMessage<Future>
    {
        public FutureMessage(string message, bool hasRequestId)
        {
            ParseInner(message, hasRequestId);
        }

        public static FutureMessage Parse(string message)
        {
            return new FutureMessage(message, false);
        }

        public static FutureMessage ParseWithRequestId(string message)
        {
            return new FutureMessage(message, true);
        }

        public void ParseInner(string message, bool hasRequestId)
        {
            foreach (var symbol in GetSymbols(message, hasRequestId))
            {
                Chains.Add(Future.Parse(symbol));
            }
        }
    }
}