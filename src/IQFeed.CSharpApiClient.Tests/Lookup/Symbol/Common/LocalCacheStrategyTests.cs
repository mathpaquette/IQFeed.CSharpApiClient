using System;
using System.IO;
using IQFeed.CSharpApiClient.Lookup.Symbol.Downloader;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Symbol.Common
{
    public class LocalCacheStrategyTests
    {
        private LocalCacheStrategy _localCacheStrategy;

        [SetUp]
        public void SetUp()
        {
            _localCacheStrategy = new LocalCacheStrategy();
        }

        [Test]
        public void Should_Expire_When_File_Does_Not_Exist()
        {
            // Act
            var expired = _localCacheStrategy.HasExpired("invalid_path.txt", TimeSpan.MaxValue);

            // Assert
            Assert.True(expired);
        }

        [Test]
        public void Should_Not_Expire_When_File_Recently_Written()
        {
            // Arrange
            var file = Path.GetTempFileName();
            File.AppendAllText(file, "content");

            // Act
            var expired = _localCacheStrategy.HasExpired(file, TimeSpan.FromMinutes(1));

            // Assert
            Assert.False(expired);
        }
    }
}