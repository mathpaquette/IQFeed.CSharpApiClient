using System;
using System.Globalization;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Extensions;
using System.Linq;
using System.Text;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class UpdateSummaryMessageDynamic : IUpdateSummaryMessageDynamic
    {
        public const string UpdateMessageTimeFormat = "HH:mm:ss.ffffff";
        public const string UpdateMessageDateFormat = "MM/dd/yyyy";

        private UpdateSummaryMessageDynamic(string symbol, string feedMessage, DynamicFieldset[] fields, string[] values)
        {
            Symbol = symbol;
            FeedMessage = feedMessage;
            _valuesByFieldType = new Dictionary<DynamicFieldset, string>();
            int i = 0;
            foreach( var field in fields)
            {
                _valuesByFieldType.Add(field, values[i++]);
            }
        }

        public string Symbol { get; private set; }
        public string FeedMessage { get; private set; }

        private readonly Dictionary<DynamicFieldset, string> _valuesByFieldType;

        public static UpdateSummaryMessageDynamic Parse(string message, DynamicFieldset[] fields)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];

            return new UpdateSummaryMessageDynamic(symbol, message, fields, values.Skip(2).ToArray());
        }

        public override bool Equals(object obj)
        {
            return obj is UpdateSummaryMessageDynamic message && Equals(FeedMessage, message.FeedMessage);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + FeedMessage.GetHashCode();
                return hash;
            }
        }
        public override string ToString()
        {
            var dynFields = typeof(DynamicFieldset).GetEnumNames();

            var sb = new StringBuilder();
            sb.Append($"{nameof(Symbol)}: {Symbol}");
            
            foreach (var dynField in _valuesByFieldType)
                sb.Append($", {dynFields[(int)dynField.Key]}: {DynamicToString(dynField.Key)}");
            return sb.ToString();
        }

        private string DynamicToString( DynamicFieldset fieldset)
        {
            switch (fieldset)
            {
                // **NOTE** - remaining cases are generated.  Advise re-generate on changes to IQFeed.CSharpApiClient.Streaming.Level1.DynamicFieldset
                case DynamicFieldset.SevenDayYield: return SevenDayYield.ToString();
                case DynamicFieldset.Ask: return Ask.ToString();
                case DynamicFieldset.AskChange: return AskChange.ToString();
                case DynamicFieldset.AskMarketCenter: return AskMarketCenter.ToString();
                case DynamicFieldset.AskSize: return AskSize.ToString();
                case DynamicFieldset.AskTime: return AskTime.ToString();
                case DynamicFieldset.AvailableRegions: return AvailableRegions.ToString();
                case DynamicFieldset.AverageMaturity: return AverageMaturity.ToString();
                case DynamicFieldset.Bid: return Bid.ToString();
                case DynamicFieldset.BidChange: return BidChange.ToString();
                case DynamicFieldset.BidMarketCenter: return BidMarketCenter.ToString();
                case DynamicFieldset.BidSize: return BidSize.ToString();
                case DynamicFieldset.BidTime: return BidTime.ToString();
                case DynamicFieldset.Change: return Change.ToString();
                case DynamicFieldset.ChangeFromOpen: return ChangeFromOpen.ToString();
                case DynamicFieldset.Close: return Close.ToString();
                case DynamicFieldset.CloseRange1: return CloseRange1.ToString();
                case DynamicFieldset.CloseRange2: return CloseRange2.ToString();
                case DynamicFieldset.DaysToExpiration: return DaysToExpiration.ToString();
                case DynamicFieldset.DecimalPrecision: return DecimalPrecision.ToString();
                case DynamicFieldset.Delay: return Delay.ToString();
                case DynamicFieldset.ExchangeID: return ExchangeID.ToString();
                case DynamicFieldset.ExtendedTrade: return ExtendedTrade.ToString();
                case DynamicFieldset.ExtendedTradeDate: return ExtendedTradeDate.ToString();
                case DynamicFieldset.ExtendedTradeMarketCenter: return ExtendedTradeMarketCenter.ToString();
                case DynamicFieldset.ExtendedTradeSize: return ExtendedTradeSize.ToString();
                case DynamicFieldset.ExtendedTradeTime: return ExtendedTradeTime.ToString();
                case DynamicFieldset.ExtendedTradingChange: return ExtendedTradingChange.ToString();
                case DynamicFieldset.ExtendedTradingDifference: return ExtendedTradingDifference.ToString();
                case DynamicFieldset.FinancialStatusIndicator: return FinancialStatusIndicator.ToString();
                case DynamicFieldset.FractionDisplayCode: return FractionDisplayCode.ToString();
                case DynamicFieldset.High: return High.ToString();
                case DynamicFieldset.Last: return Last.ToString();
                case DynamicFieldset.LastDate: return LastDate.ToString();
                case DynamicFieldset.LastMarketCenter: return LastMarketCenter.ToString();
                case DynamicFieldset.LastSize: return LastSize.ToString();
                case DynamicFieldset.LastTime: return LastTime.ToString();
                case DynamicFieldset.Low: return Low.ToString();
                case DynamicFieldset.MarketCapitalization: return MarketCapitalization.ToString();
                case DynamicFieldset.MarketOpen: return MarketOpen.ToString();
                case DynamicFieldset.MessageContents: return MessageContents.ToString();
                case DynamicFieldset.MostRecentTrade: return MostRecentTrade.ToString();
                case DynamicFieldset.MostRecentTradeConditions: return MostRecentTradeConditions.ToString();
                case DynamicFieldset.MostRecentTradeDate: return MostRecentTradeDate.ToString();
                case DynamicFieldset.MostRecentTradeMarketCenter: return MostRecentTradeMarketCenter.ToString();
                case DynamicFieldset.MostRecentTradeSize: return MostRecentTradeSize.ToString();
                case DynamicFieldset.MostRecentTradeTime: return MostRecentTradeTime.ToString();
                case DynamicFieldset.NetAssetValue: return NetAssetValue.ToString();
                case DynamicFieldset.NumberOfTradesToday: return NumberOfTradesToday.ToString();
                case DynamicFieldset.Open: return Open.ToString();
                case DynamicFieldset.OpenInterest: return OpenInterest.ToString();
                case DynamicFieldset.OpenRange1: return OpenRange1.ToString();
                case DynamicFieldset.OpenRange2: return OpenRange2.ToString();
                case DynamicFieldset.PercentChange: return PercentChange.ToString();
                case DynamicFieldset.PercentOffAverageVolume: return PercentOffAverageVolume.ToString();
                case DynamicFieldset.PreviousDayVolume: return PreviousDayVolume.ToString();
                case DynamicFieldset.PriceEarningsRatio: return PriceEarningsRatio.ToString();
                case DynamicFieldset.Range: return Range.ToString();
                case DynamicFieldset.RestrictedCode: return RestrictedCode.ToString();
                case DynamicFieldset.Settle: return Settle.ToString();
                case DynamicFieldset.SettlementDate: return SettlementDate.ToString();
                case DynamicFieldset.Spread: return Spread.ToString();
                case DynamicFieldset.Tick: return Tick.ToString();
                case DynamicFieldset.TickID: return TickID.ToString();
                case DynamicFieldset.TotalVolume: return TotalVolume.ToString();
                case DynamicFieldset.Volatility: return Volatility.ToString();
                case DynamicFieldset.VWAP: return VWAP.ToString();
            }
            throw new Exception($"Field [{fieldset}] not found");
        }

        // **NOTE** - remaining code is generated.  Advise re-generate on changes to IQFeed.CSharpApiClient.Streaming.Level1.DynamicFieldset

        public double SevenDayYield { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.SevenDayYield, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [7 Day Yield] not found"); } }
        public double Ask { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Ask, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Ask] not found"); } }
        public double AskChange { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.AskChange, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Ask Change] not found"); } }
        public int AskMarketCenter { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.AskMarketCenter, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Ask Market Center] not found"); } }
        public int AskSize { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.AskSize, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Ask Size] not found"); } }
        public TimeSpan AskTime { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.AskTime, out var value)) { DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue.TimeOfDay; } throw new Exception("Dynamic field [Ask Time] not found"); } }
        public string AvailableRegions { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.AvailableRegions, out var value)) { return value; } throw new Exception("Dynamic field [Available Regions] not found"); } }
        public double AverageMaturity { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.AverageMaturity, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Average Maturity] not found"); } }
        public double Bid { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Bid, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Bid] not found"); } }
        public double BidChange { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.BidChange, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Bid Change] not found"); } }
        public int BidMarketCenter { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.BidMarketCenter, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Bid Market Center] not found"); } }
        public int BidSize { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.BidSize, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Bid Size] not found"); } }
        public TimeSpan BidTime { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.BidTime, out var value)) { DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue.TimeOfDay; } throw new Exception("Dynamic field [Bid Time] not found"); } }
        public double Change { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Change, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Change] not found"); } }
        public double ChangeFromOpen { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.ChangeFromOpen, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Change From Open] not found"); } }
        public double Close { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Close, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Close] not found"); } }
        public double CloseRange1 { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.CloseRange1, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Close Range 1] not found"); } }
        public double CloseRange2 { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.CloseRange2, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Close Range 2] not found"); } }
        public string DaysToExpiration { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.DaysToExpiration, out var value)) { return value; } throw new Exception("Dynamic field [Days to Expiration] not found"); } }
        public string DecimalPrecision { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.DecimalPrecision, out var value)) { return value; } throw new Exception("Dynamic field [Decimal Precision] not found"); } }
        public int Delay { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Delay, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Delay] not found"); } }
        public string ExchangeID { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.ExchangeID, out var value)) { return value; } throw new Exception("Dynamic field [Exchange ID] not found"); } }
        public double ExtendedTrade { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.ExtendedTrade, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Extended Trade] not found"); } }
        public TimeSpan ExtendedTradeDate { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.ExtendedTradeDate, out var value)) { DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue.TimeOfDay; } throw new Exception("Dynamic field [Extended Trade Date] not found"); } }
        public int ExtendedTradeMarketCenter { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.ExtendedTradeMarketCenter, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Extended Trade Market Center] not found"); } }
        public int ExtendedTradeSize { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.ExtendedTradeSize, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Extended Trade Size] not found"); } }
        public TimeSpan ExtendedTradeTime { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.ExtendedTradeTime, out var value)) { DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue.TimeOfDay; } throw new Exception("Dynamic field [Extended Trade Time] not found"); } }
        public double ExtendedTradingChange { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.ExtendedTradingChange, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Extended Trading Change] not found"); } }
        public double ExtendedTradingDifference { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.ExtendedTradingDifference, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Extended Trading Difference] not found"); } }
        public string FinancialStatusIndicator { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.FinancialStatusIndicator, out var value)) { return value; } throw new Exception("Dynamic field [Financial Status Indicator] not found"); } }
        public string FractionDisplayCode { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.FractionDisplayCode, out var value)) { return value; } throw new Exception("Dynamic field [Fraction Display Code] not found"); } }
        public double High { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.High, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [High] not found"); } }
        public double Last { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Last, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Last] not found"); } }
        public TimeSpan LastDate { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.LastDate, out var value)) { DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue.TimeOfDay; } throw new Exception("Dynamic field [Last Date] not found"); } }
        public int LastMarketCenter { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.LastMarketCenter, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Last Market Center] not found"); } }
        public int LastSize { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.LastSize, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Last Size] not found"); } }
        public TimeSpan LastTime { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.LastTime, out var value)) { DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue.TimeOfDay; } throw new Exception("Dynamic field [Last Time] not found"); } }
        public double Low { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Low, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Low] not found"); } }
        public double MarketCapitalization { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.MarketCapitalization, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Market Capitalization] not found"); } }
        public int MarketOpen { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.MarketOpen, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Market Open] not found"); } }
        public string MessageContents { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.MessageContents, out var value)) { return value; } throw new Exception("Dynamic field [Message Contents] not found"); } }
        public double MostRecentTrade { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.MostRecentTrade, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Most Recent Trade] not found"); } }
        public string MostRecentTradeConditions { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.MostRecentTradeConditions, out var value)) { return value; } throw new Exception("Dynamic field [Most Recent Trade Conditions] not found"); } }
        public DateTime MostRecentTradeDate { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.MostRecentTradeDate, out var value)) { DateTime.TryParseExact(value, UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Most Recent Trade Date] not found"); } }
        public int MostRecentTradeMarketCenter { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.MostRecentTradeMarketCenter, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Most Recent Trade Market Center] not found"); } }
        public int MostRecentTradeSize { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.MostRecentTradeSize, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Most Recent Trade Size] not found"); } }
        public TimeSpan MostRecentTradeTime { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.MostRecentTradeTime, out var value)) { DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue.TimeOfDay; } throw new Exception("Dynamic field [Most Recent Trade Time] not found"); } }
        public double NetAssetValue { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.NetAssetValue, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Net Asset Value] not found"); } }
        public int NumberOfTradesToday { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.NumberOfTradesToday, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Number of Trades Today] not found"); } }
        public double Open { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Open, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Open] not found"); } }
        public int OpenInterest { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.OpenInterest, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Open Interest] not found"); } }
        public double OpenRange1 { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.OpenRange1, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Open Range 1] not found"); } }
        public double OpenRange2 { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.OpenRange2, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Open Range 2] not found"); } }
        public double PercentChange { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.PercentChange, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Percent Change] not found"); } }
        public double PercentOffAverageVolume { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.PercentOffAverageVolume, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Percent Off Average Volume] not found"); } }
        public int PreviousDayVolume { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.PreviousDayVolume, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Previous Day Volume] not found"); } }
        public double PriceEarningsRatio { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.PriceEarningsRatio, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Price-Earnings Ratio] not found"); } }
        public double Range { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Range, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Range] not found"); } }
        public string RestrictedCode { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.RestrictedCode, out var value)) { return value; } throw new Exception("Dynamic field [Restricted Code] not found"); } }
        public double Settle { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Settle, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Settle] not found"); } }
        public DateTime SettlementDate { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.SettlementDate, out var value)) { DateTime.TryParseExact(value, UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Settlement Date] not found"); } }
        public double Spread { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Spread, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Spread] not found"); } }
        public int Tick { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Tick, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Tick] not found"); } }
        public int TickID { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.TickID, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [TickID] not found"); } }
        public int TotalVolume { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.TotalVolume, out var value)) { int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Total Volume] not found"); } }
        public double Volatility { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.Volatility, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [Volatility] not found"); } }
        public double VWAP { get { if (_valuesByFieldType.TryGetValue(DynamicFieldset.VWAP, out var value)) { double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue; } throw new Exception("Dynamic field [VWAP] not found"); } }

    }
}