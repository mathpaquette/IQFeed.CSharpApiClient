using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class TradeConditionMessage : ITradeCondition
    {
        public TradeConditionMessage(int tradeConditionId, string shortName, string longName, string requestId = null)
        {
            RequestId = requestId;
            TradeConditionId = tradeConditionId;
            ShortName = shortName;
            LongName = longName;
        }

        public string RequestId { get; }
        public int TradeConditionId { get; }
        public string ShortName { get; }
        public string LongName { get; }

        public static TradeConditionMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new TradeConditionMessage(
                int.Parse(values[0], CultureInfo.InvariantCulture), 
                values[1], 
                values[2]);
        }

        public static TradeConditionMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new TradeConditionMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],
                values[3],
                requestId);
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
                hash = hash * 29 + RequestId.GetHashCode();
                hash = hash * 29 + TradeConditionId.GetHashCode();
                hash = hash * 29 + ShortName.GetHashCode();
                hash = hash * 29 + LongName.GetHashCode();
                return hash;
            }
        }
    }
}