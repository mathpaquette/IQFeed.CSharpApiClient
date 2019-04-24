using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Common.Interfaces;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public interface ILevel2Client : IClient, ILevel2Event, ILevel2Snapshot
    {
        void ReqWatch(string symbol);
        void ReqMarketMakerID(string MMID);
        void ReqUnwatch(string symbol);
        void ReqServerConnect();
        void ReqServerDisconnect();
    }
}