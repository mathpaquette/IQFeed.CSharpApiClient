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
                        _decimalHandler = MostRecentTradeHandler;
                        _doubleHandler = MostRecentTradeHandler;
                        _floatHandler = MostRecentTradeHandler;
                        break;
                    case "Most Recent Trade Size":
                        _decimalHandler = MostRecentTradeSizeHandler;
                        _doubleHandler = MostRecentTradeSizeHandler;
                        _floatHandler = MostRecentTradeSizeHandler;
                        break;
                    case "Most Recent Trade Time":
                        _decimalHandler = MostRecentTradeTimeHandler;
                        _doubleHandler = MostRecentTradeTimeHandler;
                        _floatHandler = MostRecentTradeTimeHandler;
                        break;
                    case "Most Recent Trade Market Center":
                        _decimalHandler = MostRecentTradeMarketCenterHandler;
                        _doubleHandler = MostRecentTradeMarketCenterHandler;
                        _floatHandler = MostRecentTradeMarketCenterHandler;
                        break;
                    case "Total Volume":
                        _decimalHandler = TotalVolumeHandler;
                        _doubleHandler = TotalVolumeHandler;
                        _floatHandler = TotalVolumeHandler;
                        break;
                    case "Bid":
                        _decimalHandler = BidHandler;
                        _doubleHandler = BidHandler;
                        _floatHandler = BidHandler;                        
                        break;
                    case "Bid Size":
                        _decimalHandler = BidSizeHandler;
                        _doubleHandler = BidSizeHandler;
                        _floatHandler = BidSizeHandler;
                        break;
                    case "Ask":
                        _decimalHandler = AskHandler;
                        _doubleHandler = AskHandler;
                        _floatHandler = AskHandler;
                        break;
                    case "Ask Size":
                        _decimalHandler = AskSizeHandler;
                        _doubleHandler = AskSizeHandler;
                        _floatHandler = AskSizeHandler;
                        break;
                    case "Open":
                        _decimalHandler = OpenHandler;
                        _doubleHandler = OpenHandler;
                        _floatHandler = OpenHandler;
                        break;
                    case "High":
                        _decimalHandler = HighHandler;
                        _doubleHandler = HighHandler;
                        _floatHandler = HighHandler;
                        break;
                    case "Low":
                        _decimalHandler = LowHandler;
                        _doubleHandler = LowHandler;
                        _floatHandler = LowHandler;
                        break;
                    case "Close":
                        _decimalHandler = CloseHandler;
                        _doubleHandler = CloseHandler;
                        _floatHandler = CloseHandler;
                        break;
                    case "Message Contents":
                        _decimalHandler = MessageContentsHandler;
                        _doubleHandler = MessageContentsHandler;
                        _floatHandler = MessageContentsHandler;
                        break;
                    case "Most Recent Trade Conditions":
                        _decimalHandler = MostRecentTradeConditionsHandler;
                        _doubleHandler = MostRecentTradeConditionsHandler;
                        _floatHandler = MostRecentTradeConditionsHandler;
                        break;
                    //Open Interest
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

        private static void MostRecentTradeHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal newValue);
            updateSummaryMessage.MostRecentTrade = newValue;
        }
        private static void MostRecentTradeHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue);
            updateSummaryMessage.MostRecentTrade = newValue;
        }
        private static void MostRecentTradeHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float newValue);
            updateSummaryMessage.MostRecentTrade = newValue;
        }

        private static void MostRecentTradeSizeHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.MostRecentTradeSize = newValue;
        }
        private static void MostRecentTradeSizeHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.MostRecentTradeSize = newValue;
        }
        private static void MostRecentTradeSizeHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.MostRecentTradeSize = newValue;
        }

        private static void MostRecentTradeTimeHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newValue);
            updateSummaryMessage.MostRecentTradeTime = newValue.TimeOfDay;
        }
        private static void MostRecentTradeTimeHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newValue);
            updateSummaryMessage.MostRecentTradeTime = newValue.TimeOfDay;
        }
        private static void MostRecentTradeTimeHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newValue);
            updateSummaryMessage.MostRecentTradeTime = newValue.TimeOfDay;
        }

        private static void MostRecentTradeMarketCenterHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.MostRecentTradeMarketCenter = newValue;
        }
        private static void MostRecentTradeMarketCenterHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.MostRecentTradeMarketCenter = newValue;
        }
        private static void MostRecentTradeMarketCenterHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.MostRecentTradeMarketCenter = newValue;
        }

        private static void TotalVolumeHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.TotalVolume = newValue;
        }
        private static void TotalVolumeHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.TotalVolume = newValue;
        }
        private static void TotalVolumeHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.TotalVolume = newValue;
        }

        private static void BidHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal newValue);
            updateSummaryMessage.Bid = newValue;
        }
        private static void BidHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue);
            updateSummaryMessage.Bid = newValue;
        }
        private static void BidHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float newValue);
            updateSummaryMessage.Bid = newValue;
        }

        private static void BidSizeHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.BidSize = newValue;
        }
        private static void BidSizeHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.BidSize = newValue;
        }
        private static void BidSizeHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.BidSize = newValue;
        }

        private static void AskHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal newValue);
            updateSummaryMessage.Ask = newValue;
        }
        private static void AskHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue);
            updateSummaryMessage.Ask = newValue;
        }
        private static void AskHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float newValue);
            updateSummaryMessage.Ask = newValue;
        }

        private static void AskSizeHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.AskSize = newValue;
        }
        private static void AskSizeHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.AskSize = newValue;
        }
        private static void AskSizeHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int newValue);
            updateSummaryMessage.AskSize = newValue;
        }

        private static void OpenHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal newValue);
            updateSummaryMessage.Open = newValue;
        }
        private static void OpenHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue);
            updateSummaryMessage.Open = newValue;
        }
        private static void OpenHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float newValue);
            updateSummaryMessage.Open = newValue;
        }

        private static void HighHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal newValue);
            updateSummaryMessage.High = newValue;
        }
        private static void HighHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue);
            updateSummaryMessage.High = newValue;
        }
        private static void HighHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float newValue);
            updateSummaryMessage.High = newValue;
        }

        private static void LowHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal newValue);
            updateSummaryMessage.Low = newValue;
        }
        private static void LowHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue);
            updateSummaryMessage.Low = newValue;
        }
        private static void LowHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float newValue);
            updateSummaryMessage.Low = newValue;
        }

        private static void CloseHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal newValue);
            updateSummaryMessage.Close = newValue;
        }
        private static void CloseHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double newValue);
            updateSummaryMessage.Close = newValue;
        }
        private static void CloseHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float newValue);
            updateSummaryMessage.Close = newValue;
        }

        private static void MessageContentsHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            updateSummaryMessage.MessageContents = value;
        }
        private static void MessageContentsHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            updateSummaryMessage.MessageContents = value;
        }
        private static void MessageContentsHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            updateSummaryMessage.MessageContents = value;
        }

        private static void MostRecentTradeConditionsHandler(UpdateSummaryMessage<decimal> updateSummaryMessage, string value)
        {
            updateSummaryMessage.MostRecentTradeConditions = value;
        }
        private static void MostRecentTradeConditionsHandler(UpdateSummaryMessage<double> updateSummaryMessage, string value)
        {
            updateSummaryMessage.MostRecentTradeConditions = value;
        }
        private static void MostRecentTradeConditionsHandler(UpdateSummaryMessage<float> updateSummaryMessage, string value)
        {
            updateSummaryMessage.MostRecentTradeConditions = value;
        }
    }


    public class UpdateSummaryMessage<T> : UpdateSummaryMessage, IUpdateSummaryMessage<T>
    {
        protected internal UpdateSummaryMessage(string symbol)
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
        public int OpenInterest { get; protected internal set; }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(MostRecentTrade)}: {MostRecentTrade}, {nameof(MostRecentTradeSize)}: {MostRecentTradeSize}, {nameof(MostRecentTradeTime)}: {MostRecentTradeTime}, {nameof(MostRecentTradeMarketCenter)}: {MostRecentTradeMarketCenter}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(Bid)}: {Bid}, {nameof(BidSize)}: {BidSize}, {nameof(Ask)}: {Ask}, {nameof(AskSize)}: {AskSize}, {nameof(Open)}: {Open}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Close)}: {Close}, {nameof(MessageContents)}: {MessageContents}, {nameof(MostRecentTradeConditions)}: {MostRecentTradeConditions}, {nameof(OpenInterest)}: {OpenInterest}";
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
                   OpenInterest == message.OpenInterest;
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
                hash = hash * 29 + OpenInterest.GetHashCode();
                return hash;
            }
        }
    }
}