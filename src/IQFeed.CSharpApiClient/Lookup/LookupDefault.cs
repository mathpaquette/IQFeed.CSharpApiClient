namespace IQFeed.CSharpApiClient.Lookup
{
    public class LookupDefault
    {
        /// <summary>
        /// Default timeout before cancelling tasks (5 minutes)
        /// This can be changed using LookupClientFactory paramaters 
        /// </summary>
        public const int TimeoutMs = 60 * 1000 * 5;
    }
}