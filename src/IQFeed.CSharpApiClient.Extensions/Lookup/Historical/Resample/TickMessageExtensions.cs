using System;
using System.Collections.Generic;
using System.Linq;
using IQFeed.CSharpApiClient.Extensions.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Extensions.Lookup.Historical.Resample
{
    public static class TickMessageExtensions
    {
        public static IEnumerable<HistoricalBar<double>> ToHistoricalBars(
            this IEnumerable<ITickMessage<double>> tickMessages,
            TimeSpan interval,
            DataDirection direction = DataDirection.Newest)
        {
            return direction == DataDirection.Newest
                ? ToHistoricalBarsDescending(tickMessages, interval)
                : ToHistoricalBarsAscending(tickMessages, interval);
        }

        private static IEnumerable<HistoricalBar<double>> ToHistoricalBarsAscending(this IEnumerable<ITickMessage<double>> tickMessages, TimeSpan interval)
        {
            var intervalTicks = interval.Ticks;
            DateTime? nextTimestamp = null;
            HistoricalBar<double> currentBar = null;

            var totalVolume = 0;
            var totalTrade = 0;

            foreach (var tick in tickMessages)
            {
                // skip if trade size is zero
                if (tick.LastSize == 0)
                    continue;

                totalVolume += tick.LastSize;
                totalTrade += 1;

                // to effect the price the trade must be C or E
                if (tick.BasisForLast == 'O')
                    continue;

                if (tick.Timestamp < nextTimestamp)
                {
                    if (tick.Last < currentBar.Low)
                        currentBar.Low = tick.Last;

                    if (tick.Last > currentBar.High)
                        currentBar.High = tick.Last;

                    currentBar.Close = tick.Last;

                    currentBar.PeriodVolume += tick.LastSize;
                    currentBar.PeriodTrade += 1;

                    currentBar.TotalVolume = totalVolume;
                    currentBar.TotalTrade = totalTrade;

                    currentBar.Wap += tick.Last * tick.LastSize;
                    continue;
                }

                if (currentBar != null)
                {
                    // reset the counts if dates differ
                    if (tick.Timestamp.Date != currentBar.Timestamp.Date)
                    {
                        totalVolume = tick.LastSize;
                        totalTrade = 1;
                    }

                    currentBar.Wap = currentBar.Wap / currentBar.PeriodVolume;
                    yield return currentBar;
                }

                var currentTimestamp = tick.Timestamp.Trim(intervalTicks);
                nextTimestamp = currentTimestamp.AddTicks(intervalTicks);

                currentBar = new HistoricalBar<double>(
                    currentTimestamp,
                    tick.Last,
                    tick.Last,
                    tick.Last,
                    tick.Last,
                    tick.LastSize,
                    1,
                    totalVolume,
                    totalTrade,
                    tick.Last * tick.LastSize);
            }

            // return the last created bar when last tick reached
            if (currentBar != null)
            {
                currentBar.Wap = currentBar.Wap / currentBar.PeriodVolume;
                yield return currentBar;
            }
        }

        private static IEnumerable<HistoricalBar<double>> ToHistoricalBarsDescending(this IEnumerable<ITickMessage<double>> tickMessages, TimeSpan interval)
        {
            return tickMessages.Reverse().ToHistoricalBarsAscending(interval);
        }
    }
}