using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Common
{
    /// <summary>
    /// In-process stand-in for the IQFeed lookup port. It confirms the protocol on every connection and answers each request line
    /// with the reply the test provides, or stays silent when that reply is null. Each connection answers its requests in order.
    /// </summary>
    public class FakeLookupServer : IDisposable
    {
        private readonly TcpListener _listener;
        private readonly Func<string, Task<string>> _reply;
        private readonly List<TcpClient> _clients = new List<TcpClient>();
        private int _requestsReceived;
        private int _connectionsAccepted;

        public FakeLookupServer(Func<string, string> reply, int port = 0) : this(request => Task.FromResult(reply(request)), port) { }

        public FakeLookupServer(Func<string, Task<string>> reply, int port = 0)
        {
            _reply = reply;
            _listener = new TcpListener(IPAddress.Loopback, port);
            _listener.Start();
            Port = ((IPEndPoint)_listener.LocalEndpoint).Port;
            _ = AcceptLoopAsync();
        }

        public int Port { get; }

        public int RequestsReceived => Volatile.Read(ref _requestsReceived);

        public int ConnectionsAccepted => Volatile.Read(ref _connectionsAccepted);

        public void Dispose()
        {
            _listener.Stop();
            lock (_clients)
            {
                foreach (var client in _clients)
                    client.Dispose();
            }
        }

        private async Task AcceptLoopAsync()
        {
            while (true)
            {
                TcpClient client;
                try
                {
                    client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);
                }
                catch (Exception)
                {
                    return; // stopped
                }

                Interlocked.Increment(ref _connectionsAccepted);
                lock (_clients)
                    _clients.Add(client);

                _ = ServeAsync(client);
            }
        }

        private async Task ServeAsync(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();
                var reader = new StreamReader(stream, Encoding.ASCII);

                string line;
                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    string reply;
                    if (line.StartsWith("S,SET PROTOCOL"))
                    {
                        reply = $"S,CURRENT PROTOCOL,{IQFeedDefault.ProtocolVersion}\r\n";
                    }
                    else
                    {
                        Interlocked.Increment(ref _requestsReceived);
                        reply = await _reply(line).ConfigureAwait(false);
                    }

                    if (reply == null)
                        continue;

                    var bytes = Encoding.ASCII.GetBytes(reply);
                    await stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                }
            }
            catch (Exception)
            {
                // connection closed by the client or by Dispose
            }
        }
    }
}
