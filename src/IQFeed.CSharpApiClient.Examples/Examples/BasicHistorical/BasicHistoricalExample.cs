using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Lookup;

namespace IQFeed.CSharpApiClient.Examples.Examples.BasicHistorical
{
    public class BasicHistoricalExample : IExampleAsync
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(BasicHistoricalExample).Name;

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

            // Step 5 - Make any requests you need or want!
            var tickMessages = await lookupClient.Historical.GetHistoryTickDatapointsAsync("AAPL", 100);
            var intervalMessage = await lookupClient.Historical.GetHistoryIntervalDaysAsync("AAPL", 5, 10, 100);
            var dailyMessages = await lookupClient.Historical.GetHistoryDailyDatapointsAsync("AAPL", 100);

            Console.WriteLine($"Fetched {tickMessages.Count()} Tick messages");
            Console.WriteLine($"Fetched {intervalMessage.Count()} Interval messages");
            Console.WriteLine($"Fetched {dailyMessages.Count()} Daily messages");
        }

        void IExample.Run()
        {
            throw new System.NotImplementedException();
        }
    }
}