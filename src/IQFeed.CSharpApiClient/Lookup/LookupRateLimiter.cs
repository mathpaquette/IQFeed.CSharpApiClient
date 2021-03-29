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
        public bool IsRunning { get; private set; }

        private readonly SemaphoreSlim _semaphoreSlim;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly Timer _releaseTimer;
        
        private int _remainderTicks;
        private bool _disposed;

        public LookupRateLimiter(int requestsPerSecond)
        {
            Interval = TimeSpan.FromTicks(TimeSpan.FromSeconds(1).Ticks / requestsPerSecond);
            RequestsPerSecond = requestsPerSecond;

            _stopwatch.Start();
            _semaphoreSlim = new SemaphoreSlim(0, requestsPerSecond);
            _releaseTimer = new Timer(ReleaseTimerCallback, null, TimeSpan.Zero, Interval);
            IsRunning = true;
        }

        public async Task WaitAsync()
        {
            await _semaphoreSlim.WaitAsync().ConfigureAwait(false);
        }

        private void ReleaseTimerCallback(object state)
        {
            var intervalTicks = (int)Interval.Ticks;
            _stopwatch.Stop();
            
            var totalTicks = _remainderTicks + (int)_stopwatch.ElapsedTicks;
            _remainderTicks = totalTicks % intervalTicks;
            var releaseCount = totalTicks / intervalTicks;
            var releaseCapacity = RequestsPerSecond - _semaphoreSlim.CurrentCount;
            
            _stopwatch.Restart();

            if (releaseCapacity == 0 || releaseCount == 0)
                return;

            releaseCount = releaseCount > releaseCapacity ? releaseCapacity : releaseCount;
            _semaphoreSlim.Release(releaseCount);
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
                _releaseTimer?.Dispose();
                _semaphoreSlim?.Dispose();
                IsRunning = false;
            }

            _disposed = true;
        }
    }
}