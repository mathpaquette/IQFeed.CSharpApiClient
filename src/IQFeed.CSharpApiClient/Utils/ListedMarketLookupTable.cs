using System.Linq;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;

namespace IQFeed.CSharpApiClient.Utils
{
    public class ListedMarketLookupTable
    {
        // since the number of exchange is expected to be small, it will be faster to use an array instead of concurrent dictionary
        private IListedMarket[] lookupTable;

        public ListedMarketLookupTable(LookupClient lookupClient)
        {
            // get the listed markets
            var listedMarkets = lookupClient.Symbol.ReqListedMarketsAsync().GetAwaiter().GetResult().ToArray();

            // get the maximum value of the listed market id
            int maxId = listedMarkets.Length > 0 ? listedMarkets.Max(lm => lm.ListedMarketId) : -1;

            // allocate enough buffer to fit all 
            lookupTable = new IListedMarket[maxId + 1];

            foreach(var listedMarket in listedMarkets)
            {
                lookupTable[listedMarket.ListedMarketId] = listedMarket;
            }
        }

        /// <summary>
        /// Gets the listed market for the given id
        /// </summary>
        /// <param name="listedMarketId">Listed market id</param>
        /// <returns>Listed market or <code>null</code> if not found</returns>
        public IListedMarket this[int listedMarketId]
        {
            get
            {
                return listedMarketId >= 0 || listedMarketId < lookupTable.Length
                    ? lookupTable[listedMarketId]
                    : null;
            }
        }
    }
}
