using System.Collections.Generic;

namespace IQFeed.CSharpApiClient.Common
{
    public class MessageContainer<T>
    {
        public IEnumerable<T> Messages { get; }
        public bool End { get; }
        public string ErrorMessage { get; }
        public string MessageTrace { get; }

        public MessageContainer(IEnumerable<T> messages, bool end)
        {
            Messages = messages;
            End = end;
        }

        public MessageContainer(IEnumerable<T> messages, bool end, string errorMessage, string messageTrace)
        {
            Messages = messages;
            End = end;
            ErrorMessage = errorMessage;
            MessageTrace = messageTrace;
        }
    }
}