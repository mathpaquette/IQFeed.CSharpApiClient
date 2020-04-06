using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages.Extensions;

namespace IQFeed.CSharpApiClient.Examples.Examples.MessageHandlers
{
    public class Level1MessageHandlerExample : IExampleAsync
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(Level1MessageHandlerExample).Name;

        public async Task RunAsync()
        {
            // Configure your credentials for IQConnect in user environment variable or app.config !!!
            // Check the documentation for more information.

            // Run IQConnect launcher
            IQFeedLauncher.Start();

            // Choose between 3 different handlers:
            // 1- Level1MessageDecimalHandler   (for decimal)
            // 2- Level1MessageDoubleHandler    (for double) - default one through CreateNew
            // 3- Level1MessageFloatHandler     (for float)
            var level1Client = Level1ClientFactory.CreateNew(
                IQFeedDefault.Hostname, 
                IQFeedDefault.Level1Port,
                Level1Default.SnapshotTimeout, 
                new Level1MessageDecimalHandler());

            // Connect
            level1Client.Connect();

            // retrieve UpdateSummaryMessage<decimal>
            var decimalUpdateSummary = await level1Client.GetUpdateSummarySnapshotAsync("AAPL");

            // convert UpdateSummaryMessage<decimal> to UpdateSummaryMessage<float>
            var floatUpdateSummary = decimalUpdateSummary.ToFloat();
        }

        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}