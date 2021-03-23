using System;
using System.Linq;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Lookup;

namespace IQFeed.CSharpApiClient.Examples.Examples.OptionableStocks
{
    public class OptionableStocksExample : IExample
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => nameof(OptionableStocksExample);

        public void Run()
        {
            var lookupClient = LookupClientFactory.CreateNew();

            Console.WriteLine("Downloading and Caching Market Symbols file from IQFeed servers...");
            Console.WriteLine("Please note that this file is updated every day.");
            Console.WriteLine("*** This may take a while the first time... ***\n");

            var marketSymbols = lookupClient.Symbol.GetAllMarketSymbols();
            var optionableStocks = OptionableStock.GetOptionableStocks(marketSymbols).ToList();

            foreach (var optionableStock in optionableStocks)
            {
                Console.WriteLine($"{optionableStock.MarketSymbol.Symbol} has {optionableStock.Options.Count()} options");
            }

            Console.WriteLine($"Found {optionableStocks.Count} stocks with options");
        }
    }
}