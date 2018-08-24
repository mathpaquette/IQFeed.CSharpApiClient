using System;

namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public class NoDataIQFeedException : IQFeedException
    {
        protected NoDataIQFeedException()         
        {
            //empty constructor for serialization.
        }

        public NoDataIQFeedException(string exception, string errorMessage) : base(exception, errorMessage) { }
    }
}