namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface INiacCodeInfoMessage
    {
        string Description { get; }
        int NiacCode { get; }
        string RequestId { get; }
    }
}