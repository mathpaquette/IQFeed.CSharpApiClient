using System;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public interface IOrderAddUpdateSummaryMessage
    {
        /// <summary>
        /// Level 2 Message Type
        /// </summary>
        Level2MessageType MessageType { get; }

        /// <summary>
        /// The Security's Stock Symbol
        /// </summary>
        string Symbol { get; }

        /// <summary>
        /// The unique (per symbol) order identifier for this symbol.
        /// Only used in Market By Order messages.
        /// Will be blank for Nasdaq Level 2 messages
        /// </summary>
        UInt64 OrderId { get; }

        /// <summary>
        /// Market Maker ID - Only used for Nasdaq Level 2 messages.
        /// Will be blank for Market By Order Messages.
        /// </summary>
        string MMID { get; }

        /// <summary>
        /// Buy or Sell side
        /// </summary>
        Level2Side Side { get; }

        /// <summary>
        /// The price value of the price level that was added/updated
        /// </summary>
        double Price { get; }

        /// <summary>
        /// The size of this order
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Used to position orders of the same price and side. Priority ranks from lowest to highest.
        /// </summary>
        UInt64 OrderPriority { get; }

        /// <summary>
        /// Number of places of decimal precision for the price field
        /// </summary>
        int Precision { get; set; }

        /// <summary>
        /// The time of this order
        /// </summary>
        TimeSpan OrderTime { get; }

        /// <summary>
        /// The date of this order
        /// </summary>
        DateTime OrderDate { get; }

        DateTime OrderDateTime { get; }
        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}