using System;

namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class IQFeedException : Exception
    {
        /// <summary>
        /// Request sent to IQFeed
        /// </summary>
        public string Request { get; }
        
        /// <summary>
        ///  Parsed exception contained in the message
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Last messages received by IQFeed before throwing the exception
        /// </summary>
        public string MessageTrace { get; }

        public IQFeedException(string request, string message, string errorMessage, string messageTrace) : base(message)
        {
            Request = request;
            ErrorMessage = errorMessage;
            MessageTrace = messageTrace;
        }
        
        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Request)}: {Request}, {nameof(ErrorMessage)}: {ErrorMessage}, {nameof(MessageTrace)}: {MessageTrace}";
        }
    }
}