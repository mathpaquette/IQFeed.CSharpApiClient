using System;
using System.Collections.Generic;
using System.Net.Sockets;
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
            // the timeout covers waiting for a free socket as well as the reply
            using (var ct = new CancellationTokenSource(_timeout))
            {
                var client = await _lookupDispatcher.TakeAsync(ct.Token).ConfigureAwait(false);
                return await GetMessagesAsync(client, request, messageHandler, ct.Token).ConfigureAwait(false);
            }
        }

        private async Task<IEnumerable<T>> GetMessagesAsync<T>(SocketClient client, string request, Func<byte[], int, MessageContainer<T>> messageHandler, CancellationToken timeout)
        {
            var messages = new List<T>();
            var invalidMessages = new List<InvalidMessage<T>>();
            // completed from timer, disconnect and socket threads, so callers' continuations must never run on them
            var res = new TaskCompletionSource<IEnumerable<T>>(TaskCreationOptions.RunContinuationsAsynchronously);

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

            using (timeout.Register(() => res.TrySetCanceled(), false))
            using (_lookupDispatcher.Disconnected.Register(() => res.TrySetException(new ObjectDisposedException(nameof(LookupDispatcher), "The lookup client is disconnected.")), false))
            {
                client.MessageReceived += SocketClientOnMessageReceived;
                var sent = false;
                var replace = false;
                try
                {
                    // a request that timed out or was disconnected before it was sent leaves the socket clean
                    bool slotTaken;
                    using (var timeoutOrDisconnect = CancellationTokenSource.CreateLinkedTokenSource(timeout, _lookupDispatcher.Disconnected))
                        slotTaken = await _lookupRateLimiter.TryWaitAsync(timeoutOrDisconnect.Token).ConfigureAwait(false);

                    if (slotTaken && !res.Task.IsCompleted)
                    {
                        try
                        {
                            client.Send(request);
                            sent = true;
                        }
                        catch (SocketException)
                        {
                            replace = true; // the socket is dead
                            throw;
                        }
                    }

                    return await res.Task.ConfigureAwait(false);
                }
                catch (TaskCanceledException) when (sent)
                {
                    // IQFeed may still send the rest of the abandoned response on this socket
                    replace = true;
                    throw;
                }
                finally
                {
                    client.MessageReceived -= SocketClientOnMessageReceived;
                    if (replace)
                        _lookupDispatcher.Replace(client);
                    else
                        _lookupDispatcher.Add(client);
                }
            }
        }
    }
}