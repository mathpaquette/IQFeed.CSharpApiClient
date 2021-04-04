using System;
using System.IO;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Extensions.Tests.Integration.TestData
{
    public class TestDataTests
    {
        private static readonly string TestDataDirectory = Path.Combine(@"C:\dev\src", "TestData");
        private LookupClient _lookupClient;

        [SetUp]
        public void SetUp()
        {
            RestTestDataDirectory();

            IQFeedLauncher.Start();
            _lookupClient = LookupClientFactory.CreateNew();
            _lookupClient.Connect();
        }

        [Test, Explicit]
        public async Task Generate_Test_Data()
        {
            string filename;
            var ticker = "NEPT";
            var startDate = new DateTime(2021, 03, 01, 4, 0, 0);
            var endDate = new DateTime(2021, 03, 03, 20, 00, 00);

            var maxDataPoints = 25;

            // ticks
            filename = await _lookupClient.Historical.File.GetHistoryTickTimeframeAsync(ticker, startDate, endDate, dataDirection: DataDirection.Newest);
            MoveToTestDataDirectory(filename, "ticks_newest.csv");
            filename = await _lookupClient.Historical.File.GetHistoryTickTimeframeAsync(ticker, startDate, endDate, dataDirection: DataDirection.Oldest);
            MoveToTestDataDirectory(filename, "ticks_oldest.csv");

            // intervals (1 sec)
            filename = await _lookupClient.Historical.File.GetHistoryIntervalTimeframeAsync(ticker, 1, startDate, endDate, dataDirection: DataDirection.Newest);
            MoveToTestDataDirectory(filename, "intervals_1s_newest.csv");
            filename = await _lookupClient.Historical.File.GetHistoryIntervalTimeframeAsync(ticker, 1, startDate, endDate, dataDirection: DataDirection.Oldest);
            MoveToTestDataDirectory(filename, "intervals_1s_oldest.csv");

            // intervals (5 secs)
            filename = await _lookupClient.Historical.File.GetHistoryIntervalTimeframeAsync(ticker, 5, startDate, endDate, dataDirection: DataDirection.Newest);
            MoveToTestDataDirectory(filename, "intervals_5s_newest.csv");
            filename = await _lookupClient.Historical.File.GetHistoryIntervalTimeframeAsync(ticker, 5, startDate, endDate, dataDirection: DataDirection.Oldest);
            MoveToTestDataDirectory(filename, "intervals_5s_oldest.csv");

            // dailies
            filename = await _lookupClient.Historical.File.GetHistoryDailyDatapointsAsync(ticker, maxDataPoints, DataDirection.Newest);
            MoveToTestDataDirectory(filename, "dailies_newest.csv");
            filename = await _lookupClient.Historical.File.GetHistoryDailyDatapointsAsync(ticker, maxDataPoints, DataDirection.Oldest);
            MoveToTestDataDirectory(filename, "dailies_oldest.csv");

            // weeklies
            filename = await _lookupClient.Historical.File.GetHistoryWeeklyDatapointsAsync(ticker, maxDataPoints, DataDirection.Newest);
            MoveToTestDataDirectory(filename, "weeklies_newest.csv");
            filename = await _lookupClient.Historical.File.GetHistoryWeeklyDatapointsAsync(ticker, maxDataPoints, DataDirection.Oldest);
            MoveToTestDataDirectory(filename, "weeklies_oldest.csv");

            // monthlies
            filename = await _lookupClient.Historical.File.GetHistoryMonthlyDatapointsAsync(ticker, maxDataPoints, DataDirection.Newest);
            MoveToTestDataDirectory(filename, "monthlies_newest.csv");
            filename = await _lookupClient.Historical.File.GetHistoryMonthlyDatapointsAsync(ticker, maxDataPoints, DataDirection.Oldest);
            MoveToTestDataDirectory(filename, "monthlies_oldest.csv");
        }

        private void RestTestDataDirectory()
        {
            if (Directory.Exists(TestDataDirectory))
            {
                Directory.Delete(TestDataDirectory, true);
            }
            Directory.CreateDirectory(TestDataDirectory);
        }

        private void MoveToTestDataDirectory(string source, string dest)
        {
            File.Move(source, Path.Combine(TestDataDirectory, dest));
        }
    }
}