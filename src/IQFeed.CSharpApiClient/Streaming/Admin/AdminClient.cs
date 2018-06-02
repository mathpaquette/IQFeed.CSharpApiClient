using System;
using IQFeed.CSharpApiClient.Common.Messages;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Admin.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public class AdminClient : IAdminClient
    {
        public event Action<ProtocolMessage> Protocol
        {
            add => _adminMessageHandler.Protocol += value;
            remove => _adminMessageHandler.Protocol -= value;
        }
        public event Action<ClientAppMessage> ClientApp
        {
            add => _adminMessageHandler.ClientApp += value;
            remove => _adminMessageHandler.ClientApp -= value;
        }
        public event Action<LoginIdMessage> LoginId
        {
            add => _adminMessageHandler.LoginId += value;
            remove => _adminMessageHandler.LoginId -= value;
        }
        public event Action<PasswordMessage> Password
        {
            add => _adminMessageHandler.Password += value;
            remove => _adminMessageHandler.Password -= value;
        }
        public event Action<LoginInfoMessage> LoginInfo
        {
            add => _adminMessageHandler.LoginInfo += value;
            remove => _adminMessageHandler.LoginInfo -= value;
        }
        public event Action<AutoConnectMessage> AutoConnect
        {
            add => _adminMessageHandler.AutoConnect += value;
            remove => _adminMessageHandler.AutoConnect -= value;
        }
        public event Action<StatsMessage> Stats
        {
            add => _adminMessageHandler.Stats += value;
            remove => _adminMessageHandler.Stats -= value;
        }
        public event Action<ClientStatsMessage> ClientStats
        {
            add => _adminMessageHandler.ClientStats += value;
            remove => _adminMessageHandler.ClientStats -= value;
        }

        private readonly SocketClient _socketClient;
        private readonly AdminRequestFormatter _adminRequestFormatter;
        private readonly AdminMessageHandler _adminMessageHandler;

        public AdminClient(SocketClient socketClient, AdminRequestFormatter adminRequestFormatter, AdminMessageHandler adminMessageHandler)
        {
            _socketClient = socketClient;
            _socketClient.MessageReceived += SocketClientOnMessageReceived;
            _socketClient.Connected += SocketClientOnConnected;

            _adminRequestFormatter = adminRequestFormatter;
            _adminMessageHandler = adminMessageHandler;
        }

        public void SetProtocol(string version)
        {
            var request = _adminRequestFormatter.SetProtocol(version);
            _socketClient.Send(request);
        }

        public void SetClientName(string name)
        {
            var request = _adminRequestFormatter.SetClientName(name);
            _socketClient.Send(request);
        }

        public void RegisterClientApp(string productId, string productVersion)
        {
            var request = _adminRequestFormatter.RegisterClientApp(productId, productVersion);
            _socketClient.Send(request);
        }

        public void RemoveClientApp(string productId, string productVersion)
        {
            var request = _adminRequestFormatter.RemoveClientApp(productId, productVersion);
            _socketClient.Send(request);
        }

        public void SetLoginId(string userLoginId)
        {
            var request = _adminRequestFormatter.SetLoginId(userLoginId);
            _socketClient.Send(request);
        }

        public void SetPassword(string userPassword)
        {
            var request = _adminRequestFormatter.SetPassword(userPassword);
            _socketClient.Send(request);
        }

        public void SetSaveLoginInfo(bool on = true)
        {
            var request = _adminRequestFormatter.SetSaveLoginInfo(on);
            _socketClient.Send(request);
        }

        public void SetAutoconnect(bool on = true)
        {
            var request = _adminRequestFormatter.SetAutoconnect(on);
            _socketClient.Send(request);
        }

        public void ReqServerConnect()
        {
            var request = _adminRequestFormatter.ReqServerConnect();
            _socketClient.Send(request);
        }

        public void ReqServerDisconnect()
        {
            var request = _adminRequestFormatter.ReqServerDisconnect();
            _socketClient.Send(request);
        }

        public void SetClientStats(bool on = true)
        {
            var request = _adminRequestFormatter.SetClientStats(on);
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

        private void SocketClientOnConnected(object sender, EventArgs e)
        {
            var socketClient = (SocketClient)sender;
            socketClient.Send(_adminRequestFormatter.SetProtocol(IQFeedDefault.ProtocolVersion));
            socketClient.Connected -= SocketClientOnConnected;
        }

        private void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs e)
        {
            _adminMessageHandler.ProcessMessages(e.Message, e.Count);
        }
    }
}