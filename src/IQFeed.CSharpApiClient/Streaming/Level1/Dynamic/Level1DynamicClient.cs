using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Handlers;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic
{
    public class Level1DynamicClient : BaseLevel1Client, ILevel1DynamicClient
    {
        public event Action<FundamentalMessage> Fundamental
        {
            add => _level1DynamicMessageHandler.Fundamental += value;
            remove => _level1DynamicMessageHandler.Fundamental -= value;
        }
        public event Action<IUpdateSummaryDynamicMessage> Summary
        {
            add => _level1DynamicMessageHandler.Summary += value;
            remove => _level1DynamicMessageHandler.Summary -= value;
        }
        public event Action<SystemMessage> System
        {
            add => _level1DynamicMessageHandler.System += value;
            remove => _level1DynamicMessageHandler.System -= value;
        }
        public event Action<SymbolNotFoundMessage> SymbolNotFound
        {
            add => _level1DynamicMessageHandler.SymbolNotFound += value;
            remove => _level1DynamicMessageHandler.SymbolNotFound -= value;
        }
        public event Action<ErrorMessage> Error
        {
            add => _level1DynamicMessageHandler.Error += value;
            remove => _level1DynamicMessageHandler.Error -= value;
        }
        public event Action<TimestampMessage> Timestamp
        {
            add => _level1DynamicMessageHandler.Timestamp += value;
            remove => _level1DynamicMessageHandler.Timestamp -= value;
        }
        public event Action<IUpdateSummaryDynamicMessage> Update
        {
            add => _level1DynamicMessageHandler.Update += value;
            remove => _level1DynamicMessageHandler.Update -= value;
        }
        public event Action<RegionalUpdateMessage> Regional
        {
            add => _level1DynamicMessageHandler.Regional += value;
            remove => _level1DynamicMessageHandler.Regional -= value;
        }
        public event Action<NewsMessage> News
        {
            add => _level1DynamicMessageHandler.News += value;
            remove => _level1DynamicMessageHandler.News -= value;
        }

        private readonly ILevel1DynamicMessageHandler _level1DynamicMessageHandler;
        private readonly ILevel1DynamicSnapshot _level1DynamicSnapshot;
        private readonly DynamicFieldset[] _fieldNames;

        public Level1DynamicClient(
            SocketClient socketClient, 
            Level1RequestFormatter level1RequestFormatter,
            ILevel1DynamicMessageHandler level1DynamicMessageHandler,
            ILevel1DynamicSnapshot level1DynamicSnapshot,
            DynamicFieldset[] fieldNames)
            : base(socketClient, level1RequestFormatter)
        {
            _level1DynamicSnapshot = level1DynamicSnapshot;
            _socketClient.MessageReceived += SocketClientOnMessageReceived;
            _socketClient.Connected += SocketClientOnConnected;

            _level1DynamicMessageHandler = level1DynamicMessageHandler;

            _fieldNames = fieldNames;
            _level1DynamicMessageHandler.SetDynamicFields(fieldNames);
        }

        public Task<FundamentalMessage> GetFundamentalSnapshotAsync(string symbol)
        {
            return _level1DynamicSnapshot.GetFundamentalSnapshotAsync(symbol.ToUpper());
        }

        public Task<IUpdateSummaryDynamicMessage> GetUpdateSummarySnapshotAsync(string symbol)
        {
            return _level1DynamicSnapshot.GetUpdateSummarySnapshotAsync(symbol.ToUpper());
        }

        public FundamentalMessage GetFundamentalSnapshot(string symbol)
        {
            return _level1DynamicSnapshot.GetFundamentalSnapshot(symbol);
        }

        public IUpdateSummaryDynamicMessage GetUpdateSummarySnapshot(string symbol)
        {
            return _level1DynamicSnapshot.GetUpdateSummarySnapshot(symbol);
        }

        private void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs e)
        {
            _level1DynamicMessageHandler.ProcessMessages(e.Message, e.Count);
        }

        private void SocketClientOnConnected(object sender, EventArgs eventArgs)
        {
            var socketClient = (SocketClient)sender;
            socketClient.Send(_level1RequestFormatter.SetProtocol(IQFeedDefault.ProtocolVersion));
            socketClient.Send(_level1RequestFormatter.SelectUpdateFieldName(_fieldNames));
            socketClient.Connected -= SocketClientOnConnected;
        }
    }
}