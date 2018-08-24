using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    [Serializable]
    public class FutureMessage : ChainsMessage<Future>
    {
        private FutureMessage() : base()
        {
            //empty constructor for serialization.
        }

        public FutureMessage(IEnumerable<Future> chains) : base(chains) { }

        public static FutureMessage Parse(string message)
        {
            var chains = new List<Future>();
            foreach (var symbol in GetSymbols(message))
            {
                chains.Add(Future.Parse(symbol));
            }
            return new FutureMessage(chains);
        }
    }
}