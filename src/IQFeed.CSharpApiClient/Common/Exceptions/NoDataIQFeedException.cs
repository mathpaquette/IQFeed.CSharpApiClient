namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class NoDataIQFeedException : IQFeedException
    {
        private const string NoDataMessage = "IQFeed doesn't have any data for the request sent.";

        public NoDataIQFeedException(string request, string errorMessage, string messageTrace) : base(request, NoDataMessage, errorMessage, messageTrace) { }
    }
}