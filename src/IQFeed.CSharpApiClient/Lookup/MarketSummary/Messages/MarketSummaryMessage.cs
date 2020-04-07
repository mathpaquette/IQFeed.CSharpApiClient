using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary.Messages
{
    /// <summary>
    /// Until Market Summaries are out of beta and fixed, the field names/orders/count could change, 
    /// so we need to handle them dynamically, which is slightly slower
    /// </summary>
    public class MarketSummaryMessage<T> where T : struct
    {
        public const string SourceDateFormat = "yyyyMMdd";
        public const string SourceTimeFormat = "HHmmss";

        public MarketSummaryMessage(IDictionary<string, object> fields, string requestId = null)
        {
            RequestId = requestId;
            Fields = fields;
        }

        public MarketSummaryMessage(IDictionary<string, object> fields, string symbol, int exchangeId, int type, T? last, int? tradeSize,
            int? tradedMarket, DateTime? tradeDate, TimeSpan? tradeTime, T? open, T? high, T? low, T? close,
            T? bid, int? bidMarket, int? bidSize, T? ask, int? askMarket, int? askSize, int? volume, int? pDayVolume,
            int? upVolume, int? downVolume, int? neutralVolume, int? tradeCount, int? upTrades, int? downTrades, 
            int? neutralTrades, T? vwap, T? mutualDiv, T? sevenDayYield, int? openInterest, 
            T? settlement, DateTime? settlementDate, DateTime? expirationDate, T? strike,
            string requestId = null)
        {
            RequestId = requestId;
            Symbol = symbol;
            ExchangeId = exchangeId;
            Type = type;
            Last = last;
            TradeSize = tradeSize;
            TradedMarket = tradedMarket;
            TradeDate = tradeDate;
            TradeTime = tradeTime;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Bid = bid;
            BidMarket = bidMarket;
            BidSize = bidSize;
            Ask = ask;
            AskMarket = askMarket;
            AskSize = askSize;
            Volume = volume;
            PDayVolume = pDayVolume;
            UpVolume = upVolume;
            DownVolume = downVolume;
            NeutralVolume = neutralVolume;
            TradeCount = tradeCount;
            UpTrades = upTrades;
            DownTrades = downTrades;
            NeutralTrades = neutralTrades;
            VWAP = vwap;
            MutualDiv = mutualDiv;
            SevenDayYield = sevenDayYield;
            OpenInterest = openInterest;
            Settlement = settlement;
            SettlementDate = settlementDate;
            ExpirationDate = expirationDate;
            Strike = strike;
        }

        public string RequestId { get; private set; }
        public string Symbol { get; private set; }
        public int ExchangeId { get; private set; }
        public int Type { get; private set; }
        public T? Last { get; private set; }
        public int? TradeSize { get; private set; }
        public int? TradedMarket { get; private set; }
        public DateTime? TradeDate { get; private set; }
        public TimeSpan? TradeTime { get; private set; }
        public T? Open { get; private set; }
        public T? High { get; private set; }
        public T? Low { get; private set; }
        public T? Close { get; private set; }
        public T? Bid { get; private set; }
        public int?  BidMarket { get; private set; }
        public int? BidSize { get; private set; }
        public T? Ask { get; private set; }
        public int? AskMarket { get; private set; }
        public int? AskSize { get; private set; }
        public int? Volume { get; private set; }
        public int? PDayVolume { get; private set; }
        public int? UpVolume { get; private set; }
        public int? DownVolume { get; private set; }
        public int? NeutralVolume { get; private set; }
        public int? TradeCount { get; private set; }
        public int? UpTrades { get; private set; }
        public int? DownTrades { get; private set; }
        public int? NeutralTrades { get; private set; }
        public T? VWAP { get; private set; }
        public T? MutualDiv { get; private set; }
        public T? SevenDayYield { get; private set; }
        public int? OpenInterest { get; private set; }
        public T? Settlement { get; private set; }
        public DateTime? SettlementDate { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public T? Strike { get; private set; }

        public IDictionary<string, object> Fields { get; private set; }


        public SecurityType SecurityType => (SecurityType)Type;
        public DateTime? TradeDateTime => (TradeDate != null && TradeTime != null) ? TradeDate.Value.Add(TradeTime.Value) : (DateTime?)null;

        public static MarketSummaryMessage<T> Parse(string message, MarketSummaryHandler<T> marketSummaryHandler)
        {
            if (marketSummaryHandler.FieldNames.Count == 0)
            {
                // First time through will be fieldnames
                marketSummaryHandler.FieldNames.AddRange(message.SplitFeedMessage());
                return null;
            }

            var index = 0;
            var values = message.SplitFeedMessage();
            var fields = ParseFields(marketSummaryHandler, values, index);
            return new MarketSummaryMessage<T>(
                fields,
                (string)fields["Symbol"],
                (int)fields["Exchange"],
                (int)fields["Type"],
                (T?)fields["Last"],
                (int?)fields["TradeSize"],
                (int?)fields["TradedMarket"],
                (DateTime?)fields["TradeDate"],
                (TimeSpan?)fields["TradeTime"],
                (T?)fields["Open"],
                (T?)fields["High"],
                (T?)fields["Low"],
                (T?)fields["Close"],
                (T?)fields["Bid"],
                (int?)fields["BidMarket"],
                (int?)fields["BidSize"],
                (T?)fields["Ask"],
                (int?)fields["AskMarket"],
                (int?)fields["AskSize"],
                (int?)fields["Volume"],
                (int?)fields["PDayVolume"],
                (int?)fields["UpVolume"],
                (int?)fields["DownVolume"],
                (int?)fields["NeutralVolume"],
                (int?)fields["TradeCount"],
                (int?)fields["UpTrades"],
                (int?)fields["DownTrades"],
                (int?)fields["NeutralTrades"],
                (T?)fields["VWAP"],
                (T?)fields["MutualDiv"],
                (T?)fields["SevenDayYield"],
                (int?)fields["OpenInterest"],
                (T?)fields["Settlement"],
                (DateTime?)fields["SettlementDate"],
                (DateTime?)fields["ExpirationDate"],
                (T?)fields["Strike"]
                );
        }

        public static MarketSummaryMessage<T> ParseWithRequestId(string message, MarketSummaryHandler<T> marketSummaryHandler)
        {
            if (marketSummaryHandler.FieldNames.Count == 0)
            {
                // First time through will be fieldnames
                marketSummaryHandler.FieldNames.AddRange(message.SplitFeedMessage());
                return null;
            }

            var index = 0;
            var values = message.SplitFeedMessage();
            var requestId = values[index++];
            var fields = ParseFields(marketSummaryHandler, values, index);

            return new MarketSummaryMessage<T>(
                fields,
                (string)fields["Symbol"],
                (int)fields["Exchange"],
                (int)fields["Type"],
                (T?)fields["Last"],
                (int?)fields["TradeSize"],
                (int?)fields["TradedMarket"],
                (DateTime?)fields["TradeDate"],
                (TimeSpan?)fields["TradeTime"],
                (T?)fields["Open"],
                (T?)fields["High"],
                (T?)fields["Low"],
                (T?)fields["Close"],
                (T?)fields["Bid"],
                (int?)fields["BidMarket"],
                (int?)fields["BidSize"],
                (T?)fields["Ask"],
                (int?)fields["AskMarket"],
                (int?)fields["AskSize"],
                (int?)fields["Volume"],
                (int?)fields["PDayVolume"],
                (int?)fields["UpVolume"],
                (int?)fields["DownVolume"],
                (int?)fields["NeutralVolume"],
                (int?)fields["TradeCount"],
                (int?)fields["UpTrades"],
                (int?)fields["DownTrades"],
                (int?)fields["NeutralTrades"],
                (T?)fields["VWAP"],
                (T?)fields["MutureDiv"],
                (T?)fields["SevenDayYield"],
                (int?)fields["OpenInterest"],
                (T?)fields["Settlement"],
                (DateTime?)fields["SettlementDate"],
                (DateTime?)fields["ExpirationDate"],
                (T?)fields["Strike"],
                requestId
                );
        }

        public static IDictionary<string, Func<string, object>> GetFieldsetDescriptorsDouble()
        {
            return new Dictionary<string, Func<string, object>>()
            {
                { "RequestId", (value) => value },
                { "Date", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat).Value },
                { "Symbol", (value) => value },
                { "Exchange", (value) => StringExtensions.ToNullableInt(value).Value },
                { "Type", (value) => StringExtensions.ToNullableInt(value).Value },
                { "Last", (value) => StringExtensions.ToNullableDouble(value) },
                { "TradeSize", (value) => StringExtensions.ToNullableInt(value) },
                { "TradedMarket", (value) => StringExtensions.ToNullableInt(value) },
                { "TradeDate", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat) },
                { "TradeTime", (value) => StringExtensions.ToNullableTimeSpan(value, SourceTimeFormat) },
                { "Open", (value) => StringExtensions.ToNullableDouble(value) },
                { "High", (value) => StringExtensions.ToNullableDouble(value) },
                { "Low", (value) => StringExtensions.ToNullableDouble(value) },
                { "Close", (value) => StringExtensions.ToNullableDouble(value) },
                { "Bid", (value) => StringExtensions.ToNullableDouble(value) },
                { "BidMarket", (value) => StringExtensions.ToNullableInt(value) },
                { "BidSize", (value) => StringExtensions.ToNullableInt(value) },
                { "Ask", (value) => StringExtensions.ToNullableDouble(value) },
                { "AskMarket", (value) => StringExtensions.ToNullableInt(value) },
                { "AskSize", (value) => StringExtensions.ToNullableInt(value) },
                { "Volume", (value) => StringExtensions.ToNullableInt(value) },
                { "PDayVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "UpVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "DownVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "NeutralVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "TradeCount", (value) => StringExtensions.ToNullableInt(value) },
                { "UpTrades", (value) => StringExtensions.ToNullableInt(value) },
                { "DownTrades", (value) => StringExtensions.ToNullableInt(value) },
                { "NeutralTrades", (value) => StringExtensions.ToNullableInt(value) },
                { "VWAP", (value) => StringExtensions.ToNullableDouble(value) },
                { "MutualDiv", (value) => StringExtensions.ToNullableDouble(value) },
                { "SevenDayYield", (value) => StringExtensions.ToNullableDouble(value) },
                { "OpenInterest", (value) => StringExtensions.ToNullableInt(value) },
                { "Settlement", (value) => StringExtensions.ToNullableDouble(value) },
                { "SettlementDate", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat) },
                { "ExpirationDate", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat) },
                { "Strike", (value) => StringExtensions.ToNullableDouble(value) }
            };
        }

        public static IDictionary<string, Func<string, object>> GetFieldsetDescriptorsDecimal()
        {
            return new Dictionary<string, Func<string, object>>()
            {
                { "RequestId", (value) => value },
                { "Date", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat).Value },
                { "Symbol", (value) => value },
                { "Exchange", (value) => StringExtensions.ToNullableInt(value).Value },
                { "Type", (value) => StringExtensions.ToNullableInt(value).Value },
                { "Last", (value) => StringExtensions.ToNullableDecimal(value) },
                { "TradeSize", (value) => StringExtensions.ToNullableInt(value) },
                { "TradedMarket", (value) => StringExtensions.ToNullableInt(value) },
                { "TradeDate", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat) },
                { "TradeTime", (value) => StringExtensions.ToNullableTimeSpan(value, SourceTimeFormat) },
                { "Open", (value) => StringExtensions.ToNullableDecimal(value) },
                { "High", (value) => StringExtensions.ToNullableDecimal(value) },
                { "Low", (value) => StringExtensions.ToNullableDecimal(value) },
                { "Close", (value) => StringExtensions.ToNullableDecimal(value) },
                { "Bid", (value) => StringExtensions.ToNullableDecimal(value) },
                { "BidMarket", (value) => StringExtensions.ToNullableInt(value) },
                { "BidSize", (value) => StringExtensions.ToNullableInt(value) },
                { "Ask", (value) => StringExtensions.ToNullableDecimal(value) },
                { "AskMarket", (value) => StringExtensions.ToNullableInt(value) },
                { "AskSize", (value) => StringExtensions.ToNullableInt(value) },
                { "Volume", (value) => StringExtensions.ToNullableInt(value) },
                { "PDayVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "UpVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "DownVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "NeutralVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "TradeCount", (value) => StringExtensions.ToNullableInt(value) },
                { "UpTrades", (value) => StringExtensions.ToNullableInt(value) },
                { "DownTrades", (value) => StringExtensions.ToNullableInt(value) },
                { "NeutralTrades", (value) => StringExtensions.ToNullableInt(value) },
                { "VWAP", (value) => StringExtensions.ToNullableDecimal(value) },
                { "MutualDiv", (value) => StringExtensions.ToNullableDecimal(value) },
                { "SevenDayYield", (value) => StringExtensions.ToNullableDecimal(value) },
                { "OpenInterest", (value) => StringExtensions.ToNullableInt(value) },
                { "Settlement", (value) => StringExtensions.ToNullableDecimal(value) },
                { "SettlementDate", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat) },
                { "ExpirationDate", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat) },
                { "Strike", (value) => StringExtensions.ToNullableDecimal(value) }
            };
        }

        public static IDictionary<string, Func<string, object>> GetFieldsetDescriptorsFloat()
        {
            return new Dictionary<string, Func<string, object>>()
            {
                { "RequestId", (value) => value },
                { "Date", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat).Value },
                { "Symbol", (value) => value },
                { "Exchange", (value) => StringExtensions.ToNullableInt(value).Value },
                { "Type", (value) => StringExtensions.ToNullableInt(value).Value },
                { "Last", (value) => StringExtensions.ToNullableFloat(value) },
                { "TradeSize", (value) => StringExtensions.ToNullableInt(value) },
                { "TradedMarket", (value) => StringExtensions.ToNullableInt(value) },
                { "TradeDate", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat) },
                { "TradeTime", (value) => StringExtensions.ToNullableTimeSpan(value, SourceTimeFormat) },
                { "Open", (value) => StringExtensions.ToNullableFloat(value) },
                { "High", (value) => StringExtensions.ToNullableFloat(value) },
                { "Low", (value) => StringExtensions.ToNullableFloat(value) },
                { "Close", (value) => StringExtensions.ToNullableFloat(value) },
                { "Bid", (value) => StringExtensions.ToNullableFloat(value) },
                { "BidMarket", (value) => StringExtensions.ToNullableInt(value) },
                { "BidSize", (value) => StringExtensions.ToNullableInt(value) },
                { "Ask", (value) => StringExtensions.ToNullableFloat(value) },
                { "AskMarket", (value) => StringExtensions.ToNullableInt(value) },
                { "AskSize", (value) => StringExtensions.ToNullableInt(value) },
                { "Volume", (value) => StringExtensions.ToNullableInt(value) },
                { "PDayVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "UpVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "DownVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "NeutralVolume", (value) => StringExtensions.ToNullableInt(value) },
                { "TradeCount", (value) => StringExtensions.ToNullableInt(value) },
                { "UpTrades", (value) => StringExtensions.ToNullableInt(value) },
                { "DownTrades", (value) => StringExtensions.ToNullableInt(value) },
                { "NeutralTrades", (value) => StringExtensions.ToNullableInt(value) },
                { "VWAP", (value) => StringExtensions.ToNullableFloat(value) },
                { "MutualDiv", (value) => StringExtensions.ToNullableFloat(value) },
                { "SevenDayYield", (value) => StringExtensions.ToNullableFloat(value) },
                { "OpenInterest", (value) => StringExtensions.ToNullableInt(value) },
                { "Settlement", (value) => StringExtensions.ToNullableFloat(value) },
                { "SettlementDate", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat) },
                { "ExpirationDate", (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat) },
                { "Strike", (value) => StringExtensions.ToNullableFloat(value) }
            };
        }

        public override bool Equals(object obj)
        {
            return obj is MarketSummaryMessage<T> message &&
                   RequestId == message.RequestId &&
                   Fields.CompareContentsWith(message.Fields);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + Fields.GetContentsHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{Fields.ToString(",")}";
        }

        private static IDictionary<string, object> ParseFields(MarketSummaryHandler<T> marketSummaryHandler, string[] values, int index)
        {
            // we either have to double-buffer this through a hashtable, or use reflection which is slow
            //  the other alternative would be to just keep everything in the Dictionary, and just provide accessor properties
            var fields = new Dictionary<string, object>();
            foreach (var fieldName in marketSummaryHandler.FieldNames)
            {
                fields.Add(fieldName, marketSummaryHandler.FieldConvertors[fieldName].Invoke(values[index++]));
            }

            return fields;
        }
    }
}
