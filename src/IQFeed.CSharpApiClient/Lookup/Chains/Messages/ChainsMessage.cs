using System.Collections.Generic;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class ChainsMessage<T>
    {
        public ChainsMessage(IEnumerable<T> chains)
        {
            Chains = chains;
        }

        public IEnumerable<T> Chains { get; private set; }

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