using System;
using System.Text;
using IQFeed.CSharpApiClient.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Admin.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public class AdminMessageHandler : IAdminMessage
    {
        public event Action<ProtocolMessage> Protocol;
        public event Action<ClientAppMessage> ClientApp;
        public event Action<LoginIdMessage> LoginId;
        public event Action<PasswordMessage> Password;
        public event Action<LoginInfoMessage> LoginInfo;
        public event Action<AutoConnectMessage> AutoConnect;
        public event Action<StatsMessage> Stats;
        public event Action<ClientStatsMessage> ClientStats;

        public void ProcessMessages(byte[] messageBytes, int count)
        {
            var messages = Encoding.ASCII.GetString(messageBytes, 0, count).Split(IQFeedDefault.ProtocolLineFeedCharacter);

            for (var i = 0; i < messages.Length; i++)
            {
                if (messages[i].Length == 0)
                    continue;

                if (messages[i][0] != 'S')
                    throw new NotSupportedException(); // Admin should only receive S messages

                var values = messages[i].Substring(2).Split(IQFeedDefault.ProtocolDelimiterCharacter);
                switch (values[0])
                {
                    case "CURRENT PROTOCOL":
                        ProcessProtocolMessage(messages[i], values);
                        break;
                    case "REGISTER CLIENT APP COMPLETED":
                        ProcessClientAppMessage(ClientAppMessageType.Register);
                        break;
                    case "REMOVE CLIENT APP COMPLETED":
                        ProcessClientAppMessage(ClientAppMessageType.Remove);
                        break;
                    case "CURRENT LOGINID":
                        ProcessLoginIdMessage(messages[i], values);
                        break;
                    case "CURRENT PASSWORD":
                        ProcessPasswordMessage(messages[i], values);
                        break;
                    case "LOGIN INFO SAVED":
                        ProcessLoginInfoMessage(LoginInfoMessageType.Saved);
                        break;
                    case "LOGIN INFO NOT SAVED":
                        ProcessLoginInfoMessage(LoginInfoMessageType.NotSaved);
                        break;
                    case "AUTOCONNECT ON":
                        ProcessAutoConnectMessage(AutoConnectMessageType.On);
                        break;
                    case "AUTOCONNECT OFF":
                        ProcessAutoConnectMessage(AutoConnectMessageType.Off);
                        break;
                    case "STATS":
                        ProcessStatsMessage(messages[i], values);
                        break;
                    case "CLIENTSTATS":
                        ProcessClientStatsMessage(messages[i], values);
                        break;
                }
            }
        }

        private void ProcessProtocolMessage(string msg, string[] values)
        {
            Protocol?.Invoke(new ProtocolMessage(values[1]));
        }

        private void ProcessClientAppMessage(ClientAppMessageType clientAppMessageType)
        {
            ClientApp?.Invoke(new ClientAppMessage(clientAppMessageType));
        }

        private void ProcessLoginIdMessage(string msg, string[] values)
        {
            LoginId?.Invoke(new LoginIdMessage(values[1]));
        }

        private void ProcessPasswordMessage(string msg, string[] values)
        {
           Password?.Invoke(new PasswordMessage(values[1]));
        }

        private void ProcessLoginInfoMessage(LoginInfoMessageType loginInfoMessageType)
        {
            LoginInfo?.Invoke(new LoginInfoMessage(loginInfoMessageType));
        }

        private void ProcessAutoConnectMessage(AutoConnectMessageType autoConnectMessageType)
        {
            AutoConnect?.Invoke(new AutoConnectMessage(autoConnectMessageType));
        }

        private void ProcessStatsMessage(string msg, string[] values)
        {
            var statsMessage = StatsMessage.CreateStatsMessage(values);
            Stats?.Invoke(statsMessage);
        }

        private void ProcessClientStatsMessage(string msg, string[] values)
        {
            var clientStats = ClientStatsMessage.CreateClientStatsMessage(values);
            ClientStats?.Invoke(clientStats);
        }
    }
}