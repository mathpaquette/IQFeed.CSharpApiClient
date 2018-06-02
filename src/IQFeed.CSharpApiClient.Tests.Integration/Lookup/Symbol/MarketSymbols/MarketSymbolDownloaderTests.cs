using IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Symbol.MarketSymbols
{
    public class MarketSymbolDownloaderTests
    {
        [Test]
        public void Should_Return_Text_Filename_From_Sample_Archive_File()
        {
            // Arrange
            var marketSymbolDownloader = new MarketSymbolDownloader();

            // Act
            var filename = marketSymbolDownloader.GetMarketSymbolsFile(marketSymbolsUrl: Settings.MarketSymbolsSampleUrl);

            // Assert
            Assert.True(filename.EndsWith("mktsymbols_v2_sample.txt"));
        }
    }
}