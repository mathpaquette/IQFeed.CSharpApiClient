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
            // 1- HistoricalMessageDecimalHandler   (for decimal)
            // 2- HistoricalMessageDoubleHandler    (for double) - default one through CreateNew
            // 3- HistoricalMessageFloatHandler     (for float)
            var lookupClient = LookupClientFactory.CreateNew(
                IQFeedDefault.Hostname,
                IQFeedDefault.LookupPort,
                1,
                LookupDefault.Timeout,
                LookupDefault.BufferSize,
                new HistoricalMessageDecimalHandler());

            // Connect
            lookupClient.Connect();

            // retrieve IEnumerable<TickMessage<decimal>>
            var decimalTicks = (await lookupClient.Historical.GetHistoryTickDatapointsAsync("AAPL", 1000)).ToList();

            // convert TickMessage<decimal> to TickMessage<float>
            var floatTick = decimalTicks.First().ToFloat();

            // convert IEnumerable<TickMessage<decimal>> to IEnumerable<TickMessage<float>>
            var floatTicks = decimalTicks.ToFloat().ToList();
        }

        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}