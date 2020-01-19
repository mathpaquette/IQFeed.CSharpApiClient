using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public class DerivativeClientFactory
    {
        public static DerivativeClient<decimal> CreateNew()
        {
            return CreateNew(
                IQFeedDefault.Hostname,
                IQFeedDefault.DerivativePort,
                DerivativeDefault.BufferSize,
                new DerivativeMessageHandler());
        }
        public static DerivativeClient<T> CreateNew<T>(
            string host,
            int port,
            int bufferSize,
            IDerivativeMessageHandler<T> derivativeMessageHandler)
        {
            return new DerivativeClient<T>(
                new SocketClient(host, port, bufferSize),
                new DerivativeRequestFormatter(),
                derivativeMessageHandler);
        }
    }
}