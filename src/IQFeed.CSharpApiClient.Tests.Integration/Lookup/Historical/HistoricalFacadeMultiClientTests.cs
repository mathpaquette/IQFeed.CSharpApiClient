using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Historical;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Historical
{
    public class HistoricalFacadeMultiClientTests
    {
        private const int TimeoutMs = 30000;
        private const int Datapoints = 100;
        private const int NumberOfClients = 10;
        private static readonly string[] Symbols = {"SPY", "AAPL", "NFLX", "MSFT", "TSLA", "AMD", "NVDA", "MU", "BABA", "AMZN"};

    private HistoricalFacade _historicalFacade;

        public HistoricalFacadeMultiClientTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {

            var lookupClient = LookupClientFactory.CreateNew(NumberOfClients);
            lookupClient.Connect();

            _historicalFacade = lookupClient.Historical;
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickDatapointsAsync_Parallel()
        {
            // Arrange
            var tickMessageTasks = new List<Task<IEnumerable<TickMessage>>>();

            // Act
            var sw = Stopwatch.StartNew();
            foreach (var symbol in Symbols)
            {
                var tickMessageTask = _historicalFacade.ReqHistoryTickDatapointsAsync(symbol, Datapoints);
                tickMessageTasks.Add(tickMessageTask);
            }
            await Task.WhenAll(tickMessageTasks);
            sw.Stop();

            // Assert
            foreach (var task in tickMessageTasks)
            {
                Assert.AreEqual(task.Result.Count(), Datapoints);
            }

            Console.WriteLine($"Parallelly fetching data for 10 symbols took: {sw.Elapsed.TotalMilliseconds} ms");
        }


        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickDatapointsAsync_Sequential()
        {
            // Arrange
            var listOfTickMessages = new List<IEnumerable<TickMessage>>();

            // Act
            var sw = Stopwatch.StartNew();
            foreach (var symbol in Symbols)
            {
                var tickMessages = await _historicalFacade.ReqHistoryTickDatapointsAsync(symbol, Datapoints);
                listOfTickMessages.Add(tickMessages);
            }
            sw.Stop();

            // Assert
            foreach (var tickMessage in listOfTickMessages)
            {
                Assert.AreEqual(tickMessage.Count(), Datapoints);
            }

            Console.WriteLine($"Sequentially fetching data for 10 symbols took: {sw.Elapsed.TotalMilliseconds} ms");
        }
    }
}