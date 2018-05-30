using System;
using System.Text;
using IQFeed.CSharpApiClient.Streaming.Admin.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public class AdminMessageHandler : IAdminMessage
    {
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
                        break;
                    case "REGISTER CLIENT APP COMPLETED":
                        break;
                    case "REMOVE CLIENT APP COMPLETED":
                        break;
                    case "CURRENT LOGINID":
                        break;
                    case "CURRENT PASSWORD":
                        break;
                    case "LOGIN INFO SAVED":
                        break;
                    case "LOGIN INFO NOT SAVED":
                        break;
                    case "AUTOCONNECT ON":
                        break;
                    case "AUTOCONNECT OFF":
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

        private void ProcessStatsMessage(string msg, string[] values)
        {
            var statsMessage = StatsMessage.CreateStatsMessage(values);
            Stats?.Invoke(statsMessage);
        }

        private void ProcessClientStatsMessage(string msg, string[] values)
        {
            var clientStats = new ClientStatsMessage();
            ClientStats?.Invoke(clientStats);
        }
    }
}