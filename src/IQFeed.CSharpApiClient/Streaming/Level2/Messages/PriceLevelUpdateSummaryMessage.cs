using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public class PriceLevelUpdateSummaryMessage : IPriceLevelUpdateSummaryMessage
    {
        public const string UpdateMessageTimeFormat = "hh\\:mm\\:ss\\.ffffff";
        public const string UpdateMessageDateFormat = "yyyy-MM-dd";

        public PriceLevelUpdateSummaryMessage(
            Level2MessageType messageType,
            string symbol,
            Level2Side side,
            double price,
            int size,
            int orderCount,
            int precision,
            TimeSpan time,
            DateTime date)
        {
            // message type can be any of 4 types, and we may get a lot of these,
            // so not bothering to check, for speed.

            MessageType = messageType;
            Symbol = symbol;
            Side = side;
            Price = price;
            Size = size;
            OrderCount = orderCount;
            Precision = precision;
            Time = time;
            Date = date;
        }

        /// <summary>
        /// Level 2 Message Type
        /// </summary>
        public Level2MessageType MessageType { get; private set; }
        /// <summary>
        /// The Security's Stock Symbol
        /// </summary>
        public string Symbol { get; private set; }
        /// <summary>
        /// Buy or Sell side
        /// </summary>
        public Level2Side Side { get; private set; }
        /// <summary>
        /// The price value of the price level that was added/updated
        /// </summary>
        public double Price { get; private set; }
        /// <summary>
        /// The total size of all orders at this price level
        /// </summary>
        public int Size { get; private set; }
        /// <summary>
        /// The total number of orders at this price level
        /// </summary>
        public int OrderCount { get; private set; }
        /// <summary>
        /// Number of places of decimal precision for the price field
        /// </summary>
        public int Precision { get; set; }
        /// <summary>
        /// The time of the most current order that affected this price level
        /// </summary>
        public TimeSpan Time { get; private set; }
        /// <summary>
        /// The date of the most current order that affected this price level
        /// </summary>
        public DateTime Date { get; private set; }

        public DateTime OrderDateTime => Date.Add(Time);


        public static PriceLevelUpdateSummaryMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            // as this message type can service 4 different domain message types, it's sensible to keep the MessageType as part of the message
            Enum.TryParse<Level2MessageType>(values[0], out var messageType);
            var symbol = values[1];
            Level2SideParser.TryParse(values[2], out var side);
            double.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var price);
            int.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var size);
            int.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var orderCount);
            int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var precision);
            TimeSpan.TryParseExact(values[7], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact(values[8], UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);

            return new PriceLevelUpdateSummaryMessage(
                messageType,
                symbol,
                side,
                price,
                size,
                orderCount,
                precision,
                orderTime,
                orderDate
            );
        }

        public override bool Equals(object obj)
        {
            return obj is PriceLevelUpdateSummaryMessage message &&
                   MessageType == message.MessageType &&
                   Symbol == message.Symbol &&
                   Side == message.Side &&
                   Equals(Price, message.Price) &&
                   Size == message.Size &&
                   OrderCount== message.OrderCount &&
                   Precision == message.Precision &&
                   Time == message.Time &&
                   Date == message.Date;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + MessageType.GetHashCode();
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + Side.GetHashCode();
                hash = hash * 29 + Price.GetHashCode();
                hash = hash * 29 + Size.GetHashCode();
                hash = hash * 29 + OrderCount.GetHashCode();
                hash = hash * 29 + Precision.GetHashCode();
                hash = hash * 29 + Time.GetHashCode();
                hash = hash * 29 + Date.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(MessageType)}: {MessageType}, {nameof(Symbol)}: {Symbol}, {nameof(Side)}: {Side}, {nameof(Price)}: {Price}, {nameof(Size)}: {Size}, {nameof(OrderCount)}: {OrderCount}, {nameof(Precision)}: {Precision}, {nameof(Time)}: {Time}, {nameof(Date)}: {Date}";
        }
    }
}