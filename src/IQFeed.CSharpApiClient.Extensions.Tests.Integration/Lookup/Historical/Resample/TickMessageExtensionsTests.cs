using System;
using System.Linq;
using IQFeed.CSharpApiClient.Extensions.Lookup.Historical.Resample;
using IQFeed.CSharpApiClient.Extensions.Tests.Integration.TestData;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Extensions.Tests.Integration.Lookup.Historical.Resample
{
    public class TickMessageExtensionsTests
    {
        [Test]
        public void Should_Resample_Bars_From_Ticks()
        {
            // Arrange
            var ticks = TickMessage.ParseFromFile(TestData.TestData.GetFileName(TestDataType.Ticks, DataDirection.Oldest)).ToList();
            var intervals = IntervalMessage.ParseFromFile(TestData.TestData.GetFileName(TestDataType.Intervals_1s, DataDirection.Oldest)).ToList();
            
            // Act
            var bars = ticks.ToHistoricalBars(TimeSpan.FromSeconds(1), DataDirection.Oldest).ToList().ToList();

            // Assert
            Assert.IsNotEmpty(intervals);
            Assert.AreEqual(intervals.Count, bars.Count);

            for (var i = 0; i < intervals.Count; i++)
            {
                var interval = intervals[i];
                var bar = bars[i];

                // TODO: create comparable
                if (interval.Timestamp == bar.Timestamp &&
                    interval.Open == bar.Open &&
                    interval.High == bar.High &&
                    interval.Low == bar.Low &&
                    interval.Close == bar.Close &&
                    interval.PeriodVolume == bar.PeriodVolume &&
                    interval.TotalVolume == bar.TotalVolume)
                {
                    continue;
                }

                throw new Exception();
            }
        }

        [Test]
        public void Should_Load_And_Parse_Protocol60_Data_From_File()
        {
            // Arrange
            var ticks = TickMessage.ParseFromFile(TestData.TestData.GetFileName(TestDataType.Ticks, DataDirection.Oldest, true)).ToList();

            // Assert
            Assert.IsNotEmpty(ticks);
        }
    }
}