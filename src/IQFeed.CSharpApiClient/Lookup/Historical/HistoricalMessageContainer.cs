using System.Collections.Generic;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public class HistoricalMessageContainer<T>
    {
        public IEnumerable<T> Messages { get; }
        public bool End { get; }
        public string Error { get; }

        public HistoricalMessageContainer(IEnumerable<T> messages, bool end, string error = null)
        {
            Messages = messages;
            End = end;
            Error = error;
        }
    }
}