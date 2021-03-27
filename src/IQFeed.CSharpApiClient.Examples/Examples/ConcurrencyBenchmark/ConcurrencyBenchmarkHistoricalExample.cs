using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Examples.Examples.ConcurrentHistorical
{
    public class ConcurrencyBenchmarkHistoricalExample : ConcurrentHistoricalBase, IExample
    {
        public bool Enable => true; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => nameof(ConcurrencyBenchmarkHistoricalExample);
        private const int NumberOfConcurrentClients = 8;

        public ConcurrencyBenchmarkHistoricalExample() : base(LookupClientFactory.CreateNew(NumberOfConcurrentClients), NumberOfConcurrentClients) { }

        private class Ref<T>
        {
            public T Value;
        }

        public void Run()
        {
            // Configure your credentials for IQConnect in user environment variable or app.config !!!
            // Check the documentation for more information.               

            // Run IQConnect launcher
            IQFeedLauncher.Start();

            // Connect the LookupClient created from the constructor
            LookupClient.Connect();


            const int numberOfSymbols = 50;
            const int iterations = NumberOfConcurrentClients * numberOfSymbols; //symbols
            var rand = new Random(1);
            var symbols = Repeat(MarketData.GetSymbols().OrderBy(_ => rand.Next()).ToArray()).Take(iterations).ToArray();
            var tasks = new Task[iterations];

            Console.WriteLine($"Number of Concurrent Tasks: {iterations}");
            Console.WriteLine($"Number of Symbols: {numberOfSymbols}");
            Console.WriteLine("Benchmark starting 0ms");
            var sw = Stopwatch.StartNew();
            Ref<int> messagesFetched = new Ref<int>() { Value = 0 };

            for (int i = 0; i < iterations; i++)
            {
                var fetched = messagesFetched;
                tasks[i] = LookupClient.Historical.GetHistoryDailyDatapointsAsync(symbols[i], 100).ContinueWith(t =>
                {
                    try
                    {
                        //accumulate th
                        fetched.Value += t.GetAwaiter().GetResult().Count();
                    }
                    catch (NoDataIQFeedException) { }
                    catch (System.AggregateException e)
                    {
                        //handle aggregated exception due to unwrapped exceptions from Task<T>
                        AggregateException agg = e;
                        while (e.InnerExceptions.Count == 1 && e.InnerExceptions[0] is AggregateException)
                            agg = (AggregateException)e.InnerExceptions[0];

                        if (!(e.InnerExceptions[0] is NoDataIQFeedException))
                            throw;
                    }
                });
            }

            Console.WriteLine("All tasks allocated " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            Task.WaitAll(tasks);
            sw.Stop();
            Console.WriteLine("All tasks completed " + sw.ElapsedMilliseconds);
            Console.WriteLine("Requests per second :" + (sw.ElapsedMilliseconds) / (double)iterations);

            Console.WriteLine($"\nFetched {messagesFetched.Value} Daily messages for {iterations} requests in {sw.Elapsed.TotalMilliseconds} ms.");
        }

        private IEnumerable<T> Repeat<T>(IEnumerable<T> collection)
        {
            while (true)
                foreach (var item in collection)
                {
                    yield return item;
                }
        }

        protected override Task ProcessSymbols()
        {
            return Task.CompletedTask;
        }
    }
}