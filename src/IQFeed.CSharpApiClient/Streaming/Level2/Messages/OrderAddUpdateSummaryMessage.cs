using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public class OrderAddUpdateSummaryMessage : IOrderAddUpdateSummaryMessage
    {
        public const string UpdateMessageTimeFormat = "hh\\:mm\\:ss\\.ffffff";
        public const string UpdateMessageDateFormat = "yyyy-MM-dd";

        public OrderAddUpdateSummaryMessage(
            Level2MessageType messageType,
            string symbol,
            UInt64 orderId,
            string mmid,
            Level2Side side,
            double price,
            int size,
            UInt64 orderPriority,
            int precision,
            TimeSpan orderTime,
            DateTime orderDate)
        {
            // message type can be any of 4 types, and we may get a lot of these,
            // so not bothering to check, for speed.

            MessageType = messageType;
            Symbol = symbol;
            OrderId = orderId;
            MMID = mmid;
            Side = side;
            Price = price;
            Size = size;
            OrderPriority = orderPriority;
            Precision = precision;
            OrderTime = orderTime;
            OrderDate = orderDate;
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
        /// The unique (per symbol) order identifier for this symbol.
        /// Only used in Market By Order messages.
        /// Will be blank for Nasdaq Level 2 messages
        /// </summary>
        public UInt64 OrderId { get; private set; }
        /// <summary>
        /// Market Maker ID - Only used for Nasdaq Level 2 messages.
        /// Will be blank for Market By Order Messages.
        /// </summary>
        public string MMID { get; private set; }
        /// <summary>
        /// Buy or Sell side
        /// </summary>
        public Level2Side Side { get; private set; }
        /// <summary>
        /// The price value of the price level that was added/updated
        /// </summary>
        public double Price { get; private set; }
        /// <summary>
        /// The size of this order
        /// </summary>
        public int Size { get; private set; }
        /// <summary>
        /// Used to position orders of the same price and side. Priority ranks from lowest to highest.
        /// </summary>
        public UInt64 OrderPriority { get; private set; }
        /// <summary>
        /// Number of places of decimal precision for the price field
        /// </summary>
        public int Precision { get; set; }
        /// <summary>
        /// The time of this order
        /// </summary>
        public TimeSpan OrderTime { get; private set; }
        /// <summary>
        /// The date of this order
        /// </summary>
        public DateTime OrderDate { get; private set; }

        public DateTime OrderDateTime => OrderDate.Add(OrderTime);


        public static OrderAddUpdateSummaryMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            // as this message type can service 4 different domain message types, it's sensible to keep the MessageType as part of the message
            Enum.TryParse<Level2MessageType>(values[0], out var messageType);
            var symbol = values[1];
            UInt64.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var orderId);
            var mmid = values[3];
            Level2SideParser.TryParse(values[4], out var side);
            double.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var price);
            int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var size);
            UInt64.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var orderPriority);
            int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var precision);
            TimeSpan.TryParseExact(values[9], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact(values[10], UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);

            return new OrderAddUpdateSummaryMessage(
                messageType,
                symbol,
                orderId,
                mmid,
                side,
                price,
                size,
                orderPriority,
                precision,
                orderTime,
                orderDate
            );
        }

        public override bool Equals(object obj)
        {
            return obj is OrderAddUpdateSummaryMessage message &&
                   MessageType == message.MessageType &&
                   Symbol == message.Symbol &&
                   OrderId == message.OrderId &&
                   MMID == message.MMID &&
                   Side == message.Side &&
                   Equals(Price, message.Price) &&
                   Size == message.Size &&
                   OrderPriority == message.OrderPriority &&
                   Precision == message.Precision &&
                   OrderTime == message.OrderTime &&
                   OrderDate == message.OrderDate;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + MessageType.GetHashCode();
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + OrderId.GetHashCode();
                hash = hash * 29 + MMID.GetHashCode();
                hash = hash * 29 + Side.GetHashCode();
                hash = hash * 29 + Price.GetHashCode();
                hash = hash * 29 + Size.GetHashCode();
                hash = hash * 29 + OrderPriority.GetHashCode();
                hash = hash * 29 + Precision.GetHashCode();
                hash = hash * 29 + OrderTime.GetHashCode();
                hash = hash * 29 + OrderDate.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(MessageType)}: {MessageType}, {nameof(Symbol)}: {Symbol}, {nameof(OrderId)}: {OrderId}, {nameof(MMID)}: {MMID}, {nameof(Side)}: {Side}, {nameof(Price)}: {Price}, {nameof(Size)}: {Size}, {nameof(OrderPriority)}: {OrderPriority}, {nameof(Precision)}: {Precision}, {nameof(OrderTime)}: {OrderTime}, {nameof(OrderDate)}: {OrderDate}";
        }
    }
}