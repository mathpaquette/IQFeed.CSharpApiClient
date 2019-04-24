using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Streaming.Level2;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Examples.Examples.StreamingLevel2
{
    public class StreamingLevel2Example : IExampleAsync
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(StreamingLevel2Example).Name;

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
            level2Client.Summary += Level2ClientOnSummary;
            level2Client.Update += Level2ClientOnSummary;
            level2Client.Timestamp += Level2ClientOnTimestamp;

            // Step 6 - Make your streaming Leve1 requests
            level2Client.ReqWatch("@ESM19");

            Console.WriteLine("Watching @ESM19 for the next 20 seconds... Please be patient ;-)\n");
            await Task.Delay(TimeSpan.FromSeconds(20));

            // Step 7 - Unwatch and unregister events
            level2Client.ReqUnwatch("@ESM19");

            level2Client.Summary -= Level2ClientOnSummary;
            level2Client.Update -= Level2ClientOnSummary;
            level2Client.Timestamp -= Level2ClientOnTimestamp;
        }

        private void Level2ClientOnTimestamp(TimestampMessage msg)
        {
            Console.WriteLine(msg);
        }

        private void Level2ClientOnSummary(UpdateSummaryMessage msg)
        {
            Console.WriteLine(msg);
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
