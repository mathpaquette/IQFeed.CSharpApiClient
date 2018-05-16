using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Socket
{
    public class SocketClient
    {
        public event EventHandler<SocketMessageEventArgs> MessageReceived;
        public event EventHandler Connected;

        private readonly IPEndPoint _hostEndPoint;
        private readonly System.Net.Sockets.Socket _clientSocket;
        private readonly int _bufferSize;
        private readonly SocketMessageHandler _socketMessageHandler;
        private readonly SocketMessageEventArgs _socketMessageEventArgs;

        public SocketClient(string hostname, int port, int bufferSize = 8192)
        {
            // Get host related information.
            IPHostEntry host = Dns.GetHostEntry(hostname);

            // Addres of the host.
            IPAddress[] addressList = host.AddressList;

            _bufferSize = bufferSize;

            // Instantiates the endpoint and socket.
            _hostEndPoint = new IPEndPoint(addressList[addressList.Length - 1], port);
            _clientSocket = new System.Net.Sockets.Socket(_hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _socketMessageHandler = new SocketMessageHandler(_bufferSize, '\n');    // TODO: could be injected in the constructor
            _socketMessageEventArgs = new SocketMessageEventArgs();
        }

        public void Connect()
        {
            _clientSocket.Connect(_hostEndPoint);
            Connected.RaiseEvent(this, EventArgs.Empty);

            SocketAsyncEventArgs readEventArgs = new SocketAsyncEventArgs();
            readEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            readEventArgs.SetBuffer(new byte[_bufferSize], 0, _bufferSize);

            // As soon as the client is connected, post a receive to the connection 
            bool willRaiseEvent = _clientSocket.ReceiveAsync(readEventArgs);
            if (!willRaiseEvent)
            {
                ProcessReceive(readEventArgs);
            }
        }

        public void Send(string message)
        {
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

                var willRaiseEvent = _clientSocket.ReceiveAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessReceive(e);
                }
            }
        }

        protected virtual void ProcessSend(SocketAsyncEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}