using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public class Level2Client : ILevel2Client
    {
        public event Action<UpdateSummaryMessage> Summary
        {
            add => _level2MessageHandler.Summary += value;
            remove => _level2MessageHandler.Summary -= value;
        }
        public event Action<SymbolNotFoundMessage> SymbolNotFound
        {
            add => _level2MessageHandler.SymbolNotFound += value;
            remove => _level2MessageHandler.SymbolNotFound -= value;
        }
        public event Action<SymbolHasNoDepthAvailableMessage> SymbolHasNoDepthAvailable
        {
            add => _level2MessageHandler.SymbolHasNoDepthAvailable += value;
            remove => _level2MessageHandler.SymbolHasNoDepthAvailable -= value;
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
        public event Action<UpdateSummaryMessage> Update
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

        // protocol 6.2 events
        public event Action<OrderAddUpdateSummaryMessage> OrderAdd
        {
            add => _level2MessageHandler.OrderAdd += value;
            remove => _level2MessageHandler.OrderAdd -= value;
        }

        public event Action<OrderDeleteMessage> OrderDelete
        {
            add => _level2MessageHandler.OrderDelete += value;
            remove => _level2MessageHandler.OrderDelete -= value;
        }

        public event Action<OrderAddUpdateSummaryMessage> OrderSummary
        {
            add => _level2MessageHandler.OrderSummary += value;
            remove => _level2MessageHandler.OrderSummary -= value;
        }

        public event Action<OrderAddUpdateSummaryMessage> OrderUpdate
        {
            add => _level2MessageHandler.OrderUpdate += value;
            remove => _level2MessageHandler.OrderUpdate -= value;
        }

        public event Action<PriceLevelDeleteMessage> PriceLevelDelete
        {
            add => _level2MessageHandler.PriceLevelDelete += value;
            remove => _level2MessageHandler.PriceLevelDelete -= value;
        }

        public event Action<PriceLevelOrderMessage> PriceLevelOrder
        {
            add => _level2MessageHandler.PriceLevelOrder += value;
            remove => _level2MessageHandler.PriceLevelOrder -= value;
        }

        public event Action<PriceLevelUpdateSummaryMessage> PriceLevelSummary
        {
            add => _level2MessageHandler.PriceLevelSummary += value;
            remove => _level2MessageHandler.PriceLevelSummary -= value;
        }

        public event Action<PriceLevelUpdateSummaryMessage> PriceLevelUpdate
        {
            add => _level2MessageHandler.PriceLevelUpdate += value;
            remove => _level2MessageHandler.PriceLevelUpdate -= value;
        }

        private readonly SocketClient _socketClient;
        private readonly Level2RequestFormatter _level2RequestFormatter;
        private readonly ILevel2MessageHandler _level2MessageHandler;
        private readonly ILevel2Snapshot _level2Snapshot;

        private readonly string _protocolVersion;

        public string ProtocolVersion => _protocolVersion;

        /// <summary>
        /// Level 2 Client
        /// </summary>
        /// <param name="socketClient"></param>
        /// <param name="level2RequestFormatter"></param>
        /// <param name="level2MessageHandler"></param>
        /// <param name="level2Snapshot"></param>
        /// <param name="protocolVersion">Optional. Will default to current highest protocol available.
        /// Override ONLY if you need to access older data structures not supported by the current protocol.</param>
        public Level2Client(
            SocketClient socketClient, 
            Level2RequestFormatter level2RequestFormatter, 
            ILevel2MessageHandler level2MessageHandler, 
            ILevel2Snapshot level2Snapshot,
            string protocolVersion = IQFeedDefault.ProtocolVersion)
        {
            _level2Snapshot = level2Snapshot;
            _socketClient = socketClient;
            _socketClient.MessageReceived += SocketClientOnMessageReceived;
            _socketClient.Connected += SocketClientOnConnected;

            _level2RequestFormatter = level2RequestFormatter;
            _level2MessageHandler = level2MessageHandler;
            _protocolVersion = protocolVersion;
        }

        [Obsolete("ReqWatch is only supported in protocols 6.1 and below. For 6.2 please use ReqWatchMarketByPrice")]
        public void ReqWatch(string symbol)
        {
            if (GetProtocolVersionAsNumber() > 6.1M)
            {
                throw new Exception($"ReqWatch is only supported in protocols 6.1 and below. For 6.2 please use ReqWatchMarketByPrice");
            }
            
            var request = _level2RequestFormatter.ReqWatch(symbol);
            _socketClient.Send(request);
        }

        public void ReqWatchMarketByPrice(string symbol, int? maxPriceLevels = null)
        {
            if (GetProtocolVersionAsNumber() <= 6.1M)
            {
                throw new Exception($"ReqWatchMarketByPrice is only supported in protocols 6.2 and above.");
            }

            var request = _level2RequestFormatter.ReqWatchMarketByPrice(symbol, maxPriceLevels);
            _socketClient.Send(request);
        }

        public void ReqWatchMarketByOrder(string symbol)
        {
            if (GetProtocolVersionAsNumber() <= 6.1M)
            {
                throw new Exception($"ReqWatchMarketByOrder is only supported in protocols 6.2 and above.");
            }

            var request = _level2RequestFormatter.ReqWatchMarketByOrder(symbol);
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

        public Task<IEnumerable<UpdateSummaryMessage>> GetSummarySnapshotAsync(string symbol)
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

        private decimal GetProtocolVersionAsNumber()
        {
            return decimal.Parse(_protocolVersion);
        }

        private void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs e)
        {
            _level2MessageHandler.ProcessMessages(e.Message, e.Count);
        }

        private void SocketClientOnConnected(object sender, EventArgs eventArgs)
        {
            var socketClient = (SocketClient)sender;
            socketClient.Send(_level2RequestFormatter.SetProtocol(_protocolVersion));
            socketClient.Connected -= SocketClientOnConnected;
        }
    }
}