using System.Collections.Generic;
using System.Text.RegularExpressions;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class FutureSpreadMessage : ChainsMessage<FutureSpread>
    {
        public FutureSpreadMessage(IEnumerable<FutureSpread> chains) : base(chains) { }

        public static FutureSpreadMessage Parse(string message)
        {
            var chains = new List<FutureSpread>();
            foreach (var symbol in GetSymbols(message))
            {
                chains.Add(FutureSpread.Parse(symbol));
            }
            return new FutureSpreadMessage(chains);
        }
    }
}