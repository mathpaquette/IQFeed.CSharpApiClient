using System;

namespace IQFeed.CSharpApiClient.Extensions.Lookup.Historical
{
    public class HistoricalBar<T>
    {
        public HistoricalBar() { }

        public HistoricalBar(DateTime timestamp, T open, T high, T low, T close, int periodVolume, int periodTrade, long totalVolume, int totalTrade, T wap)
        {
            Timestamp = timestamp;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            PeriodVolume = periodVolume;
            PeriodTrade = periodTrade;
            TotalVolume = totalVolume;
            TotalTrade = totalTrade;
            Wap = wap;
        }

        public DateTime Timestamp { get; set; }
        public T Open { get; set; }
        public T High { get; set; }
        public T Low { get; set; }
        public T Close { get; set; }
        public int PeriodVolume { get; set; }
        public int PeriodTrade { get; set; }
        public long TotalVolume { get; set; }
        public int TotalTrade { get; set; }
        public T Wap { get; set; }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Open)}: {Open}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Close)}: {Close}, {nameof(PeriodVolume)}: {PeriodVolume}, {nameof(PeriodTrade)}: {PeriodTrade}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(TotalTrade)}: {TotalTrade}, {nameof(Wap)}: {Wap}";
        }
    }
}