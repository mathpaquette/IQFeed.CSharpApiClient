namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class NoDataIQFeedException : IQFeedException
    {
        public NoDataIQFeedException(string exception, string errorMessage) : base(exception, errorMessage) { }
    }
}