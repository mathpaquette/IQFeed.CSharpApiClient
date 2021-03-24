using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.MarketSummary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary
{
    public class MarketSummaryHandler : BaseLookupMessageHandler
    {
        public const string SourceDateFormat = "yyyyMMdd";
        public const string SourceTimeFormat = "hhmmss";

        public MarketSummaryHandler()
        {
            FieldNames = new List<MarketSummaryDynamicFieldset>();
            FieldConvertors = GetFieldConvertors();
        }

        public List<MarketSummaryDynamicFieldset> FieldNames { get; private set; }
        public Func<string, object>[] FieldConvertors { get; private set; }

        public MessageContainer<MarketSummaryMessage> GetMarketSummaryMessages(byte[] rawMessage, int count)
        {
            return ProcessMessages<MarketSummaryMessage>((message) => MarketSummaryMessage.Parse(message, this), ParseErrorMessage, rawMessage, count);
        }

        public MessageContainer<MarketSummaryMessage> GetMarketSummaryMessagesWithRequestId(byte[] rawMessage, int count)
        {
            return ProcessMessages<MarketSummaryMessage>((message) => MarketSummaryMessage.ParseWithRequestId(message, this), ParseErrorMessageWithRequestId, rawMessage, count);
        }

        private Func<string, object>[] GetFieldConvertors()
        {
            var fieldConvertors = new Func<string, object>[Enum.GetNames(typeof(MarketSummaryDynamicFieldset)).Length];
            fieldConvertors[(int)MarketSummaryDynamicFieldset.RequestId] = (value) => value;
            fieldConvertors[(int) MarketSummaryDynamicFieldset.Symbol] = (value) => value;
            fieldConvertors[(int) MarketSummaryDynamicFieldset.Exchange] = (value) => int.Parse(value);
            fieldConvertors[(int) MarketSummaryDynamicFieldset.Type] = (value) => int.Parse(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.Last] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.TradeSize] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.TradedMarket] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.TradeDate] = (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.TradeTime] = (value) => StringExtensions.ToNullableTimeSpan(value, SourceTimeFormat);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.Open] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.High] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.Low] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.Close] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.Bid] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.BidMarket] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.BidSize] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.Ask] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.AskMarket] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.AskSize] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.Volume] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.PDayVolume] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.UpVolume] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.DownVolume] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.NeutralVolume] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.TradeCount] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.UpTrades] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.DownTrades] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.NeutralTrades] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.VWAP] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.MutualDiv] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.SevenDayYield] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.OpenInterest] = (value) => StringExtensions.ToNullableInt(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.Settlement] = (value) => StringExtensions.ToNullableDouble(value);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.SettlementDate] = (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat);
            fieldConvertors[(int) MarketSummaryDynamicFieldset.ExpirationDate] = (value) => StringExtensions.ToNullableDateTime(value, SourceDateFormat);
            fieldConvertors[(int)MarketSummaryDynamicFieldset.Strike] = (value) => StringExtensions.ToNullableDouble(value);
            return fieldConvertors;
        }
    }
}
