namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Handlers
{
    public interface ILevel1DynamicMessageHandler : ILevel1DynamicEvent
    {
        void ProcessMessages(byte[] messageBytes, int count);
        void SetDynamicFields(params DynamicFieldset[] fieldNames);
    }
}