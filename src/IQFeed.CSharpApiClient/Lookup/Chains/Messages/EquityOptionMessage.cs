using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class EquityOptionMessage : ChainsMessage<EquityOption>
    {
        public EquityOptionMessage(string message, bool hasRequestId)
        {
            ParseInner(message, hasRequestId);
        }

        public static EquityOptionMessage Parse(string message)
        {
            return new EquityOptionMessage(message, false);
        }

        public static EquityOptionMessage ParseWithRequestId(string message)
        {
            return new EquityOptionMessage(message, true);
        }

        private void ParseInner(string message, bool hasRequestid)
        {
            foreach (var symbol in GetSymbols(message, hasRequestid))
            {
                Chains.Add(EquityOption.Parse(symbol));
            }
        }
    }
}