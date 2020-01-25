namespace IQFeed.CSharpApiClient.Streaming.Derivative.Handlers
{
    public interface IDerivativeMessageHandler<T> : IDerivativeEvent<T>
    {
        void ProcessMessages(byte[] messageBytes, int count);
    }
}