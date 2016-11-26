using System.Collections.Generic;

namespace TimiShared.Extensions {
    public static class DictionaryExtensions {
        public static V GetOrAdd<K, V>(this Dictionary<K, V> dictionary, K key, V value) {
            V existingValue;
            if (dictionary.TryGetValue(key, out existingValue)) {
                return existingValue;
            }
            dictionary.Add(key, value);
            return value;
        }

    }
}