using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Examples.Examples.StreamingLevel2
{
    public class StreamingLevel2Example : IExampleAsync
    {
        public bool Enable => true; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => nameof(StreamingLevel2Example);

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

            //level2Client.Summary += Level2ClientOnSummary; // protocol 6.1 and prior
            //level2Client.Update += Level2ClientOnSummary; // protocol 6.1 and prior

            // Comment/uncomment the following line for MBP data **********************************
            //level2Client.PriceLevelSummary += Level2Client_PriceLevelSummary;
            //level2Client.PriceLevelUpdate += Level2Client_PriceLevelSummary;

            // Comment/uncomment the following line for MBO data **********************************
            level2Client.OrderSummary += Level2Client_MBOSummary;
            level2Client.OrderUpdate += Level2Client_MBOSummary;

            level2Client.Timestamp += Level2ClientOnTimestamp;

            // Step 6 - Make your streaming Level 2 requests
            // ReqWatch is only supported in protocols 6.1 and below.
            // For protocol 6.2+, use ReqWatchMarketByPrice or ReqWatchMarketByOrder 
            //level2Client.ReqWatchMarketByPrice("@ES#"); // MBP data *******************************
            level2Client.ReqWatchMarketByOrder("@ES#"); // MBO data *******************************

            Console.WriteLine("Watching @ES# (front month) for the next 20 seconds... Please be patient ;-)\n");
            await Task.Delay(TimeSpan.FromSeconds(20));

            // Step 7 - Unwatch and unregister events
            level2Client.ReqUnwatch("@ES#");

            //level2Client.Summary -= Level2ClientOnSummary; // protocol 6.1 and prior
            //level2Client.Update -= Level2ClientOnSummary; // protocol 6.1 and prior

            // Comment/uncomment the following 2 lines for MBP data *******************************
            //level2Client.PriceLevelSummary -= Level2Client_PriceLevelSummary;
            //level2Client.PriceLevelUpdate -= Level2Client_PriceLevelSummary;

            // Comment/uncomment the following 2 lines for MBO data *******************************
            level2Client.OrderSummary -= Level2Client_MBOSummary;
            level2Client.OrderUpdate -= Level2Client_MBOSummary;

            level2Client.Timestamp -= Level2ClientOnTimestamp;
        }

        private void Level2Client_PriceLevelSummary(PriceLevelUpdateSummaryMessage msg)    // Added for protocol 6.2 MBP
        {
            Console.WriteLine(msg);
        }

        private void Level2Client_MBOSummary(OrderAddUpdateSummaryMessage msg)    // Added for protocol 6.2 MBO
        {
            Console.WriteLine(msg);
        }

        private void Level2ClientOnTimestamp(TimestampMessage msg)
        {
            Console.WriteLine(msg);
        }

        //private void Level2ClientOnSummary(UpdateSummaryMessage msg) // protocol 6.1 and prior
        //{
        //    Console.WriteLine(msg);
        //}

        private void Level2ClientOnError(ErrorMessage msg)
        {
            Console.WriteLine(msg);
        }

        private void Level2ClientOnSystem(SystemMessage msg)
        {
            Console.WriteLine(msg);
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
