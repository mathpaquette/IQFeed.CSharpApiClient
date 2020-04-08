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
        public static IEnumerable<HistoricalBar<double>> ToHistoricalBars(
            this IEnumerable<IIntervalMessage<double>> intervalMessages,
            TimeSpan interval,
            DataDirection dataDirection = DataDirection.Newest)
        {
            return dataDirection == DataDirection.Newest
                ? intervalMessages.ToHistoricalBarsDescending(interval)
                : intervalMessages.ToHistoricalBarsAscending(interval);
        }

        private static IEnumerable<HistoricalBar<double>> ToHistoricalBarsAscending(this IEnumerable<IIntervalMessage<double>> intervalMessages, TimeSpan timeInterval)
        {
            var intervalTicks = timeInterval.Ticks;
            DateTime? nextTimestamp = null;
            HistoricalBar<double> currentBar = null;

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

                    currentBar.Wap += (interval.High + interval.Low + interval.Close) / 3 * interval.PeriodVolume; // VWAP estimation
                    continue;
                }

                if (currentBar != null)
                {
                    // reset the counts if dates differ
                    if (interval.Timestamp.Date != currentBar.Timestamp.Date)
                    {
                        totalTrade = 0;
                    }

                    currentBar.Wap = currentBar.Wap / currentBar.PeriodVolume;
                    yield return currentBar;
                }

                totalTrade += interval.NumberOfTrades;

                var currentTimestamp = interval.Timestamp.Trim(intervalTicks);
                nextTimestamp = currentTimestamp.AddTicks(intervalTicks);

                currentBar = new HistoricalBar<double>()
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

                    Wap = (interval.High + interval.Low + interval.Close) / 3 * interval.PeriodVolume // VWAP estimation
                };
            }

            // return the last created bar when last interval reached
            if (currentBar != null)
            {
                currentBar.Wap = currentBar.Wap / currentBar.PeriodVolume;
                yield return currentBar;
            }
        }

        private static IEnumerable<HistoricalBar<double>> ToHistoricalBarsDescending(this IEnumerable<IIntervalMessage<double>> intervals, TimeSpan interval)
        {
            return intervals.Reverse().ToHistoricalBarsAscending(interval);
        }
    }
}