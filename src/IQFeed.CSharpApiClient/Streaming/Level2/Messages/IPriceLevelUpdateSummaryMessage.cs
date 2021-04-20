using System;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public interface IPriceLevelUpdateSummaryMessage
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
        /// The price value of the price level that was added/updated
        /// </summary>
        double Price { get; }

        /// <summary>
        /// The total size of all orders at this price level
        /// </summary>
        int Size { get; }

        /// <summary>
        /// The total number of orders at this price level
        /// </summary>
        int OrderCount { get; }

        /// <summary>
        /// Number of places of decimal precision for the price field
        /// </summary>
        int Precision { get; set; }

        /// <summary>
        /// The time of the most current order that affected this price level
        /// </summary>
        TimeSpan Time { get; }

        /// <summary>
        /// The date of the most current order that affected this price level
        /// </summary>
        DateTime Date { get; }

        DateTime OrderDateTime { get; }
        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}