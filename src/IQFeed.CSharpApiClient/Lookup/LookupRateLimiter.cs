using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;

namespace IQFeed.CSharpApiClient.Lookup
{
    /// <summary>
    /// Implementation of IQFeed requests rate limit
    /// http://forums.iqfeed.net/index.cfm?page=topic&topicID=5832
    /// </summary>
    public sealed class LookupRateLimiter : IDisposable
    {
        private bool _disposed;
        private volatile bool _running;

        private readonly int _intervalFrequency;
        private readonly int _intervalMilliseconds;
        private readonly int _requestsPerSecond;
        private readonly SemaphoreSlim _sendPermission;
        private readonly Timer _timer;
        private FrequencyStopwatch _frequencyStopwatch;
        private int _returnable;

        public TimeSpan Interval => new TimeSpan(_intervalFrequency);

        public int RequestsPerSecond => _requestsPerSecond;

        public bool IsRunning => _running;

        /// <param name="requestsPerSecond"></param>
        /// <param name="intialPermissions"></param>
        /// <param name="higherPrecision">
        ///     When false, the timer will sleep a fixed amount of time without changing the paramters. this is best for high frequency per second. No heap allocations
        ///     When true, the timer will sleep almost exactly the time remaining until next invocation, best for lower frequecy. 1 heap allocation per tick.
        /// </param>
        public LookupRateLimiter(int requestsPerSecond, int intialPermissions = 0, bool higherPrecision = false)
        {
            if (intialPermissions > requestsPerSecond)
                throw new ArgumentOutOfRangeException(nameof(intialPermissions));
            
            _intervalFrequency = (int)(TimeSpan.FromSeconds(1).Ticks / requestsPerSecond);
            _intervalMilliseconds = (int)new TimeSpan(_intervalFrequency).TotalMilliseconds;
            _frequencyStopwatch = new FrequencyStopwatch(_intervalFrequency);
            _requestsPerSecond = requestsPerSecond;
            _running = true;

            _sendPermission = new SemaphoreSlim(intialPermissions, requestsPerSecond);
            if (intialPermissions != requestsPerSecond)
            {
                _returnable = requestsPerSecond - intialPermissions;
                _frequencyStopwatch.Start();
            }

            _timer = higherPrecision
                ? new Timer(delegate(object context) { ((LookupRateLimiter)context).OnCallbackHighPrecision(); }, this, _intervalMilliseconds, -1)
                : new Timer(delegate(object context) { ((LookupRateLimiter)context).OnCallback(); }, this, _intervalMilliseconds, _intervalMilliseconds);
        }

        public Task WaitAsync()
        {
            return _sendPermission.WaitAsync();
        }

        public void Release()
        {
            lock (_sendPermission)
            {
                //if stopwatch not running, start:
                _frequencyStopwatch.Start();
                _returnable++;
            }
        }

        private void OnCallback()
        {
            if (!_running || !_frequencyStopwatch.IsRunning)
                return;

            int returnableNow;
            lock (_sendPermission)
            {
                if (_returnable == 0)
                    return;

                returnableNow = Math.Min(_returnable, (int)(_frequencyStopwatch.Checkpoint() / _intervalFrequency));
                if (returnableNow == 0)
                    return;

                _returnable -= returnableNow;
            }

            var result = _sendPermission.Release(returnableNow);
            if (returnableNow + result == _requestsPerSecond)
                _frequencyStopwatch.Reset();
        }

        private void OnCallbackHighPrecision()
        {
            if (!_running || !_frequencyStopwatch.IsRunning)
                return;

            int returnableNow;
            lock (_sendPermission)
            {
                if (_returnable == 0)
                    goto _waitNextData;

                returnableNow = (int)(_frequencyStopwatch.Checkpoint() / _intervalFrequency);
                if (returnableNow == 0)
                    goto _waitNextData;

                _returnable -= returnableNow;
            }

            var result = _sendPermission.Release(returnableNow);
            if (returnableNow + result == _requestsPerSecond)
            {
                _frequencyStopwatch.Reset();
                goto _waitNextData;
            }

            _timer.Change(Math.Max(0, _intervalMilliseconds - _frequencyStopwatch.ExcessMilliseconds), -1);
            return;

            _waitNextData:
            _timer.Change(_intervalMilliseconds / 2, -1);
            return;
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            _running = false;
            _timer.Dispose();
            _sendPermission.Dispose();
        }
    }
}