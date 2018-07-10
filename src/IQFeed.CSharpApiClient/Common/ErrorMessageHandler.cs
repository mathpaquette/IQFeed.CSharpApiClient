using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Common
{
    public class ErrorMessageHandler
    {
        public IQFeedException GetException(string errorMessage)
        {
            var values = errorMessage.SplitFeedMessage();
            var exception = values[1];
            switch (exception)
            {
                case "!NO_DATA!":
                    return new NoDataIQFeedException(exception, errorMessage);
                default:
                    return new IQFeedException(exception, errorMessage);
            }
        }
    }
}