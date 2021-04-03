using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Examples.Examples.StreamingLevel1
{
    public class StreamingLevel1DynamicV2Example : IExampleAsync
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => nameof(StreamingLevel1DynamicV2Example);

        public async Task RunAsync()
        {
            // *************************************

            // Step 1 - !!! Configure your credentials for IQConnect in user environment variable or app.config !!!
            //              Check the documentation for more information.               

            // Step 2 - Run IQConnect launcher
            IQFeedLauncher.Start();

            // Step 3 - Use the appropriate factory to create the client and specify which fields are needed
            var level1DynamicClient = Level1DynamicClientFactory.CreateNew(
                DynamicFieldset.Symbol,
                DynamicFieldset.MostRecentTrade,
                DynamicFieldset.MostRecentTradeSize,
                DynamicFieldset.MostRecentTradeTime,
                DynamicFieldset.MostRecentTradeMarketCenter,
                DynamicFieldset.TotalVolume,
                DynamicFieldset.Bid,
                DynamicFieldset.BidSize,
                DynamicFieldset.Ask,
                DynamicFieldset.AskSize,
                DynamicFieldset.Open,
                DynamicFieldset.High,
                DynamicFieldset.Low,
                DynamicFieldset.Close,
                DynamicFieldset.MessageContents,
                DynamicFieldset.MostRecentTradeConditions,
                DynamicFieldset.MostRecentTradeDate,                
                DynamicFieldset.Volatility,
                DynamicFieldset.VWAP);

            // Step 4 - Connect it
            level1DynamicClient.Connect();

            // Step 5 - Register to appropriate events
            level1DynamicClient.Fundamental += Level1ClientOnFundamental;
            level1DynamicClient.Summary += Level1ClientOnSummary;
            level1DynamicClient.Update += Level1ClientOnSummary;
            level1DynamicClient.Timestamp += Level1ClientOnTimestamp;

            // Step 6 - Make your streaming Level 1 requests
            level1DynamicClient.ReqWatch("AAPL");

            Console.WriteLine("Watching APPL for the next 10 seconds... Please be patient ;-)\n");
            await Task.Delay(TimeSpan.FromSeconds(10));

            // Step 7 - Unwatch and unregister events
            level1DynamicClient.ReqUnwatch("AAPL");            

            level1DynamicClient.Fundamental -= Level1ClientOnFundamental;
            level1DynamicClient.Summary -= Level1ClientOnSummary;
            level1DynamicClient.Update -= Level1ClientOnSummary;
            level1DynamicClient.Timestamp -= Level1ClientOnTimestamp;

            // Step 8 - Disconnect the client from IQ Feed
            level1DynamicClient.Disconnect();
        }

        private void Level1ClientOnTimestamp(TimestampMessage msg)
        {
            Console.WriteLine(msg);
        }

        private void Level1ClientOnSummary(IUpdateSummaryDynamicMessage msg)
        {
            Console.WriteLine("Summary >>> " + msg);
            Console.WriteLine();
        }

        private void Level1ClientOnUpdate(IUpdateSummaryDynamicMessage msg)
        {
            Console.WriteLine("Update >>> " + msg);
        }

        private void Level1ClientOnFundamental(FundamentalMessage msg)
        {
            Console.WriteLine("Fundamental >>> " + msg);
            Console.WriteLine();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}