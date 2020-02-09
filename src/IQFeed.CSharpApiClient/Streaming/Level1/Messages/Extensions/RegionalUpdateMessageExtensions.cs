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
        
        public static IEnumerable<RegionalUpdateMessage<decimal>> ToDecimal(this IEnumerable<IRegionalUpdateMessage<double>> messages)
        {
            return messages.Select(message => message.ToDecimal());
        }

        public static IEnumerable<RegionalUpdateMessage<float>> ToFloat(this IEnumerable<IRegionalUpdateMessage<double>> messages)
        {
            return messages.Select(message => message.ToFloat());
        }

        public static IEnumerable<RegionalUpdateMessage<decimal>> ToDecimal(this IEnumerable<IRegionalUpdateMessage<float>> messages)
        {
            return messages.Select(message => message.ToDecimal());
        }

        public static IEnumerable<RegionalUpdateMessage<double>> ToDouble(this IEnumerable<IRegionalUpdateMessage<float>> messages)
        {
            return messages.Select(message => message.ToDouble());
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

        public static RegionalUpdateMessage<decimal> ToDecimal(this IRegionalUpdateMessage<double> message)
        {
            return new RegionalUpdateMessage<decimal>(
                message.Symbol,
                message.Exchange,
                (decimal)message.RegionalBid,
                message.RegionalBidSize,
                message.RegionalBidTime,
                (decimal)message.RegionalAsk,
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

        public static RegionalUpdateMessage<decimal> ToDecimal(this IRegionalUpdateMessage<float> message)
        {
            return new RegionalUpdateMessage<decimal>(
                message.Symbol,
                message.Exchange,
                (decimal)message.RegionalBid,
                message.RegionalBidSize,
                message.RegionalBidTime,
                (decimal)message.RegionalAsk,
                message.RegionalAskSize,
                message.RegionalAskTime,
                message.FractionDisplayCode,
                message.DecimalPrecision,
                message.MarketCenter);
        }

        public static RegionalUpdateMessage<double> ToDouble(this IRegionalUpdateMessage<float> message)
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
    }
}