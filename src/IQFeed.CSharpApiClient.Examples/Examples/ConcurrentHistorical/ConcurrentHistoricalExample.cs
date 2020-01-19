using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Examples.Examples.ConcurrentHistorical
{
    public class ConcurrentHistoricalExample : ConcurrentHistoricalBase, IExample
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(ConcurrentHistoricalExample).Name;
        private const int NumberOfConcurrentClients = 15;

        private readonly ConcurrentDictionary<string, List<DailyWeeklyMonthlyMessage<decimal>>> _dailyMessagesBySymbol;

        public ConcurrentHistoricalExample() : base(LookupClientFactory.CreateNew(NumberOfConcurrentClients), NumberOfConcurrentClients)
        {
            _dailyMessagesBySymbol = new ConcurrentDictionary<string, List<DailyWeeklyMonthlyMessage<decimal>>>();
        }

        public void Run()
        {
            // Configure your credentials for IQConnect in user environment variable or app.config !!!
            // Check the documentation for more information.               

            // Run IQConnect launcher
            IQFeedLauncher.Start();

            // Connect the LookupClient created from the constructor
            LookupClient.Connect();

            // Add 100 symbols to the concurrent queue
            foreach (var symbol in MarketData.GetSymbols().Skip(100).Take(200))
            {
                Symbols.Enqueue(symbol);
            }

            // Download data for all added symbols
            var sw = Stopwatch.StartNew();
            Start();
            sw.Stop();

            // Count the total of daily messages received
            var messagesFetched = 0;
            foreach (var dailyMessages in _dailyMessagesBySymbol)
            {
                messagesFetched += dailyMessages.Value.Count;
            }

            Console.WriteLine($"\nFetched {messagesFetched} Daily messages for {_dailyMessagesBySymbol.Count} stocks in {sw.Elapsed.TotalMilliseconds} ms.");
        }

        protected override async Task ProcessSymbols()
        {
            while (Symbols.TryDequeue(out var symbol))
            {
                ShowDownloadStatus();

                try
                {
                    var dailyMessages = await LookupClient.Historical.ReqHistoryDailyDatapointsAsync(symbol, 100);
                    _dailyMessagesBySymbol.TryAdd(symbol, dailyMessages.ToList());
                }
                catch (NoDataIQFeedException) { }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}