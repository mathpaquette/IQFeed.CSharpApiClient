namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public interface IListedMarket
    {
        int ListedMarketId { get; }
        string ShortName { get; }
        string LongName { get; }        
        string GroupID { get; }
        string ShortGroupName { get; }        
    }
}