﻿namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class NoDataIQFeedException : IQFeedException
    {
        public NoDataIQFeedException(string message) : base(message) { }
    }
}