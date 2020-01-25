﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages.Extensions
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

        public static IEnumerable<UpdateSummaryMessage<float>> ToFloat(this IEnumerable<IUpdateSummaryMessage<double>> messages)
        {
            return messages.Select(message => message.ToFloat());
        }

        public static UpdateSummaryMessage<double> ToDouble(this IUpdateSummaryMessage<decimal> message)
        {
            return new UpdateSummaryMessage<double>(
                message.Symbol,
                (double)message.MostRecentTrade, 
                message.MostRecentTradeSize,
                DateTime.Now + message.MostRecentTradeTime,
                message.MostRecentTradeMarketCenter, 
                message.TotalVolume,
                (double)message.Bid,
                message.BidSize,
                (double)message.Ask,
                message.AskSize,
                (double)message.Open,
                (double)message.High,
                (double)message.Low,
                (double)message.Close,
                message.MessageContents,
                message.MostRecentTradeConditions);
        }

        public static UpdateSummaryMessage<float> ToFloat(this IUpdateSummaryMessage<decimal> message)
        {
            return new UpdateSummaryMessage<float>(
                message.Symbol,
                (float)message.MostRecentTrade,
                message.MostRecentTradeSize,
                DateTime.Now + message.MostRecentTradeTime,
                message.MostRecentTradeMarketCenter,
                message.TotalVolume,
                (float)message.Bid,
                message.BidSize,
                (float)message.Ask,
                message.AskSize,
                (float)message.Open,
                (float)message.High,
                (float)message.Low,
                (float)message.Close,
                message.MessageContents,
                message.MostRecentTradeConditions);
        }

        public static UpdateSummaryMessage<float> ToFloat(this IUpdateSummaryMessage<double> message)
        {
            return new UpdateSummaryMessage<float>(
                message.Symbol,
                (float)message.MostRecentTrade,
                message.MostRecentTradeSize,
                DateTime.Now + message.MostRecentTradeTime,
                message.MostRecentTradeMarketCenter,
                message.TotalVolume,
                (float)message.Bid,
                message.BidSize,
                (float)message.Ask,
                message.AskSize,
                (float)message.Open,
                (float)message.High,
                (float)message.Low,
                (float)message.Close,
                message.MessageContents,
                message.MostRecentTradeConditions);
        }
    }
}