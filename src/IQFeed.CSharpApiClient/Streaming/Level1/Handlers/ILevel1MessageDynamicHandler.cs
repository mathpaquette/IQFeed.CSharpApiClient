namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public interface ILevel1MessageDynamicHandler : ILevel1MessageHandler
    {
        void SetDynamicFields(params DynamicFieldset[] fieldNames);
    }
}