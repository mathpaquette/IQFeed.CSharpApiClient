namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface ISymbolByFilter
    {
        string Symbol { get; }
        int ListedMarketId { get; }
        int SecurityTypeId { get; }
        string Description { get; }
    }
}