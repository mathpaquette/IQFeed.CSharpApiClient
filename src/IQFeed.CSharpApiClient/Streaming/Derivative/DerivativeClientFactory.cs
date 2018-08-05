using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public class DerivativeClientFactory
    {
        public static DerivativeClient CreateNew(string host = IQFeedDefault.Hostname, int port = IQFeedDefault.DerivativePort, int bufferSize = DerivativeDefault.BufferSize)
        {
            return new DerivativeClient(
                new SocketClient(host, port, bufferSize), 
                new DerivativeRequestFormatter(), 
                new DerivativeMessageHandler());
        }
    }
}