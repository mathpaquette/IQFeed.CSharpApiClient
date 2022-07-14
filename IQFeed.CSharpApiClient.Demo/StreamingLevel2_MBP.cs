using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Demo
{
    internal class StreamingLevel2_MBP
    {
        public async Task RunAsync()
        {
            // Step 1 - !!! Configure your credentials for IQConnect in user environment variable or app.config !!!
            //              Check the documentation for more information.               

            // Step 2 - Run IQConnect launcher
            IQFeedLauncher.Start();

            // Step 3 - Use the appropriate factory to create the client
            var level2Client = Level2ClientFactory.CreateNew();

            // Step 4 - Connect it
            level2Client.Connect();

            // Step 5 - Register to appropriate events
            // ** IMPORTANT ** you should always subscribe to System event
            level2Client.System += Level2ClientOnSystem;
            level2Client.Error += Level2ClientOnError;

            level2Client.PriceLevelSummary += Level2Client_PriceLevelSummary;
            level2Client.PriceLevelUpdate += Level2Client_PriceLevelSummary;

            level2Client.Timestamp += Level2ClientOnTimestamp;

            // Step 6 - Make your streaming Level 2 requests
            // ReqWatch is only supported in protocols 6.1 and below.
            // For protocol 6.2+, use ReqWatchMarketByPrice
            level2Client.ReqWatchMarketByPrice("@ES#"); 

            Console.WriteLine("Watching @ES# (front month) for the next 20 seconds... Please be patient ;-)\n");
            await Task.Delay(TimeSpan.FromSeconds(20));

            // Step 7 - Unwatch and unregister events
            level2Client.ReqUnwatch("@ES#");

            level2Client.PriceLevelSummary -= Level2Client_PriceLevelSummary;
            level2Client.PriceLevelUpdate -= Level2Client_PriceLevelSummary;

            level2Client.Timestamp -= Level2ClientOnTimestamp;
        }

        private void Level2Client_PriceLevelSummary(PriceLevelUpdateSummaryMessage msg)    // Added for protocol 6.2 MBP
        {
            Console.WriteLine(msg);
        }

        private void Level2ClientOnTimestamp(TimestampMessage msg)
        {
            Console.WriteLine(msg);
        }

        private void Level2ClientOnSummary(UpdateSummaryMessage msg)
        {
            Console.WriteLine(msg);
        }

        private void Level2ClientOnError(ErrorMessage msg)
        {
            Console.WriteLine(msg);
        }

        private void Level2ClientOnSystem(SystemMessage msg)
        {
            Console.WriteLine(msg);
        }
    }
}
