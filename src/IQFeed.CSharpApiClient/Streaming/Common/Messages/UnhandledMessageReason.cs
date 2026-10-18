namespace IQFeed.CSharpApiClient.Streaming.Common.Messages
{
    public enum UnhandledMessageReason
    {
        /// <summary>
        /// The line was empty, or held only the carriage return of a CRLF line ending.
        /// </summary>
        Empty,

        /// <summary>
        /// The line starts with a message type the handler does not know.
        /// </summary>
        UnknownType,

        /// <summary>
        /// Processing the line threw: parsing it, or a subscriber to the event it raised.
        /// </summary>
        ProcessingFailed
    }
}
