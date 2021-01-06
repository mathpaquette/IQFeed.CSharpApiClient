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

        public TickMessage(DateTime timestamp, double last, int lastSize, long totalVolume, double bid, double ask,
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
        public long TotalVolume { get; private set; }
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
                timestamp: DateTime.ParseExact(s: values[0], format: TickDateTimeFormat, CultureInfo.InvariantCulture),
                last: double.Parse(values[1], CultureInfo.InvariantCulture),
                lastSize: int.Parse(values[2], CultureInfo.InvariantCulture),
                totalVolume: long.Parse(values[3], CultureInfo.InvariantCulture),
                bid: double.Parse(values[4], CultureInfo.InvariantCulture),
                ask: double.Parse(values[5], CultureInfo.InvariantCulture),
                tickId: long.Parse(values[6], CultureInfo.InvariantCulture),
                basisForLast: char.Parse(values[7]),
                tradeMarketCenter: int.Parse(s: values[8], CultureInfo.InvariantCulture),
                tradeConditions: values[9]);
        }

        public static TickMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new TickMessage(
                timestamp: DateTime.ParseExact(values[1], TickDateTimeFormat, CultureInfo.InvariantCulture),
                last: double.Parse(values[2], CultureInfo.InvariantCulture),
                lastSize: int.Parse(values[3], CultureInfo.InvariantCulture),
                totalVolume: long.Parse(values[4], CultureInfo.InvariantCulture),
                bid: double.Parse(values[5], CultureInfo.InvariantCulture),
                ask: double.Parse(values[6], CultureInfo.InvariantCulture),
                tickId: long.Parse(values[7], CultureInfo.InvariantCulture),
                basisForLast: char.Parse(values[8]),
                tradeMarketCenter: int.Parse(values[9], CultureInfo.InvariantCulture),
                tradeConditions: values[10],
                requestId: requestId);
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