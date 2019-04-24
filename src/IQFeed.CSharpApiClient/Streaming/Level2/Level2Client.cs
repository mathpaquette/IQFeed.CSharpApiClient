using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public class Level2Client : ILevel2Client
    {
        public event Action<UpdateSummaryMessage> Summary
        {
            add => _Level2MessageHandler.Summary += value;
            remove => _Level2MessageHandler.Summary -= value;
        }
        public event Action<SymbolNotFoundMessage> SymbolNotFound
        {
            add => _Level2MessageHandler.SymbolNotFound += value;
            remove => _Level2MessageHandler.SymbolNotFound -= value;
        }
        public event Action<ErrorMessage> Error
        {
            add => _Level2MessageHandler.Error += value;
            remove => _Level2MessageHandler.Error -= value;
        }
        public event Action<TimestampMessage> Timestamp
        {
            add => _Level2MessageHandler.Timestamp += value;
            remove => _Level2MessageHandler.Timestamp -= value;
        }
        public event Action<UpdateSummaryMessage> Update
        {
            add => _Level2MessageHandler.Update += value;
            remove => _Level2MessageHandler.Update -= value;
        }

        private readonly SocketClient _socketClient;
        private readonly Level2RequestFormatter _Level2RequestFormatter;
        private readonly Level2MessageHandler _Level2MessageHandler;
        private readonly ILevel2Snapshot _Level2Snapshot;

        public event Action<NameLevelQueryResponseMessage> QueryResponse;

        public Level2Client(SocketClient socketClient, Level2RequestFormatter Level2RequestFormatter, Level2MessageHandler Level2MessageHandler, ILevel2Snapshot Level2Snapshot)
        {
            _Level2Snapshot = Level2Snapshot;
            _socketClient = socketClient;
            _socketClient.MessageReceived += SocketClientOnMessageReceived;
            _socketClient.Connected += SocketClientOnConnected;

            _Level2RequestFormatter = Level2RequestFormatter;
            _Level2MessageHandler = Level2MessageHandler;
        }

        public void ReqWatch(string symbol)
        {
            var request = _Level2RequestFormatter.ReqWatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqUnwatch(string symbol)
        {
            var request = _Level2RequestFormatter.ReqUnwatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqServerConnect()
        {
            var request = _Level2RequestFormatter.ReqServerConnect();
            _socketClient.Send(request);
        }

        public void ReqServerDisconnect()
        {
            var request = _Level2RequestFormatter.ReqServerDisconnect();
            _socketClient.Send(request);
        }

        public Task<UpdateSummaryMessage> GetUpdateSummarySnapshotAsync(string symbol)
        {
            return _Level2Snapshot.GetUpdateSummarySnapshotAsync(symbol.ToUpper());
        }

        private void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs e)
        {
            _Level2MessageHandler.ProcessMessages(e.Message, e.Count);
        }

        private void SocketClientOnConnected(object sender, EventArgs eventArgs)
        {
            var socketClient = (SocketClient)sender;
            socketClient.Send(_Level2RequestFormatter.SetProtocol(IQFeedDefault.ProtocolVersion));
            socketClient.Connected -= SocketClientOnConnected;
        }

        public void ReqMarketMakerID(string MMID)
        {
            throw new NotImplementedException();
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