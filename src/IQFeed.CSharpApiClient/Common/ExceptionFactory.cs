using System.Collections.Generic;
using IQFeed.CSharpApiClient.Common.Exceptions;

namespace IQFeed.CSharpApiClient.Common
{
    public class ExceptionFactory
    {
        private const string DefaultMessage = "IQFeed exception received.";
        
        public IQFeedException CreateNew(string request, string errorMessage, string messageTrace)
        {
            switch (errorMessage)
            {
                case IQFeedDefault.ProtocolNoDataCharacters:
                    return new NoDataIQFeedException(request, errorMessage, messageTrace);
                default:
                    return new IQFeedException(request, DefaultMessage, errorMessage, messageTrace);
            }
        }

        public InvalidDataIQFeedException<T> CreateNew<T>(string request, IEnumerable<InvalidMessage<T>> invalidMessages, IEnumerable<T> messages)
        {
            return new InvalidDataIQFeedException<T>(request, invalidMessages, messages);
        }
    }
}