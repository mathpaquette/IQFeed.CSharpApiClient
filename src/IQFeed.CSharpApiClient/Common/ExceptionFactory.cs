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
                case InvalidDataIQFeedException.InvalidData:
                    return new InvalidDataIQFeedException(request, errorMessage, messageTrace);
                default:
                    return new IQFeedException(request, DefaultMessage, errorMessage, messageTrace);
            }
        }
    }
}