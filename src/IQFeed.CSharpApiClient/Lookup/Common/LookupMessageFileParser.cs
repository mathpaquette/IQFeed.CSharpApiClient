using System;
using System.Collections.Generic;
using System.IO;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public class LookupMessageFileParser
    {
        public static IEnumerable<T> ParseFromFile<T>(Func<string, T> parserFunc, string path)
        {
            using (var file = new StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line) || line[0] == '!')
                        continue;

                    yield return parserFunc(line);
                }
            }
        }
    }
}