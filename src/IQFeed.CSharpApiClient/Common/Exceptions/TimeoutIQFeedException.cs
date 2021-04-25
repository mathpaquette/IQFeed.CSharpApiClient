namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class TimeoutIQFeedException : IQFeedException
    {
        public TimeoutIQFeedException(string request) : base(request, "Unable to receive complete response.", "Request timeout", string.Empty) { }
    }
}