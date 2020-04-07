using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToString<TK, TV>(this IDictionary<TK, TV> dictionary, string separator)
        {
            return ToString(dictionary, (key, value) => $"{key.ToString()}: {value.ToString()}", separator);
        }

        public static string ToString<TK, TV>(this IDictionary<TK, TV> dictionary, Func<TK, TV, string> formatFunc, string seperator)
        {
            var output = new StringBuilder();
            foreach (var keyValuePair in dictionary)
            {
                if (output.Length > 0)
                    output.Append(seperator);

                output.Append(formatFunc.Invoke(keyValuePair.Key, keyValuePair.Value));
            }

            return output.ToString();
        }

        public static int GetContentsHashCode<TK, TV>(this IDictionary<TK, TV> dictionary)
        {
            var hash = 17;
            foreach (var keyValuePair in dictionary)
            {
                hash = hash * 29 + keyValuePair.Key.GetHashCode();
                hash = hash * 29 + keyValuePair.Value.GetHashCode();
            }

            return hash;
        }

        public static bool CompareContentsWith<TK, TV>(this IDictionary<TK, TV> dictionary, IDictionary<TK, TV> compareDictionary)
        {
            if (dictionary.Count() != compareDictionary.Count() || dictionary.Count == 0)
                return false;

            var dictionaryEnum = dictionary.GetEnumerator();
            var compareEnum = compareDictionary.GetEnumerator();
            for (var idx = 0; idx < dictionary.Count; idx++)
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
