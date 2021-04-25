using System.Collections.Generic;

namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class InvalidDataIQFeedException<T> : IQFeedException
    {
        public IEnumerable<InvalidMessage<T>> InvalidMessages { get; }
        public IEnumerable<T> Messages { get; }

        public InvalidDataIQFeedException(string request, IEnumerable<InvalidMessage<T>> invalidMessages, IEnumerable<T> messages) : 
            base(request, "Unable to parse received data.", "Invalid data", $"Please check ${nameof(InvalidMessages)} property.")
        {
            InvalidMessages = invalidMessages;
            Messages = messages;
        }
    }
}