using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class TickMessage : ITickMessage
    {
        public const string TickDateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffffff";

        public TickMessage(DateTime timestamp, double last, int lastSize, int totalVolume, double bid, double ask,
            long tickId, char basisForLast, int tradeMarketCenter, string tradeConditions, string requestId = null)
        {
            Timestamp = timestamp;
            Last = last;
            LastSize = lastSize;
            TotalVolume = totalVolume;
            Bid = bid;
            Ask = ask;
            TickId = tickId;
            BasisForLast = basisForLast;
            TradeMarketCenter = tradeMarketCenter;
            TradeConditions = tradeConditions;
            RequestId = requestId;
        }

        public DateTime Timestamp { get; private set; }
        public double Last { get; private set; }
        public int LastSize { get; private set; }
        public int TotalVolume { get; private set; }
        public double Bid { get; private set; }
        public double Ask { get; private set; }
        public long TickId { get; private set; }
        public char BasisForLast { get; private set; }
        public int TradeMarketCenter { get; private set; }
        public string TradeConditions { get; private set; }
        public string RequestId { get; private set; }

        public static TickMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new TickMessage(
                DateTime.ParseExact(values[0], TickDateTimeFormat, CultureInfo.InvariantCulture),
                double.Parse(values[1], CultureInfo.InvariantCulture),
                int.Parse(values[2], CultureInfo.InvariantCulture),
                int.Parse(values[3], CultureInfo.InvariantCulture),
                double.Parse(values[4], CultureInfo.InvariantCulture),
                double.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                char.Parse(values[7]),
                int.Parse(values[8], CultureInfo.InvariantCulture),
                values[9]);
        }

        public static bool TryParse(string message, out TickMessage tickMessage)
        {
            tickMessage = default;
            DateTime timestamp = default;
            double last = default;
            int lastSize = default;
            int totalVolume = default;
            double bid = default;
            double ask = default;
            long tickId = default;
            char basisForLast = default;
            int tradeMarketCenter = default;

            var values = message.SplitFeedMessage();
            var parsed = values.Length >= 10 &&
                         DateTime.TryParseExact(values[0], TickDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out timestamp) &&
                         double.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out last) &&
                         int.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out lastSize) &&
                         int.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out totalVolume) &&
                         double.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out bid) &&
                         double.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out ask) &&
                         long.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out tickId) &&
                         char.TryParse(values[7], out basisForLast) &&
                         int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out tradeMarketCenter);

            if (!parsed)
                return false;

            tickMessage = new TickMessage(timestamp, last, lastSize, totalVolume, bid, ask, tickId, basisForLast, tradeMarketCenter, values[9]);
            return true;
        }

        public static TickMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            return new TickMessage(
                DateTime.ParseExact(values[1], TickDateTimeFormat, CultureInfo.InvariantCulture),
                double.Parse(values[2], CultureInfo.InvariantCulture),
                int.Parse(values[3], CultureInfo.InvariantCulture),
                int.Parse(values[4], CultureInfo.InvariantCulture),
                double.Parse(values[5], CultureInfo.InvariantCulture),
                double.Parse(values[6], CultureInfo.InvariantCulture),
                long.Parse(values[7], CultureInfo.InvariantCulture),
                char.Parse(values[8]),
                int.Parse(values[9], CultureInfo.InvariantCulture),
                values[10],
                values[0]);
        }

        public static IEnumerable<TickMessage> ParseFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(Parse, path)
                : LookupMessageFileParser.ParseFromFile(ParseWithRequestId, path);
        }

        public string ToCsv()
        {
            return RequestId == null
                ? FormattableString.Invariant($"{Timestamp.ToString(TickDateTimeFormat, CultureInfo.InvariantCulture)},{Last},{LastSize},{TotalVolume},{Bid},{Ask},{TickId},{BasisForLast},{TradeMarketCenter},{TradeConditions}")
                : FormattableString.Invariant($"{RequestId},{Timestamp.ToString(TickDateTimeFormat, CultureInfo.InvariantCulture)},{Last},{LastSize},{TotalVolume},{Bid},{Ask},{TickId},{BasisForLast},{TradeMarketCenter},{TradeConditions}");
        }

        public override bool Equals(object obj)
        {
            return obj is TickMessage message &&
                   RequestId == message.RequestId &&
                   Timestamp == message.Timestamp &&
                   Equals(Last, message.Last) &&
                   LastSize == message.LastSize &&
                   TotalVolume == message.TotalVolume &&
                   Equals(Bid, message.Bid) &&
                   Equals(Ask, message.Ask) &&
                   TickId == message.TickId &&
                   BasisForLast == message.BasisForLast &&
                   TradeMarketCenter == message.TradeMarketCenter &&
                   TradeConditions == message.TradeConditions;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + Timestamp.GetHashCode();
                hash = hash * 29 + Last.GetHashCode();
                hash = hash * 29 + LastSize.GetHashCode();
                hash = hash * 29 + TotalVolume.GetHashCode();
                hash = hash * 29 + Bid.GetHashCode();
                hash = hash * 29 + Ask.GetHashCode();
                hash = hash * 29 + TickId.GetHashCode();
                hash = hash * 29 + BasisForLast.GetHashCode();
                hash = hash * 29 + TradeMarketCenter.GetHashCode();
                hash = hash * 29 + TradeConditions.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Last)}: {Last}, {nameof(LastSize)}: {LastSize}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(Bid)}: {Bid}, {nameof(Ask)}: {Ask}, {nameof(TickId)}: {TickId}, {nameof(BasisForLast)}: {BasisForLast}, {nameof(TradeMarketCenter)}: {TradeMarketCenter}, {nameof(TradeConditions)}: {TradeConditions}, {nameof(RequestId)}: {RequestId}";
        }
    }
}