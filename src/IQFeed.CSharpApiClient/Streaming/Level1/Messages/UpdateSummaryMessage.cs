using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class UpdateSummaryMessage
    {
        public const string UpdateMessageTimeFormat = "HH:mm:ss.ffffff";

        // S,CURRENT UPDATE FIELDNAMES,Symbol,Most Recent Trade,Most Recent Trade Size,Most Recent Trade Time,Most Recent Trade Market Center,Total Volume,Bid,Bid Size,Ask,Ask Size,Open,High,Low,Close,Message Contents,Most Recent Trade Conditions
        public UpdateSummaryMessage(string symbol, float mostRecentTrade, int mostRecentTradeSize, DateTime mostRecentTradeTime, int mostRecentTradeMarketCenter, int totalVolume,
            float bid, int bidSize, float ask, int askSize, float open, float high, float low, float close, string messageContents, string mostRecentTradeConditions)
        {
            Symbol = symbol;
            MostRecentTrade = mostRecentTrade;
            MostRecentTradeSize = mostRecentTradeSize;
            MostRecentTradeTime = mostRecentTradeTime;
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
        public DateTime MostRecentTradeTime { get; }
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

        public static UpdateSummaryMessage CreateUpdateSummaryMessage(string[] values)
        {
            var symbol = values[0];                                             // field 2
            float.TryParse(values[1], out var mostRecentTrade);                 // field 71
            int.TryParse(values[2], out var mostRecentTradeSize);               // field 72
            DateTime.TryParse(values[3], out var mostRecentTradeTime);
            int.TryParse(values[4], out var mostRecentTradeMarketCenter);       // field 75
            int.TryParse(values[5], out var totalVolume);                       // field 7
            float.TryParse(values[6], out var bid);                             // field 11
            int.TryParse(values[7], out var bidSize);                           // field 13
            float.TryParse(values[8], out var ask);                             // field 12
            int.TryParse(values[9], out var askSize);                           // field 14
            float.TryParse(values[10], out var open);                           // field 20
            float.TryParse(values[11], out var high);                           // field 9
            float.TryParse(values[12], out var low);                            // field 10
            float.TryParse(values[13], out var close);                          // field 21
            var messageContents = values[14];                                   // field 80
            var mostRecentTradeConditions = values[15];                         // field 74

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
    }
}