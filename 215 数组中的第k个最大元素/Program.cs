using Tool;

public class Solution {
    public int FindKthLargest(int[] nums, int k)
    {
        Comparer<int> comparer = Comparer<int>.Create((x, y) => y - x);
        return Find.RandomSelect(nums, k, comparer);
    }
}