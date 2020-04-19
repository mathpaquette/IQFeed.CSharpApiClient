using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        public const string SourceTimeFormat = "hhmmss";

        public MarketSummaryMessage(string symbol, int exchange, int type, T? last, int? tradeSize,
            int? tradedMarket, DateTime? tradeDate, TimeSpan? tradeTime, T? open, T? high, T? low, T? close,
            T? bid, int? bidMarket, int? bidSize, T? ask, int? askMarket, int? askSize, int? volume, int? pDayVolume,
            int? upVolume, int? downVolume, int? neutralVolume, int? tradeCount, int? upTrades, int? downTrades,
            int? neutralTrades, T? vwap, T? mutualDiv, T? sevenDayYield, int? openInterest,
            T? settlement, DateTime? settlementDate, DateTime? expirationDate, T? strike,
            string requestId = null)
        {
            RequestId = requestId;
            Symbol = symbol;
            Exchange = exchange;
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
        public int Exchange { get; private set; }
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
        public int? BidMarket { get; private set; }
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


        public SecurityType SecurityType => (SecurityType)Type;
        public DateTime? TradeDateTime => (TradeDate != null && TradeTime != null) ? TradeDate.Value.Add(TradeTime.Value) : (DateTime?)null;

        public static MarketSummaryMessage<T> Parse(string message, MarketSummaryHandler<T> marketSummaryHandler)
        {
            if (marketSummaryHandler.FieldNames.Count == 0)
            {
                // First time through will be fieldnames
                Array.ForEach(message.SplitFeedMessage(), m => marketSummaryHandler.FieldNames.Add(m));
                return null;
            }

            var index = 0;
            var values = message.SplitFeedMessage();
            var fields = ParseFields(marketSummaryHandler, values, index);
            return new MarketSummaryMessage<T>(
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
                Array.ForEach(message.SplitFeedMessage(), m => marketSummaryHandler.FieldNames.Add(m));
                return null;
            }

            var index = 0;
            var values = message.SplitFeedMessage();
            var requestId = values[index++];
            var fields = ParseFields(marketSummaryHandler, values, index);

            return new MarketSummaryMessage<T>(
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
                (T?)fields["Strike"],
                requestId
                );
        }

        public static MarketSummaryMessage<T> ParseFromFieldsDictionary(IDictionary<string, object> usedFields, MarketSummaryHandler<T> marketSummaryHandler, string requestId = null)
        {
            var fields = EnsureAllFields(usedFields, marketSummaryHandler);

            return new MarketSummaryMessage<T>(
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
                    Symbol == message.Symbol &&
                    Exchange == message.Exchange &&
                    Type == message.Type &&
                    Equals(Last, message.Last) &&
                    TradeSize == message.TradeSize &&
                    TradedMarket == message.TradedMarket &&
                    TradeDate == message.TradeDate &&
                    TradeTime == message.TradeTime &&
                    Equals(Open, message.Open) &&
                    Equals(High, message.High) &&
                    Equals(Low, message.Low) &&
                    Equals(Close, message.Close) &&
                    Equals(Bid, message.Bid) &&
                    BidMarket == message.BidMarket &&
                    BidSize == message.BidSize &&
                    Equals(Ask, message.Ask) &&
                    AskMarket == message.AskMarket &&
                    AskSize == message.AskSize &&
                    Volume == message.Volume &&
                    PDayVolume == message.PDayVolume &&
                    UpVolume == message.UpVolume &&
                    DownVolume == message.DownVolume &&
                    NeutralVolume == message.NeutralVolume &&
                    TradeCount == message.TradeCount &&
                    UpTrades == message.UpTrades &&
                    DownTrades == message.DownTrades &&
                    NeutralTrades == message.NeutralTrades &&
                    Equals(VWAP, message.VWAP) &&
                    Equals(MutualDiv, message.MutualDiv) &&
                    Equals(SevenDayYield, message.SevenDayYield) &&
                    OpenInterest == message.OpenInterest &&
                    Equals(Settlement, message.Settlement) &&
                    SettlementDate == message.SettlementDate &&
                    ExpirationDate == message.ExpirationDate &&
                    Equals(Strike, message.Strike);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + Symbol != null ? Symbol.GetHashCode() : 0;
                hash = hash * 29 + Exchange.GetHashCode();
                hash = hash * 29 + Type.GetHashCode();
                hash = hash * 29 + (Last != null ? Last.GetHashCode() : 0);
                hash = hash * 29 + TradeSize != null ? TradeSize.GetHashCode() : 0;
                hash = hash * 29 + TradedMarket != null ? TradedMarket.GetHashCode() : 0;
                hash = hash * 29 + TradeDate.GetHashCode();
                hash = hash * 29 + TradeTime.GetHashCode();
                hash = hash * 29 + (Open != null ? Open.GetHashCode() : 0);
                hash = hash * 29 + (High != null ? High.GetHashCode() : 0);
                hash = hash * 29 + (Low != null ? Low.GetHashCode() : 0);
                hash = hash * 29 + (Close != null ? Close.GetHashCode() : 0);
                hash = hash * 29 + (Bid != null ? Bid.GetHashCode() : 0);
                hash = hash * 29 + BidMarket != null ? BidMarket.GetHashCode() : 0;
                hash = hash * 29 + BidSize != null ? BidSize.GetHashCode() : 0;
                hash = hash * 29 + (Ask != null ? Ask.GetHashCode() : 0);
                hash = hash * 29 + AskMarket != null ? AskMarket.GetHashCode() : 0;
                hash = hash * 29 + AskSize != null ? AskSize.GetHashCode() : 0;
                hash = hash * 29 + Volume != null ? Volume.GetHashCode() : 0;
                hash = hash * 29 + PDayVolume != null ? PDayVolume.GetHashCode() : 0;
                hash = hash * 29 + UpVolume != null ? UpVolume.GetHashCode() : 0;
                hash = hash * 29 + DownVolume != null ? DownVolume.GetHashCode() : 0;
                hash = hash * 29 + NeutralVolume != null ? NeutralVolume.GetHashCode() : 0;
                hash = hash * 29 + TradeCount != null ? TradeCount.GetHashCode() : 0;
                hash = hash * 29 + UpTrades != null ? UpTrades.GetHashCode() : 0;
                hash = hash * 29 + DownTrades != null ? DownTrades.GetHashCode() : 0;
                hash = hash * 29 + NeutralTrades != null ? NeutralTrades.GetHashCode() : 0;
                hash = hash * 29 + (VWAP != null ? VWAP.GetHashCode() : 0);
                hash = hash * 29 + (MutualDiv != null ? MutualDiv.GetHashCode() : 0);
                hash = hash * 29 + (SevenDayYield != null ? SevenDayYield.GetHashCode() : 0);
                hash = hash * 29 + OpenInterest != null ? OpenInterest.GetHashCode() : 0;
                hash = hash * 29 + (Settlement != null ? Settlement.GetHashCode() : 0);
                hash = hash * 29 + (SettlementDate != null ? SettlementDate.GetHashCode() : 0);
                hash = hash * 29 + (ExpirationDate != null ? ExpirationDate.GetHashCode() : 0);
                hash = hash * 29 + (Strike != null ? Strike.GetHashCode() : 0);

                return hash;
            }
        }

        // we won't be doing ToString in production running very often, so slow reflection is OK
        public override string ToString()
        {
            return ToStringProperties(
                a => a.RequestId,
                a => a.Exchange,
                a => a.Type,
                a => a.Last,
                a => a.TradeSize,
                a => a.TradedMarket,
                a => a.TradeDate,
                a => a.TradeTime,
                a => a.Open,
                a => a.High,
                a => a.Low,
                a => a.Close,
                a => a.Bid,
                a => a.BidMarket,
                a => a.BidSize,
                a => a.Ask,
                a => a.AskMarket,
                a => a.AskSize,
                a => a.Volume,
                a => a.PDayVolume,
                a => a.UpVolume,
                a => a.DownVolume,
                a => a.NeutralVolume,
                a => a.TradeCount,
                a => a.UpTrades,
                a => a.DownTrades,
                a => a.NeutralTrades,
                a => a.VWAP,
                a => a.MutualDiv,
                a => a.SevenDayYield,
                a => a.OpenInterest,
                a => a.Settlement,
                a => a.SettlementDate,
                a => a.ExpirationDate,
                a => a.Strike
                );
        }

        private string ToStringProperties(params Expression<Func<MarketSummaryMessage<T>, object>>[] properties)
        {
            var output = new StringBuilder();
            foreach(var property in properties)
            {
                if (output.Length > 0)
                {
                    output.Append(", ");
                }

                switch (property.Body.NodeType.ToString())
                {
                    case "MemberAccess":
                        var expr1 = (MemberExpression)property.Body;
                        var prop1 = (PropertyInfo)expr1.Member;
                        output.Append(ToStringNameValue(prop1.Name, prop1.GetValue(this)?.ToString()));
                        break;

                    default:
                        var expr2 = (UnaryExpression)property.Body;
                        var prop2 = (PropertyInfo)((MemberExpression)expr2.Operand).Member;
                        output.Append(ToStringNameValue(prop2.Name, prop2.GetValue(this)?.ToString()));
                        break;
                }

                //var expr = (DynamicExpression)property.Body;
                //var prop = (PropertyInfo)expr..Member;
                //output.Append(ToStringNameValue(prop.Name, prop.GetValue(this)?.ToString()));
                //output.Append(ToStringNameValue(expr..Method.Name, expr.Method.Invoke(this, BindingFlags.Public, null, null, CultureInfo.InvariantCulture)?.ToString()));
            }

            return output.ToString();
        }

        private string ToStringNameValue(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return $"{name}: null";
            }

            return $"{name}: {value} ";
        }

        private static IDictionary<string, object> ParseFields(MarketSummaryHandler<T> marketSummaryHandler, string[] values, int index)
        {
            // we either have to double-buffer this through a hashtable, or use reflection which is slow
            //  the other alternative would be to just keep everything in the Dictionary, and just provide accessor properties
            var fields = new Dictionary<string, object>();
            foreach (var fieldName in marketSummaryHandler.FieldConvertors.Keys)
            {
                if (marketSummaryHandler.FieldNames.Contains(fieldName))
                {
                    fields.Add(fieldName, marketSummaryHandler.FieldConvertors[fieldName].Invoke(values[index++]));
                }
                else
                {
                    fields.Add(fieldName, null); // we must add nulls for all unknown items to ensure the Parse does not fail
                }
            }

            return fields;
        }

        private static IDictionary<string, object> EnsureAllFields(IDictionary<string, object> fields, MarketSummaryHandler<T> marketSummaryHandler)
        {
            foreach(var fieldName in marketSummaryHandler.FieldConvertors.Keys)
            {
                if (!fields.ContainsKey(fieldName))
                {
                    fields.Add(fieldName, null);
                }
            }

            return fields;
        }
    }
}
