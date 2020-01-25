using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Historical.Handlers;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages.Extensions;

namespace IQFeed.CSharpApiClient.Examples.Examples.MessageHandlers
{
    public class HistoricalMessageHandlerExample : IExampleAsync
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(HistoricalMessageHandlerExample).Name;

        public async Task RunAsync()
        {
            // Configure your credentials for IQConnect in user environment variable or app.config !!!
            // Check the documentation for more information.

            // Run IQConnect launcher
            IQFeedLauncher.Start();
            
            // Choose between 3 different handlers:
            // 1- HistoricalMessageDecimalHandler   (for decimal) - default one through CreateNew
            // 2- HistoricalMessageDoubleHandler    (for double)
            // 3- HistoricalMessageFloatHandler     (for float)
            var lookupClient = LookupClientFactory.CreateNew(
                IQFeedDefault.Hostname,
                IQFeedDefault.LookupPort,
                LookupDefault.TimeoutMs,
                1,
                LookupDefault.BufferSize,
                new HistoricalMessageDoubleHandler());

            // Connect
            lookupClient.Connect();

            // retrieve IEnumerable<TickMessage<double>>
            var doubleTicks = (await lookupClient.Historical.GetHistoryTickDatapointsAsync("AAPL", 1000)).ToList();

            // convert TickMessage<double> to TickMessage<float>
            var floatTick = doubleTicks.First().ToFloat();

            // convert IEnumerable<TickMessage<double>> to IEnumerable<TickMessage<float>>
            var floatTicks = doubleTicks.ToFloat().ToList();
        }

        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}