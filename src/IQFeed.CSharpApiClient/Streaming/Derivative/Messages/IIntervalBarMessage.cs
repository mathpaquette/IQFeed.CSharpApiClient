using System;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Messages
{
    public interface IIntervalBarMessage
    {
        string RequestId { get; }
        IntervalBarType Type { get; }
        string Symbol { get; }
        DateTime Timestamp { get; }
        double Open { get; }
        double High { get; }
        double Low { get; }
        double Last { get; }

        /// <summary>
        /// Last cummulative volume in the interval
        /// </summary>
        int CummulativeVolume { get; }

        /// <summary>
        /// Interval volume for the interval
        /// </summary>
        int IntervalVolume { get; }

        /// <summary>
        /// Number of trades in the interval (only valid for tick interval)
        /// </summary>
        int NumberOfTrades { get; }
    }
}