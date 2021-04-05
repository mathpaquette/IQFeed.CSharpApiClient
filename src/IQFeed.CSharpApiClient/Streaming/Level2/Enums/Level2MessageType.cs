using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Enums
{
    public enum Level2MessageType
    {
        PriceLevelOrder = 0,
        OrderAdd = 3,
        OrderLevelUpdate = 4,
        OrderDelete = 5,
        OrderLevelSummary = 6,
        PriceLevelSummary = 7,
        PriceLevelUpdate = 8,
        PriceLevelDelete = 9
    }
}
