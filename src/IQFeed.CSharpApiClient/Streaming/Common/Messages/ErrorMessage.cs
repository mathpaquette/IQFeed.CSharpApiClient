using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Common.Messages
{
    public class ErrorMessage
    {
        public ErrorMessage(string error)
        {
            Error = error;
        }

        public string Error { get; }

        public static ErrorMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            return new ErrorMessage(values[1]);
        }

        public override bool Equals(object obj)
        {
            return obj is ErrorMessage message && Error == message.Error;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 2010064793 + Error.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"{nameof(Error)}: {Error}";
        }
    }
}