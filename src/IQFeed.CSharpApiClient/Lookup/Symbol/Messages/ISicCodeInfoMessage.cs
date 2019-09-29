namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface ISicCodeInfoMessage
    {
        string Description { get; }
        int SicCode { get; }
    }
}