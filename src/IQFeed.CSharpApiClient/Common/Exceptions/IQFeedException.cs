using System;

namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public class IQFeedException : Exception
    {
        public string ErrorMessage { get; }

        protected IQFeedException()         
        {
            //empty constructor for serialization.
        }

        public IQFeedException(string exception, string errorMessage) : base(exception)
        {
            ErrorMessage = errorMessage;
        }
    }
}