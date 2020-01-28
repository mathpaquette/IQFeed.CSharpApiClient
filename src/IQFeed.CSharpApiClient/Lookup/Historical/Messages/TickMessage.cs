using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public abstract class TickMessage
    {
        public const string TickDateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffffff";

        public static TickMessage<decimal> Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new TickMessage<decimal>(
                DateTime.ParseExact(values[0], TickDateTimeFormat, CultureInfo.InvariantCulture),
                decimal.Parse(values[1], CultureInfo.InvariantCulture),
                int.Parse(values[2], CultureInfo.InvariantCulture),
                int.Parse(values[3], CultureInfo.InvariantCulture),
                decimal.Parse(values[4], CultureInfo.InvariantCulture),
                decimal.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                char.Parse(values[7]),
                int.Parse(values[8], CultureInfo.InvariantCulture),
                values[9]);
        }

        public static TickMessage<decimal> ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new TickMessage<decimal>(
                DateTime.ParseExact(values[1], TickDateTimeFormat, CultureInfo.InvariantCulture),
                decimal.Parse(values[2], CultureInfo.InvariantCulture),
                int.Parse(values[3], CultureInfo.InvariantCulture),
                int.Parse(values[4], CultureInfo.InvariantCulture),
                decimal.Parse(values[5], CultureInfo.InvariantCulture),
                decimal.Parse(values[6], CultureInfo.InvariantCulture),
                long.Parse(values[7], CultureInfo.InvariantCulture),
                char.Parse(values[8]),
                int.Parse(values[9], CultureInfo.InvariantCulture),
                values[10],
                requestId);
        }

        public static TickMessage<double> ParseDouble(string message)
        {
            var values = message.SplitFeedMessage();

            return new TickMessage<double>(
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

        public static TickMessage<double> ParseDoubleWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new TickMessage<double>(
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
                requestId);
        }

        public static TickMessage<float> ParseFloat(string message)
        {
            var values = message.SplitFeedMessage();

            return new TickMessage<float>(
                DateTime.ParseExact(values[0], TickDateTimeFormat, CultureInfo.InvariantCulture),
                float.Parse(values[1], CultureInfo.InvariantCulture),
                int.Parse(values[2], CultureInfo.InvariantCulture),
                int.Parse(values[3], CultureInfo.InvariantCulture),
                float.Parse(values[4], CultureInfo.InvariantCulture),
                float.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                char.Parse(values[7]),
                int.Parse(values[8], CultureInfo.InvariantCulture),
                values[9]);
        }

        public static TickMessage<float> ParseFloatWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new TickMessage<float>(
                DateTime.ParseExact(values[1], TickDateTimeFormat, CultureInfo.InvariantCulture),
                float.Parse(values[2], CultureInfo.InvariantCulture),
                int.Parse(values[3], CultureInfo.InvariantCulture),
                int.Parse(values[4], CultureInfo.InvariantCulture),
                float.Parse(values[5], CultureInfo.InvariantCulture),
                float.Parse(values[6], CultureInfo.InvariantCulture),
                long.Parse(values[7], CultureInfo.InvariantCulture),
                char.Parse(values[8]),
                int.Parse(values[9], CultureInfo.InvariantCulture),
                values[10],
                requestId);
        }

        public static IEnumerable<TickMessage<decimal>> ParseFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(Parse, path)
                : LookupMessageFileParser.ParseFromFile(ParseWithRequestId, path);
        }

        public static IEnumerable<TickMessage<double>> ParseDoubleFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(ParseDouble, path)
                : LookupMessageFileParser.ParseFromFile(ParseDoubleWithRequestId, path);
        }

        public static IEnumerable<TickMessage<float>> ParseFloatFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(ParseFloat, path)
                : LookupMessageFileParser.ParseFromFile(ParseFloatWithRequestId, path);
        }
    }

    public class TickMessage<T> : TickMessage, ITickMessage<T>
    {
        public TickMessage(DateTime timestamp, T last, int lastSize, int totalVolume, T bid, T ask,
            long tickId, char basisForLast, int tradeMarketCenter, string tradeConditions, string requestId = null)
        {
            RequestId = requestId;
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
        }

        public string RequestId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public T Last { get; private set; }
        public int LastSize { get; private set; }
        public int TotalVolume { get; private set; }
        public T Bid { get; private set; }
        public T Ask { get; private set; }
        public long TickId { get; private set; }
        public char BasisForLast { get; private set; }
        public int TradeMarketCenter { get; private set; }
        public string TradeConditions { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is TickMessage<T> message &&
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
            return $"{nameof(RequestId)}: {RequestId}, {nameof(Timestamp)}: {Timestamp}, {nameof(Last)}: {Last}, {nameof(LastSize)}: {LastSize}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(Bid)}: {Bid}, {nameof(Ask)}: {Ask}, {nameof(TickId)}: {TickId}, {nameof(BasisForLast)}: {BasisForLast}, {nameof(TradeMarketCenter)}: {TradeMarketCenter}, {nameof(TradeConditions)}: {TradeConditions}";
        }
    }
}