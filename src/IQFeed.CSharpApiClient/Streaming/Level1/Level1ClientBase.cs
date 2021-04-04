using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public abstract class BaseLevel1Client
    {
        protected readonly SocketClient _socketClient;
        protected readonly Level1RequestFormatter _level1RequestFormatter;
        
        public BaseLevel1Client(SocketClient socketClient, Level1RequestFormatter level1RequestFormatter)
        {
            _socketClient = socketClient;
            _level1RequestFormatter = level1RequestFormatter;
        }

        public void ReqWatch(string symbol)
        {
            var request = _level1RequestFormatter.ReqWatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqTradesOnlyWatch(string symbol)
        {
            var request = _level1RequestFormatter.ReqTradesOnlyWatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqUnwatch(string symbol)
        {
            var request = _level1RequestFormatter.ReqUnwatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqForcedRefresh(string symbol)
        {
            var request = _level1RequestFormatter.ReqForcedRefresh(symbol);
            _socketClient.Send(request);
        }

        public void ReqTimestamp()
        {
            var request = _level1RequestFormatter.ReqTimestamp();
            _socketClient.Send(request);
        }

        public void ReqTimestamps(bool on = true)
        {
            var request = _level1RequestFormatter.ReqTimestamps(on);
            _socketClient.Send(request);
        }

        public void ReqRegionalWatch(string symbol)
        {
            var request = _level1RequestFormatter.ReqRegionalWatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqRegionalUnwatch(string symbol)
        {
            var request = _level1RequestFormatter.ReqRegionalUnwatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqNews(bool on = true)
        {
            var request = _level1RequestFormatter.ReqNews(on);
            _socketClient.Send(request);
        }

        public void ReqStats()
        {
            var request = _level1RequestFormatter.ReqStats();
            _socketClient.Send(request);
        }

        public void ReqFundamentalFieldnames()
        {
            var request = _level1RequestFormatter.ReqFundamentalFieldnames();
            _socketClient.Send(request);
        }

        public void ReqUpdateFieldnames()
        {
            var request = _level1RequestFormatter.ReqUpdateFieldnames();
            _socketClient.Send(request);
        }

        public void ReqCurrentUpdateFieldNames()
        {
            var request = _level1RequestFormatter.ReqCurrentUpdateFieldNames();
            _socketClient.Send(request);
        }
        
        public void SetLogLevels(params LoggingLevel[] logLevels)
        {
            var request = _level1RequestFormatter.SetLogLevels(logLevels);
            _socketClient.Send(request);
        }

        public void ReqWatchList()
        {
            var request = _level1RequestFormatter.ReqWatchList();
            _socketClient.Send(request);
        }

        public void ReqUnwatchAll()
        {
            var request = _level1RequestFormatter.ReqUnwatchAll();
            _socketClient.Send(request);
        }

        public void ReqServerConnect()
        {
            var request = _level1RequestFormatter.ReqServerConnect();
            _socketClient.Send(request);
        }

        public void ReqServerDisconnect()
        {
            var request = _level1RequestFormatter.ReqServerDisconnect();
            _socketClient.Send(request);
        }

        public void Connect()
        {
            _socketClient.Connect();
        }

        public void Disconnect()
        {
            _socketClient.Disconnect();
        }
    }
}