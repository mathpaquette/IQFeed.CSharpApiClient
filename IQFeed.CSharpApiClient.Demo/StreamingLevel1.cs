using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Demo.Common;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Demo
{
    internal class StreamingLevel1
    {
        public async Task RunAsync()
        {
            // Step 1 - !!! Configure your credentials for IQConnect in user environment variable or app.config !!!
            //              Check the documentation for more information.               

            // Step 2 - Run IQConnect launcher
            IQFeedLauncher.Start();

            // Step 3 - Use the appropriate factory to create the client
            var level1Client = Level1ClientFactory.CreateNew();

            // Step 4 - Connect it
            level1Client.Connect();

            // Step 5 - Register to appropriate events
            level1Client.Fundamental += Level1ClientOnFundamental;
            level1Client.Summary += Level1ClientOnSummary;
            level1Client.Update += Level1ClientOnSummary;
            level1Client.Timestamp += Level1ClientOnTimestamp;

            // Step 6 - Make your streaming Level 1 requests
            level1Client.ReqWatch("@ES#");

            Console.WriteLine("Watching @ES# (front month) for the next 20 seconds... Please be patient ;-)\n");
            await Task.Delay(TimeSpan.FromSeconds(20));

            // Step 7 - Unwatch and unregister events
            level1Client.ReqUnwatch("@ESU22");

            level1Client.Fundamental -= Level1ClientOnFundamental;
            level1Client.Summary -= Level1ClientOnSummary;
            level1Client.Update -= Level1ClientOnSummary;
            level1Client.Timestamp -= Level1ClientOnTimestamp;
        }

        private void Level1ClientOnTimestamp(TimestampMessage msg)
        {
            Console.WriteLine(msg);
        }

        private void Level1ClientOnSummary(IUpdateSummaryMessage msg)
        {
            Console.WriteLine(msg);
        }

        private void Level1ClientOnFundamental(FundamentalMessage msg)
        {
            Console.WriteLine(msg);
        }
    }
}
