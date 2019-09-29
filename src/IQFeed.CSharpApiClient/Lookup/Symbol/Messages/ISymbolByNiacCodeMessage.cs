namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface ISymbolByNiacCodeMessage
    {
        string Description { get; }
        int ListedMarketId { get; }
        int NiacCode { get; }
        int SecurityTypeId { get; }
        string Symbol { get; }
    }
}