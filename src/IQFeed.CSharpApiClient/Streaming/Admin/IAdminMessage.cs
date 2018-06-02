using System;
using IQFeed.CSharpApiClient.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Admin.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public interface IAdminMessage
    {
        event Action<ProtocolMessage> Protocol;
        event Action<ClientAppMessage> ClientApp;
        event Action<LoginIdMessage> LoginId;
        event Action<PasswordMessage> Password;
        event Action<LoginInfoMessage> LoginInfo;
        event Action<AutoConnectMessage> AutoConnect;
        event Action<StatsMessage> Stats;
        event Action<ClientStatsMessage> ClientStats;
    }
}