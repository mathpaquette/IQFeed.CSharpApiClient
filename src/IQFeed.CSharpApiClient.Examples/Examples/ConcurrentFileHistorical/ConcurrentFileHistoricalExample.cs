using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Lookup;

namespace IQFeed.CSharpApiClient.Examples.Examples.ConcurrentFileHistorical
{
    public class ConcurrentFileHistoricalExample : ConcurrentHistoricalBase, IExample
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(ConcurrentFileHistoricalExample).Name;

        private const int NumberOfConcurrentClients = 10;
        private const string DownloadBasePath = "downloads";

        private const bool DownloadTicks = true;
        private const bool DownloadIntervals = true;
        private const bool DownloadEODs = true;

        private const int MaxDatapoints = 1000;
        private const int TickDays = 10;
        private const int IntervalDays = 10;
        private const int EodDays = 10;
        private const int IntervalInSeconds = 5;

        private readonly string _basePath;

        public ConcurrentFileHistoricalExample() : base(LookupClientFactory.CreateNew(NumberOfConcurrentClients), NumberOfConcurrentClients)
        {
            _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DownloadBasePath, DateTime.Now.ToString("yyyyMMddHHmmss"));
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
            foreach (var symbol in MarketData.GetSymbols().Take(100))
            {
                Symbols.Enqueue(symbol);
            }

            // Create required directories for saving files
            CreateDirectories();

            // Download data for all added symbols
            Start();
        }

        protected override async Task ProcessSymbols()
        {
            while (Symbols.TryDequeue(out var symbol))
            {
                ShowDownloadStatus();

                if (DownloadTicks)
                    await DownloadTick(symbol);

                if (DownloadIntervals)
                    await DownloadInterval(symbol);

                if (DownloadEODs)
                    await DownloadEOD(symbol);
            }
        }

        private async Task DownloadTick(string symbol)
        {
            try
            {
                // Using the "file" historical facade will save received data directly to the disk
                var tmpFilename = await LookupClient.Historical.File.GetHistoryTickDaysAsync(symbol, TickDays, MaxDatapoints);
                MoveDownloadFile(tmpFilename, symbol, DataType.Tick);
            }
            catch (NoDataIQFeedException) { }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task DownloadInterval(string symbol)
        {
            try
            {
                var tmpFilename = await LookupClient.Historical.File.GetHistoryIntervalDaysAsync(symbol, IntervalInSeconds, IntervalDays, MaxDatapoints);
                MoveDownloadFile(tmpFilename, symbol, DataType.Interval);
            }
            catch (NoDataIQFeedException) { }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task DownloadEOD(string symbol)
        {
            try
            {
                var tmpFilename = await LookupClient.Historical.File.GetHistoryDailyDatapointsAsync(symbol, EodDays);
                MoveDownloadFile(tmpFilename, symbol, DataType.EOD);
            }
            catch (NoDataIQFeedException) { }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void MoveDownloadFile(string file, string symbol, string dataType)
        {
            var filename = $"{symbol}.csv";
            File.Move(file, Path.Combine(_basePath, dataType, filename));
        }

        private void CreateDirectories()
        {
            foreach (var dataType in new List<string>() { DataType.Tick, DataType.Interval, DataType.EOD })
            {
                Directory.CreateDirectory(Path.Combine(_basePath, dataType));
            }

            Console.WriteLine($"Download Base Path: {_basePath}");
        }
    }
}