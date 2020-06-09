using System;
using System.Diagnostics;
using IQFeed.CSharpApiClient.Streaming.Level1;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1
{
    [Explicit]
    [Category("Performance")]
    public class Level1DynamicFieldsPerformanceTests
    {
        [Test]
        public void Should_Convert_SevenDayYield()
        {
            // Arrange
            var fields = new[] { DynamicFieldset.Symbol };
            var values = new[] { "AAPL" };

            // Act
            for (int i = 0; i < 5; i++)
            {
                var sw = Stopwatch.StartNew();

                for (int j = 0; j < 1000000; j++)
                {
                    var dynamicFields = new Level1DynamicFields(values, fields);
                }

                sw.Stop();
                Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            }
        }
    }
}