namespace IQFeed.CSharpApiClient.Streaming.Derivative.Messages
{
    public enum IntervalBarType
    {
        /// <summary>
        /// Update type; 'U' - updated interval bar
        /// </summary>
        U,
        
        /// <summary>
        /// 'H' - complete interval bar from history
        /// </summary>
        H,
        
        /// <summary>
        ///  'C' - complete interval bar from stream
        /// </summary>
        C
    }
}