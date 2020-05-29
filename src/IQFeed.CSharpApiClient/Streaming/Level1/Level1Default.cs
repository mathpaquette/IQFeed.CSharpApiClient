using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1Default
    {
        public static TimeSpan SnapshotTimeout = TimeSpan.FromMinutes(1);
        public static string DefaultUpdateSummaryFieldNames = "Most Recent Trade,Most Recent Trade Size,Most Recent Trade Time,Most Recent Trade Market Center,Total Volume,Bid,Bid Size,Ask,Ask Size,Open,High,Low,Close,Message Contents,Most Recent Trade Conditions";
    }
}