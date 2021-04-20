using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class FutureOptionMessage : ChainsMessage<FutureOption>
    {
        public FutureOptionMessage(string message, bool hasRequestId)
        {
            ParseInner(message, hasRequestId);
        }

        public static FutureOptionMessage Parse(string message)
        {
            return new FutureOptionMessage(message, false);
        }

        public static FutureOptionMessage ParseWithRequestId(string message)
        {
            return new FutureOptionMessage(message, true);
        }

        private void ParseInner(string message, bool hasRequestId)
        {
            foreach (var symbol in GetSymbols(message, hasRequestId))
            {
                Chains.Add(FutureOption.Parse(symbol));
            }
        }
    }
}