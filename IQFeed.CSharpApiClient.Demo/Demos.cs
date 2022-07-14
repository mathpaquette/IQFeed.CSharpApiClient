using IQFeed.CSharpApiClient.Demo.Common;
using IQFeed.CSharpApiClient.Lookup;

namespace IQFeed.CSharpApiClient.Demo
{
    internal class Demos
    {
        #region BasicHistoricalLookUp
        // ***********************************************************************************************************
        internal static void BasicHistoricalLookUp()
        {
            static async Task RunAsync()
            {
                // Step 1 - !!! Configure your credentials for IQConnect in user environment variable or app.config !!!
                //              Check the documentation for more information.               

                // Step 2 - Run IQConnect launcher
                IQFeedLauncher.Start();

                // Step 3 - Use the appropriate factory to create the client
                var lookupClient = LookupClientFactory.CreateNew();

                // Step 4 - Connect it
                lookupClient.Connect();

                // Step 5 - Make any requests you need or want!
                var tickMessages = await lookupClient.Historical.GetHistoryTickDatapointsAsync("AAPL", 10);
                var intervalMessages = await lookupClient.Historical.GetHistoryIntervalDaysAsync("AAPL", 5, 10, 10);
                var dailyMessages = await lookupClient.Historical.GetHistoryDailyDatapointsAsync("AAPL", 10);

                Console.WriteLine($"Fetched {tickMessages.Count()} Tick messages for symbol AAPL:");
                foreach (var msg in tickMessages)
                {
                    Console.WriteLine(msg);
                }
                Console.WriteLine();

                 
                foreach (var msg in intervalMessages)
                {
                    Console.WriteLine(msg);
                }
                Console.WriteLine();

                foreach (var msg in dailyMessages)
                {
                    Console.WriteLine(msg);
                }
                Console.WriteLine();

                lookupClient.Disconnect();
            }

            RunAsync().Wait();
            ConsoleHelper.Continue();
        }
        #endregion

        #region ConcurrentFileHistorical
        // ***********************************************************************************************************
        internal static void ConcurrentFileHistorical()
        {
            var cfh = new ConcurrentFileHistoricalExample(); 
            cfh.Run();
            ConsoleHelper.Continue();
        }
        #endregion

        #region StreamingLevel1
        // ***********************************************************************************************************
        internal static async Task StreamingLevel1()
        {
            var l1 = new StreamingLevel1();
            await l1.RunAsync();
            ConsoleHelper.Continue();
        }
        #endregion

        #region StreamingLevel2_MBP
        // ***********************************************************************************************************
        internal static async Task StreamingLevel2_MBP()
        {
            var l2MBP = new StreamingLevel2_MBP();
            await l2MBP.RunAsync();
            ConsoleHelper.Continue();
        }
        #endregion

        #region StreamingLevel2_MBO
        // ***********************************************************************************************************
        internal static async Task StreamingLevel2_MBO()
        {
            var l2MBO = new StreamingLevel2_MBO();
            await l2MBO.RunAsync();
            ConsoleHelper.Continue();
        }
        #endregion
    }
}
