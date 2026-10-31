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
        private readonly CancellationTokenSource _stopping = new CancellationTokenSource();
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

        /// <summary>
        /// Waits for the next request slot, giving up when the token is canceled first.
        /// </summary>
        /// <returns><c>true</c> when a slot was taken, <c>false</c> when the wait was canceled</returns>
        public virtual async Task<bool> TryWaitAsync(CancellationToken cancellationToken)
        {
            _started = true; // signal the start
            try
            {
                await _semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }

        private async Task ReleaseSemaphoreAsync(TimeSpan interval, int maxCount)
        {
            // every delay ends early on dispose, so Dispose does not wait out the warm-up below
            var stopping = _stopping.Token;
            try
            {
                // start only after the first request goes through
                while (!_started && _running)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(10), stopping).ConfigureAwait(false);
                }

                if (_running) // if we dispose before the first request no need to wait
                {
                    // calm down the initial burst by delaying leaky bucket operation for one second
                    // this allows gracefully consume the initial per second allowance without going over the limit
                    await Task.Delay(TimeSpan.FromSeconds(1), stopping).ConfigureAwait(false);
                }

                var intervalTicks = (int)interval.Ticks;
                var remainderTicks = 0;
                var sw = new Stopwatch();

                while (_running)
                {
                    sw.Restart();
                    await Task.Delay(interval, stopping).ConfigureAwait(false);
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
            catch (OperationCanceledException) when (stopping.IsCancellationRequested)
            {
                // disposed
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
                _stopping.Cancel();
                _releaseTask.Wait();

                _semaphoreSlim?.Dispose();
                _stopping.Dispose();
            }

            _disposed = true;
        }
    }
}