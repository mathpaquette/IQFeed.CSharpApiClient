using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class RegionalUpdateMessage
    {
        public RegionalUpdateMessage(string symbol, string exchange, float regionalBid, int regionalBidSize, TimeSpan regionalBidTime, float regionalAsk, int regionalAskSize, TimeSpan regionalAskTime, string fractionDisplayCode, string decimalPrecision, string marketCenter)
        {
            Symbol = symbol;
            Exchange = exchange;
            RegionalBid = regionalBid;
            RegionalBidSize = regionalBidSize;
            RegionalBidTime = regionalBidTime;
            RegionalAsk = regionalAsk;
            RegionalAskSize = regionalAskSize;
            RegionalAskTime = regionalAskTime;
            FractionDisplayCode = fractionDisplayCode;
            DecimalPrecision = decimalPrecision;
            MarketCenter = marketCenter;
        }

        public string Symbol { get; }
        public string Exchange { get; }
        public float RegionalBid { get; }
        public int RegionalBidSize { get; }
        public TimeSpan RegionalBidTime { get; }
        public float RegionalAsk { get; }
        public int RegionalAskSize { get; }
        public TimeSpan RegionalAskTime { get; }
        public string FractionDisplayCode { get; }
        public string DecimalPrecision { get; }
        public string MarketCenter { get; }

        public static RegionalUpdateMessage CreateRegionalUpdateMessage(string[] values)
        {
            var symbol = values[0];
            var exchange = values[1];
            float.TryParse(values[2], out var regionalBid);
            int.TryParse(values[3], out var regionalBidSize);
            TimeSpan.TryParse(values[4], out var regionalBidTime);
            float.TryParse(values[5], out var regionalAsk);
            int.TryParse(values[6], out var regionalAskSize);
            TimeSpan.TryParse(values[7], out var regionalAskTime);
            var fractionDisplayCode = values[8];
            var decimalPrecision = values[9];
            var marketCenter = values[10];

            return new RegionalUpdateMessage(
                symbol,
                exchange,
                regionalBid,
                regionalBidSize,
                regionalBidTime,
                regionalAsk,
                regionalAskSize,
                regionalAskTime,
                fractionDisplayCode,
                decimalPrecision,
                marketCenter
            );
        }
    }
}