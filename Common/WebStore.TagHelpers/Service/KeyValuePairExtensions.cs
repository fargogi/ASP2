namespace System.Collections.Generic
{
    internal static class KeyValuePairExtensions
    {
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> item, out TKey key, out TValue value)
        {
            key = item.Key;
            value = item.Value;
        }

    }
}
