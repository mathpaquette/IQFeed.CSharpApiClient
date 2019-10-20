using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class UpdateSummaryMessage
    {
        public const string UpdateMessageTimeFormat = "HH:mm:ss.ffffff";

        // S,CURRENT UPDATE FIELDNAMES,Symbol,Most Recent Trade,Most Recent Trade Size,Most Recent Trade Time,Most Recent Trade Market Center,Total Volume,Bid,Bid Size,Ask,Ask Size,Open,High,Low,Close,Message Contents,Most Recent Trade Conditions
        public UpdateSummaryMessage(
            string symbol,
            float mostRecentTrade,
            int mostRecentTradeSize,
            DateTime mostRecentTradeTime,
            int mostRecentTradeMarketCenter,
            int totalVolume,
            float bid,
            int bidSize,
            float ask,
            int askSize,
            float open,
            float high,
            float low,
            float close,
            string messageContents,
            string mostRecentTradeConditions)
        {
            Symbol = symbol;
            MostRecentTrade = mostRecentTrade;
            MostRecentTradeSize = mostRecentTradeSize;
            MostRecentTradeTime = mostRecentTradeTime.TimeOfDay;
            MostRecentTradeMarketCenter = mostRecentTradeMarketCenter;
            TotalVolume = totalVolume;
            Bid = bid;
            BidSize = bidSize;
            Ask = ask;
            AskSize = askSize;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            MessageContents = messageContents;
            MostRecentTradeConditions = mostRecentTradeConditions;
        }

        public string Symbol { get; }
        public float MostRecentTrade { get; }
        public int MostRecentTradeSize { get; }
        public TimeSpan MostRecentTradeTime { get; }
        public int MostRecentTradeMarketCenter { get; }
        public int TotalVolume { get; }
        public float Bid { get; }
        public int BidSize { get; }
        public float Ask { get; }
        public int AskSize { get; }
        public float Open { get; }
        public float High { get; }
        public float Low { get; }
        public float Close { get; }
        public string MessageContents { get; }
        public string MostRecentTradeConditions { get; }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(MostRecentTrade)}: {MostRecentTrade}, {nameof(MostRecentTradeSize)}: {MostRecentTradeSize}, {nameof(MostRecentTradeTime)}: {MostRecentTradeTime}, {nameof(MostRecentTradeMarketCenter)}: {MostRecentTradeMarketCenter}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(Bid)}: {Bid}, {nameof(BidSize)}: {BidSize}, {nameof(Ask)}: {Ask}, {nameof(AskSize)}: {AskSize}, {nameof(Open)}: {Open}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Close)}: {Close}, {nameof(MessageContents)}: {MessageContents}, {nameof(MostRecentTradeConditions)}: {MostRecentTradeConditions}";
        }

        public static UpdateSummaryMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];                                                                                                                         // field 2
            float.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTrade);                                             // field 71
            int.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeSize);                                           // field 72
            DateTime.TryParseExact(values[4], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var mostRecentTradeTime);
            int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeMarketCenter);                                   // field 75
            int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var totalVolume);                                                   // field 7
            float.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var bid);                                                         // field 11
            int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var bidSize);                                                       // field 13
            float.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var ask);                                                         // field 12
            int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var askSize);                                                       // field 14
            float.TryParse(values[11], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);                                                       // field 20
            float.TryParse(values[12], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);                                                       // field 9
            float.TryParse(values[13], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);                                                        // field 10
            float.TryParse(values[14], NumberStyles.Any, CultureInfo.InvariantCulture, out var close);                                                      // field 21
            var messageContents = values[15];                                                                                                               // field 80
            var mostRecentTradeConditions = values[16];                                                                                                     // field 74

            return new UpdateSummaryMessage(
                symbol,
                mostRecentTrade,
                mostRecentTradeSize,
                mostRecentTradeTime,
                mostRecentTradeMarketCenter,
                totalVolume,
                bid,
                bidSize,
                ask,
                askSize,
                open,
                high,
                low,
                close,
                messageContents,
                mostRecentTradeConditions
            );
        }

        public override bool Equals(object obj)
        {
            return obj is UpdateSummaryMessage message &&
                   Symbol == message.Symbol &&
                   MostRecentTrade == message.MostRecentTrade &&
                   MostRecentTradeSize == message.MostRecentTradeSize &&
                   MostRecentTradeTime == message.MostRecentTradeTime &&
                   MostRecentTradeMarketCenter == message.MostRecentTradeMarketCenter &&
                   TotalVolume == message.TotalVolume &&
                   Bid == message.Bid &&
                   BidSize == message.BidSize &&
                   Ask == message.Ask &&
                   AskSize == message.AskSize &&
                   Open == message.Open &&
                   High == message.High &&
                   Low == message.Low &&
                   Close == message.Close &&
                   MessageContents == message.MessageContents &&
                   MostRecentTradeConditions == message.MostRecentTradeConditions;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + MostRecentTrade.GetHashCode();
                hash = hash * 29 + MostRecentTradeSize.GetHashCode();
                hash = hash * 29 + MostRecentTradeTime.GetHashCode();
                hash = hash * 29 + MostRecentTradeMarketCenter.GetHashCode();
                hash = hash * 29 + TotalVolume.GetHashCode();
                hash = hash * 29 + Bid.GetHashCode();
                hash = hash * 29 + BidSize.GetHashCode();
                hash = hash * 29 + Ask.GetHashCode();
                hash = hash * 29 + AskSize.GetHashCode();
                hash = hash * 29 + Open.GetHashCode();
                hash = hash * 29 + High.GetHashCode();
                hash = hash * 29 + Low.GetHashCode();
                hash = hash * 29 + Close.GetHashCode();
                hash = hash * 29 + MessageContents.GetHashCode();
                hash = hash * 29 + MostRecentTradeConditions.GetHashCode();
                return hash;
            }
        }
    }
}