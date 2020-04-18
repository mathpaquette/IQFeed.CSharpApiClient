using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Symbol.Enums;
using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public interface ISymbolFacadeSync
    {
        IEnumerable<SymbolByFilterMessage> GetSymbolsByFilter(FieldToSearch fieldToSearch, string searchString, FilterType? filterType, IEnumerable<int> filterValues, string requestId = null);
        IEnumerable<SymbolBySicCodeMessage> GetSymbolsBySicCode(string sicCodePrefix, string requestId = null);
        IEnumerable<SymbolByNaicsCodeMessage> GetSymbolsByNaicsCode(string naicsCodePrefix, string requestId = null);
        IEnumerable<ListedMarketMessage> GetListedMarkets(string requestId = null);
        IEnumerable<SecurityTypeMessage> GetSecurityTypes(string requestId = null);
        IEnumerable<TradeConditionMessage> GetTradeConditions(string requestId = null);
        IEnumerable<SicCodeInfoMessage> GetSicCodes(string requestId = null);
        IEnumerable<NaicsCodeInfoMessage> GetNaicsCodes(string requestId = null);
    }
}