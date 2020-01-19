using IQFeed.CSharpApiClient.Common.Interfaces;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public interface ILevel2Client<T> : IClient, ILevel2Event<T>, ILevel2Snapshot<T>
    {
        void ReqWatch(string symbol);
        void ReqMarketMakerNameById(string mmid);
        void ReqUnwatch(string symbol);
        void ReqServerConnect();
        void ReqServerDisconnect();
    }
}