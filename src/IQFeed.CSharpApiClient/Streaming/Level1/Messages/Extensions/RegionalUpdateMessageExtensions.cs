using System.Collections.Generic;
using System.Linq;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages.Extensions
{
    public static class RegionalUpdateMessageExtensions
    {
        public static IEnumerable<RegionalUpdateMessage<double>> ToDouble(this IEnumerable<IRegionalUpdateMessage<decimal>> messages)
        {
            return messages.Select(message => message.ToDouble());
        }

        public static IEnumerable<RegionalUpdateMessage<float>> ToFloat(this IEnumerable<IRegionalUpdateMessage<decimal>> messages)
        {
            return messages.Select(message => message.ToFloat());
        }

        public static IEnumerable<RegionalUpdateMessage<float>> ToFloat(this IEnumerable<IRegionalUpdateMessage<double>> messages)
        {
            return messages.Select(message => message.ToFloat());
        }

        public static RegionalUpdateMessage<double> ToDouble(this IRegionalUpdateMessage<decimal> message)
        {
            return new RegionalUpdateMessage<double>(
                message.Symbol,
                message.Exchange,
                (double)message.RegionalBid,
                message.RegionalBidSize,
                message.RegionalBidTime,
                (double)message.RegionalAsk,
                message.RegionalAskSize,
                message.RegionalAskTime,
                message.FractionDisplayCode,
                message.DecimalPrecision,
                message.MarketCenter);
        }

        public static RegionalUpdateMessage<float> ToFloat(this IRegionalUpdateMessage<decimal> message)
        {
            return new RegionalUpdateMessage<float>(
                message.Symbol,
                message.Exchange,
                (float)message.RegionalBid,
                message.RegionalBidSize,
                message.RegionalBidTime,
                (float)message.RegionalAsk,
                message.RegionalAskSize,
                message.RegionalAskTime,
                message.FractionDisplayCode,
                message.DecimalPrecision,
                message.MarketCenter);
        }

        public static RegionalUpdateMessage<float> ToFloat(this IRegionalUpdateMessage<double> message)
        {
            return new RegionalUpdateMessage<float>(
                message.Symbol,
                message.Exchange,
                (float)message.RegionalBid,
                message.RegionalBidSize,
                message.RegionalBidTime,
                (float)message.RegionalAsk,
                message.RegionalAskSize,
                message.RegionalAskTime,
                message.FractionDisplayCode,
                message.DecimalPrecision,
                message.MarketCenter);
        }
    }
}