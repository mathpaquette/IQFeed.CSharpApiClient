using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public class OrderDeleteMessage : IOrderDeleteMessage
    {
        public const string UpdateMessageTimeFormat = "hh\\:mm\\:ss\\.ffffff";
        public const string UpdateMessageDateFormat = "yyyy-MM-dd";

        public OrderDeleteMessage(
            Level2MessageType messageType,
            string symbol,
            UInt64 orderId,
            Level2Side side,
            TimeSpan orderTime,
            DateTime orderDate)
        {
            MessageType = messageType;
            Symbol = symbol;
            OrderId = orderId;
            Side = Side;
            OrderTime = OrderTime;
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
        /// Buy or Sell side
        /// </summary>
        public Level2Side Side { get; private set; }
        /// <summary>
        /// The time of this order
        /// </summary>
        public TimeSpan OrderTime { get; private set; }
        /// <summary>
        /// The date of this order
        /// </summary>
        public DateTime OrderDate { get; private set; }

        public DateTime OrderDateTime => OrderDate.Add(OrderTime);

        public static OrderDeleteMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            // as this message type can service 4 different domain message types, it's sensible to keep the MessageType as part of the message
            Enum.TryParse<Level2MessageType>(values[0], out var messageType);
            var symbol = values[1];
            UInt64.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var orderId);
            // values[3] is reserved in protocol 6.2
            Enum.TryParse<Level2Side>(values[4], out var side);
            TimeSpan.TryParseExact(values[5], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact(values[6], UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);

            return new OrderDeleteMessage
            (
                messageType,
                symbol,
                orderId,
                side,
                orderTime,
                orderDate
            );
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDeleteMessage message &&
                   MessageType == message.MessageType &&
                   Symbol == message.Symbol &&
                   OrderId == message.OrderId &&
                   Side == message.Side &&
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
                hash = hash * 29 + Side.GetHashCode();
                hash = hash * 29 + OrderTime.GetHashCode();
                hash = hash * 29 + OrderDate.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(MessageType)}: {MessageType}, {nameof(Symbol)}: {Symbol}, {nameof(OrderId)}: {OrderId}, {nameof(Side)}: {Side}, {nameof(OrderTime)}: {OrderTime}, {nameof(OrderDate)}: {OrderDate}";
        }
    }
}