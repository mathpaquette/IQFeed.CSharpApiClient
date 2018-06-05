using IQFeed.CSharpApiClient.Lookup.Chains.Futures;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Chains.Futures
{
    public class FutureMonthCodeTests
    {
        [TestCase("F", 1)]
        [TestCase("G", 2)]
        [TestCase("H", 3)]
        [TestCase("J", 4)]
        [TestCase("K", 5)]
        [TestCase("M", 6)]
        [TestCase("N", 7)]
        [TestCase("Q", 8)]
        [TestCase("U", 9)]
        [TestCase("V", 10)]
        [TestCase("X", 11)]
        [TestCase("Z", 12)]
        public void Should_Return_Decoded_Month(string monthCode, int result)
        {
            // Act
            var month = FutureMonthCode.Decode(monthCode);

            // Assert
            Assert.AreEqual(month, result);
        }
    }
}