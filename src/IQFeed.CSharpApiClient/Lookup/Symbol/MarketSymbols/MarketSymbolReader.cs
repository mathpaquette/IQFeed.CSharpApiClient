using System.Collections.Generic;
using System.IO;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols
{
    public class MarketSymbolReader
    {
        public IEnumerable<MarketSymbol> GetMarketSymbols(string filename)
        {
            var lineCount = 0;

            using (var file = new StreamReader(filename))
            {
                string line;
                string[] values;

                while ((line = file.ReadLine()) != null && (values = line.Split('\t')).Length == 8)
                {
                    lineCount++;

                    if (lineCount == 1) // ignore the header
                        continue;

                    yield return new MarketSymbol(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);
                }
            }
        }
    }
}