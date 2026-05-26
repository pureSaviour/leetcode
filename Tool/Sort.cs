using System.Collections;

namespace Tool;

public static class Sort
{
    private static IComparer<T> GetDefaultComparer<T>()
    {
        return Comparer<T>.Default;
    }

    private static readonly Random SharedRandom = new Random();
    
    public static void QuickSort<T>(T[] array, IComparer<T>? comparer)
    {
        ThrowIfNull(array);
        comparer ??= GetDefaultComparer<T>();
        Random random = SharedRandom;
        SortInRange(0, array.Length - 1);
        return;
        
        void SortInRange(int left, int right)
        {
            if(left >= right)
                return;
            T pivot = array[left + random.Next(0, right - left + 1)];
            var pIndex = Partition(left, right, pivot);
            SortInRange(left, pIndex.left - 1);
            SortInRange(pIndex.right + 1, right);
        }
        
        // 分区
        (int left, int right) Partition(int left, int right, T pivot)
        {
            int i = left, fist = left, last = right;
            while ( i <= last)
            {
                switch (comparer.Compare(array[i], pivot))
                {
                    case < 0:
                        (array[fist], array[i]) = (array[i], array[fist]);
                        ++fist;
                        break;
                    case > 0:
                        (array[last], array[i]) = (array[i], array[last]);
                        --last;
                        continue;
                }
                ++i;
            }
            return  (fist, last);
        }
    }

    private static void ThrowIfNull<T>(T[] array)
    {
        ArgumentNullException.ThrowIfNull(array);
    }
}