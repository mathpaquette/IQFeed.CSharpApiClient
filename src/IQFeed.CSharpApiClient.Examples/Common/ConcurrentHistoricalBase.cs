using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;

namespace IQFeed.CSharpApiClient.Examples.Common
{
    public abstract class ConcurrentHistoricalBase
    {
        protected readonly LookupClient<decimal> LookupClient;
        protected readonly ConcurrentQueue<string> Symbols;
        protected int InitialCount = 0;

        private readonly Task[] _concurrentTasks;

        protected ConcurrentHistoricalBase(LookupClient<decimal> lookupClient, int numberOfClients)
        {
            LookupClient = lookupClient;
            Symbols = new ConcurrentQueue<string>();
            _concurrentTasks = new Task[numberOfClients];
        }

        public void Start()
        {
            InitialCount = Symbols.Count;
            
            Console.WriteLine($"Number of Concurrent Tasks: {_concurrentTasks.Length}");
            Console.WriteLine($"Number of Symbols: {InitialCount}");
            Console.Write("Status: ");
            
            for (var i = 0; i < _concurrentTasks.Length; i++)
            {
                _concurrentTasks[i] = Task.Run(async () => await ProcessSymbols());
            }

            Task.WaitAll(_concurrentTasks);
        }

        protected void ShowDownloadStatus()
        {
            var count = Symbols.Count;
            if (count % 10 != 0) return;

            var progress = (InitialCount - count) / (decimal)InitialCount * 100;
            Console.Write($"{progress}%...");
        }

        protected abstract Task ProcessSymbols();
    }
}