namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface ISymbolByNaicsCodeMessage
    {
        string Description { get; }
        int ListedMarketId { get; }
        int NaicsCode { get; }
        int SecurityTypeId { get; }
        string Symbol { get; }
    }
}