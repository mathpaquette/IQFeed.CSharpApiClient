using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class ChainsMessage<T>
    {
        public const string ChainsDataId = "LC";

        public ChainsMessage()
        {
            Chains = new List<T>();
        }

        public ChainsMessage(IList<T> chains)
        {
            Chains = chains;
        }

        public IList<T> Chains { get; private set; }

        public string RequestId { get; set; }

        protected IEnumerable<string> GetSymbols(string message, bool hasRequestId)
        {
            // we now have either: LC,ListOfSymbols OR requestId,LC,ListOfSymbols
            var values = message.SplitFeedMessage();
            RequestId = hasRequestId ? values[0] : null;
            var symbolBase = hasRequestId ? 2 : 1;
            var length = values.Length - symbolBase;
            var symbols = new Memory<string>(values, symbolBase, length).ToArray();

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