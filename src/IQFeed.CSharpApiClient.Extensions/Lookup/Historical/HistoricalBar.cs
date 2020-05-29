using System;

namespace IQFeed.CSharpApiClient.Extensions.Lookup.Historical
{
    public class HistoricalBar
    {
        public HistoricalBar() { }

        public HistoricalBar(DateTime timestamp, double open, double high, double low, double close, int periodVolume, int periodTrade, long totalVolume, int totalTrade, double vwap)
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
            VWAP = vwap;
        }

        public DateTime Timestamp { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int PeriodVolume { get; set; }
        public int PeriodTrade { get; set; }
        public long TotalVolume { get; set; }
        public int TotalTrade { get; set; }
        public double VWAP { get; set; }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Open)}: {Open}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Close)}: {Close}, {nameof(PeriodVolume)}: {PeriodVolume}, {nameof(PeriodTrade)}: {PeriodTrade}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(TotalTrade)}: {TotalTrade}, {nameof(VWAP)}: {VWAP}";
        }
    }
}