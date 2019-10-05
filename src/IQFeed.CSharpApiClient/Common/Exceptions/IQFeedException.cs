using System;

namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class IQFeedException : Exception
    {
        public IQFeedException(string message) : base(message) { }
    }
}