using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class TickMessage
    {
        public TickMessage(DateTime timestamp, float last, int lastSize, int totalVolume, float bid, float ask, 
            long tickId, char basisForLast, int tradeMarketCenter, string tradeConditions)
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
        public long TickId { get; }
        public char BasisForLast { get; }
        public int TradeMarketCenter { get; }
        public string TradeConditions { get; }

        public static TickMessage CreateTickMessage(string[] values)
        {
            return new TickMessage(
                DateTime.Parse(values[0]),
                float.Parse(values[1]),
                int.Parse(values[2]),
                int.Parse(values[3]),
                float.Parse(values[4]),
                float.Parse(values[5]),
                long.Parse(values[6]),
                char.Parse(values[7]),
                int.Parse(values[8]),
                values[9]);
        }

        public override bool Equals(object obj)
        {
            return obj is TickMessage message &&
                   Timestamp == message.Timestamp &&
                   Last == message.Last &&
                   LastSize == message.LastSize &&
                   TotalVolume == message.TotalVolume &&
                   Bid == message.Bid &&
                   Ask == message.Ask &&
                   TickId == message.TickId &&
                   BasisForLast == message.BasisForLast &&
                   TradeMarketCenter == message.TradeMarketCenter &&
                   TradeConditions == message.TradeConditions;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Timestamp.GetHashCode();
                hash = hash * 29 + TickId.GetHashCode();
                return hash;
            }
        }
    }
}