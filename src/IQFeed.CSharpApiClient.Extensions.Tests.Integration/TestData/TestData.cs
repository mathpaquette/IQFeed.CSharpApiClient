using System.IO;
using System.IO.Compression;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Extensions.Tests.Integration.TestData
{
    public class TestData
    {
        public static string GetFileName(TestDataType type, DataDirection direction, bool protocol60 = false)
        {
            var protocol60String = protocol60 ? "60" : string.Empty;
            var filename = $@"{type.ToString().ToLower()}_{direction.ToString().ToLower()}{protocol60String}";
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "data", $@"{filename}.zip");
            var tmpDir = GetTemporaryDirectory();
            ZipFile.ExtractToDirectory(path, tmpDir);
            return Path.Combine(tmpDir, $@"{filename}.csv");
        }

        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}