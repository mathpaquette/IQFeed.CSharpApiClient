using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Common
{
    /// <summary>
    /// A rate limiter whose slots never come, so a request waiting on it can only end by its own timeout or a disconnect.
    /// Tests use it instead of racing the real limiter's one-second schedule against a timer.
    /// </summary>
    public class FakeExhaustedLookupRateLimiter : LookupRateLimiter
    {
        public FakeExhaustedLookupRateLimiter() : base(LookupDefault.RequestsPerSecond) { }

        public override async Task<bool> TryWaitAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(Timeout.Infinite, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
            }

            return false;
        }
    }
}