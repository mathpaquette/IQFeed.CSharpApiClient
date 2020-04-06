using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public abstract class UpdateSummaryMessage
    {
        public const string UpdateMessageTimeFormat = "HH:mm:ss.ffffff";

        public static UpdateSummaryMessage<decimal> ParseDecimal(string message, DynamicFieldsetHandler dynamicFieldsetHandler = null)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];                                                                                                                            // field 2
            decimal.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTrade);                                        // field 71
            int.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeSize);                                           // field 72
            DateTime.TryParseExact(values[4], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var mostRecentTradeTime);
            int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeMarketCenter);                                   // field 75
            int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var totalVolume);                                                   // field 7
            decimal.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var bid);                                                    // field 11
            int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var bidSize);                                                       // field 13
            decimal.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var ask);                                                    // field 12
            int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var askSize);                                                      // field 14
            decimal.TryParse(values[11], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);                                                  // field 20
            decimal.TryParse(values[12], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);                                                  // field 9
            decimal.TryParse(values[13], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);                                                   // field 10
            decimal.TryParse(values[14], NumberStyles.Any, CultureInfo.InvariantCulture, out var close);                                                 // field 21
            var messageContents = values[15];                                                                                                                  // field 80
            var mostRecentTradeConditions = values[16];                                                                                                        // field 74
            var dynamicFields = ParseDynamicFields<decimal>(values, dynamicFieldsetHandler);

            return new UpdateSummaryMessage<decimal>(
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
                mostRecentTradeConditions,
                dynamicFields
            );
        }

        public static UpdateSummaryMessage<double> Parse(string message, DynamicFieldsetHandler dynamicFieldsetHandler = null)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];                                                                                                                            // field 2
            double.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTrade);                                        // field 71
            int.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeSize);                                           // field 72
            DateTime.TryParseExact(values[4], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var mostRecentTradeTime);
            int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeMarketCenter);                                   // field 75
            int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var totalVolume);                                                   // field 7
            double.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var bid);                                                    // field 11
            int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var bidSize);                                                       // field 13
            double.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var ask);                                                    // field 12
            int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var askSize);                                                      // field 14
            double.TryParse(values[11], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);                                                  // field 20
            double.TryParse(values[12], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);                                                  // field 9
            double.TryParse(values[13], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);                                                   // field 10
            double.TryParse(values[14], NumberStyles.Any, CultureInfo.InvariantCulture, out var close);                                                 // field 21
            var messageContents = values[15];                                                                                                                  // field 80
            var mostRecentTradeConditions = values[16];                                                                                                        // field 74
            var dynamicFields = ParseDynamicFields<double>(values, dynamicFieldsetHandler);

            return new UpdateSummaryMessage<double>(
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
                mostRecentTradeConditions,
                dynamicFields
            );
        }

        public static UpdateSummaryMessage<float> ParseFloat(string message, DynamicFieldsetHandler dynamicFieldsetHandler = null)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];                                                                                                                            // field 2
            float.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTrade);                                        // field 71
            int.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeSize);                                           // field 72
            DateTime.TryParseExact(values[4], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var mostRecentTradeTime);
            int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeMarketCenter);                                   // field 75
            int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var totalVolume);                                                   // field 7
            float.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var bid);                                                    // field 11
            int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var bidSize);                                                       // field 13
            float.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var ask);                                                    // field 12
            int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var askSize);                                                      // field 14
            float.TryParse(values[11], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);                                                  // field 20
            float.TryParse(values[12], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);                                                  // field 9
            float.TryParse(values[13], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);                                                   // field 10
            float.TryParse(values[14], NumberStyles.Any, CultureInfo.InvariantCulture, out var close);                                                 // field 21
            var messageContents = values[15];                                                                                                                  // field 80
            var mostRecentTradeConditions = values[16];                                                                                                        // field 74
            var dynamicFields = ParseDynamicFields<float>(values, dynamicFieldsetHandler);

            return new UpdateSummaryMessage<float>(
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
                mostRecentTradeConditions, 
                dynamicFields
            );
        }

        private static IDictionary<string, object> ParseDynamicFields<TD>(string[] values, DynamicFieldsetHandler dynamicFieldsetHandler)
        {
            if (dynamicFieldsetHandler == null)
            {
                return null;
            }

            // Dynamic Fieldset Handler will contain additional fields we need to parse (in order)
            var index = 17;
            var dynamicFields = new Dictionary<string, object>();
            foreach (var dynamicField in dynamicFieldsetHandler.Fields)
            {
                var fieldsetDescriptor = GetFieldsetDescriptionAttribute(dynamicField);
                if (fieldsetDescriptor == null)
                {
                    throw new Exception($"Dynamic Field {dynamicField} has no FieldSetDescriptionAttribute!");
                }

                if (fieldsetDescriptor.Type == typeof(double))
                {
                    // if it's a double, then convert to T
                    dynamicFields.Add(dynamicField.ToString(), Convert.ChangeType(values[index++], typeof(TD)));
                }
                else
                {
                    dynamicFields.Add(dynamicField.ToString(), Convert.ChangeType(values[index++], fieldsetDescriptor.Type));
                }
            }

            return dynamicFields;
        }

        private static FieldsetDescriptionAttribute GetFieldsetDescriptionAttribute(DynamicFieldset dynamicField)
        {
            var members = dynamicField.GetType().GetMember(dynamicField.ToString());
            if (members.Length > 0)
            {
                var attributes = members[0].GetCustomAttributes(typeof(FieldsetDescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return (FieldsetDescriptionAttribute)attributes[0];
                }
            }

            return null;
        }
    }

    public class UpdateSummaryMessage<T> : UpdateSummaryMessage, IUpdateSummaryMessage<T>
    {
        // S,CURRENT UPDATE FIELDNAMES,Symbol,Most Recent Trade,Most Recent Trade Size,Most Recent Trade Time,Most Recent Trade Market Center,Total Volume,Bid,Bid Size,Ask,Ask Size,Open,High,Low,Close,Message Contents,Most Recent Trade Conditions
        public UpdateSummaryMessage(
            string symbol,
            T mostRecentTrade,
            int mostRecentTradeSize,
            DateTime mostRecentTradeTime,
            int mostRecentTradeMarketCenter,
            int totalVolume,
            T bid,
            int bidSize,
            T ask,
            int askSize,
            T open,
            T high,
            T low,
            T close,
            string messageContents,
            string mostRecentTradeConditions,
            IDictionary<string, object> dynamicFields = null
            )
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
            DynamicFields = dynamicFields;
        }

        public string Symbol { get; private set; }
        public T MostRecentTrade { get; private set; }
        public int MostRecentTradeSize { get; private set; }
        public TimeSpan MostRecentTradeTime { get; private set; }
        public int MostRecentTradeMarketCenter { get; private set; }
        public int TotalVolume { get; private set; }
        public T Bid { get; private set; }
        public int BidSize { get; private set; }
        public T Ask { get; private set; }
        public int AskSize { get; private set; }
        public T Open { get; private set; }
        public T High { get; private set; }
        public T Low { get; private set; }
        public T Close { get; private set; }
        public string MessageContents { get; private set; }
        public string MostRecentTradeConditions { get; private set; }
        public IDictionary<string, object> DynamicFields { get; private set; }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(MostRecentTrade)}: {MostRecentTrade}, {nameof(MostRecentTradeSize)}: {MostRecentTradeSize}, {nameof(MostRecentTradeTime)}: {MostRecentTradeTime}, {nameof(MostRecentTradeMarketCenter)}: {MostRecentTradeMarketCenter}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(Bid)}: {Bid}, {nameof(BidSize)}: {BidSize}, {nameof(Ask)}: {Ask}, {nameof(AskSize)}: {AskSize}, {nameof(Open)}: {Open}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Close)}: {Close}, {nameof(MessageContents)}: {MessageContents}, {nameof(MostRecentTradeConditions)}: {MostRecentTradeConditions}";
        }


        public override bool Equals(object obj)
        {
            return obj is UpdateSummaryMessage<T> message &&
                   Symbol == message.Symbol &&
                   Equals(MostRecentTrade, message.MostRecentTrade) &&
                   MostRecentTradeSize == message.MostRecentTradeSize &&
                   MostRecentTradeTime == message.MostRecentTradeTime &&
                   MostRecentTradeMarketCenter == message.MostRecentTradeMarketCenter &&
                   TotalVolume == message.TotalVolume &&
                   Equals(Bid, message.Bid) &&
                   BidSize == message.BidSize &&
                   Equals(Ask, message.Ask) &&
                   AskSize == message.AskSize &&
                   Equals(Open, message.Open) &&
                   Equals(High, message.High) &&
                   Equals(Low, message.Low) &&
                   Equals(Close, message.Close) &&
                   MessageContents == message.MessageContents &&
                   MostRecentTradeConditions == message.MostRecentTradeConditions &&
                   ((DynamicFields == null && message.DynamicFields == null) || DynamicFields.CompareContentsWith(message.DynamicFields))
                   ;
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
                if (DynamicFields != null)
                {
                    foreach (var dynamicField in DynamicFields)
                    {
                        hash = hash * 29 + dynamicField.GetHashCode();
                    }
                }
                return hash;
            }
        }
    }
}