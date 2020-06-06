using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Examples.Examples.StreamingLevel1
{
    public class StreamingLevel1DynamicExample : IExampleAsync
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(StreamingLevel1Example).Name;

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
                //Feed always includes Symbol as first field, regardless of request
                //DynamicFieldset.Symbol,

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

            // Step 4 - Pass the array of fields to Level1ClientFactory.CreateNew() to create the dynamic client
            var level1Client = Level1ClientFactory.CreateNew(fields);

            // Step 5 - Connect it
            level1Client.Connect();

            // Step 6 - Register to appropriate events
            // (use SummaryDynamic and UpdateDynamic instead of Summary and Update when using dynamic fields)
            level1Client.Fundamental += Level1ClientOnFundamental;
            level1Client.SummaryDynamic += Level1ClientOnSummaryDynamic;
            level1Client.UpdateDynamic += Level1ClientOnUpdateDynamic;
            level1Client.Timestamp += Level1ClientOnTimestamp;
            level1Client.Error += Level1ClientOnError;

            // Step 7 - Make your streaming Level 1 requests
            var _watchSymbol = "AAPL";
            level1Client.ReqWatch(_watchSymbol);

            Console.WriteLine("Watching APPL for the next 10 seconds... Please be patient ;-)\n");
            await Task.Delay(TimeSpan.FromSeconds(10));

            // Step 8 - Unwatch and unregister events
            level1Client.ReqUnwatch(_watchSymbol);

            level1Client.Fundamental -= Level1ClientOnFundamental;
            level1Client.SummaryDynamic -= Level1ClientOnSummaryDynamic;
            level1Client.UpdateDynamic -= Level1ClientOnUpdateDynamic;
            level1Client.Timestamp -= Level1ClientOnTimestamp;
        }
        private void Level1ClientOnTimestamp(TimestampMessage msg)
        {
            Console.WriteLine($"TimestampMessage:Level1ClientOnTimestamp: [{msg}]");
        }

        private void Level1ClientOnSummaryDynamic(UpdateSummaryMessageDynamic msg)
        {
            Console.WriteLine($"UpdateSummaryMessageDynamic:Level1ClientOnSummaryDynamic: [{msg}");
            Console.WriteLine($"]");
        }

        private void Level1ClientOnUpdateDynamic(UpdateSummaryMessageDynamic msg)
        {
            Console.WriteLine($"UpdateSummaryMessageDynamic:Level1ClientOnUpdateDynamic: [{msg}");
            Console.WriteLine($"]");
        }
        private void Level1ClientOnFundamental(FundamentalMessage msg)
        {
            Console.WriteLine($"FundamentalMessage:Level1ClientOnFundamental: [{msg}");
            Console.WriteLine($"]");
        }

        private void Level1ClientOnError(ErrorMessage msg)
        {
            Console.WriteLine($"ErrorMessage:Level1ClientOnError: [{msg}]");
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}