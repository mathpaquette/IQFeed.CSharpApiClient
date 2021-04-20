using System;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public interface IPriceLevelDeleteMessage
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
        /// Buy or Sell side
        /// </summary>
        Level2Side Side { get; }

        /// <summary>
        /// The price value of the price level that was deleted
        /// </summary>
        double Price { get; }

        /// <summary>
        /// The time of this order
        /// </summary>
        TimeSpan Time { get; }

        /// <summary>
        /// The date of this order
        /// </summary>
        DateTime Date { get; }

        DateTime DateTime { get; }
        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}