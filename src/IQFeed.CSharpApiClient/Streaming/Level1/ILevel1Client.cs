using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Common.Interfaces;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1Client: IClient, ILevel1Event, ILevel1Snapshot
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
        //void SelectUpdateFieldName(params DynamicFieldset[] fieldNames);
        void SetLogLevels(params LoggingLevel[] logLevels);
        void ReqWatchList();
        void ReqUnwatchAll();
        void ReqServerConnect();
        void ReqServerDisconnect();
    }
}