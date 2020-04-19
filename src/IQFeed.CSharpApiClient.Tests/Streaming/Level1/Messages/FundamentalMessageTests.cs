﻿using System;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    public class FundamentalMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_FundamentalMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "F,MSFT,5,65.6,28624000,97.9500,67.1400,97.9500,83.8300,,,,,,1.49," +
                ",0.06,06,MICROSOFT,MSFT,76.800,1.27,,159851.0,64527.0,03/31/2018,76073.0," +
                "7683198,0.50 02/18/2003,0.50 03/29/1999,14,4,7372,24.82,1,21,05/10/2018," +
                "05/18/2017,05/10/2018,02/09/2018,85.5400,,,," +
                ",511210,,,,09:00:00,17:00:00,USD,125000,HMUZ,0.1," +
                "01/01/2016,TEST,1,";

            // Act
            var fundementalMessageParsed = FundamentalMessage.Parse(message);
            var fundamentalMessage = new FundamentalMessage(
                "MSFT", "5", 65.6d, 28624000, 97.95d, 67.14d, 97.95d, 83.83d, null, null, null, null, null, 1.49d, 
                null, 0.06d, 6, "MICROSOFT", "MSFT", 76.8d, 1.27d, null, 159851d, 64527d, new DateTime(2018, 03, 31), 76073d, 
                7683198d, "0.50 02/18/2003", "0.50 03/29/1999", "14", 4, 7372, 24.82d, "1", "21", new DateTime(2018, 05, 10), 
                new DateTime(2017, 05, 18), new DateTime(2018, 05, 10), new DateTime(2018, 02, 09), 85.54d, null, null, null, 
                null, 511210, null, null, null, new TimeSpan(09,00,00), new TimeSpan(17,00,00), "USD", "125000", "HMUZ", 0.1, 
                new DateTime(2016,01,01),"TEST",1);

            // Assert
            Assert.AreEqual(fundementalMessageParsed, fundamentalMessage);
        }

        [Test]
        public void Should_Get_Different_HashCode()
        {
            // Arrange
            var fundamental1 = new FundamentalMessage("MSFT", "5", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            var fundamental2 = new FundamentalMessage("MSFT", "5", "1.0".ToNullableDouble(), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            // Act
            var hashCode1 = fundamental1.GetHashCode();
            var hashCode2 = fundamental2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2);
        }
    }
}