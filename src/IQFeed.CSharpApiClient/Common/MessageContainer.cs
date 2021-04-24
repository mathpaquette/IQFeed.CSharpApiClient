using System.Collections.Generic;

namespace IQFeed.CSharpApiClient.Common
{
    public class MessageContainer<T>
    {
        public IEnumerable<T> Messages { get; }
        public IEnumerable<InvalidMessage<T>> InvalidMessages { get; }
        public bool End { get; }
        public string ErrorMessage { get; }
        public string MessageTrace { get; }

        public MessageContainer(IEnumerable<T> messages, bool end)
        {
            Messages = messages;
            InvalidMessages = new List<InvalidMessage<T>>();
            End = end;
        }

        public MessageContainer(IEnumerable<T> messages, IEnumerable<InvalidMessage<T>> invalidMessages, bool end)
        {
            Messages = messages;
            InvalidMessages = invalidMessages;
            End = end;
        }

        public MessageContainer(string errorMessage, string messageTrace)
        {
            Messages = new List<T>();
            InvalidMessages = new List<InvalidMessage<T>>();
            End = true;
            ErrorMessage = errorMessage;
            MessageTrace = messageTrace;
        }
    }
}