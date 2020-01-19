using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public abstract class RegionalUpdateMessage
    {
        public const string RegionalUpdateTimeFormat = "HH:mm:ss";

        public static RegionalUpdateMessage<decimal> Parse(string message)
        {
            var values = message.SplitFeedMessage();

            var symbol = values[1];
            var exchange = values[2];
            decimal.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var regionalBid);
            int.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var regionalBidSize);
            DateTime.TryParseExact(values[5], RegionalUpdateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var regionalBidTime);
            decimal.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var regionalAsk);
            int.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var regionalAskSize);
            DateTime.TryParseExact(values[8], RegionalUpdateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var regionalAskTime);
            int.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var fractionDisplayCode);
            int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalPrecision);
            int.TryParse(values[11], NumberStyles.Any, CultureInfo.InvariantCulture, out var marketCenter);

            return new RegionalUpdateMessage<decimal>(
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

    public class RegionalUpdateMessage<T> : IRegionalUpdateMessage<T>
    {
        public RegionalUpdateMessage(
            string symbol, 
            string exchange, 
            T regionalBid, 
            int regionalBidSize, 
            DateTime regionalBidTime,
            T regionalAsk, 
            int regionalAskSize,
            DateTime regionalAskTime,
            int fractionDisplayCode, 
            int decimalPrecision, 
            int marketCenter)
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

        public string Symbol { get; private set; }
        public string Exchange { get; private set; }
        public T RegionalBid { get; private set; }
        public int RegionalBidSize { get; private set; }
        public DateTime RegionalBidTime { get; private set; }
        public T RegionalAsk { get; private set; }
        public int RegionalAskSize { get; private set; }
        public DateTime RegionalAskTime { get; private set; }
        public int FractionDisplayCode { get; private set; }
        public int DecimalPrecision { get; private set; }
        public int MarketCenter { get; private set; }

        

        public override bool Equals(object obj)
        {
            return obj is RegionalUpdateMessage<T> message &&
                   Symbol == message.Symbol &&
                   Exchange == message.Exchange &&
                   Equals(RegionalBid,message.RegionalBid) &&
                   RegionalBidSize == message.RegionalBidSize &&
                   RegionalBidTime.TimeOfDay == message.RegionalBidTime.TimeOfDay &&
                   Equals(RegionalAsk, message.RegionalAsk) &&
                   RegionalAskSize == message.RegionalAskSize &&
                   RegionalAskTime.TimeOfDay == message.RegionalAskTime.TimeOfDay &&
                   FractionDisplayCode == message.FractionDisplayCode &&
                   DecimalPrecision == message.DecimalPrecision &&
                   MarketCenter == message.MarketCenter;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + Exchange.GetHashCode();
                hash = hash * 29 + RegionalBid.GetHashCode();
                hash = hash * 29 + RegionalBidSize.GetHashCode();
                hash = hash * 29 + RegionalBidTime.GetHashCode();
                hash = hash * 29 + RegionalAsk.GetHashCode();
                hash = hash * 29 + RegionalAskSize.GetHashCode();
                hash = hash * 29 + RegionalAskTime.GetHashCode();
                hash = hash * 29 + FractionDisplayCode.GetHashCode();
                hash = hash * 29 + DecimalPrecision.GetHashCode();
                hash = hash * 29 + MarketCenter.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(Exchange)}: {Exchange}, {nameof(RegionalBid)}: {RegionalBid}, {nameof(RegionalBidSize)}: {RegionalBidSize}, {nameof(RegionalBidTime)}: {RegionalBidTime}, {nameof(RegionalAsk)}: {RegionalAsk}, {nameof(RegionalAskSize)}: {RegionalAskSize}, {nameof(RegionalAskTime)}: {RegionalAskTime}, {nameof(FractionDisplayCode)}: {FractionDisplayCode}, {nameof(DecimalPrecision)}: {DecimalPrecision}, {nameof(MarketCenter)}: {MarketCenter}";
        }
    }
}