using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Common.Interfaces;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic
{
    public interface ILevel1DynamicClient : IClient, ILevel1DynamicEvent, ILevel1DynamicSnapshot
    {
        void ReqWatch(string symbol);
        void ReqTradesOnlyWatch(string symbol);
        void ReqUnwatch(string symbol);
        void ReqForcedRefresh(string symbol);
        void ReqTimestamp();
        void ReqTimestamps(bool on);
        void ReqRegionalWatch(string symbol);
        void ReqRegionalUnwatch(string symbol);
        void ReqNews(bool on);
        void ReqStats();
        void ReqFundamentalFieldnames();
        void ReqUpdateFieldnames();
        void ReqCurrentUpdateFieldNames();        
        void SetLogLevels(params LoggingLevel[] logLevels);
        void ReqWatchList();
        void ReqUnwatchAll();
        void ReqServerConnect();
        void ReqServerDisconnect();
    }
}