public class Solution {
    public int NumIdenticalPairs(int[] nums)
    {
        int n = nums.Length;
        int[] counts = new int[101];
        int count = 0;
        for (int i = 0; i < n; i++)
            counts[nums[i]]++;
        foreach (var t in counts)
            if (t > 1)
                count += t * (t - 1) / 2;

        return count;
    }
    
}