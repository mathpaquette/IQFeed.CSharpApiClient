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
        private readonly ExceptionFactory _exceptionFactory;
        private readonly TimeSpan _timeout;

        protected BaseLookupFacade(LookupDispatcher lookupDispatcher, ExceptionFactory exceptionFactory, TimeSpan timeout)
        {
            _lookupDispatcher = lookupDispatcher;
            _exceptionFactory = exceptionFactory;
            _timeout = timeout;
        }

        protected async Task<IEnumerable<T>> GetMessagesAsync<T>(string request, Func<byte[], int, MessageContainer<T>> messageHandler)
        {
            var client = await _lookupDispatcher.TakeAsync();

            var messages = new List<T>();
            var ct = new CancellationTokenSource(_timeout);
            var res = new TaskCompletionSource<IEnumerable<T>>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                var container = messageHandler(args.Message, args.Count);

                if (container.ErrorMessage != null)
                {
                    res.TrySetException(_exceptionFactory.CreateNew(container.ErrorMessage, container.MessageTrace));
                    return;
                }

                messages.AddRange(container.Messages);

                if (container.End)
                    res.TrySetResult(messages);
            }

            client.MessageReceived += SocketClientOnMessageReceived;
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