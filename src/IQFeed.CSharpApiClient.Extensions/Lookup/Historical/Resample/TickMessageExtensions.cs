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
        public static IEnumerable<HistoricalBar> ToHistoricalBars(
            this IEnumerable<ITickMessage> tickMessages,
            TimeSpan interval,
            DataDirection direction = DataDirection.Newest)
        {
            return direction == DataDirection.Newest
                ? ToHistoricalBarsDescending(tickMessages, interval)
                : ToHistoricalBarsAscending(tickMessages, interval);
        }

        public static IEnumerable<HistoricalBar> ToHistoricalBars(
            this IEnumerable<ITickMessage> tickMessages,
            int ticks,
            DataDirection direction = DataDirection.Newest)
        {
            return direction == DataDirection.Newest
                ? ToHistoricalBarsDescending(tickMessages, ticks)
                : ToHistoricalBarsAscending(tickMessages, ticks);
        }

        public static IEnumerable<HistoricalBar> ToHistoricalBarsUncompressed(
            this IEnumerable<ITickMessage> tickMessages,
            TimeSpan interval,
            DataDirection direction = DataDirection.Newest)
        {
            return direction == DataDirection.Newest
                ? ToHistoricalBarsUncompressedDescending(tickMessages, interval)
                : ToHistoricalBarsUncompressedAscending(tickMessages, interval);
        }

        private static IEnumerable<HistoricalBar> ToHistoricalBarsAscending(this IEnumerable<ITickMessage> tickMessages, TimeSpan interval)
        {
            var intervalTicks = interval.Ticks;
            DateTime? nextTimestamp = null;
            DateTime? currentDate = null;
            HistoricalBar currentBar = null;

            var totalVolume = 0;
            var totalTrade = 0;

            foreach (var tick in tickMessages)
            {
                // skip if trade size is zero
                if (tick.LastSize == 0)
                    continue;

                // Check if we need to close off the current bar before we do anything else
                if (currentBar != null && tick.Timestamp >= nextTimestamp)
                {
                    // We've reached the end of this bar. Send it out! Setting the currentBar to null
                    //   will cause a new bar to be generated on the first valid tick, below.
                    currentBar.VWAP = currentBar.VWAP / currentBar.PeriodVolume;
                    yield return currentBar;

                    currentBar = null;
                }

                // Check for date change up here otherwise 'O' ticks will be ignored
                //   if they are the first ticks of the day, which will make total volume incorrect
                if (currentDate != null && tick.Timestamp.Date != currentDate)
                {
                    // Date has changed. Reset the numbers for the next one
                    // Don't start a new bar here, because if the next tick is an 'O' we want to count the volume
                    //  but not the tick, and if it's the only tick in a bar, then there shouldn't be a bar! (Apparently)
                    totalVolume = 0;
                    totalTrade = 0;
                    currentDate = tick.Timestamp.Date;
                }

                totalVolume += tick.LastSize;
                totalTrade += 1;

                // to effect the price the trade must be C or E
                if (tick.BasisForLast == 'O')
                    continue;

                if (currentBar == null)
                {
                    // if we get here, we have a valid trade tick, and we need a new bar
                    var currentTimestamp = tick.Timestamp.Trim(intervalTicks);
                    currentBar = new HistoricalBar(
                        currentTimestamp,
                        tick.Last,
                        tick.Last,
                        tick.Last,
                        tick.Last,
                        0,
                        0,
                        0,
                        0,
                        tick.Last * tick.LastSize);
                    nextTimestamp = currentTimestamp.AddTicks(intervalTicks);
                    currentDate = currentTimestamp.Date;
                }

                if (tick.Last < currentBar.Low)
                    currentBar.Low = tick.Last;

                if (tick.Last > currentBar.High)
                    currentBar.High = tick.Last;

                currentBar.Close = tick.Last;

                currentBar.PeriodVolume += tick.LastSize;
                currentBar.PeriodTrade += 1;

                currentBar.TotalVolume = totalVolume;
                currentBar.TotalTrade = totalTrade;

                currentBar.VWAP += tick.Last * tick.LastSize;
            }

            // return the last created bar when last tick reached
            if (currentBar != null)
            {
                currentBar.VWAP = currentBar.VWAP / currentBar.PeriodVolume;
                yield return currentBar;
            }
        }

        private static IEnumerable<HistoricalBar> ToHistoricalBarsDescending(this IEnumerable<ITickMessage> tickMessages, TimeSpan interval)
        {
            return tickMessages.Reverse().ToHistoricalBarsAscending(interval).Reverse();
        }

        private static IEnumerable<HistoricalBar> ToHistoricalBarsAscending(this IEnumerable<ITickMessage> tickMessages, int ticks)
        {
            HistoricalBar currentBar = null;

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

                if (currentBar != null)
                {
                    // reset the counts if dates differ
                    if (tick.Timestamp.Date != currentBar.Timestamp.Date)
                    {
                        totalVolume = tick.LastSize;
                        totalTrade = 1;
                    }
                    else if (currentBar.PeriodTrade < ticks)
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

                        currentBar.VWAP += tick.Last * tick.LastSize;
                        continue;
                    }

                    currentBar.VWAP = currentBar.VWAP / currentBar.PeriodVolume;
                    yield return currentBar;
                }

                currentBar = new HistoricalBar(
                    tick.Timestamp,
                    tick.Last,
                    tick.Last,
                    tick.Last,
                    tick.Last,
                    totalVolume,
                    tick.LastSize,
                    totalTrade,
                    1,
                    tick.Last * tick.LastSize);
            }

            // return the last created bar when last tick reached
            if (currentBar != null)
            {
                currentBar.VWAP = currentBar.VWAP / currentBar.PeriodVolume;
                yield return currentBar;
            }
        }

        private static IEnumerable<HistoricalBar> ToHistoricalBarsDescending(this IEnumerable<ITickMessage> tickMessages, int ticks)
        {
            return tickMessages.Reverse().ToHistoricalBarsAscending(ticks).Reverse();
        }

        private static IEnumerable<HistoricalBar> ToHistoricalBarsUncompressedAscending(this IEnumerable<ITickMessage> tickMessages, TimeSpan interval)
        {
            var intervalTicks = interval.Ticks;
            var nextTimestamp = new DateTime();
            HistoricalBar previousBar = null;

            foreach (var bar in tickMessages.ToHistoricalBars(interval, DataDirection.Oldest))
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

        private static IEnumerable<HistoricalBar> ToHistoricalBarsUncompressedDescending(this IEnumerable<ITickMessage> tickMessages, TimeSpan interval)
        {
            return tickMessages.Reverse().ToHistoricalBarsUncompressedAscending(interval).Reverse();
        }
    }
}