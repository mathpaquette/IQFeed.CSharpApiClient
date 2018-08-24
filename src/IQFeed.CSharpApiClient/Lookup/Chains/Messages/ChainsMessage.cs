using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    [Serializable]
    public class ChainsMessage<T>
    {
        protected ChainsMessage()
        {
            //empty constructor for serialization.
        }
        public ChainsMessage(IEnumerable<T> chains)
        {
            Chains = chains;
        }

        public IEnumerable<T> Chains { get; }

        protected static IEnumerable<string> GetSymbols(string message)
        {
            var symbols = message.SplitFeedMessage();
            foreach (var symbol in symbols)
            {
                // skip characters
                if (symbol == ":" || symbol == string.Empty || symbol == " ")
                    continue;

                yield return symbol;
            }
        }    
    }
}