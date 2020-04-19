using System;
using System.Collections.Generic;
using System.Linq;
using IQFeed.CSharpApiClient.Extensions.Lookup.Historical.Resample;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Extensions.Tests.Lookup.Historical.Resample
{
    public class TickMessageExtensionsTests
    {
        private readonly List<TickMessage<double>> _ticksAscending;
        private readonly List<TickMessage<double>> _ticksDescending;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public TickMessageExtensionsTests()
        {
            _ticksAscending = new List<TickMessage<double>>()
            {
                new TickMessage<double>(new DateTime(2000, 01, 01, 9, 30, 4), 1, 1, 1, 1, 1, 1, 'C', 1, ""),
                new TickMessage<double>(new DateTime(2000, 01, 01, 9, 34, 1), 1, 1, 1, 1, 1, 1, 'C', 1, ""),
            };

            _ticksDescending = new List<TickMessage<double>>(_ticksAscending);
            _ticksDescending.Reverse();
        }

        [Test]
        public void Should_Resample_Ascending()
        {
            // Act
            var resampledBars = _ticksAscending.ToHistoricalBars(_interval, DataDirection.Oldest).ToList();

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
            var resampledBars = _ticksDescending.ToHistoricalBars(_interval, DataDirection.Newest).ToList();

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
            var resampledBars = _ticksAscending.ToHistoricalBarsUncompressed(_interval, DataDirection.Oldest).ToList();

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
            var resampledBars = _ticksDescending.ToHistoricalBarsUncompressed(_interval, DataDirection.Newest).ToList();

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