using System;
using System.Collections.Generic;
using System.Linq;
using IQFeed.CSharpApiClient.Extensions.Lookup.Historical.Resample;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Extensions.Tests.Lookup.Historical.Resample
{
    public class IntervalMessageExtensionsTests
    {
        private readonly List<IntervalMessage<double>> _intervalsAscending;
        private readonly List<IntervalMessage<double>> _intervalsDescending;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public IntervalMessageExtensionsTests()
        {
            _intervalsAscending = new List<IntervalMessage<double>>()
            {
                new IntervalMessage<double>(new DateTime(2000, 01, 01, 9, 30, 4), 1, 1, 1, 1, 1, 1, 1),
                new IntervalMessage<double>(new DateTime(2000, 01, 01, 9, 34, 1), 1, 1, 1, 1, 2, 1, 1),
            };

            _intervalsDescending = new List<IntervalMessage<double>>(_intervalsAscending);
            _intervalsDescending.Reverse();
        }

        [Test]
        public void Should_Resample_Ascending()
        {
            // Act
            var resampledBars = _intervalsAscending.ToHistoricalBars(_interval, DataDirection.Oldest).ToList();

            // Assert
            var expectedTimestamps = new List<DateTime>()
            {
                new DateTime(2000, 01, 01, 9, 30, 0),
                new DateTime(2000, 01, 01, 9, 34, 0),
            };
            Assert.AreEqual(resampledBars.Select(x => x.Timestamp), expectedTimestamps);
        }

        [Test]
        public void Should_Resample_Descending()
        {
            // Act
            var resampledBars = _intervalsDescending.ToHistoricalBars(_interval, DataDirection.Newest).ToList();

            // Assert
            var expectedTimestamps = new List<DateTime>()
            {
                new DateTime(2000, 01, 01, 9, 34, 0),
                new DateTime(2000, 01, 01, 9, 30, 0),
            };
            Assert.AreEqual(resampledBars.Select(x => x.Timestamp), expectedTimestamps);
        }

        [Test]
        public void Should_Resample_Uncompressed_Ascending()
        {
            // Act
            var resampledBars = _intervalsAscending.ToHistoricalBarsUncompressed(_interval, DataDirection.Oldest).ToList();

            // Assert
            var expectedTimestamps = new List<DateTime>()
            {
                new DateTime(2000, 01, 01, 9, 30, 0),
                new DateTime(2000, 01, 01, 9, 31, 0),
                new DateTime(2000, 01, 01, 9, 32, 0),
                new DateTime(2000, 01, 01, 9, 33, 0),
                new DateTime(2000, 01, 01, 9, 34, 0),
            };
            Assert.AreEqual(resampledBars.Select(x => x.Timestamp), expectedTimestamps);
        }

        [Test]
        public void Should_Resample_Uncompressed_Descending()
        {
            // Act
            var resampledBars = _intervalsDescending.ToHistoricalBarsUncompressed(_interval, DataDirection.Newest).ToList();

            // Assert
            var expectedTimestamps = new List<DateTime>()
            {
                new DateTime(2000, 01, 01, 9, 34, 0),
                new DateTime(2000, 01, 01, 9, 33, 0),
                new DateTime(2000, 01, 01, 9, 32, 0),
                new DateTime(2000, 01, 01, 9, 31, 0),
                new DateTime(2000, 01, 01, 9, 30, 0),
            };
            Assert.AreEqual(resampledBars.Select(x => x.Timestamp), expectedTimestamps);
        }
    }
}