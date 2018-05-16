using System;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Level1.EventArgs;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1Client : ILevel1Client
    {
        public event EventHandler<FundamentalEventArgs> Fundamental
        {
            add => _level1MessageHandler.Fundamental += value;
            remove => _level1MessageHandler.Fundamental -= value;
        }

        public event EventHandler<UpdateSummaryEventArgs> Summary
        {
            add => _level1MessageHandler.Summary += value;
            remove => _level1MessageHandler.Summary -= value;
        }

        public event EventHandler<SystemEventArgs> System
        {
            add => _level1MessageHandler.System += value;
            remove => _level1MessageHandler.System -= value;
        }
        public event EventHandler<SymbolNotFoundEventArgs> SymbolNotFound
        {
            add => _level1MessageHandler.SymbolNotFound += value;
            remove => _level1MessageHandler.SymbolNotFound -= value;
        }
        public event EventHandler<ErrorEventArgs> Error
        {
            add => _level1MessageHandler.Error += value;
            remove => _level1MessageHandler.Error -= value;
        }
        public event EventHandler<TimestampEventArgs> Timestamp
        {
            add => _level1MessageHandler.Timestamp += value;
            remove => _level1MessageHandler.Timestamp -= value;
        }
        public event EventHandler<UpdateSummaryEventArgs> Update
        {
            add => _level1MessageHandler.Update += value;
            remove => _level1MessageHandler.Update -= value;
        }
        public event EventHandler<RegionalUpdateEventArgs> Regional
        {
            add => _level1MessageHandler.Regional += value;
            remove => _level1MessageHandler.Regional -= value;
        }
        public event EventHandler<NewsEventArgs> News
        {
            add => _level1MessageHandler.News += value;
            remove => _level1MessageHandler.News -= value;
        }

        private readonly SocketClient _socketClient;
        private readonly Level1RequestFormatter _level1RequestFormatter;
        private readonly Level1MessageHandler _level1MessageHandler;

        public Level1Client(SocketClient socketClient, Level1RequestFormatter level1RequestFormatter, Level1MessageHandler level1MessageHandler)
        {
            _socketClient = socketClient;
            _socketClient.MessageReceived += SocketClientOnMessageReceived;
            _socketClient.Connected += SocketClientOnConnected;

            _level1RequestFormatter = level1RequestFormatter;
            _level1MessageHandler = level1MessageHandler;
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

        public void ReqTimestampsOn()
        {
            var request = _level1RequestFormatter.ReqTimestampsOn();
            _socketClient.Send(request);
        }

        public void ReqTimestampsOff()
        {
            var request = _level1RequestFormatter.ReqTimestampsOff();
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

        public void ReqNewsOn()
        {
            var request = _level1RequestFormatter.ReqNewsOn();
            _socketClient.Send(request);
        }

        public void ReqNewsOff()
        {
            var request = _level1RequestFormatter.ReqNewsOff();
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

        public void SelectUpdateFieldName(string[] fieldNames)
        {
            var request = _level1RequestFormatter.SelectUpdateFieldName(fieldNames);
            _socketClient.Send(request);
        }

        public void SetLogLevels(string[] logLevels)
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

        private void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs e)
        {
            _level1MessageHandler.ProcessMessages(e.Message, e.Count);
        }
        private void SocketClientOnConnected(object sender, System.EventArgs eventArgs)
        {
            var socketClient = (SocketClient)sender;
            socketClient.Send(_level1RequestFormatter.SetProtocol(IQFeedDefault.ProtocolVersion));
            socketClient.Connected -= SocketClientOnConnected;
        }
    }
}