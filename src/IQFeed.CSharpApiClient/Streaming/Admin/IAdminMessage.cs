using System;
using IQFeed.CSharpApiClient.Streaming.Admin.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public interface IAdminMessage
    {
        event Action<StatsMessage> Stats;
        event Action<ClientStatsMessage> ClientStats;
    }
}