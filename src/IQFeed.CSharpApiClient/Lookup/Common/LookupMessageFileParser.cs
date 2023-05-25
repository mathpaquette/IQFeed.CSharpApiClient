using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public class LookupMessageFileParser
    {
        public static IEnumerable<T> ParseFromFile<T>(Func<string, T> parseFunc, string path)
        {
            return ParseFromFile(parseFunc, new StreamReader(path));
        }

        public static IEnumerable<T> ParseFromFile<T>(Func<string, T> parseFunc, Stream stream)
        {
            return ParseFromFile(parseFunc, new StreamReader(stream));
        }

        public static IEnumerable<T> ParseFromFile<T>(Func<string, T> parseFunc, StreamReader file)
        {
            using (file)
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line) || line[0] == '!')
                        continue;

                    yield return parseFunc(line);
                }
            }
        }

        public static IEnumerable<T> ParseFromArchive<T>(Func<string, T> parseFunc, string zipPath)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (var entry in archive.Entries)
                {
                    using (var stream = entry.Open())
                    {
                        foreach (var message in ParseFromFile(parseFunc, stream))
                        {
                            yield return message;
                        }
                    }
                }
            }
        }
    }
}