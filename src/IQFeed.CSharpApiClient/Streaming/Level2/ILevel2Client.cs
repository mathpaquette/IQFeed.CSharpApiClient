using IQFeed.CSharpApiClient.Common.Interfaces;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public interface ILevel2Client : IClient, ILevel2Event, ILevel2Snapshot
    {
        void ReqWatch(string symbol);
        void ReqWatchMarketByPrice(string symbol, int? maxPriceLevels = null);
        void ReqWatchMarketByOrder(string symbol);
        void ReqMarketMakerNameById(string mmid);
        void ReqUnwatch(string symbol);
        void ReqServerConnect();
        void ReqServerDisconnect();
    }
}