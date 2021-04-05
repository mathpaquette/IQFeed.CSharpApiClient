using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    /// <summary>
    /// This message is actually the same format as the OrderAddUpdateSummaryMessage
    /// </summary>
    public class PriceLevelOrderMessage : OrderAddUpdateSummaryMessage
    {
        public PriceLevelOrderMessage(
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
            DateTime orderDate) : base(messageType, symbol, orderId, mmid, side, price, size, orderPriority, precision, orderTime, orderDate)
        {
        }

        public new static PriceLevelOrderMessage Parse(string message)
        {
            var messageParsed = OrderAddUpdateSummaryMessage.Parse(message);
            return new PriceLevelOrderMessage(
                messageParsed.MessageType, 
                messageParsed.Symbol,
                messageParsed.OrderId,
                messageParsed.MMID,
                messageParsed.Side,
                messageParsed.Price,
                messageParsed.Size,
                messageParsed.OrderPriority,
                messageParsed.Precision,
                messageParsed.OrderTime,
                messageParsed.OrderDate
                );
        }
    }
}
