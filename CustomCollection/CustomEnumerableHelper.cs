namespace CustomCollection;

internal static class CustomEnumerableHelper
{
    internal static T[] ToArray<T>(IEnumerable<T> source, out int length)
    {
        ArgumentNullException.ThrowIfNull(source);
        
        if (source is ICollection<T> collection)
        {
            length = collection.Count;
            var array = new T[length];
            collection.CopyTo(array, 0);
            return array;
        }
        
        using var enumerator = source.GetEnumerator();
        if (enumerator.MoveNext())
        {
            int capacity = 4;
            var array1 = new T[capacity];
            int size = 0;
            do
            {
                if (size == capacity)
                {
                    capacity *= 2;
                    Array.Resize(ref array1, capacity);
                }
                array1[size++] = enumerator.Current;
            } while (enumerator.MoveNext());
            
            length = size;
            return array1;
        }

        length = 0;
        return [];
    }
}