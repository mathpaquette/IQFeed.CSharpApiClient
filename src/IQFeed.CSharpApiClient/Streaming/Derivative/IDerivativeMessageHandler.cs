namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public interface IDerivativeMessageHandler : IDerivativeEvent
    {
        void ProcessMessages(byte[] messageBytes, int count);
    }
}