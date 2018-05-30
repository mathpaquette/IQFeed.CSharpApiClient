using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Streaming.Level1;

namespace IQFeed.CSharpApiClient.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            RunHistoricalExample();
            RunLevel1Example();

            Console.WriteLine("*************************************");
            Console.WriteLine("**    Press enter to continue.     **");
            Console.WriteLine("*************************************");

            Console.ReadLine();
        }

        static async void RunHistoricalExample()
        {
            // *************************************

            // Step 1 - !!! Configure your credentials for IQConnect in user environment variable or app.config !!!
            //              Check the documentation for more information.               

            // Step 2 - Run IQConnect launcher
            IQFeedLauncher.Start();

            // Step 3 - Use the appropriate factory to create the client
            var lookupClient = LookupClientFactory.CreateNew();

            // Step 4 - Connect it
            lookupClient.Connect();

            // Step 5 - Make any requests you need or want!
            var ticksMessages = await lookupClient.Historical.ReqHistoryTickDatapointsAsync("AAPL", 100);
            var ticksFilename = await lookupClient.Historical.Raw.ReqHistoryTickDaysAsync("AAPL", 100);

            // *************************************
        }

        static async void RunLevel1Example()
        {
            // *************************************

            // Step 1 - !!! Configure your credentials for IQConnect in user environment variable or app.config !!!
            //              Check the documentation for more information.               

            // Step 2 - Run IQConnect launcher
            IQFeedLauncher.Start();

            // Step 3 - Use the appropriate factory to create the client
            var level1Client = Level1ClientFactory.CreateNew();

            // Step 4 - Connect it
            level1Client.Connect();

            // Step 5 - Register to appropriate events
            level1Client.Timestamp += timestampMsg => Console.WriteLine(timestampMsg);
            level1Client.Update += updateMsg => Console.WriteLine(updateMsg);

            // Step 6 - Make your streaming Leve1 requests
            level1Client.ReqWatch("AAPL");

            await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }
}
