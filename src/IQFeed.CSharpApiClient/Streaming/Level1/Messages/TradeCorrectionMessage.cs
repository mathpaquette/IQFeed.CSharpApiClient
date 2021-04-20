using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    /// <summary>
    /// NOTE FROM IQFEED: Correction messages are not guaranteed to contain complete trade information.
    /// We pass the information we receive from the exchange through the feed to your application.
    /// We have found that occasionally the exchange will send incomplete information that doesn't
    /// allow for identification of the exact trade that was supposed to be corrected.
    /// This means that sometimes you will have to lookup trades by several fields to try to
    /// identify the correct trade to insert/delete. Keep in mind that we apply these same
    /// corrections to our own historical data so you can also check our historical data
    /// to verify you applied the correction in the same way we do.
    /// </summary>
    public class TradeCorrectionMessage : ITradeCorrectionMessage
    {
        public const string TradeCorrectionMessageDateFormat = "MM/dd/yyyy";
        public const string TradeCorrectionMessageTimeFormat = "hh\\:mm\\:ss\\.ffffff";

        // C,Symbol,CorrectionType,TradeDate,TradeTime,TradePrice,TradeSize,TickId,TradeConditions,TradeMarketCentre
        public TradeCorrectionMessage(
            string symbol,
            string correctionType,
            DateTime tradeDate,
            TimeSpan tradeTime,
            double tradePrice,
            int tradeSize,
            int tickId,
            string tradeConditions,
            int tradeMarketCentre)
        {
            Symbol = symbol;
            CorrectionType = correctionType;
            TradeDate = tradeDate;
            TradeTime = tradeTime;
            TradePrice = tradePrice;
            TradeSize = tradeSize;
            TickId = tickId;
            TradeConditions = tradeConditions;
            TradeMarketCentre = tradeMarketCentre;
        }

        public string Symbol { get; private set; }
        
        /// <summary>
        /// "I" == Trade Insert
        /// "X" == Trade Delete
        /// </summary>
        public string CorrectionType { get; private set; }
        public DateTime TradeDate { get; private set; }
        public TimeSpan TradeTime { get; private set; }
        public double TradePrice { get; private set; }
        public int TradeSize { get; private set; }
        public int TickId { get; private set; }
        public string TradeConditions { get; private set; }
        public int TradeMarketCentre { get; private set; }

        // for convenience
        public DateTime TradeDateTime => TradeDate.Add(TradeTime);

        public static TradeCorrectionMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];                                                                                                                             // field 1
            var correctionType = values[2];                                                                                                                     // field 2
            DateTime.TryParseExact(values[3], TradeCorrectionMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var tradeDate);        // field 3
            TimeSpan.TryParseExact(values[4],TradeCorrectionMessageTimeFormat, CultureInfo.InvariantCulture, out var tradeTime);                          // field 4
            double.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var tradePrice);                                               // field 5
            int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var tradeSize);                                                     // field 6
            int.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var tickId);                                                       // field 7
            var tradeConditions = values[8];                                                                                                                 // field 8
            int.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var tradeMarketCentre);                                           // field 9

            return new TradeCorrectionMessage(
                symbol,
                correctionType,
                tradeDate,
                tradeTime,
                    tradePrice,
                tradeSize,
                tickId,
                tradeConditions,
                tradeMarketCentre
            );
        }

        public override bool Equals(object obj)
        {
            return obj is TradeCorrectionMessage message &&
                   Symbol == message.Symbol &&
                   CorrectionType== message.CorrectionType &&
                   TradeDate == message.TradeDate &&
                   TradeTime == message.TradeTime &&
                   Equals(TradePrice, message.TradePrice) &&
                   TradeSize == message.TradeSize &&
                   TickId == message.TickId &&
                   TradeConditions == message.TradeConditions &&
                   TradeMarketCentre == message.TradeMarketCentre;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + CorrectionType.GetHashCode();
                hash = hash * 29 + TradeDate.GetHashCode();
                hash = hash * 29 + TradeTime.GetHashCode();
                hash = hash * 29 + TradePrice.GetHashCode();
                hash = hash * 29 + TradeSize.GetHashCode();
                hash = hash * 29 + TickId.GetHashCode();
                hash = hash * 29 + TradeConditions.GetHashCode();
                hash = hash * 29 + TradeMarketCentre.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(CorrectionType)}: {CorrectionType}, {nameof(TradeDate)}: {TradeDate}, {nameof(TradeTime)}: {TradeSize}, {nameof(TradePrice)}: {TradePrice}, {nameof(TradeSize)}: {TradeSize}, {nameof(TickId)}: {TickId}, {nameof(TradeConditions)}: {TradeConditions}, {nameof(TradeMarketCentre)}: {TradeMarketCentre}";
        }
    }
}