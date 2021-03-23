using System;
using System.Collections.Generic;
using System.Diagnostics;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    [Category("Performance")]
    [Explicit]
    public class UpdateSummaryMessagePerformanceTests
    {
        public const string Message = "Q,AAPL,322.9100,13,16:53:16.164236,11,37620407,322.8700,100,323.0000,3300,312.6000,318.4000,312.1900,308.9500,O,8717,2,17";

        [Test]
        public void Should_Parse_Double()
        {
            var messages = new List<UpdateSummaryMessage>();

            var sw = Stopwatch.StartNew();

            for (var i = 0; i < 1000000; i++)
            {
                var parsed = UpdateSummaryMessage.Parse(Message);
                messages.Add(parsed);
            }

            sw.Stop();

            Console.WriteLine(sw.Elapsed.Ticks);
        }
    }
}