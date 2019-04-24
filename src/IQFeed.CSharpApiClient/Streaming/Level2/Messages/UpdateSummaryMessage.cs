using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public class UpdateSummaryMessage
    {
        public const string UpdateMessageTimeFormat = "HH:mm:ss.ffffff";
        public const string UpdateMessageDateFormat = "yyyy-MM-dd";

        public UpdateSummaryMessage(string symbol,
                                    string mmID,
                                    float bid,
                                    float ask,
                                    int bidSize,
                                    int askSize,
                                    DateTime bidTime,
                                    DateTime date,
                                    string conditionCode,
                                    DateTime askTime,
                                    string bidInfoValid,
                                    string askInfoValid,
                                    string endOfMessageGroup)
        {
            Symbol = symbol;
            MMID = mmID;
            Bid = bid;
            Ask = ask;
            BidSize = bidSize;
            AskSize = askSize;
            BidTime = bidTime;
            Date = date;
            ConditionCode = conditionCode;
            AskTime = askTime;
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
        public DateTime BidTime { get; }
        public DateTime Date { get; }
        public string ConditionCode { get; }
        public DateTime AskTime { get; }
        public string BidInfoValid { get; }
        public string AskInfoValid { get; }
        public string EndOfMessageGroup { get; }

        public override string ToString()
        {
            return $"{Symbol} {MMID} {Bid:f2} {Ask:f2} {BidSize,8:d} {AskSize,8:d} {BidTime: HH:mm:ss.ffffff} " +
                   $"{Date: MM/dd/yyyy} {ConditionCode} {AskTime: HH:mm:ss.ffffff} {BidInfoValid} {AskInfoValid} {EndOfMessageGroup} ";
        }//MM/dd/yyyy

        public static UpdateSummaryMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];
            var mmID = values[2];
            float.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var bid);
            float.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var ask);
            int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var bidSize);
            int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var askSize);
            DateTime.TryParseExact(values[7], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var bidTime);
            DateTime.TryParseExact(values[8], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            var conditionCode = values[9];
            DateTime.TryParseExact(values[10], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var askTime);
            var bidInfoValid = values[11];
            var askInfoValid = values[12];
            var endOfMessageGroup = values[13];

            return new UpdateSummaryMessage(
                symbol,
                mmID,
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
    }
}