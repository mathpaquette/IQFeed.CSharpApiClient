namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1Client: ILevel1EventHandler
    {
        void ReqWatch(string symbol);
        void ReqTradesOnlyWatch(string symbol);
        void ReqUnwatch(string symbol);
        void ReqForcedRefresh(string symbol);
        void ReqTimestamp();
        void ReqTimestampsOn();
        void ReqTimestampsOff();
        void ReqRegionalWatch(string symbol);
        void ReqRegionalUnwatch(string symbol);
        void ReqNewsOn();
        void ReqNewsOff();
        void ReqStats();
        void ReqFundamentalFieldnames();
        void ReqUpdateFieldnames();
        void ReqCurrentUpdateFieldNames();
        void SelectUpdateFieldName(string[] fieldNames);
        void SetLogLevels(string[] logLevels);
        void ReqWatchList();
        void ReqUnwatchAll();
        void ReqServerConnect();
        void ReqServerDisconnect();
    }
}