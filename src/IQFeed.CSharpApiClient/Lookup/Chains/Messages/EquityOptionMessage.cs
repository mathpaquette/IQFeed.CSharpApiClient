using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class EquityOptionMessage : ChainsMessage<EquityOption>
    {
        public EquityOptionMessage(IEnumerable<EquityOption> chains) : base(chains) { }
       
        public static EquityOptionMessage Parse(string message)
        {
            var chains = new List<EquityOption>();
            foreach (var symbol in GetSymbols(message))
            {
                chains.Add(EquityOption.Parse(symbol));
            }
            return new EquityOptionMessage(chains);
        }
    }
}