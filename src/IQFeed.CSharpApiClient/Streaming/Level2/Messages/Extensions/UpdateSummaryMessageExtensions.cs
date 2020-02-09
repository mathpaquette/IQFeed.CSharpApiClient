using System;
using System.Collections.Generic;
using System.Linq;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages.Extensions
{
    public static class UpdateSummaryMessageExtensions
    {
        public static IEnumerable<UpdateSummaryMessage<double>> ToDouble(this IEnumerable<IUpdateSummaryMessage<decimal>> messages)
        {
            return messages.Select(message => message.ToDouble());
        }

        public static IEnumerable<UpdateSummaryMessage<float>> ToFloat(this IEnumerable<IUpdateSummaryMessage<decimal>> messages)
        {
            return messages.Select(message => message.ToFloat());
        }

        public static IEnumerable<UpdateSummaryMessage<decimal>> ToDecimal(this IEnumerable<IUpdateSummaryMessage<double>> messages)
        {
            return messages.Select(message => message.ToDecimal());
        }

        public static IEnumerable<UpdateSummaryMessage<float>> ToFloat(this IEnumerable<IUpdateSummaryMessage<double>> messages)
        {
            return messages.Select(message => message.ToFloat());
        }

        public static IEnumerable<UpdateSummaryMessage<decimal>> ToDecimal(this IEnumerable<IUpdateSummaryMessage<float>> messages)
        {
            return messages.Select(message => message.ToDecimal());
        }

        public static IEnumerable<UpdateSummaryMessage<double>> ToDouble(this IEnumerable<IUpdateSummaryMessage<float>> messages)
        {
            return messages.Select(message => message.ToDouble());
        }

        public static UpdateSummaryMessage<double> ToDouble(this IUpdateSummaryMessage<decimal> message)
        {
            return new UpdateSummaryMessage<double>(
                message.Symbol,
                message.MMID,
                (double)message.Bid,
                (double)message.Ask, 
                message.BidSize,
                message.AskSize,
                DateTime.Now + message.BidTime, 
                message.Date, 
                message.ConditionCode,
                DateTime.Now + message.AskTime,
                message.BidInfoValid,
                message.AskInfoValid,
                message.EndOfMessageGroup);
        }

        public static UpdateSummaryMessage<float> ToFloat(this IUpdateSummaryMessage<decimal> message)
        {
            return new UpdateSummaryMessage<float>(
                message.Symbol,
                message.MMID,
                (float)message.Bid,
                (float)message.Ask,
                message.BidSize,
                message.AskSize,
                DateTime.Now + message.BidTime,
                message.Date,
                message.ConditionCode,
                DateTime.Now + message.AskTime,
                message.BidInfoValid,
                message.AskInfoValid,
                message.EndOfMessageGroup);
        }

        public static UpdateSummaryMessage<decimal> ToDecimal(this IUpdateSummaryMessage<double> message)
        {
            return new UpdateSummaryMessage<decimal>(
                message.Symbol,
                message.MMID,
                (decimal)message.Bid,
                (decimal)message.Ask,
                message.BidSize,
                message.AskSize,
                DateTime.Now + message.BidTime,
                message.Date,
                message.ConditionCode,
                DateTime.Now + message.AskTime,
                message.BidInfoValid,
                message.AskInfoValid,
                message.EndOfMessageGroup);
        }

        public static UpdateSummaryMessage<float> ToFloat(this IUpdateSummaryMessage<double> message)
        {
            return new UpdateSummaryMessage<float>(
                message.Symbol,
                message.MMID,
                (float)message.Bid,
                (float)message.Ask,
                message.BidSize,
                message.AskSize,
                DateTime.Now + message.BidTime,
                message.Date,
                message.ConditionCode,
                DateTime.Now + message.AskTime,
                message.BidInfoValid,
                message.AskInfoValid,
                message.EndOfMessageGroup);
        }

        public static UpdateSummaryMessage<decimal> ToDecimal(this IUpdateSummaryMessage<float> message)
        {
            return new UpdateSummaryMessage<decimal>(
                message.Symbol,
                message.MMID,
                (decimal)message.Bid,
                (decimal)message.Ask,
                message.BidSize,
                message.AskSize,
                DateTime.Now + message.BidTime,
                message.Date,
                message.ConditionCode,
                DateTime.Now + message.AskTime,
                message.BidInfoValid,
                message.AskInfoValid,
                message.EndOfMessageGroup);
        }

        public static UpdateSummaryMessage<double> ToDouble(this IUpdateSummaryMessage<float> message)
        {
            return new UpdateSummaryMessage<double>(
                message.Symbol,
                message.MMID,
                (double)message.Bid,
                (double)message.Ask,
                message.BidSize,
                message.AskSize,
                DateTime.Now + message.BidTime,
                message.Date,
                message.ConditionCode,
                DateTime.Now + message.AskTime,
                message.BidInfoValid,
                message.AskInfoValid,
                message.EndOfMessageGroup);
        }
    }
}