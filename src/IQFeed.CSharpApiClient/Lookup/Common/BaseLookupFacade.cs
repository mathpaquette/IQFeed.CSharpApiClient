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

        protected BaseLookupFacade(LookupDispatcher lookupDispatcher, LookupRateLimiter lookupRateLimiter, ExceptionFactory exceptionFactory, TimeSpan timeout)
        {
            _lookupRateLimiter = lookupRateLimiter;
            _lookupDispatcher = lookupDispatcher;
            _exceptionFactory = exceptionFactory;
            _timeout = timeout;
        }

        protected async Task<IEnumerable<T>> GetMessagesAsync<T>(string request, Func<byte[], int, MessageContainer<T>> messageHandler)
        {
            var client = await _lookupDispatcher.TakeAsync().ConfigureAwait(false);

            var messages = new List<T>();
            var ct = new CancellationTokenSource(_timeout);
            var res = new TaskCompletionSource<IEnumerable<T>>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                var container = messageHandler(args.Message, args.Count);

                if (container.ErrorMessage != null)
                {
                    client.MessageReceived -= SocketClientOnMessageReceived;
                    _lookupRateLimiter.Release(); //start timer for next permission
                    _lookupDispatcher.Add(client);
                    ct.Dispose();
                    res.TrySetException(_exceptionFactory.CreateNew(request, container.ErrorMessage, container.MessageTrace));
                    return;
                }

                messages.AddRange(container.Messages);

                if (container.End)
                {
                    client.MessageReceived -= SocketClientOnMessageReceived;
                    _lookupRateLimiter.Release(); //start timer for next permission
                    _lookupDispatcher.Add(client);
                    ct.Dispose();
                    res.TrySetResult(messages);
                }
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            await _lookupRateLimiter.WaitAsync(); //ask permission to send request
            client.Send(request);
            
            return await res.Task.ConfigureAwait(false);
        }
    }
}