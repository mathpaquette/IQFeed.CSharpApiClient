using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Examples.Examples.StreamingLevel1
{
    public class StreamingLevel1DynamicExample : IExampleAsync
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => nameof(StreamingLevel1DynamicExample);

        public async Task RunAsync()
        {
            // *************************************

            // Step 1 - !!! Configure your credentials for IQConnect in user environment variable or app.config !!!
            //              Check the documentation for more information.               

            // Step 2 - Run IQConnect launcher
            IQFeedLauncher.Start();

            // Step 3 - Create an array that includes the fields desired for Update and Summary messages
            var fields = new[]
            {
                // The IQFeed servers *ALWAYS* include Symbol as first message data field, regardless of fields requested via SelectUpdateFieldName/SELECT UPDATE FIELDS
                // DynamicFieldset.Symbol *MUST* be the first dynamic field in the array at this time.  Parsing errors will result otherwise.
                
                DynamicFieldset.Symbol,
                DynamicFieldset.SevenDayYield,
                DynamicFieldset.Ask,
                DynamicFieldset.AskChange,
                DynamicFieldset.AskMarketCenter,
                DynamicFieldset.AskSize,
                DynamicFieldset.AskTime,
                DynamicFieldset.AvailableRegions,
                DynamicFieldset.AverageMaturity,
                DynamicFieldset.Bid,
                DynamicFieldset.BidChange,
                DynamicFieldset.BidMarketCenter,
                DynamicFieldset.BidSize,
                DynamicFieldset.BidTime,
                DynamicFieldset.Change,
                DynamicFieldset.ChangeFromOpen,
                DynamicFieldset.Close,
                DynamicFieldset.CloseRange1,
                DynamicFieldset.CloseRange2,
                DynamicFieldset.DaysToExpiration,
                DynamicFieldset.DecimalPrecision,
                DynamicFieldset.Delay,
                DynamicFieldset.ExchangeID,
                DynamicFieldset.ExtendedTrade,
                DynamicFieldset.ExtendedTradeDate,
                DynamicFieldset.ExtendedTradeMarketCenter,
                DynamicFieldset.ExtendedTradeSize,
                DynamicFieldset.ExtendedTradeTime,
                DynamicFieldset.ExtendedTradingChange,
                DynamicFieldset.ExtendedTradingDifference,
                DynamicFieldset.FinancialStatusIndicator,
                DynamicFieldset.FractionDisplayCode,
                DynamicFieldset.High,
                DynamicFieldset.Last,
                DynamicFieldset.LastDate,
                DynamicFieldset.LastMarketCenter,
                DynamicFieldset.LastSize,
                DynamicFieldset.LastTime,
                DynamicFieldset.Low,
                DynamicFieldset.MarketCapitalization,
                DynamicFieldset.MarketOpen,
                DynamicFieldset.MessageContents,
                DynamicFieldset.MostRecentTrade,
                DynamicFieldset.MostRecentTradeConditions,
                DynamicFieldset.MostRecentTradeDate,
                DynamicFieldset.MostRecentTradeMarketCenter,
                DynamicFieldset.MostRecentTradeSize,
                DynamicFieldset.MostRecentTradeTime,
                DynamicFieldset.NetAssetValue,
                DynamicFieldset.NumberOfTradesToday,
                DynamicFieldset.Open,
                DynamicFieldset.OpenInterest,
                DynamicFieldset.OpenRange1,
                DynamicFieldset.OpenRange2,
                DynamicFieldset.PercentChange,
                DynamicFieldset.PercentOffAverageVolume,
                DynamicFieldset.PreviousDayVolume,
                DynamicFieldset.PriceEarningsRatio,
                DynamicFieldset.Range,
                DynamicFieldset.RestrictedCode,
                DynamicFieldset.Settle,
                DynamicFieldset.SettlementDate,
                DynamicFieldset.Spread,
                DynamicFieldset.Tick,
                DynamicFieldset.TickID,
                DynamicFieldset.TotalVolume,
                DynamicFieldset.Volatility,
                DynamicFieldset.VWAP,
            };

            // Step 4 - Use the appropriate factory to create the client. Pass a new Level1MessageDynamicHandler to indicate dynamic fields in use
            var level1Client = Level1ClientFactory.CreateNew(new Level1MessageDynamicHandler());

            // Step 5 - Connect it
            level1Client.Connect();

            // Step 6 - Request the feed to begin returning selected fields in Summary and Update messages
            level1Client.SelectUpdateFieldName(fields);

            // Step 7 - Register to appropriate events
            level1Client.Fundamental += Level1ClientOnFundamental;
            level1Client.Summary += Level1ClientOnSummary;
            level1Client.Update += Level1ClientOnSummary;
            level1Client.Timestamp += Level1ClientOnTimestamp;

            // Step 8 - Make your streaming Level 1 requests
            level1Client.ReqWatch("AAPL");

            Console.WriteLine("Watching APPL for the next 10 seconds... Please be patient ;-)\n");
            await Task.Delay(TimeSpan.FromSeconds(10));

            // Step 9 - Unwatch and unregister events
            level1Client.ReqUnwatch("AAPL");

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
            Console.WriteLine(msg.DynamicFields); // dynamic message here
        }

        private void Level1ClientOnFundamental(FundamentalMessage msg)
        {
            Console.WriteLine(msg);
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}