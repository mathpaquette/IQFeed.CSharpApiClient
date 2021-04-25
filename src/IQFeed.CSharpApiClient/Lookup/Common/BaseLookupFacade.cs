using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public abstract class BaseLookupFacade
    {
        private readonly LookupDispatcher _lookupDispatcher;
        private readonly LookupRateLimiter _lookupRateLimiter;
        private readonly ExceptionFactory _exceptionFactory;
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
            _timeout = timeout;
        }

        protected async Task<IEnumerable<T>> GetMessagesAsync<T>(string request, Func<byte[], int, MessageContainer<T>> messageHandler)
        {
            var client = await _lookupDispatcher.TakeAsync();

            var messages = new List<T>();
            var invalidMessages = new List<InvalidMessage<T>>();
            var ct = new CancellationTokenSource(_timeout);
            var res = new TaskCompletionSource<IEnumerable<T>>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
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

                if (!container.End) return;

                if (invalidMessages.Count > 0)
                {
                    res.TrySetException(_exceptionFactory.CreateNew(request, invalidMessages, messages));
                    return;
                }

                res.TrySetResult(messages);
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            await _lookupRateLimiter.WaitAsync().ConfigureAwait(false);
            client.Send(request);

            await res.Task.ContinueWith(x =>
            {
                client.MessageReceived -= SocketClientOnMessageReceived;
                _lookupDispatcher.Add(client);
                ct.Dispose();
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }
    }
}