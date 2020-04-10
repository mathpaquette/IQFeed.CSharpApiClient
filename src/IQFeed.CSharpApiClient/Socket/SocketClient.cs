using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Socket
{
    public class SocketClient : IDisposable
    {
        public event EventHandler<SocketMessageEventArgs> MessageReceived;
        public event EventHandler Connected;

        public static bool ForceIPv4 { get; set; } = true;

        private bool _disposed;
        private readonly IPEndPoint _hostEndPoint;
        private readonly System.Net.Sockets.Socket _clientSocket;
        private readonly int _bufferSize;
        private readonly SocketMessageHandler _socketMessageHandler;
        private readonly SocketMessageEventArgs _socketMessageEventArgs;
        private readonly SocketAsyncEventArgs _readEventArgs;

        public SocketClient(string hostname, int port, int bufferSize = 8192)
        {
            // Get host related information.
            IPHostEntry host = Dns.GetHostEntry(hostname);

            // Addres of the host.
            IPAddress[] addressList = ForceIPv4 ?
                host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToArray() :
                host.AddressList;

            _bufferSize = bufferSize;

            // Instantiates the endpoint and socket.
            _hostEndPoint = new IPEndPoint(addressList[addressList.Length - 1], port);
            _clientSocket = new System.Net.Sockets.Socket(_hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _socketMessageHandler = new SocketMessageHandler(_bufferSize, '\n'); // TODO: could be injected in the constructor
            _socketMessageEventArgs = new SocketMessageEventArgs();
            _readEventArgs = new SocketAsyncEventArgs();
        }

        public SocketClient(IPAddress host, int port, int bufferSize = 8192)
        {
            _bufferSize = bufferSize;

            // Instantiates the endpoint and socket.
            _hostEndPoint = new IPEndPoint(host, port);
            _clientSocket = new System.Net.Sockets.Socket(_hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _socketMessageHandler = new SocketMessageHandler(_bufferSize, '\n'); // TODO: could be injected in the constructor
            _socketMessageEventArgs = new SocketMessageEventArgs();
            _readEventArgs = new SocketAsyncEventArgs();
        }

        public void Connect()
        {
            if (_disposed)
                throw new ObjectDisposedException($"Can't connect because SocketClient is disposed.");

            _clientSocket.Connect(_hostEndPoint);
            Connected.RaiseEvent(this, EventArgs.Empty);

            _readEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            _readEventArgs.SetBuffer(new byte[_bufferSize], 0, _bufferSize);

            // As soon as the client is connected, post a receive to the connection 
            bool willRaiseEvent = _clientSocket.ReceiveAsync(_readEventArgs);
            if (!willRaiseEvent)
            {
                ProcessReceive(_readEventArgs);
            }
        }

        public async Task ConnectAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException($"Can't connect because SocketClient is disposed.");

            var tcs = new TaskCompletionSource<bool>();
            var args = new SocketAsyncEventArgs { RemoteEndPoint = _hostEndPoint };
            args.Completed += (sender, eventArgs) => { tcs.SetResult(true); };

            _clientSocket.ConnectAsync(args);
            await tcs.Task;

            if (!_clientSocket.Connected)
            {
                throw new SocketException((int)args.SocketError);
            }

            Connected.RaiseEvent(this, EventArgs.Empty);

            _readEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            _readEventArgs.SetBuffer(new byte[_bufferSize], 0, _bufferSize);
        }

        public void Disconnect() { Dispose(); }

        public async Task DisconnectAsync()
        {
            if (_disposed)
                return;

            var tcs = new TaskCompletionSource<bool>();
            var args = new SocketAsyncEventArgs { RemoteEndPoint = _hostEndPoint };
            args.Completed += (sender, eventArgs) => { tcs.SetResult(true); };

            _clientSocket.DisconnectAsync(args);
            await tcs.Task;
        }

        public void Send(string message)
        {
            if (_disposed)
                throw new ObjectDisposedException($"Can't send because SocketClient is disposed.");

            if (_clientSocket.Connected)
            {
                _clientSocket.Send(Encoding.ASCII.GetBytes(message));
            }
            else
            {
                throw new SocketException((int)SocketError.NotConnected);
            }
        }

        // This method is called whenever a receive or send operation is completed on a socket 
        //
        // <param name="e">SocketAsyncEventArg associated with the completed receive operation</param>
        private void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        protected virtual void ProcessReceive(SocketAsyncEventArgs e)
        {
            // check if the remote host closed the connection
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                _socketMessageHandler.Write(e.Buffer, e.Offset, e.BytesTransferred);
                var count = _socketMessageHandler.TryRead(out var readBytes);

                if (count > 0)
                {
                    _socketMessageEventArgs.Message = readBytes;
                    _socketMessageEventArgs.Count = count;

                    // inform that the socket just received complete message
                    MessageReceived.RaiseEvent(this, _socketMessageEventArgs);
                }

                // don't attempt another receive if socket is already disposed
                if (_disposed)
                    return;

                var willRaiseEvent = _clientSocket.ReceiveAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessReceive(e);
                }
            }
        }

        protected virtual void ProcessSend(SocketAsyncEventArgs e) { throw new NotImplementedException(); }

        #region IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _clientSocket?.Dispose();
            _socketMessageHandler.Dispose();
            _readEventArgs?.Dispose();
            MessageReceived = null;
            Connected = null;
        }

        #endregion
    }
}