namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class NoDataIQFeedException : IQFeedException
    {
        private const string NoDataMessage = "IQFeed doesn't have any data for the request sent.";

        public NoDataIQFeedException(string errorMessage, string messageTrace) : base(NoDataMessage, errorMessage, messageTrace) { }
    }
}