namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface ITradeCondition
    {
        int TradeConditionId { get; }
        string ShortName { get; }
        string LongName { get; }
    }
}