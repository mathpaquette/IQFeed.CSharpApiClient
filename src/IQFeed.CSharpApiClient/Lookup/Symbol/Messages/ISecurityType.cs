namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface ISecurityType
    {
        int SecurityTypeId { get; }
        string ShortName { get; }
        string LongName { get; }
    }
}