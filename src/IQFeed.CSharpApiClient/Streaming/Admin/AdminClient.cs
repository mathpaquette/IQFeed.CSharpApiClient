using System;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Admin.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    // TODO: finish the implementation of AdminClient
    public class AdminClient : IAdminClient
    {
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

        public void SetProtocol()
        {
            throw new NotImplementedException();
        }

        public void SetClientName(string name)
        {
            throw new NotImplementedException();
        }

        public void RegisterClientApp(string appName)
        {
            throw new NotImplementedException();
        }

        public void RemoveClientApp(string appName)
        {
            throw new NotImplementedException();
        }

        public void SetLoginId(string login)
        {
            throw new NotImplementedException();
        }

        public void SetPassword(string password)
        {
            throw new NotImplementedException();
        }

        public void SetSaveLoginInfo()
        {
            throw new NotImplementedException();
        }

        public void SetAutoconnect()
        {
            throw new NotImplementedException();
        }

        public void SetClientStatusOn()
        {
            throw new NotImplementedException();
        }

        public void SetClientStatusOff()
        {
            throw new NotImplementedException();
        }

        public void ReqServerConnect()
        {
            throw new NotImplementedException();
        }

        public void ReqServerDisconnect()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            _socketClient.Connect();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
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