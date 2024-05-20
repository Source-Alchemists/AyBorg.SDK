namespace AyBorg.SDK.Common;

public static class IEnumerableExtension
{
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> enumerator, int size)
    {
        int length = enumerator.Count();
        int pos = 0;
        do
        {
            yield return enumerator.Skip(pos).Take(size);
            pos += size;
        } while (pos < length);
    }
}