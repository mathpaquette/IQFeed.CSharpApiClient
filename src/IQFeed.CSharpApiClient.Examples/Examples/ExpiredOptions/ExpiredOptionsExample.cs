using System;
using System.Linq;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Lookup;

namespace IQFeed.CSharpApiClient.Examples.Examples.ExpiredOptions
{
    public class ExpiredOptionsExample : IExample
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(ExpiredOptionsExample).Name;

        /// <summary>
        /// Please note that Expired Options file is huge.
        /// It's about 600 MB so iterating through all rows
        /// will take a while, especially if you don't have SSD.
        /// </summary>
        public void Run()
        {
            var lookupClient = LookupClientFactory.CreateNew();

            Console.WriteLine("Downloading and Caching Expired Options file from IQFeed servers...");
            Console.WriteLine("*** This may take a while the first time... ***\n");

            // Getting the first 10000 expired options with Expiration date >= Today - 180 days
            var expiredOptionsGreaterThan180Days = lookupClient.Symbol.GetAllExpiredOptions()
                .Select(x => x.EquityOption)
                .Where(x => x.Expiration >= DateTime.Now.AddDays(-180))
                .Take(10000) // COMMENT OUT THIS LINE TO GET THEM ALL
                .ToList();

            Console.WriteLine($"Found {expiredOptionsGreaterThan180Days.Count} expired options matching Expiration date >= Today - 180 days");


            // Getting the first 10000 expired options for APPL stock
            var expiredOptionsForSpecificStock = lookupClient.Symbol.GetAllExpiredOptions()
                .Select(x => x.EquityOption)
                .Where(x => x.EquitySymbol == "AAPL")
                .Take(10000) // COMMENT OUT THIS LINE TO GET THEM ALL
                .ToList();

            Console.WriteLine($"Found {expiredOptionsForSpecificStock.Count} expired options for AAPL stock");
        }
    }
}