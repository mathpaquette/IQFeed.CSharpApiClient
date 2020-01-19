using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public class Level2Client<T> : ILevel2Client<T>
    {
        public event Action<UpdateSummaryMessage<T>> Summary
        {
            add => _level2MessageHandler.Summary += value;
            remove => _level2MessageHandler.Summary -= value;
        }
        public event Action<SymbolNotFoundMessage> SymbolNotFound
        {
            add => _level2MessageHandler.SymbolNotFound += value;
            remove => _level2MessageHandler.SymbolNotFound -= value;
        }
        public event Action<ErrorMessage> Error
        {
            add => _level2MessageHandler.Error += value;
            remove => _level2MessageHandler.Error -= value;
        }
        public event Action<TimestampMessage> Timestamp
        {
            add => _level2MessageHandler.Timestamp += value;
            remove => _level2MessageHandler.Timestamp -= value;
        }
        public event Action<UpdateSummaryMessage<T>> Update
        {
            add => _level2MessageHandler.Update += value;
            remove => _level2MessageHandler.Update -= value;
        }
        public event Action<MarketMakerNameMessage> Query
        {
            add => _level2MessageHandler.Query += value;
            remove => _level2MessageHandler.Query -= value;
        }
        public event Action<SystemMessage> System
        {
            add => _level2MessageHandler.System += value;
            remove => _level2MessageHandler.System -= value;
        }

        private readonly SocketClient _socketClient;
        private readonly Level2RequestFormatter _level2RequestFormatter;
        private readonly ILevel2MessageHandler<T> _level2MessageHandler;
        private readonly ILevel2Snapshot<T> _level2Snapshot;

        public Level2Client(
            SocketClient socketClient, 
            Level2RequestFormatter level2RequestFormatter, 
            ILevel2MessageHandler<T> level2MessageHandler, 
            ILevel2Snapshot<T> level2Snapshot)
        {
            _level2Snapshot = level2Snapshot;
            _socketClient = socketClient;
            _socketClient.MessageReceived += SocketClientOnMessageReceived;
            _socketClient.Connected += SocketClientOnConnected;

            _level2RequestFormatter = level2RequestFormatter;
            _level2MessageHandler = level2MessageHandler;
        }

        public void ReqWatch(string symbol)
        {
            var request = _level2RequestFormatter.ReqWatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqUnwatch(string symbol)
        {
            var request = _level2RequestFormatter.ReqUnwatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqServerConnect()
        {
            var request = _level2RequestFormatter.ReqServerConnect();
            _socketClient.Send(request);
        }

        public void ReqServerDisconnect()
        {
            var request = _level2RequestFormatter.ReqServerDisconnect();
            _socketClient.Send(request);
        }

        public Task<IEnumerable<UpdateSummaryMessage<T>>> GetSummarySnapshotAsync(string symbol)
        {
            return _level2Snapshot.GetSummarySnapshotAsync(symbol.ToUpper());
        }

        public void ReqMarketMakerNameById(string mmid)
        {
            var request = _level2RequestFormatter.ReqMarketMakerNameById(mmid);
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

        private void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs e)
        {
            _level2MessageHandler.ProcessMessages(e.Message, e.Count);
        }

        private void SocketClientOnConnected(object sender, EventArgs eventArgs)
        {
            var socketClient = (SocketClient)sender;
            socketClient.Send(_level2RequestFormatter.SetProtocol(IQFeedDefault.ProtocolVersion));
            socketClient.Connected -= SocketClientOnConnected;
        }
    }
}