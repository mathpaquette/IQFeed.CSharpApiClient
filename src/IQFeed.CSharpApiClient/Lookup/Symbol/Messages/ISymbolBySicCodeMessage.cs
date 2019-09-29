namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface ISymbolBySicCodeMessage
    {
        string Description { get; }
        int ListedMarketId { get; }
        int SecurityTypeId { get; }
        int SicCode { get; }
        string Symbol { get; }
    }
}