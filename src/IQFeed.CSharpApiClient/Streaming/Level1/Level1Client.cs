using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1Client : BaseLevel1Client, ILevel1Client
    {
        public event Action<FundamentalMessage> Fundamental
        {
            add => _level1MessageHandler.Fundamental += value;
            remove => _level1MessageHandler.Fundamental -= value;
        }
        public event Action<IUpdateSummaryMessage> Summary
        {
            add => _level1MessageHandler.Summary += value;
            remove => _level1MessageHandler.Summary -= value;
        }
        public event Action<SystemMessage> System
        {
            add => _level1MessageHandler.System += value;
            remove => _level1MessageHandler.System -= value;
        }
        public event Action<SymbolNotFoundMessage> SymbolNotFound
        {
            add => _level1MessageHandler.SymbolNotFound += value;
            remove => _level1MessageHandler.SymbolNotFound -= value;
        }
        public event Action<ErrorMessage> Error
        {
            add => _level1MessageHandler.Error += value;
            remove => _level1MessageHandler.Error -= value;
        }
        public event Action<TimestampMessage> Timestamp
        {
            add => _level1MessageHandler.Timestamp += value;
            remove => _level1MessageHandler.Timestamp -= value;
        }
        public event Action<IUpdateSummaryMessage> Update
        {
            add => _level1MessageHandler.Update += value;
            remove => _level1MessageHandler.Update -= value;
        }
        public event Action<RegionalUpdateMessage> Regional
        {
            add => _level1MessageHandler.Regional += value;
            remove => _level1MessageHandler.Regional -= value;
        }
        public event Action<NewsMessage> News
        {
            add => _level1MessageHandler.News += value;
            remove => _level1MessageHandler.News -= value;
        }

        private readonly ILevel1MessageHandler _level1MessageHandler;
        private readonly ILevel1Snapshot _level1Snapshot;

        public Level1Client(
            SocketClient socketClient, 
            Level1RequestFormatter level1RequestFormatter, 
            ILevel1MessageHandler level1MessageHandler, 
            ILevel1Snapshot level1Snapshot) 
            : base(socketClient, level1RequestFormatter)
        {
            _level1Snapshot = level1Snapshot;
            _socketClient.MessageReceived += SocketClientOnMessageReceived;
            _socketClient.Connected += SocketClientOnConnected;

            _level1MessageHandler = level1MessageHandler;
        }        

        public void SelectUpdateFieldName(params DynamicFieldset[] fieldNames)
        {
            var dynamicFieldHandler = _level1MessageHandler as ILevel1MessageDynamicHandler;
            if (dynamicFieldHandler == null)
                throw new Exception($"{nameof(Level1MessageDynamicHandler)} is required to enable Dynamic Fields!");
            
            dynamicFieldHandler.SetDynamicFields(fieldNames);

            var request = _level1RequestFormatter.SelectUpdateFieldName(fieldNames);
            _socketClient.Send(request);
        }

        public Task<FundamentalMessage> GetFundamentalSnapshotAsync(string symbol)
        {
            return _level1Snapshot.GetFundamentalSnapshotAsync(symbol.ToUpper());
        }

        public Task<IUpdateSummaryMessage> GetUpdateSummarySnapshotAsync(string symbol)
        {
            return _level1Snapshot.GetUpdateSummarySnapshotAsync(symbol.ToUpper());
        }

        public FundamentalMessage GetFundamentalSnapshot(string symbol)
        {
            return _level1Snapshot.GetFundamentalSnapshot(symbol);
        }

        public IUpdateSummaryMessage GetUpdateSummarySnapshot(string symbol)
        {
            return _level1Snapshot.GetUpdateSummarySnapshot(symbol);
        }

        private void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs e)
        {
            _level1MessageHandler.ProcessMessages(e.Message, e.Count);
        }

        private void SocketClientOnConnected(object sender, EventArgs eventArgs)
        {
            var socketClient = (SocketClient)sender;
            socketClient.Send(_level1RequestFormatter.SetProtocol(IQFeedDefault.ProtocolVersion));
            socketClient.Connected -= SocketClientOnConnected;
        }
    }
}