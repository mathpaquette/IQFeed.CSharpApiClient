namespace IQFeed.CSharpApiClient.Common
{
    public class InvalidMessage<T>
    {
        public T Message { get; }
        public string Data { get; }

        public InvalidMessage(T message, string data)
        {
            Message = message;
            Data = data;
        }

        public override string ToString()
        {
            return $"{nameof(Message)}: {Message}, {nameof(Data)}: {Data}";
        }
    }
}