using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class TickMessage : ITickMessage
    {
        // TickMessage wire format can have:
        //  10 fields - proto <=6.0 with no requestId   (baseline)
        //  11 fields - proto <=6.0 with requestId      (baseline + rId)
        //  12 fields - proto ==6.1 with no requestId   (baseline + trade aggressor + daycode)
        //  13 fields - proto ==6.1 with requestId      (baseline + trade aggressor + daycode + requestId)
        //  13 fields - proto ==6.2 with no requestId   (baseline + trade aggressor + daycode + "LH")
        //  14 fields - proto ==6.2 with requestId      (baseline + trade aggressor + daycode + "LH" + requestId)
        // we know whether we're expecting requestId, so we can use the length of the values array to handle appropriately

        public const string TickDateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffffff";
        
        // relative field ids
        public const int Field_TimeStamp = 0;
        public const int Field_Last = 1;
        public const int Field_LastSize = 2;
        public const int Field_TotalVolume = 3;
        public const int Field_Bid = 4;
        public const int Field_Ask = 5;
        public const int Field_TickId = 6;
        public const int Field_BasisForLast = 7;
        public const int Field_TradeMarketCenter = 8;
        public const int Field_TradeConditions = 9;
        public const int Field_TradeAggressor = 10;
        public const int Field_DayCode = 11;

        public TickMessage(DateTime timestamp, double last, int lastSize, int totalVolume, double bid, double ask,
            long tickId, char basisForLast, int tradeMarketCenter, string tradeConditions, int tradeAggressor,
            int dayCode, string requestId = null)
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
            TradeAggressor = tradeAggressor;
            DayCode = dayCode;
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
        public int TradeAggressor { get; private set; }
        public int DayCode { get; private set; }
        public string RequestId { get; private set; }

        public static TickMessage Parse(string message)
        {
            if (TickMessage.TryParse(message, out var tickMessage))
            {
                return tickMessage;
            }

            throw new Exception($"Unable to parse message into TickMessage\nmessage={message}");
        }

        public static bool TryParse(string message, out TickMessage tickMessage)
        {
            bool parsedTradeAggressor = false;
            bool parsedDayCode = false;
            int indexBase;

            var values = message.SplitFeedMessage();
            int tradeAggressor = default;
            int dayCode = default;

            switch (values.Length)
            {
                case 10: // protocol <=6.0
                    indexBase = 0;
                    tradeAggressor = 0;
                    dayCode = 0;
                    parsedTradeAggressor = true;
                    parsedDayCode = true;
                    break;

                case 12: // protocol ==6.1
                    indexBase = 0;
                    parsedTradeAggressor = int.TryParse(values[indexBase + Field_TradeAggressor], NumberStyles.Any, CultureInfo.InvariantCulture, out tradeAggressor);
                    parsedDayCode = int.TryParse(values[indexBase + Field_DayCode], NumberStyles.Any, CultureInfo.InvariantCulture, out dayCode);
                    break;

                case 13: // protocol ==6.2
                    indexBase = 1; // skip over the constant 'LH'
                    parsedTradeAggressor = int.TryParse(values[indexBase + Field_TradeAggressor], NumberStyles.Any, CultureInfo.InvariantCulture, out tradeAggressor);
                    parsedDayCode = int.TryParse(values[indexBase + Field_DayCode], NumberStyles.Any, CultureInfo.InvariantCulture, out dayCode);
                    break;

                default: // it's not a value length, so don't even bother
                    tickMessage = null;
                    return false;
                    break;
            }

            return TryParseInner(values, out tickMessage, indexBase, false, tradeAggressor, dayCode, parsedTradeAggressor, parsedDayCode);
        }

        public static TickMessage ParseWithRequestId(string message)
        {
            if (TickMessage.TryParseWithRequestId(message, out var tickMessage))
            {
                return tickMessage;
            }

            throw new Exception($"Unable to parse message into TickMessage with requestId\nmessage={message}");
        }

        public static bool TryParseWithRequestId(string message, out TickMessage tickMessage)
        {
            bool parsedTradeAggressor = false;
            bool parsedDayCode = false;
            int indexBase;

            var values = message.SplitFeedMessage();
            int tradeAggressor = default;
            int dayCode = default;

            switch (values.Length)
            {
                case 11: // protocol <=6.0
                    indexBase = 1;
                    tradeAggressor = 0;
                    dayCode = 0;
                    parsedTradeAggressor = true;
                    parsedDayCode = true;
                    break;

                case 13: // protocol ==6.1
                    indexBase = 1;
                    parsedTradeAggressor = int.TryParse(values[indexBase + Field_TradeAggressor], NumberStyles.Any, CultureInfo.InvariantCulture, out tradeAggressor);
                    parsedDayCode = int.TryParse(values[indexBase + Field_DayCode], NumberStyles.Any, CultureInfo.InvariantCulture, out dayCode);
                    break;

                case 14: // protocol ==6.2
                    indexBase = 2; // skip over the constant 'LH'
                    parsedTradeAggressor = int.TryParse(values[indexBase + Field_TradeAggressor], NumberStyles.Any, CultureInfo.InvariantCulture, out tradeAggressor);
                    parsedDayCode = int.TryParse(values[indexBase + Field_DayCode], NumberStyles.Any, CultureInfo.InvariantCulture, out dayCode);
                    break;

                default: // it's not a value length, so don't even bother
                    tickMessage = null;
                    return false;
                    break;
            }

            return TryParseInner(values, out tickMessage, indexBase, true, tradeAggressor, dayCode, parsedTradeAggressor, parsedDayCode);
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
                ? FormattableString.Invariant($"{Timestamp.ToString(TickDateTimeFormat, CultureInfo.InvariantCulture)},{Last},{LastSize},{TotalVolume},{Bid},{Ask},{TickId},{BasisForLast},{TradeMarketCenter},{TradeConditions},{TradeAggressor},{DayCode}")
                : FormattableString.Invariant($"{RequestId},{Timestamp.ToString(TickDateTimeFormat, CultureInfo.InvariantCulture)},{Last},{LastSize},{TotalVolume},{Bid},{Ask},{TickId},{BasisForLast},{TradeMarketCenter},{TradeConditions},{TradeAggressor},{DayCode}");
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
                   TradeConditions == message.TradeConditions &&
                   TradeAggressor == message.TradeAggressor &&
                   DayCode == message.DayCode;
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
                hash = hash * 29 + TradeAggressor.GetHashCode();
                hash = hash * 29 + DayCode.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(Last)}: {Last}, {nameof(LastSize)}: {LastSize}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(Bid)}: {Bid}, {nameof(Ask)}: {Ask}, {nameof(TickId)}: {TickId}, {nameof(BasisForLast)}: {BasisForLast}, {nameof(TradeMarketCenter)}: {TradeMarketCenter}, {nameof(TradeConditions)}: {TradeConditions}, {nameof(TradeAggressor)}: {TradeAggressor}, {nameof(DayCode)}: {DayCode}, {nameof(RequestId)}: {RequestId}";
        }

        private static bool TryParseInner(string[] values, out TickMessage tickMessage, 
            int indexBase, bool parseRequestId, int tradeAggressor, 
            int dayCode, bool parsedTradeAggressor, bool parsedDayCode)
        {
            tickMessage = null;

            DateTime timestamp = default;
            double last = default;
            int lastSize = default;
            int totalVolume = default;
            double bid = default;
            double ask = default;
            long tickId = default;
            char basisForLast = default;
            int tradeMarketCenter = default;

            var parsed = 
                DateTime.TryParseExact(values[indexBase + Field_TimeStamp], TickDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out timestamp) &&
                double.TryParse(values[indexBase + Field_Last], NumberStyles.Any, CultureInfo.InvariantCulture, out last) &&
                int.TryParse(values[indexBase + Field_LastSize], NumberStyles.Any, CultureInfo.InvariantCulture, out lastSize) &&
                int.TryParse(values[indexBase + Field_TotalVolume], NumberStyles.Any, CultureInfo.InvariantCulture, out totalVolume) &&
                double.TryParse(values[indexBase + Field_Bid], NumberStyles.Any, CultureInfo.InvariantCulture, out bid) &&
                double.TryParse(values[indexBase + Field_Ask], NumberStyles.Any, CultureInfo.InvariantCulture, out ask) &&
                long.TryParse(values[indexBase + Field_TickId], NumberStyles.Any, CultureInfo.InvariantCulture, out tickId) &&
                char.TryParse(values[indexBase + Field_BasisForLast], out basisForLast) &&
                int.TryParse(values[indexBase + Field_TradeMarketCenter], NumberStyles.Any, CultureInfo.InvariantCulture, out tradeMarketCenter) &&
                parsedTradeAggressor &&
                parsedDayCode;

            var tradeConditions = values[indexBase + Field_TradeConditions];

            if (!parsed)
            {
                return false;
            }

            tickMessage = new TickMessage(
                timestamp,
                last,
                lastSize,
                totalVolume,
                bid,
                ask,
                tickId,
                basisForLast,
                tradeMarketCenter,
                tradeConditions,
                tradeAggressor,
                dayCode,
                parseRequestId ? values[0] : null
            );

            return true;
        }
    }
}