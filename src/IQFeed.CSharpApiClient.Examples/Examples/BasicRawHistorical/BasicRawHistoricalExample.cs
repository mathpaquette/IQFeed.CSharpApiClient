﻿using System;
using System.IO;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Lookup;

namespace IQFeed.CSharpApiClient.Examples.Examples.BasicRawHistorical
{
    public class BasicRawHistoricalExample : IExampleAsync
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(BasicRawHistoricalExample).Name;

        private const string DownloadBasePath = "downloads";
        private const string Symbol = "AAPL";

        public async Task RunAsync()
        {
            // *************************************

            // Step 1 - !!! Configure your credentials for IQConnect in user environment variable or app.config !!!
            //              Check the documentation for more information.               

            // Step 2 - Run IQConnect launcher
            IQFeedLauncher.Start();

            // Step 3 - Use the appropriate factory to create the client
            var lookupClient = LookupClientFactory.CreateNew();

            // Step 4 - Connect it
            lookupClient.Connect();

            // Step 5 - Make raw request!
            var tmpFilename = await lookupClient.Historical.Raw.GetHistoryTickDatapointsAsync(Symbol, 1000);

            // Step 6 - Move the file
            var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DownloadBasePath);
            var fullPath = Path.Combine(basePath, $"{DateTime.Now:yyyyMMddHHmmss}-{Symbol}.csv");

            Directory.CreateDirectory(basePath);
            File.Move(tmpFilename, fullPath);

            Console.WriteLine($"Saved {Symbol} ticks in {fullPath}");
        }

        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}