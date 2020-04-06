using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public static class DictionaryExtensions
    {
        public static bool CompareContentsWith<TK,TV>(this IDictionary<TK,TV> dictionary, IDictionary<TK,TV> compareDictionary)
        {
            if (dictionary.Count() != compareDictionary.Count() || dictionary.Count == 0)
                return false;

            var dictionaryEnum = dictionary.GetEnumerator();
            var compareEnum = compareDictionary.GetEnumerator();
            for(var idx = 0; idx < dictionary.Count; idx++)
            {
                var dictionaryEnd = dictionaryEnum.MoveNext();
                var compareEnd = compareEnum.MoveNext();
                if (!dictionaryEnum.Current.Key.Equals(compareEnum.Current.Key))
                    return false;

                if (!dictionaryEnum.Current.Value.Equals(compareEnum.Current.Value))
                    return false;
            }

            return true;
        }
    }
}
