﻿using System;
using System.Text;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Handlers
{
    public abstract class BaseLevel2MessageHandler<T> : ILevel2MessageHandler<T>
    {
        private readonly Func<string, UpdateSummaryMessage<T>> _updateSummaryMessageParser;

        public event Action<UpdateSummaryMessage<T>> Summary;
        public event Action<UpdateSummaryMessage<T>> Update;
        public event Action<SymbolNotFoundMessage> SymbolNotFound;
        public event Action<MarketMakerNameMessage> Query;
        public event Action<ErrorMessage> Error;
        public event Action<TimestampMessage> Timestamp;
        public event Action<SystemMessage> System;
        
        protected BaseLevel2MessageHandler(Func<string, UpdateSummaryMessage<T>> updateSummaryMessageParser)
        {
            _updateSummaryMessageParser = updateSummaryMessageParser;
        }

        public void ProcessMessages(byte[] messageBytes, int count)
        {
            var messages = Encoding.ASCII.GetString(messageBytes, 0, count).SplitFeedLine();

            for (int i = 0; i < messages.Length; i++)
            {
                var message = messages[i];
                switch (messages[i][0])
                {
                    case 'Z': // A summary message
                        ProcessSummaryMessage(message);
                        break;
                    case '2': // An update message
                        ProcessUpdateMessage(message);
                        break;
                    case 'T': // A timestamp message
                        ProcessTimestampMessage(message);
                        break;
                    case 'M': // A Market Maker name OR order book level query response message.
                        ProcessMarketMakerNameMessage(message);
                        break;
                    case 'S': // A system message
                        ProcessSystemMessage(message);
                        break;
                    case 'n': // Symbol not found message
                        ProcessSymbolNotFoundMessage(message);
                        break;
                    case 'E': // An error message
                        ProcessErrorMessage(message);
                        break;
                    case 'O': // A deprecated message included only for backward compability
                        break;
                    default:
                        throw new Exception("Unknown type of level 2 message received.");
                }
            }
        }

        private void ProcessSummaryMessage(string msg)
        {
            var updateSummaryMessage = _updateSummaryMessageParser(msg);
            Summary?.Invoke(updateSummaryMessage);
        }

        private void ProcessUpdateMessage(string msg)
        {
            var updateSummaryMessage = _updateSummaryMessageParser(msg);
            Update?.Invoke(updateSummaryMessage);
        }

        private void ProcessTimestampMessage(string msg)
        {
            var timestampMessage = TimestampMessage.Parse(msg);
            Timestamp?.Invoke(timestampMessage);
        }

        private void ProcessMarketMakerNameMessage(string msg)
        {
            var marketMakerNameMessage = MarketMakerNameMessage.Parse(msg);
            Query?.Invoke(marketMakerNameMessage);
        }

        private void ProcessSystemMessage(string msg)
        {
            var systemMessage = SystemMessage.Parse(msg);
            System?.Invoke(systemMessage);
        }

        private void ProcessSymbolNotFoundMessage(string msg)
        {
            var symbolNotFoundMessage = SymbolNotFoundMessage.Parse(msg);
            SymbolNotFound?.Invoke(symbolNotFoundMessage);
        }

        private void ProcessErrorMessage(string msg)
        {
            var errorMessage = ErrorMessage.Parse(msg);
            Error?.Invoke(errorMessage);
        }
    }
}