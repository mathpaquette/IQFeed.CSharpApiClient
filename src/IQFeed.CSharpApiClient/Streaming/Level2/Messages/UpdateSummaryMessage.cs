using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public class UpdateSummaryMessage
    {
        public const string UpdateMessageTimeFormat = "HH:mm:ss.ffffff";
        public const string UpdateMessageDateFormat = "yyyy-MM-dd";

        public UpdateSummaryMessage(
            string symbol,
            string mmid,
            float bid,
            float ask,
            int bidSize,
            int askSize,
            DateTime bidTime,
            DateTime date,
            string conditionCode,
            DateTime askTime,
            char bidInfoValid,
            char askInfoValid,
            char endOfMessageGroup)
        {
            Symbol = symbol;
            MMID = mmid;
            Bid = bid;
            Ask = ask;
            BidSize = bidSize;
            AskSize = askSize;
            BidTime = bidTime.TimeOfDay;
            Date = date;
            ConditionCode = conditionCode;
            AskTime = askTime.TimeOfDay;
            BidInfoValid = bidInfoValid;
            AskInfoValid = askInfoValid;
            EndOfMessageGroup = endOfMessageGroup;
        }

        public string Symbol { get; }
        public string MMID { get; }
        public float Bid { get; }
        public float Ask { get; }
        public int BidSize { get; }
        public int AskSize { get; }
        public TimeSpan BidTime { get; }
        public DateTime Date { get; }
        public string ConditionCode { get; }
        public TimeSpan AskTime { get; }
        public char BidInfoValid { get; }
        public char AskInfoValid { get; }
        public char EndOfMessageGroup { get; }

        public static UpdateSummaryMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];
            var mmid = values[2];
            float.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var bid);
            float.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var ask);
            int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var bidSize);
            int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var askSize);
            DateTime.TryParseExact(values[7], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var bidTime);
            DateTime.TryParseExact(values[8], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            var conditionCode = values[9];
            DateTime.TryParseExact(values[10], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var askTime);
            char.TryParse(values[11], out var bidInfoValid);
            char.TryParse(values[12], out var askInfoValid); 
            char.TryParse(values[13], out var endOfMessageGroup);

            return new UpdateSummaryMessage(
                symbol,
                mmid,
                bid,
                ask,
                bidSize,
                askSize,
                bidTime,
                date,
                conditionCode,
                askTime,
                bidInfoValid,
                askInfoValid,
                endOfMessageGroup
            );
        }

        public override bool Equals(object obj)
        {
            return obj is UpdateSummaryMessage message &&
                   Symbol == message.Symbol &&
                   MMID == message.MMID &&
                   Bid == message.Bid &&
                   Ask == message.Ask &&
                   BidSize == message.BidSize &&
                   AskSize == message.AskSize &&
                   BidTime == message.BidTime &&
                   BidSize == message.BidSize &&
                   Date == message.Date &&
                   ConditionCode == message.ConditionCode &&
                   AskTime == message.AskTime &&
                   BidInfoValid == message.BidInfoValid &&
                   AskInfoValid == message.AskInfoValid &&
                   EndOfMessageGroup == message.EndOfMessageGroup;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + MMID.GetHashCode();
                hash = hash * 29 + Bid.GetHashCode();
                hash = hash * 29 + Ask.GetHashCode();
                hash = hash * 29 + BidSize.GetHashCode();
                hash = hash * 29 + AskSize.GetHashCode();
                hash = hash * 29 + BidTime.GetHashCode();
                hash = hash * 29 + BidSize.GetHashCode();
                hash = hash * 29 + Date.GetHashCode();
                hash = hash * 29 + ConditionCode.GetHashCode();
                hash = hash * 29 + AskTime.GetHashCode();
                hash = hash * 29 + BidInfoValid.GetHashCode();
                hash = hash * 29 + AskInfoValid.GetHashCode();
                hash = hash * 29 + EndOfMessageGroup.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(MMID)}: {MMID}, {nameof(Bid)}: {Bid}, {nameof(Ask)}: {Ask}, {nameof(BidSize)}: {BidSize}, {nameof(AskSize)}: {AskSize}, {nameof(BidTime)}: {BidTime}, {nameof(Date)}: {Date}, {nameof(ConditionCode)}: {ConditionCode}, {nameof(AskTime)}: {AskTime}, {nameof(BidInfoValid)}: {BidInfoValid}, {nameof(AskInfoValid)}: {AskInfoValid}, {nameof(EndOfMessageGroup)}: {EndOfMessageGroup}";
        }
    }
}