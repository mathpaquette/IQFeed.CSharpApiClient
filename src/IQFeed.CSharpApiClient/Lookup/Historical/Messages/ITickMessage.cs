namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface ITickMessage<T> : IHistoricalMessage
    {
        T Ask { get; }
        char BasisForLast { get; }
        T Bid { get; }
        T Last { get; }
        int LastSize { get; }
        long TickId { get; }
        int TotalVolume { get; }
        string TradeConditions { get; }
        int TradeMarketCenter { get; }
    }
}