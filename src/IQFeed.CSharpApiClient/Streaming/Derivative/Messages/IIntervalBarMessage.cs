using System;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Messages
{
    public interface IIntervalBarMessage<T>
    {
        string RequestId { get; }
        IntervalBarType Type { get; }
        string Symbol { get; }
        DateTime Timestamp { get; }
        T Open { get; }
        T High { get; }
        T Low { get; }
        T Last { get; }

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