using IQFeed.CSharpApiClient.Common.Exceptions;

namespace IQFeed.CSharpApiClient.Common
{
    public class ErrorMessageHandler
    {
        public IQFeedException GetException(string errorMessage)
        {
            switch (errorMessage)
            {
                case "!NO_DATA!":
                    return new NoDataIQFeedException(errorMessage);
                default:
                    return new IQFeedException(errorMessage);
            }
        }
    }
}