using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class TradeConditionMessage
    {
        public TradeConditionMessage(int tradeConditionId, string shortName, string longName, string requestId = null)
        {
            TradeConditionId = tradeConditionId;
            ShortName = shortName;
            LongName = longName;
            RequestId = requestId;
        }

        public int TradeConditionId { get; private set; }
        public string ShortName { get; private set; }
        public string LongName { get; private set; }
        public string RequestId { get; private set; }

        public static TradeConditionMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            if (values[0] == SymbolDefault.SymbolsDataId)
            {
                return new TradeConditionMessage(
                    int.Parse(values[1], CultureInfo.InvariantCulture),
                    values[2],
                    values[3]);
            }

            return new TradeConditionMessage(
                int.Parse(values[0], CultureInfo.InvariantCulture),
                values[1],
                values[2]);
        }

        public static TradeConditionMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            if (values[1] == SymbolDefault.SymbolsDataId)
            {
                return new TradeConditionMessage(
                    int.Parse(values[2], CultureInfo.InvariantCulture),
                    values[3],
                    values[4],
                    values[0]);
            }

            return new TradeConditionMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],
                values[3],
                values[0]);
        }

        public override bool Equals(object obj)
        {
            return obj is TradeConditionMessage message &&
                   RequestId == message.RequestId &&
                   TradeConditionId == message.TradeConditionId &&
                   ShortName == message.ShortName &&
                   LongName == message.LongName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + (RequestId != null ? RequestId.GetHashCode() : 0);
                hash = hash * 29 + TradeConditionId.GetHashCode();
                hash = hash * 29 + ShortName.GetHashCode();
                hash = hash * 29 + LongName.GetHashCode();
                return hash;
            }
        }
        public override string ToString()
        {
            return $"{nameof(TradeConditionId)}: {TradeConditionId}, {nameof(ShortName)}: {ShortName}, {nameof(LongName)}: {LongName}, {nameof(RequestId)}: {RequestId}";
        }
    }
}