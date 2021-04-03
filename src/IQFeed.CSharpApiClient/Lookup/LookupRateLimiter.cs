using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup
{
    /// <summary>
    /// Implementation of IQFeed requests rate limit
    /// http://forums.iqfeed.net/index.cfm?page=topic&topicID=5832
    /// </summary>
    public class LookupRateLimiter : IDisposable
    {
        public TimeSpan Interval { get; }
        public int RequestsPerSecond { get; }
        /**
         * By setting MaxCount half of RequestsPerSecond,
         * this will reduce of the initial burst of requests
         */
        public int MaxCount { get; }
        public bool IsRunning => !_releaseTask.IsCompleted;

        private readonly SemaphoreSlim _semaphoreSlim;
        private readonly Task _releaseTask;
        
        private bool _disposed;
        private volatile bool _running = true;

        public LookupRateLimiter(int requestsPerSecond)
        {
            Interval = TimeSpan.FromTicks(TimeSpan.FromSeconds(1).Ticks / requestsPerSecond);
            RequestsPerSecond = requestsPerSecond;
            MaxCount = requestsPerSecond / 2;

            _semaphoreSlim = new SemaphoreSlim(0, MaxCount);
            _releaseTask = ReleaseSemaphoreAsync(Interval, MaxCount);
        }

        public Task WaitAsync()
        {
            return _semaphoreSlim.WaitAsync();
        }

        private async Task ReleaseSemaphoreAsync(TimeSpan interval, int maxCount)
        {
            var intervalTicks = (int)interval.Ticks;
            var remainderTicks = 0;
            var sw = new Stopwatch();

            while (_running)
            {
                sw.Restart();
                await Task.Delay(interval).ConfigureAwait(false);
                sw.Stop();

                var totalTicks = remainderTicks + (int)sw.ElapsedTicks;
                remainderTicks = totalTicks % intervalTicks;
                var releaseCount = totalTicks / intervalTicks;
                var releaseCapacity = maxCount - _semaphoreSlim.CurrentCount;

                if (releaseCapacity == 0 || releaseCount == 0)
                    continue;

                releaseCount = releaseCount > releaseCapacity ? releaseCapacity : releaseCount;
                _semaphoreSlim.Release(releaseCount);
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _running = false;
                _releaseTask.Wait();

                _semaphoreSlim?.Dispose();
            }

            _disposed = true;
        }
    }
}