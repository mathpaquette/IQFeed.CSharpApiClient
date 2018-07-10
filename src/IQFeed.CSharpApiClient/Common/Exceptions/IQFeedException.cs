using System;

namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class IQFeedException : Exception
    {
        public string ErrorMessage { get; }

        public IQFeedException(string exception, string errorMessage) : base(exception)
        {
            ErrorMessage = errorMessage;
        }
    }
}