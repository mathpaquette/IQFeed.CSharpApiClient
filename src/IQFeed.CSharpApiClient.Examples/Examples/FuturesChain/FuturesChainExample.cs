using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Lookup;


namespace IQFeed.CSharpApiClient.Examples.Examples.FuturesChain
{
    public class FuturesChainExample : IExampleAsync
    {
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
        public string Name => typeof(FuturesChainExample).Name;
        #region symbols
        private static readonly List<string> Symbols = new List<string>()
        {
            "@ES",
            "@NQ",
            "@BTC",
            "@RTY",
            "@YM",
            "@AD",
            "@BP",
            "@BR",
            "@CD",
            "@EU",
            "@JY",
            "@PX",
            "@SF" //,
            //"@ED",
            //"@FV",
            //"@US",
            //"@TY",
            //"@FF",
            //"QCL",
            //"QNG",
            //"QGC",
            //"QHG",
            //"QSI",
            //"@GF",
            //"@LE",
            //"@HE",
            //"@S",
            //"@SM",
            //"@BO",
            //"@C",
            //"@W"
        };
        #endregion

        public async Task RunAsync()
        {
            // Step 1 - !!! Configure your credentials for IQConnect in user environment variable or app.config !!!
            //              Check the documentation for more information.               

            // Step 2 - Run IQConnect launcher
            IQFeedLauncher.Start();

            // Step 3 - Use the appropriate factory to create the client
            var lookupClient = LookupClientFactory.CreateNew();

            // Step 4 - Connect it
            lookupClient.Connect();

            // Step 5 - Make any requests you need or want!
            //var futureChain1 = await lookupClient.Chains.ReqChainFutureAsync("@GC", "FGHJKMNQUVXZ", "89012334567");
            //var futureChain2 = await lookupClient.Chains.ReqChainFutureAsync("@ED", "FGHJKMNQUVXZ", "89012334567");
            //var futureChain3 = await lookupClient.Chains.ReqChainFutureAsync("QCL", "FGHJKMNQUVXZ", "89012334567");

            foreach (var symbol in Symbols)
            {
                var futureChain1 = await lookupClient.Chains.ReqChainFutureAsync(symbol, "FGHJKMNQUVXZ", "89012334567");
                Console.WriteLine($"Fetched {futureChain1.Count(),3:d} contract months for {symbol}:");
                foreach (var msg in futureChain1)
                {
                    Console.WriteLine($"{msg.Expiration:MMM-yyyy} {msg.FutureRoot} {msg.Symbol} ");
                }
            }

            //Console.WriteLine($"Fetched {futureChain2.Count()} Future Chain 2 messages:");
            //foreach (var msg in futureChain2)
            //{
            //    Console.WriteLine($"{msg.Expiration} {msg.FutureRoot} {msg.Symbol} ");
            //}

            //Console.WriteLine($"Fetched {futureChain3.Count()} Future Chain 3 messages:");
            //foreach (var msg in futureChain3)
            //{
            //    Console.WriteLine($"{msg.Expiration} {msg.FutureRoot} {msg.Symbol} ");
            //}
        }

        void IExample.Run()
        {
            throw new System.NotImplementedException();
        }
    }
}

