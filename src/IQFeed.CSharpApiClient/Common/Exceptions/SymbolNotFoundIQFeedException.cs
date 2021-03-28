namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class SymbolNotFoundIQFeedException : IQFeedException
    {
        public SymbolNotFoundIQFeedException(string symbol) : base(string.Empty, $"The specified Symbol '{symbol}' wasn't found on IQFeed.", string.Empty, string.Empty) { }
    }
}