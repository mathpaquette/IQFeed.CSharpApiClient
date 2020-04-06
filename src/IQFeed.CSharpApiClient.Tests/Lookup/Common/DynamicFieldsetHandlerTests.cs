using System;
using System.Collections;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Tests.Common;
using NSubstitute;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Common
{
    public class DynamicFieldsetHandlerTests
    {
        private BaseLookupMessageHandlerTestClass _baseLookupMessageHandlerTestClass;
        private Func<string, string> _parserFunc;
        private Func<string[], string> _errorParserFunc;

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Should_Return_Union_Array()
        {
            // Arrange
            var dynamicFieldsetHandler = new DynamicFieldsetHandler();
            dynamicFieldsetHandler.SetFields(DynamicFieldset.MostRecentTradeAggressor);

            // Act
            var combinedList = dynamicFieldsetHandler.GetFullFieldsetList();

            // Assert
            Assert.True(combinedList.Length == IQFeedDefault.DefaultLevel1SummaryFields.Length + 1);
            Assert.True(combinedList[combinedList.Length - 1] == DynamicFieldset.MostRecentTradeAggressor);
        }
    }
}