using System.Collections.Generic;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public class ChainsMessageContainer<T>
    {
        public IEnumerable<T> Messages { get; }
        public bool End { get; }
        public string Error { get; }

        public ChainsMessageContainer(IEnumerable<T> messages, bool end, string error = null)
        {
            Messages = messages;
            End = end;
            Error = error;
        }
    }
}