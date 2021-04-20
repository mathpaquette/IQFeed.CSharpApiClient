using System;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public interface IOrderDeleteMessage
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
        /// Buy or Sell side
        /// </summary>
        Level2Side Side { get; }

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