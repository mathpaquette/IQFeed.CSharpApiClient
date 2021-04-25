using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Extensions.Lookup.Historical
{
    public class HistoricalBar
    {
        public const string HistoricalBarDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public HistoricalBar() { }

        public HistoricalBar(
            DateTime timestamp,
            double high,
            double low,
            double open,
            double close,
            long totalVolume,
            long periodVolume,
            int totalTrade,
            int periodTrade,
            double vwap)
        {
            Timestamp = timestamp;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            TotalVolume = totalVolume;
            PeriodVolume = periodVolume;
            TotalTrade = totalTrade;
            PeriodTrade = periodTrade;
            VWAP = vwap;
        }

        public DateTime Timestamp { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public long TotalVolume { get; set; }
        public long PeriodVolume { get; set; }
        public int TotalTrade { get; set; }
        public int PeriodTrade { get; set; }
        public double VWAP { get; set; }

        public static HistoricalBar Parse(string csv)
        {
            var values = csv.SplitFeedMessage();

            return new HistoricalBar(
                DateTime.ParseExact(values[0], HistoricalBarDateTimeFormat, CultureInfo.InvariantCulture),
                double.Parse(values[1], CultureInfo.InvariantCulture),
                double.Parse(values[2], CultureInfo.InvariantCulture),
                double.Parse(values[3], CultureInfo.InvariantCulture),
                double.Parse(values[4], CultureInfo.InvariantCulture),
                long.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture),
                int.Parse(values[8], CultureInfo.InvariantCulture),
            double.Parse(values[9], CultureInfo.InvariantCulture));
        }

        public static IEnumerable<HistoricalBar> ParseFromFile(string path)
        {
            return LookupMessageFileParser.ParseFromFile(Parse, path);
        }

        public string ToCsv()
        {
            return FormattableString.Invariant($"{Timestamp.ToString(HistoricalBarDateTimeFormat, CultureInfo.InvariantCulture)},{High},{Low},{Open},{Close},{TotalVolume},{PeriodVolume},{TotalTrade},{PeriodTrade},{Math.Round(VWAP, 4)}");
        }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Open)}: {Open}, {nameof(Close)}: {Close}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(PeriodVolume)}: {PeriodVolume}, {nameof(TotalTrade)}: {TotalTrade}, {nameof(PeriodTrade)}: {PeriodTrade}, {nameof(VWAP)}: {VWAP}";
        }

        protected bool Equals(HistoricalBar other)
        {
            return Timestamp.Equals(other.Timestamp) &&
                   High.Equals(other.High) &&
                   Low.Equals(other.Low) &&
                   Open.Equals(other.Open) &&
                   Close.Equals(other.Close) &&
                   TotalVolume == other.TotalVolume &&
                   PeriodVolume == other.PeriodVolume &&
                   TotalTrade == other.TotalTrade &&
                   PeriodTrade == other.PeriodTrade &&
                   VWAP.Equals(other.VWAP);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HistoricalBar)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Timestamp.GetHashCode();
                hashCode = (hashCode * 397) ^ High.GetHashCode();
                hashCode = (hashCode * 397) ^ Low.GetHashCode();
                hashCode = (hashCode * 397) ^ Open.GetHashCode();
                hashCode = (hashCode * 397) ^ Close.GetHashCode();
                hashCode = (hashCode * 397) ^ TotalVolume.GetHashCode();
                hashCode = (hashCode * 397) ^ PeriodVolume.GetHashCode();
                hashCode = (hashCode * 397) ^ TotalTrade;
                hashCode = (hashCode * 397) ^ PeriodTrade;
                hashCode = (hashCode * 397) ^ VWAP.GetHashCode();
                return hashCode;
            }
        }
    }
}