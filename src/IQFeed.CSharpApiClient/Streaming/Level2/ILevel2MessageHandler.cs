namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public interface ILevel2MessageHandler : ILevel2Event
    {
        void ProcessMessages(byte[] messageBytes, int count);
    }
}