using System;
using System.Linq;
using IQFeed.CSharpApiClient.Extensions.Lookup.Historical.Resample;
using IQFeed.CSharpApiClient.Extensions.Tests.Integration.TestData;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Extensions.Tests.Integration.Lookup.Historical.Resample
{
    public class IntervalMessageExtensionsTests
    {
        [Test]
        public void Should_Resample_Bars_From_Intervals()
        {
            // Arrange
            var intervals1S = IntervalMessage.ParseFromFile(TestData.TestData.GetFileName(TestDataType.Intervals_1s, DataDirection.Oldest)).ToList();
            var intervals5S = IntervalMessage.ParseFromFile(TestData.TestData.GetFileName(TestDataType.Intervals_5s, DataDirection.Oldest)).ToList();

            // Act
            var bars = intervals1S.ToHistoricalBars(TimeSpan.FromSeconds(5), DataDirection.Oldest).ToList().ToList();

            // Assert
            Assert.IsNotEmpty(intervals5S);
            Assert.AreEqual(intervals5S.Count, bars.Count);

            for (var i = 0; i < intervals5S.Count; i++)
            {
                var interval = intervals5S[i];
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
    }
}