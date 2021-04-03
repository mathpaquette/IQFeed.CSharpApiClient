using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Handlers;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Dynamic.Handlers
{
    [Category("Performance")]
    [Explicit]
    public class Level1DynamicMessageHandlerPerformanceTests
    {
        [Test]
        public void Should_Process_Update_Message_With_Standard_Fields()
        {
            // Arrange
            var level1DynamicMessageHandler = new Level1DynamicMessageHandler();
            // set the exact fields that are currently used in UpdateSummaryMessage
            level1DynamicMessageHandler.SetDynamicFields(
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
                DynamicFieldset.MostRecentTradeConditions);
            var msg = "Q,AAPL,322.7500,40,16:53:23.256494,11,37629453,322.6800,100,322.8700,100,312.6000,318.4000,312.1900,308.9500,ba,873D17,\r\n";
            var msgBytes = Encoding.ASCII.GetBytes(msg);
            var count = msgBytes.Length;

            level1DynamicMessageHandler.Update += message => { };

            const int ExecutionsCount = 5;
            var results = new double[ExecutionsCount];

            for (int i = 0; i < ExecutionsCount; i++)
            {
                var sw = Stopwatch.StartNew();
                for (var j = 0; j < 1000000; j++)
                {
                    level1DynamicMessageHandler.ProcessMessages(msgBytes, count);
                }
                sw.Stop();

                results[i] = sw.Elapsed.TotalMilliseconds;
                Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine($"Min: {results.Min()}");
            Console.WriteLine($"Avg: {results.Average()}");
            Console.WriteLine($"Max: {results.Max()}");
        }

        [Test]
        public void Should_Process_Update_Message_With_Less_Fields()
        {
            // Arrange
            var level1DynamicMessageHandler = new Level1DynamicMessageHandler();
            // set less fields that are currently used in UpdateSummaryMessage
            level1DynamicMessageHandler.SetDynamicFields(
                DynamicFieldset.Symbol,
                DynamicFieldset.MostRecentTrade,
                DynamicFieldset.MostRecentTradeSize,
                DynamicFieldset.MostRecentTradeTime,
                DynamicFieldset.TotalVolume);
            var msg = "Q,AAPL,123.0000,50,19:59:53.475143,0,\r\n";
            var msgBytes = Encoding.ASCII.GetBytes(msg);
            var count = msgBytes.Length;

            level1DynamicMessageHandler.Update += message => { };

            const int ExecutionsCount = 5;
            var results = new double[ExecutionsCount];

            for (int i = 0; i < ExecutionsCount; i++)
            {
                var sw = Stopwatch.StartNew();
                for (var j = 0; j < 1000000; j++)
                {
                    level1DynamicMessageHandler.ProcessMessages(msgBytes, count);
                }
                sw.Stop();

                results[i] = sw.Elapsed.TotalMilliseconds;
                Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine($"Min: {results.Min()}");
            Console.WriteLine($"Avg: {results.Average()}");
            Console.WriteLine($"Max: {results.Max()}");
        }

        [Test]
        public void Should_Process_Update_Message_With_More_Fields()
        {
            // Arrange
            var level1DynamicMessageHandler = new Level1DynamicMessageHandler();
            // set more fields that are currently used in UpdateSummaryMessage
            level1DynamicMessageHandler.SetDynamicFields(
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
            var msg = "Q,AAPL,123.0000,50,19:59:53.475143,11,0,123.0000,1700,123.0500,200,,,,123.0000,Cbacv,400142,04/01/2021,,0.0000,\r\n";
            var msgBytes = Encoding.ASCII.GetBytes(msg);
            var count = msgBytes.Length;

            level1DynamicMessageHandler.Update += message => { };

            const int ExecutionsCount = 5;
            var results = new double[ExecutionsCount];

            for (int i = 0; i < ExecutionsCount; i++)
            {
                var sw = Stopwatch.StartNew();
                for (var j = 0; j < 1000000; j++)
                {
                    level1DynamicMessageHandler.ProcessMessages(msgBytes, count);
                }
                sw.Stop();

                results[i] = sw.Elapsed.TotalMilliseconds;
                Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine($"Min: {results.Min()}");
            Console.WriteLine($"Avg: {results.Average()}");
            Console.WriteLine($"Max: {results.Max()}");
        }
    }
}