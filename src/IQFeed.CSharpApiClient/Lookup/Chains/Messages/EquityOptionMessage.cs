using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    [Serializable]
    public class EquityOptionMessage : ChainsMessage<EquityOption>
    {
        private EquityOptionMessage() : base() 
        {
            //empty constructor for serialization.
        }
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