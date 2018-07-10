using System.Collections.Generic;

namespace IQFeed.CSharpApiClient.Common
{
    public class MessageContainer<T>
    {
        public IEnumerable<T> Messages { get; }
        public bool End { get; }
        public string ErrorMessage { get; }

        public MessageContainer(IEnumerable<T> messages, bool end, string errorMessage = null)
        {
            Messages = messages;
            End = end;
            ErrorMessage = errorMessage;
        }
    }
}