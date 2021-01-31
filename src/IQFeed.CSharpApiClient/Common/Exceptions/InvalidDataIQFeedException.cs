namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class InvalidDataIQFeedException : IQFeedException
    {
        public const string InvalidData = "Invalid data";
        private const string InvalidDataMessage = "Unable to parse received data.";

        public InvalidDataIQFeedException(string request, string errorMessage, string messageTrace) : base(request, InvalidDataMessage, errorMessage, messageTrace) { }
    }
}