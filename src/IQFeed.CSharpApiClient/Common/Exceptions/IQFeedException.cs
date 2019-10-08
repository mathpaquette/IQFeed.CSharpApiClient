using System;

namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class IQFeedException : Exception
    {
        /// <summary>
        ///  Parsed exception contained in the message
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Last messages received by IQFeed before throwing the exception
        /// </summary>
        public string MessageTrace { get; }

        public IQFeedException(string message, string errorMessage, string messageTrace) : base(message)
        {
            ErrorMessage = errorMessage;
            MessageTrace = messageTrace;
        }
    }
}