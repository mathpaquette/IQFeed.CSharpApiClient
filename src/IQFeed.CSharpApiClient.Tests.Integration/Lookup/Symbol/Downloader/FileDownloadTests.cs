using System;
using System.IO;
using IQFeed.CSharpApiClient.Lookup.Symbol.Downloader;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Symbol.Downloader
{
    public class FileDownloadTests
    {
        private FileDownloader _fileDownloader;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new FileDownloader(new LocalCacheStrategy());
        }

        [Test]
        public void Should_Return_Text_Filename_From_Sample_Archive_File()
        {
            // Act
            var filename = _fileDownloader.GetFile(Settings.MarketSymbolsSampleUrl, false, TimeSpan.FromDays(1));

            // Assert
            Assert.True(filename.EndsWith("mktsymbols_v2_sample.txt"));
        }

        [Test]
        public void Should_Return_Text_Filename_From_Sample_Archive_File_Folder()
        {
            // Arrange
            var downloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dowload_folder");
            CreateDirectory(downloadPath);

            // Act
            var filename = _fileDownloader.GetFile(downloadPath, Settings.MarketSymbolsSampleUrl, false, TimeSpan.FromDays(1));

            // Assert
            Assert.True(filename.EndsWith("mktsymbols_v2_sample.txt"));
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}