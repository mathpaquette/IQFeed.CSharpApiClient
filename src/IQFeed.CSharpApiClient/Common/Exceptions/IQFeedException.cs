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

        protected bool Equals(IQFeedException other)
        {
            return Request == other.Request && ErrorMessage == other.ErrorMessage && MessageTrace == other.MessageTrace;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IQFeedException)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Request != null ? Request.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ErrorMessage != null ? ErrorMessage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MessageTrace != null ? MessageTrace.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}