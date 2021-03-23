using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public class DerivativeClientFactory
    {
        public static DerivativeClient CreateNew(string host, int port, int bufferSize)
        {
            return new DerivativeClient(new SocketClient(host, port, bufferSize), new DerivativeRequestFormatter(), new DerivativeMessageHandler());
        }

        public static DerivativeClient CreateNew()
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.DerivativePort, DerivativeDefault.BufferSize);
        }

        public static DerivativeClient CreateNew(string host, int port)
        {
            return CreateNew(host, port, DerivativeDefault.BufferSize);
        }
    }
}