using System;

namespace IQFeed.CSharpApiClient.Streaming.Common.Messages
{
    /// <summary>
    /// A line received from IQFeed that the message handler could not process. The line is skipped and the rest of the batch is still processed.
    /// </summary>
    public class UnhandledMessage
    {
        public UnhandledMessage(string message, UnhandledMessageReason reason, Exception exception)
        {
            Message = message;
            Reason = reason;
            Exception = exception;
        }

        /// <summary>
        /// The raw line, without its line feed.
        /// </summary>
        public string Message { get; private set; }

        public UnhandledMessageReason Reason { get; private set; }

        /// <summary>
        /// The exception thrown while processing the line; null unless <see cref="Reason"/> is <see cref="UnhandledMessageReason.ProcessingFailed"/>.
        /// </summary>
        public Exception Exception { get; private set; }

        public override string ToString()
        {
            return $"{nameof(Reason)}: {Reason}, {nameof(Message)}: {Message}, {nameof(Exception)}: {Exception?.Message}";
        }
    }
}
