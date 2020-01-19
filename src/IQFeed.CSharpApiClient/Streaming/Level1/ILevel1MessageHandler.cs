namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1MessageHandler<T> : ILevel1Event<T>
    {
        void ProcessMessages(byte[] messageBytes, int count);
    }
}