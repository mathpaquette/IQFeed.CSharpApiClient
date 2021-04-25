using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Common
{
    public static class Tasks
    {

        public static async Task RunPeriodicAsync(Action action, TimeSpan interval, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(interval, token).ConfigureAwait(false);
                }
                catch (Exception) { }
                action?.Invoke();
            }
        }
    }
}