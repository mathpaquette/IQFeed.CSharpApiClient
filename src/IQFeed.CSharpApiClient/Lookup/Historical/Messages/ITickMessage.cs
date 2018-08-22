namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface ITickMessage : IMessage
    {
        float Ask { get; }
        char BasisForLast { get; }
        float Bid { get; }
        float Last { get; }
        int LastSize { get; }
        long TickId { get; }
        int TotalVolume { get; }
        string TradeConditions { get; }
        int TradeMarketCenter { get; }
    }
}