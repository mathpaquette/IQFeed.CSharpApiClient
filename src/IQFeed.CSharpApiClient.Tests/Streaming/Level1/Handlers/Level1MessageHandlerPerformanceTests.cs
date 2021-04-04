using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Handlers
{
    [Category("Performance")]
    [Explicit]
    public class Level1MessageHandlerPerformanceTests
    {
        [Test]
        public void Should_Process_Update_Message()
        {
            var level1MessageHandler = new Level1MessageHandler();

            // Arrange
            var msg = "Q,AAPL,322.7500,40,16:53:23.256494,11,37629453,322.6800,100,322.8700,100,312.6000,318.4000,312.1900,308.9500,ba,873D17,2,17\r\n";
            var msgBytes = Encoding.ASCII.GetBytes(msg);
            var count = msgBytes.Length;

            level1MessageHandler.Update += message => { };

            const int ExecutionsCount = 5;
            var results = new double[ExecutionsCount];

            for (int i = 0; i < ExecutionsCount; i++)
            {
                var sw = Stopwatch.StartNew();
                for (var j = 0; j < 1000000; j++)
                {
                    level1MessageHandler.ProcessMessages(msgBytes, count);
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