using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Interfaces;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Lookup
{
    public class LookupDispatcher
    {
        // a replacement that cannot connect - IQConnect restarting, say - is retried after this, doubling up to the maximum
        private static readonly TimeSpan ReplacementRetryDelay = TimeSpan.FromMilliseconds(100);
        private static readonly TimeSpan ReplacementRetryMaxDelay = TimeSpan.FromSeconds(10);

        private readonly SemaphoreSlim _semaphoreSlim;
        private readonly List<SocketClient> _socketClients;
        private readonly Queue<SocketClient> _socketClientsAvailable;
        private readonly string _host;
        private readonly int _port;
        private readonly int _bufferSize;
        private readonly string _protocol;
        private readonly IRequestFormatter _requestFormatter;
        private readonly CancellationTokenSource _disconnected = new CancellationTokenSource();

        public LookupDispatcher(string host, int port, int bufferSize, string protocol, int numberOfClients, IRequestFormatter requestFormatter)
        {
            _host = host;
            _port = port;
            _bufferSize = bufferSize;
            _protocol = protocol;
            _requestFormatter = requestFormatter;
            _semaphoreSlim = new SemaphoreSlim(0, numberOfClients);
            _socketClients = new List<SocketClient>();
            _socketClientsAvailable = new Queue<SocketClient>();

            for (var i = 0; i < numberOfClients; i++)
            {
                _socketClients.Add(CreateSocketClient());
            }
        }

        /// <summary>
        /// Canceled by <see cref="DisconnectAll"/>, so requests waiting for a socket or for a reply can fail at once.
        /// </summary>
        internal CancellationToken Disconnected => _disconnected.Token;

        public void ConnectAll()
        {
            foreach (var socketClient in GetSocketClients())
            {
                socketClient.Connect();
            }
        }

        public void DisconnectAll()
        {
            _disconnected.Cancel();

            foreach (var socketClient in GetSocketClients())
            {
                socketClient.Disconnect();
            }
        }

        public Task<SocketClient> TakeAsync()
        {
            return TakeAsync(CancellationToken.None);
        }

        /// <summary>
        /// Waits for a free socket. Throws <see cref="TaskCanceledException"/> when the token is canceled first,
        /// and <see cref="ObjectDisposedException"/> when the dispatcher is disconnected before or while waiting.
        /// </summary>
        public async Task<SocketClient> TakeAsync(CancellationToken cancellationToken)
        {
            using (var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _disconnected.Token))
            {
                try
                {
                    await _semaphoreSlim.WaitAsync(linked.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException) when (_disconnected.IsCancellationRequested)
                {
                    throw new ObjectDisposedException(nameof(LookupDispatcher), "The lookup client is disconnected.");
                }
                catch (OperationCanceledException ex) when (!(ex is TaskCanceledException))
                {
                    // same exception a request timeout has always surfaced as
                    throw new TaskCanceledException("No lookup socket became free before the request was canceled.", ex);
                }
            }

            lock (_socketClientsAvailable)
            {
                return _socketClientsAvailable.Dequeue();
            }
        }

        public void Add(SocketClient socketClient)
        {
            lock (_socketClientsAvailable)
            {
                _socketClientsAvailable.Enqueue(socketClient);
            }
            _semaphoreSlim.Release();
        }

        /// <summary>
        /// Disposes a socket taken with <see cref="TakeAsync()"/> whose state is unknown - it may still receive the rest of an abandoned
        /// response - and connects a new one in its place, retrying until it connects or <see cref="DisconnectAll"/> is called.
        /// The new socket joins the pool once IQFeed confirms the protocol. Call it at most once per socket taken.
        /// </summary>
        public void Replace(SocketClient socketClient)
        {
            socketClient.Disconnect();

            lock (_socketClients)
            {
                _socketClients.Remove(socketClient);
            }

            // off the caller's thread: resolving the host and connecting can take a while, and must not fail the caller's request
            _ = Task.Run(ConnectReplacementAsync);
        }

        private async Task ConnectReplacementAsync()
        {
            var delay = ReplacementRetryDelay;
            while (true)
            {
                SocketClient replacement = null;
                try
                {
                    replacement = CreateSocketClient();
                    lock (_socketClients)
                    {
                        // DisconnectAll cancels before it disposes the list, so a socket added here is always disposed with the others
                        if (_disconnected.IsCancellationRequested)
                        {
                            replacement.Disconnect();
                            return;
                        }

                        _socketClients.Add(replacement);
                    }

                    replacement.Connect();
                    return;
                }
                catch (Exception)
                {
                    if (replacement != null)
                    {
                        lock (_socketClients)
                        {
                            _socketClients.Remove(replacement);
                        }

                        replacement.Disconnect();
                    }
                }

                try
                {
                    await Task.Delay(delay, _disconnected.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    return;
                }

                delay = TimeSpan.FromTicks(Math.Min(delay.Ticks * 2, ReplacementRetryMaxDelay.Ticks));
            }
        }

        private SocketClient CreateSocketClient()
        {
            var socketClient = new SocketClient(_host, _port, _bufferSize);
            socketClient.MessageReceived += OnMessageReceived;
            socketClient.Connected += OnConnected;
            return socketClient;
        }

        private SocketClient[] GetSocketClients()
        {
            lock (_socketClients)
            {
                return _socketClients.ToArray();
            }
        }

        private void OnConnected(object sender, EventArgs eventArgs)
        {
            var socketClient = (SocketClient)sender;
            socketClient.Send(_requestFormatter.SetProtocol(_protocol));
            socketClient.Connected -= OnConnected;
        }

        private void OnMessageReceived(object sender, SocketMessageEventArgs socketMessageEventArgs)
        {
            var socketClient = (SocketClient)sender;
            socketClient.MessageReceived -= OnMessageReceived;  // TODO: validate the protocol confirmation
            Add(socketClient);
        }
    }
}