using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    public class FundamentalMessageTests
    {
        [Test]
        public void Should_Return_FundamentalMessage()
        {
            // TODO: full fundamental message assertion
            var message = @"F,MSFT,5,65.6,28624000,97.9500,67.1400,97.9500,83.8300,,,,,,,,,,,1.49,,0.06,06,,MICROSOFT,MSFT,76.800,1.27,,159851.0,64527.0,03/31/2018,76073.0,7683198,,0.50 02/18/2003,0.50 03/29/1999,,,14,4,7372,24.82,1,21,05/10/2018,05/18/2017,05/10/2018,02/09/2018,85.5400,,,,,511210,,,,";
            var values = Level1MessageHandler.GetValuesFromMessage(message);
            var fundamentalMsg = FundamentalMessage.CreateFundamentalMessage(values);

            Assert.AreEqual(fundamentalMsg.Symbol, "MSFT");
            Assert.AreEqual(fundamentalMsg.ExchangeId, "5");

            Assert.AreEqual(fundamentalMsg.OptionsPremiumMultiplier, null);
            Assert.AreEqual(fundamentalMsg.OptionsMultipleDeliverables, null);
        }
    }
}