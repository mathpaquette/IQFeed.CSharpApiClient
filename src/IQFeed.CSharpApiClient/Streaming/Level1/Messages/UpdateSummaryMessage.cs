using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public abstract class UpdateSummaryMessage
    {
        public const string UpdateMessageTimeFormat = "HH:mm:ss.ffffff";

        private static Action<UpdateSummaryMessage<decimal>, string>[] decimalHandlers;
        private static Action<UpdateSummaryMessage<double>, string>[] doubleHandlers;
        private static Action<UpdateSummaryMessage<float>, string>[] floatHandlers;

        public static UpdateSummaryMessage<decimal> ParseDecimal(string message)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];
            var updateSummaryMessage = new UpdateSummaryMessage<decimal>(symbol);
            for (int i = 0; i < decimalHandlers.Count() - 1; i++)
            {
                decimalHandlers[i]?.Invoke(updateSummaryMessage, values[i+2]);
            }
            return updateSummaryMessage;
        }
        //DateTime.TryParseExact(values[4], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var mostRecentTradeTime);
        //int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeMarketCenter);                                   // field 75
        //int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var totalVolume);                                                   // field 7
        //decimal.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var bid);                                                    // field 11
        //int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var bidSize);                                                       // field 13
        //decimal.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var ask);                                                    // field 12
        //int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var askSize);                                                      // field 14
        //decimal.TryParse(values[11], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);                                                  // field 20
        //decimal.TryParse(values[12], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);                                                  // field 9
        //decimal.TryParse(values[13], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);                                                   // field 10
        //decimal.TryParse(values[14], NumberStyles.Any, CultureInfo.InvariantCulture, out var close);                                                 // field 21
        //var messageContents = values[15];                                                                                                                  // field 80
        //var mostRecentTradeConditions = values[16];                                                                                                        // field 74

        public static UpdateSummaryMessage<double> ParseDouble(string message)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];
            var updateSummaryMessage = new UpdateSummaryMessage<double>(symbol);
            for (int i = 0; i < doubleHandlers.Count() - 1; i++)
            {
                doubleHandlers[i]?.Invoke(updateSummaryMessage, values[i+2]);
            }
            return updateSummaryMessage;
        }
        //int.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeSize);                                           // field 72
        //DateTime.TryParseExact(values[4], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var mostRecentTradeTime);
        //int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeMarketCenter);                                   // field 75
        //int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var totalVolume);                                                   // field 7
        //double.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var bid);                                                    // field 11
        //int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var bidSize);                                                       // field 13
        //double.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var ask);                                                    // field 12
        //int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var askSize);                                                      // field 14
        //double.TryParse(values[11], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);                                                  // field 20
        //double.TryParse(values[12], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);                                                  // field 9
        //double.TryParse(values[13], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);                                                   // field 10
        //double.TryParse(values[14], NumberStyles.Any, CultureInfo.InvariantCulture, out var close);                                                 // field 21
        //var messageContents = values[15];                                                                                                                  // field 80
        //var mostRecentTradeConditions = values[16];                                                                                                        // field 74

        public static UpdateSummaryMessage<float> ParseFloat(string message)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];
            var updateSummaryMessage = new UpdateSummaryMessage<float>(symbol);
            for (int i = 0; i < floatHandlers.Count() - 1; i++)
            {
                floatHandlers[i]?.Invoke(updateSummaryMessage, values[i+2]);
            }
            return updateSummaryMessage;
        }
        //int.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeSize);                                           // field 72
        //DateTime.TryParseExact(values[4], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var mostRecentTradeTime);
        //int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var mostRecentTradeMarketCenter);                                   // field 75
        //int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var totalVolume);                                                   // field 7
        //float.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var bid);                                                    // field 11
        //int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var bidSize);                                                       // field 13
        //float.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var ask);                                                    // field 12
        //int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var askSize);                                                      // field 14
        //float.TryParse(values[11], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);                                                  // field 20
        //float.TryParse(values[12], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);                                                  // field 9
        //float.TryParse(values[13], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);                                                   // field 10
        //float.TryParse(values[14], NumberStyles.Any, CultureInfo.InvariantCulture, out var close);                                                 // field 21
        //var messageContents = values[15];                                                                                                                  // field 80
        //var mostRecentTradeConditions = values[16];                                                                                                        // field 74

        public static void PrepareDynamicFieldHandlers(string[] currentFields)
        {
            var _decimalHandlers = new List<Action<UpdateSummaryMessage<decimal>,string>>();
            var _doubleHandlers = new List<Action<UpdateSummaryMessage<double>, string>>();
            var _floatHandlers = new List<Action<UpdateSummaryMessage<float>, string>>();
            Action<UpdateSummaryMessage<decimal>, string> _decimalHandler = null;
            Action<UpdateSummaryMessage<double>, string> _doubleHandler = null;
            Action<UpdateSummaryMessage<float>, string> _floatHandler = null;
            foreach (var field in currentFields)
            {
                switch (field)
                {
                    case "Most Recent Trade":
                        _decimalHandler = mostRecentTradeDecimalHandler;
                        _doubleHandler = mostRecentTradeDoubleHandler;
                        _floatHandler = mostRecentTradeFloatHandler;
                        break;
                    case "Most Recent Trade Size":
                        _decimalHandler = mostRecentTradeSizeHandler;
                        _doubleHandler = mostRecentTradeSizeHandler;
                        _floatHandler = mostRecentTradeSizeHandler;
                        break;
                    //case "Most Recent Trade Time":
                    //    break;
                    //case "Most Recent Trade Market Center":
                    //    break;
                    //case "Total Volume":
                    //    break;
                    //case "Bid":
                    //    break;
                    //case "Bid Size":
                    //    break;
                    //case "Ask":
                    //    break;
                    //case "Ask Size":
                    //    break;
                    //case "Open":
                    //    break;
                    //case "High":
                    //    break;
                    //case "Low":
                    //    break;
                    //case "Close":
                    //    break;
                    //case "Message Contents":
                    //    break;
                    //case "Most Recent Trade Conditions":
                    //    break;
                    default:
                        throw new NotSupportedException($"Field {field} not supported");
                }
                _decimalHandlers.Add(_decimalHandler);
                _doubleHandlers.Add(_doubleHandler);
                _floatHandlers.Add(_floatHandler);
            }
            decimalHandlers = _decimalHandlers.ToArray();
            doubleHandlers = _doubleHandlers.ToArray();
            floatHandlers = _floatHandlers.ToArray();
        }

        private static void mostRecentTradeDecimalHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            decimal newValue;
            decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out newValue);
            updateSummaryMessage.MostRecentTrade = newValue;
        }

        private static void mostRecentTradeDoubleHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            double newValue;
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out newValue);
            updateSummaryMessage.MostRecentTrade = newValue;
        }

        private static void mostRecentTradeFloatHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            float newValue;
            float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out newValue);
            updateSummaryMessage.MostRecentTrade = newValue;
        }
        private static void mostRecentTradeSizeHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.MostRecentTradeSize = newValue;
        }
        private static void mostRecentTradeSizeHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.MostRecentTradeSize = newValue;
        }
        private static void mostRecentTradeSizeHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.MostRecentTradeSize = newValue;
        }
    }


    public class UpdateSummaryMessage<T> : UpdateSummaryMessage, IUpdateSummaryMessage<T>
    {
        public UpdateSummaryMessage(string symbol)
        {
            Symbol = symbol;
        }

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

        public string Symbol { get; private set; }
        public T MostRecentTrade { get; protected internal set; }
        public int MostRecentTradeSize { get; protected internal set; }
        public TimeSpan MostRecentTradeTime { get; protected internal set; }
        public int MostRecentTradeMarketCenter { get; protected internal set; }
        public int TotalVolume { get; protected internal set; }
        public T Bid { get; protected internal set; }
        public int BidSize { get; protected internal set; }
        public T Ask { get; protected internal set; }
        public int AskSize { get; protected internal set; }
        public T Open { get; protected internal set; }
        public T High { get; protected internal set; }
        public T Low { get; protected internal set; }
        public T Close { get; protected internal set; }
        public string MessageContents { get; protected internal set; }
        public string MostRecentTradeConditions { get; protected internal set; }

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