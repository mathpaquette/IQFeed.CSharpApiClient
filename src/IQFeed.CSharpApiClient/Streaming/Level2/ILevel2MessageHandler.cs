namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public interface ILevel2MessageHandler<T> : ILevel2Event<T>
    {
        void ProcessMessages(byte[] messageBytes, int count);
    }
}