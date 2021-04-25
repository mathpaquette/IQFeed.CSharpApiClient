using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Socket;
using static IQFeed.CSharpApiClient.Common.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public abstract class BaseLookupFacade
    {
        private readonly LookupDispatcher _lookupDispatcher;
        private readonly LookupRateLimiter _lookupRateLimiter;
        private readonly ExceptionFactory _exceptionFactory;
        private readonly TimeSpan _timeoutInterval;
        private readonly TimeSpan _timeout;

        protected BaseLookupFacade(
            LookupDispatcher lookupDispatcher,
            LookupRateLimiter lookupRateLimiter,
            ExceptionFactory exceptionFactory,
            TimeSpan timeout)
        {
            _lookupRateLimiter = lookupRateLimiter;
            _lookupDispatcher = lookupDispatcher;
            _exceptionFactory = exceptionFactory;

            _timeoutInterval = new TimeSpan(timeout.Ticks / 2);
            _timeout = timeout;
        }

        protected async Task<IEnumerable<T>> GetMessagesAsync<T>(string request, Func<byte[], int, MessageContainer<T>> messageHandler)
        {
            var client = await _lookupDispatcher.TakeAsync();

            var messages = new List<T>();
            var invalidMessages = new List<InvalidMessage<T>>();
            var ct = new CancellationTokenSource();
            var res = new TaskCompletionSource<IEnumerable<T>>();
            var sw = new Stopwatch();

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                sw.Restart(); // reset counter when receiving data
                var container = messageHandler(args.Message, args.Count);

                // exception must be throw at the very end when all messages have been received and parsed to avoid
                // continuation in the next request since we don't use request id
                if (container.ErrorMessage != null)
                {
                    res.TrySetException(_exceptionFactory.CreateNew(request, container.ErrorMessage, container.MessageTrace));
                    return;
                }

                messages.AddRange(container.Messages);
                invalidMessages.AddRange(container.InvalidMessages);

                if (!container.End)
                    return;

                if (invalidMessages.Count > 0)
                {
                    res.TrySetException(_exceptionFactory.CreateNew(request, invalidMessages, messages));
                    return;
                }

                res.TrySetResult(messages);
            }

            void CheckIfRequestTimeout()
            {
                if (sw.Elapsed > _timeout)
                    res.TrySetException(new TimeoutIQFeedException(request));
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            await _lookupRateLimiter.WaitAsync().ConfigureAwait(false);
            client.Send(request);
            sw.Start();
            _ = RunPeriodicAsync(CheckIfRequestTimeout, _timeoutInterval, ct.Token);

            await res.Task.ContinueWith(x =>
            {
                client.MessageReceived -= SocketClientOnMessageReceived;
                _lookupDispatcher.Add(client);
                ct.Cancel();
                ct.Dispose();
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }
    }
}