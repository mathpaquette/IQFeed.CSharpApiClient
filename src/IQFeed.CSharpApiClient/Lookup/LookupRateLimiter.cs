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
        private volatile bool _started = false;
        private volatile bool _running = true;

        public LookupRateLimiter(int requestsPerSecond)
        {
            Interval = TimeSpan.FromTicks(TimeSpan.FromSeconds(1).Ticks / requestsPerSecond);
            RequestsPerSecond = requestsPerSecond;
            MaxCount = requestsPerSecond;

            _semaphoreSlim = new SemaphoreSlim(MaxCount, MaxCount);
            _releaseTask = ReleaseSemaphoreAsync(Interval, MaxCount);
        }

        public Task WaitAsync()
        {
            _started = true; // signal the start
            return _semaphoreSlim.WaitAsync();
        }

        private async Task ReleaseSemaphoreAsync(TimeSpan interval, int maxCount)
        {
            // start only after the first request goes through
            while (!_started && _running)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10)).ConfigureAwait(false);
            }

            if (_running) // if we dispose before the first request no need to wait
            {
                // calm down the initial burst by delaying leaky bucket operation for one second
                // this allows gracefully consume the initial per second allowance without going over the limit
                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            }
            
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