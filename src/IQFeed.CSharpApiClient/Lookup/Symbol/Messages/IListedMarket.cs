namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface IListedMarket
    {
        int ListedMarketId { get; }
        string ShortName { get; }
        string LongName { get; }        
        int GroupId { get; }
        string ShortGroupName { get; }        
    }
}