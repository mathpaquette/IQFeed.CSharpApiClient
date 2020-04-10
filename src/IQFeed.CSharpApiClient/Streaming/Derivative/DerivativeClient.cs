using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Derivative.Handlers;
using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public class DerivativeClient<T> : IDerivativeClient<T>
    {
        public event Action<ErrorMessage> Error
        {
            add => _derivativeMessageHandler.Error += value;
            remove => _derivativeMessageHandler.Error -= value;
        }
        public event Action<IntervalBarMessage<T>> IntervalBar
        {
            add => _derivativeMessageHandler.IntervalBar += value;
            remove => _derivativeMessageHandler.IntervalBar -= value;
        }
        public event Action<SymbolNotFoundMessage> SymbolNotFound
        {
            add => _derivativeMessageHandler.SymbolNotFound += value;
            remove => _derivativeMessageHandler.SymbolNotFound -= value;
        }
        public event Action<SystemMessage> System
        {
            add => _derivativeMessageHandler.System += value;
            remove => _derivativeMessageHandler.System -= value;
        }

        private readonly SocketClient _socketClient;
        private readonly DerivativeRequestFormatter _derivativeRequestFormatter;
        private readonly IDerivativeMessageHandler<T> _derivativeMessageHandler;

        public DerivativeClient(
            SocketClient socketClient,
            DerivativeRequestFormatter derivativeRequestFormatter,
            IDerivativeMessageHandler<T> derivativeMessageHandler)
        {
            _socketClient = socketClient;
            _derivativeRequestFormatter = derivativeRequestFormatter;
            _derivativeMessageHandler = derivativeMessageHandler;

            _socketClient.MessageReceived += SocketClientOnMessageReceived;
            _socketClient.Connected += SocketClientOnConnected;
        }

        private void SocketClientOnConnected(object sender, EventArgs e)
        {
            var socketClient = (SocketClient)sender;
            socketClient.Send(_derivativeRequestFormatter.SetProtocol(IQFeedDefault.ProtocolVersion));
            socketClient.Connected -= SocketClientOnConnected;
        }

        private void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs e)
        {
            _derivativeMessageHandler.ProcessMessages(e.Message, e.Count);
        }

        public void Connect()
        {
            _socketClient.Connect();
        }

        public async Task ConnectAsync()
        {
            await _socketClient.ConnectAsync();
        }

        public void Disconnect()
        {
            _socketClient.Disconnect();
        }

        public async Task DisconnectAsync()
        {
            await _socketClient.DisconnectAsync();
        }

        public void SetClientName(string name)
        {
            var request = _derivativeRequestFormatter.SetClientName(name);
            _socketClient.Send(request);
        }

        public void ReqBarWatch(string symbol, int interval, DateTime? beginDate = null, int? maxDaysOfDatapoints = null, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, string requestId = null, DerivativeIntervalType? intervalType = null, int? updateInterval = null)
        {
            var request = _derivativeRequestFormatter.ReqBarWatch(symbol, interval, beginDate, maxDaysOfDatapoints, maxDatapoints, beginFilterTime, endFilterTime, requestId, intervalType, updateInterval);
            _socketClient.Send(request);
        }

        public async Task ReqBarWatchAsync(string symbol, int interval, DateTime? beginDate = null, int? maxDaysOfDatapoints = null, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, string requestId = null, DerivativeIntervalType? intervalType = null, int? updateInterval = null)
        {
            var request = _derivativeRequestFormatter.ReqBarWatch(symbol, interval, beginDate, maxDaysOfDatapoints, maxDatapoints, beginFilterTime, endFilterTime, requestId, intervalType, updateInterval);
            await _socketClient.SendAsync(request);
        }

        public void ReqBarUnwatch(string symbol, string requestId)
        {
            var request = _derivativeRequestFormatter.ReqBarUnwatch(symbol, requestId);
            _socketClient.Send(request);
        }

        public void ReqWatches() // TODO: should add async method also.
        {
            var request = _derivativeRequestFormatter.ReqWatches();
            _socketClient.Send(request);
        }

        public void UnwatchAll()
        {
            var request = _derivativeRequestFormatter.UnwatchAll();
            _socketClient.Send(request);
        }

        public async Task UnwatchAllAsync()
        {
            var request = _derivativeRequestFormatter.UnwatchAll();
            await _socketClient.SendAsync(request);
        }
    }
}