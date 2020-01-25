using System.Collections.Generic;
using System.Linq;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages.Extensions
{
    public static class TickMessageExtensions
    {
        public static IEnumerable<TickMessage<double>> ToDouble(this IEnumerable<ITickMessage<decimal>> messages)
        {
            return messages.Select(message => message.ToDouble());
        }

        public static IEnumerable<TickMessage<float>> ToFloat(this IEnumerable<ITickMessage<decimal>> messages)
        {
            return messages.Select(message => message.ToFloat());
        }

        public static IEnumerable<TickMessage<float>> ToFloat(this IEnumerable<ITickMessage<double>> messages)
        {
            return messages.Select(message => message.ToFloat());
        }

        public static TickMessage<double> ToDouble(this ITickMessage<decimal> message)
        {
            return new TickMessage<double>(
                message.Timestamp,
                (double)message.Last,
                message.LastSize,
                message.TotalVolume,
                (double)message.Bid,
                (double)message.Ask,
                message.TickId,
                message.BasisForLast,
                message.TradeMarketCenter,
                message.TradeConditions,
                message.RequestId);
        }

        public static TickMessage<float> ToFloat(this ITickMessage<decimal> message)
        {
            return new TickMessage<float>(
                message.Timestamp,
                (float)message.Last,
                message.LastSize,
                message.TotalVolume,
                (float)message.Bid,
                (float)message.Ask,
                message.TickId,
                message.BasisForLast,
                message.TradeMarketCenter,
                message.TradeConditions,
                message.RequestId);
        }

        public static TickMessage<float> ToFloat(this ITickMessage<double> message)
        {
            return new TickMessage<float>(
                message.Timestamp,
                (float)message.Last,
                message.LastSize,
                message.TotalVolume, 
                (float)message.Bid,
                (float)message.Ask,
                message.TickId,
                message.BasisForLast,
                message.TradeMarketCenter,
                message.TradeConditions,
                message.RequestId);
        }
    }
}