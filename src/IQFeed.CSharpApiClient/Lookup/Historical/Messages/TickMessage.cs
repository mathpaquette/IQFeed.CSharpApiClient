using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class TickMessage
    {
        public TickMessage(DateTime timestamp, float last, int lastSize, int totalVolume, float bid, float ask, 
            int tickId, char basisForLast, int tradeMarketCenter, string tradeConditions)
        {
            Timestamp = timestamp;
            Last = last;
            LastSize = lastSize;
            TotalVolume = totalVolume;
            Bid = bid;
            Ask = ask;
            TickId = tickId;
            BasisForLast = basisForLast;
            TradeMarketCenter = tradeMarketCenter;
            TradeConditions = tradeConditions;
        }

        public DateTime Timestamp { get; }
        public float Last { get; }
        public int LastSize { get; }
        public int TotalVolume { get; }
        public float Bid { get; }
        public float Ask { get; }
        public int TickId { get; }
        public char BasisForLast { get; }
        public int TradeMarketCenter { get; }
        public string TradeConditions { get; }
    }
}