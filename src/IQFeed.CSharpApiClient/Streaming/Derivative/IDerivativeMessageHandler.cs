namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public interface IDerivativeMessageHandler<T> : IDerivativeEvent<T>
    {
        void ProcessMessages(byte[] messageBytes, int count);
    }
}