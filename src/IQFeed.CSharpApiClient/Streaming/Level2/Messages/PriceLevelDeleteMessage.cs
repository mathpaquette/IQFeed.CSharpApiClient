using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public class PriceLevelDeleteMessage : IPriceLevelDeleteMessage
    {
        public const string UpdateMessageTimeFormat = "hh\\:mm\\:ss\\.ffffff";
        public const string UpdateMessageDateFormat = "yyyy-MM-dd";

        public PriceLevelDeleteMessage(
            Level2MessageType messageType,
            string symbol,
            Level2Side side,
            double price,
            TimeSpan time,
            DateTime date)
        {
            MessageType = messageType;
            Symbol = symbol;
            Side = Side;
            Price = price;
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
        /// The price value of the price level that was deleted
        /// </summary>
        public double Price { get; private set; }

        /// <summary>
        /// The time of this order
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// The date of this order
        /// </summary>
        public DateTime Date { get; private set; }

        public DateTime DateTime => Date.Add(Time);

        public static PriceLevelDeleteMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            // as this message type can service 4 different domain message types, it's sensible to keep the MessageType as part of the message
            Enum.TryParse<Level2MessageType>(values[0], out var messageType);
            var symbol = values[1];
            Enum.TryParse<Level2Side>(values[2], out var side);
            double.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var price);
            TimeSpan.TryParseExact(values[4], UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var time);
            DateTime.TryParseExact(values[5], UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);

            return new PriceLevelDeleteMessage
            (
                messageType,
                symbol,
                side,
                price,
                time,
                date
            );
        }

        public override bool Equals(object obj)
        {
            return obj is PriceLevelDeleteMessage message &&
                   MessageType == message.MessageType &&
                   Symbol == message.Symbol &&
                   Side == message.Side &&
                   Equals(Price, message.Price) &&
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
                hash = hash * 29 + Time.GetHashCode();
                hash = hash * 29 + Date.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(MessageType)}: {MessageType}, {nameof(Symbol)}: {Symbol}, {nameof(Side)}: {Side}, {nameof(Price)}: {Price}, {nameof(Time)}: {Time}, {nameof(Date)}: {Date}";
        }
    }
}