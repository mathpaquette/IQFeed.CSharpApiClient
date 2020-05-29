namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1MessageHandler : ILevel1Event
    {
        void ProcessMessages(byte[] messageBytes, int count);
    }
}