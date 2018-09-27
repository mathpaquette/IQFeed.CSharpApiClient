using System.Collections.Generic;
using System.IO;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.ExpiredOptions
{
    public class ExpiredOptionReader
    {
        public IEnumerable<ExpiredOption> GetExpiredOptions(string filename, bool header = false)
        {
            var lineCount = 0;
            using (var file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    lineCount++;

                    // ignore the header if required
                    if (header && lineCount == 1)
                        continue;
                 
                    yield return ExpiredOption.Parse(line);
                }
            }
        }
    }
}