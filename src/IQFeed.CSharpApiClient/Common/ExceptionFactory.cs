using IQFeed.CSharpApiClient.Common.Exceptions;

namespace IQFeed.CSharpApiClient.Common
{
    public class ExceptionFactory
    {
        private const string DefaultMessage = "IQFeed exception received.";

        public IQFeedException CreateNew(string errorMessage, string messageTrace)
        {
            switch (errorMessage)
            {
                case IQFeedDefault.ProtocolNoDataCharacters:
                    return new NoDataIQFeedException(errorMessage, messageTrace);
                default:
                    return new IQFeedException(DefaultMessage, errorMessage, messageTrace);
            }
        }
    }
}