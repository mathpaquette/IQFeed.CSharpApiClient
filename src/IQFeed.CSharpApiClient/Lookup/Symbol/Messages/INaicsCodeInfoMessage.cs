namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface INaicsCodeInfoMessage
    {
        string Description { get; }
        int NaicsCode { get; }
        string RequestId { get; }
    }
}