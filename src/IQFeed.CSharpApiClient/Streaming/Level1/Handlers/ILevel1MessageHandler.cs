namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public interface ILevel1MessageHandler : ILevel1Event
    {
        void ProcessMessages(byte[] messageBytes, int count);
    }
}