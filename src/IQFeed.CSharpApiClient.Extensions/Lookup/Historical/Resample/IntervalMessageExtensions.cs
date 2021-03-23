using System;
using System.Collections.Generic;
using System.Linq;
using IQFeed.CSharpApiClient.Extensions.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Extensions.Lookup.Historical.Resample
{
    public static class IntervalMessageExtensions
    {
        public static IEnumerable<HistoricalBar> ToHistoricalBars(
            this IEnumerable<IIntervalMessage> intervalMessages,
            TimeSpan interval,
            DataDirection dataDirection = DataDirection.Newest)
        {
            return dataDirection == DataDirection.Newest
                ? intervalMessages.ToHistoricalBarsDescending(interval)
                : intervalMessages.ToHistoricalBarsAscending(interval);
        }

        public static IEnumerable<HistoricalBar> ToHistoricalBarsUncompressed(
            this IEnumerable<IIntervalMessage> intervalMessages,
            TimeSpan interval,
            DataDirection dataDirection = DataDirection.Newest)
        {
            return dataDirection == DataDirection.Newest
                ? intervalMessages.ToHistoricalBarsUncompressedDescending(interval)
                : intervalMessages.ToHistoricalBarsUncompressedAscending(interval);
        }

        private static IEnumerable<HistoricalBar> ToHistoricalBarsAscending(this IEnumerable<IIntervalMessage> intervalMessages, TimeSpan timeInterval)
        {
            var intervalTicks = timeInterval.Ticks;
            DateTime? nextTimestamp = null;
            HistoricalBar currentBar = null;

            var totalTrade = 0;

            foreach (var interval in intervalMessages)
            {
                if (interval.Timestamp < nextTimestamp)
                {
                    totalTrade += interval.NumberOfTrades;

                    if (interval.Low < currentBar.Low)
                        currentBar.Low = interval.Low;

                    if (interval.High > currentBar.High)
                        currentBar.High = interval.High;

                    currentBar.Close = interval.Close;

                    currentBar.PeriodVolume += interval.PeriodVolume;
                    currentBar.PeriodTrade += interval.NumberOfTrades;

                    currentBar.TotalVolume = interval.TotalVolume;
                    currentBar.TotalTrade = totalTrade;

                    currentBar.VWAP += (interval.High + interval.Low + interval.Close) / 3 * interval.PeriodVolume; // VWAP estimation
                    continue;
                }

                if (currentBar != null)
                {
                    // reset the counts if dates differ
                    if (interval.Timestamp.Date != currentBar.Timestamp.Date)
                    {
                        totalTrade = 0;
                    }

                    currentBar.VWAP = currentBar.VWAP / currentBar.PeriodVolume;
                    yield return currentBar;
                }

                totalTrade += interval.NumberOfTrades;

                var currentTimestamp = interval.Timestamp.Trim(intervalTicks);
                nextTimestamp = currentTimestamp.AddTicks(intervalTicks);

                currentBar = new HistoricalBar()
                {
                    Timestamp = currentTimestamp,
                    Open = interval.Open,
                    High = interval.High,
                    Low = interval.Low,
                    Close = interval.Close,

                    PeriodVolume = interval.PeriodVolume,
                    PeriodTrade = interval.NumberOfTrades,

                    TotalVolume = interval.TotalVolume,
                    TotalTrade = totalTrade,

                    VWAP = (interval.High + interval.Low + interval.Close) / 3 * interval.PeriodVolume // VWAP estimation
                };
            }

            // return the last created bar when last interval reached
            if (currentBar != null)
            {
                currentBar.VWAP = currentBar.VWAP / currentBar.PeriodVolume;
                yield return currentBar;
            }
        }

        private static IEnumerable<HistoricalBar> ToHistoricalBarsDescending(this IEnumerable<IIntervalMessage> intervals, TimeSpan interval)
        {
            return intervals.Reverse().ToHistoricalBarsAscending(interval).Reverse();
        }

        private static IEnumerable<HistoricalBar> ToHistoricalBarsUncompressedAscending(this IEnumerable<IIntervalMessage> intervalMessages, TimeSpan interval)
        {
            var intervalTicks = interval.Ticks;
            var nextTimestamp = new DateTime();
            HistoricalBar previousBar = null;

            foreach (var bar in intervalMessages.ToHistoricalBars(interval, DataDirection.Oldest))
            {
                if (bar.Timestamp != nextTimestamp && previousBar != null)
                {
                    while (bar.Timestamp != nextTimestamp)
                    {
                        yield return new HistoricalBar()
                        {
                            Timestamp = nextTimestamp,
                            Open = previousBar.Close,
                            High = previousBar.Close,
                            Low = previousBar.Close,
                            Close = previousBar.Close,
                            PeriodVolume = 0,
                            PeriodTrade = 0,
                            TotalVolume = previousBar.TotalVolume,
                            TotalTrade = previousBar.TotalTrade,
                            VWAP = previousBar.Close
                        };

                        nextTimestamp = nextTimestamp.AddTicks(intervalTicks);
                    }
                }

                previousBar = bar;
                nextTimestamp = bar.Timestamp.AddTicks(intervalTicks);

                yield return bar;
            }
        }

        private static IEnumerable<HistoricalBar> ToHistoricalBarsUncompressedDescending(this IEnumerable<IIntervalMessage> intervalMessages, TimeSpan interval)
        {
            return intervalMessages.Reverse().ToHistoricalBarsUncompressedAscending(interval).Reverse();
        }
    }
}