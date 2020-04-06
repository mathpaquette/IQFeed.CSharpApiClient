namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public interface ILevel1MessageHandler<T> : ILevel1Event<T>
    {
        void ProcessMessages(byte[] messageBytes, int count, DynamicFieldsetHandler dynamicFieldsetHandler = null);
    }
}