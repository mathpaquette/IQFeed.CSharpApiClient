using System;
using System.Diagnostics;
using System.Text;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Handlers
{
    [Category("Performance")]
    public class Level1MessageDecimalHandlerPerformanceTests
    {
        [Test]
        public void Should_Process_Update_Message()
        {
            var level1MessageDecimalHandler = new Level1MessageDecimalHandler();

            // Arrange
            var msg = "Q,AAPL,322.7500,40,16:53:23.256494,11,37629453,322.6800,100,322.8700,100,312.6000,318.4000,312.1900,308.9500,ba,873D17,2,17,\r\n";
            var msgBytes = Encoding.ASCII.GetBytes(msg);
            var count = msgBytes.Length;

            level1MessageDecimalHandler.Update += message => { };

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                level1MessageDecimalHandler.ProcessMessages(msgBytes, count);
            }
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
        }
    }
}