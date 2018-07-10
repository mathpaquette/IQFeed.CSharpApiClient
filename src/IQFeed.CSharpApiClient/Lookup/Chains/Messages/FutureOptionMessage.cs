using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class FutureOptionMessage : ChainsMessage<FutureOption>
    {
        public FutureOptionMessage(IEnumerable<FutureOption> chains) : base(chains) { }

        public static FutureOptionMessage Parse(string message)
        {
            var chains = new List<FutureOption>();
            foreach (var symbol in GetSymbols(message))
            {
                chains.Add(FutureOption.Parse(symbol));
            }
            return new FutureOptionMessage(chains);
        }
    }
}