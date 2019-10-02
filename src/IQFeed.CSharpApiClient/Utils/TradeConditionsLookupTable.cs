using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;

namespace IQFeed.CSharpApiClient.Utils
{
    public class TradeConditionsLookupTable
    {
        private static readonly ITradeCondition[] NoTradeConditions = new ITradeCondition[0];

        // since the number of exchange is expected to be small, it will be faster to use an array instead of concurrent dictionary
        private ITradeCondition[] lookupTable;

        public TradeConditionsLookupTable(LookupClient lookupClient)
        {
            // get the trade conditions
            var tradeConditions = lookupClient.Symbol.ReqTradeConditionsAsync().GetAwaiter().GetResult().ToArray();

            // get the maximum value of the trade condition id
            int maxId = tradeConditions.Length > 0 ? tradeConditions.Max(lm => lm.TradeConditionId) : -1;

            // allocate enough buffer to fit all 
            lookupTable = new ITradeCondition[maxId + 1];

            foreach(var tradeCondition in tradeConditions)
            {
                lookupTable[tradeCondition.TradeConditionId] = tradeCondition;
            }
        }

        /// <summary>
        /// Gets all trade conditions
        /// </summary>
        public IEnumerable<ITradeCondition> All => this.lookupTable.Where(tc => tc != null);

        /// <summary>
        /// Gets the trade condition for the given id
        /// </summary>
        /// <param name="tradeConditionId">Trade condition id</param>
        /// <returns>Trade condition or <code>null</code> if not found</returns>
        public ITradeCondition this[int tradeConditionId]
        {
            get
            {
                return tradeConditionId >= 0 || tradeConditionId < lookupTable.Length
                    ? lookupTable[tradeConditionId]
                    : null;
            }
        }

        /// <summary>
        /// Parses the trade conditions string and returns an array of trade condition objects
        /// </summary>
        /// <param name="tradeConditionsString">Trade conditions string value in hexadecimal 2 digit groups</param>
        /// <returns>Array of trade conditions</returns>
        public ITradeCondition[] Parse(string tradeConditionsString)
        {
            if(string.IsNullOrEmpty(tradeConditionsString)) return NoTradeConditions;

            var tradeConditionsList = new List<ITradeCondition>();

            // consume 2 chars at a time
            for(int i = 0; i < tradeConditionsString.Length - 1; i += 2)
            {
                int tradeConditionId = ParseTradeConditionHexValue(tradeConditionsString.Substring(i, 2));
                tradeConditionsList.Add(this[tradeConditionId]);
            }

            return tradeConditionsList.ToArray();
        }

        /// <summary>
        /// Parses a single trade condition hex value and returns the corresponding integer
        /// </summary>
        /// <param name="tradeConditionHexValue">Single trade condition hex value</param>
        /// <returns>Trade condition int value</returns>
        private static int ParseTradeConditionHexValue(string tradeConditionHexValue)
        {
            if(!int.TryParse(tradeConditionHexValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int tradeConditionId))
            {
                throw new ArgumentException("Invalid Trade Condition value format: " + tradeConditionHexValue);
            }

            return tradeConditionId;
        }
    }
}
