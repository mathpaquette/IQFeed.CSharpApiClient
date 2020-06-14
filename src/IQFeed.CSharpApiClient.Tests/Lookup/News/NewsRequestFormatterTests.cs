using System;
using IQFeed.CSharpApiClient.Lookup.News;
using IQFeed.CSharpApiClient.Lookup.News.Enums;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.News
{
    public class NewsRequestFormatterTests
    {
        private NewsRequestFormatter _newsRequestFormatter;

        [SetUp]
        public void SetUp()
        {
            _newsRequestFormatter = new NewsRequestFormatter();
        }

        [Test]
        public void Should_ReqNewsConfiguration()
        {
            // Act
            var request = _newsRequestFormatter.ReqNewsConfiguration(FormatType.XML, "TEST123");
            
            // Assert
            var expectedRequest = "NCG,x,TEST123\r\n";
            Assert.AreEqual(request, expectedRequest);
        }

        [Test]
        public void Should_ReqNewsHeadlines()
        {
            // Arrange
            var sources = new[] { "Source1", "Source2" };
            var symbols = new[] { "Symbol1", "Symbol2" };

            // Act
            var request = _newsRequestFormatter.ReqNewsHeadlines(sources, symbols, FormatType.Text, 10, new DateTime(2000, 12, 30), "TEST123");

            // Assert
            var expectedRequest = "NHL,Source1;Source2,Symbol1;Symbol2,t,10,20001230,TEST123\r\n";
            Assert.AreEqual(request, expectedRequest);
        }

        [Test]
        public void Should_ReqNewsStory()
        {
            // Act
            var request = _newsRequestFormatter.ReqNewsStory("1234", NewsFormatType.Email, "test@email.com", "TEST123");

            // Assert
            var expectedRequest = "NSY,1234,e,test@email.com,TEST123\r\n";
            Assert.AreEqual(request, expectedRequest);
        }

        [Test]
        public void Should_ReqNewsStoryCount()
        {
            // Arrange
            var sources = new[] { "Source1", "Source2" };
            var symbols = new[] { "Symbol1", "Symbol2" };

            // Act
            var request = _newsRequestFormatter.ReqNewsStoryCount(symbols, FormatType.XML, sources, new DateTime(2000, 12, 01), new DateTime(2000, 12, 30), "TEST123");

            // Assert
            var expectedRequest = "NSC,Symbol1;Symbol2,x,Source1;Source2,20001201-20001230,TEST123\r\n";
            Assert.AreEqual(request, expectedRequest);
        }
    }
}