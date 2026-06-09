public class Solution {
    public int[] PivotArray(int[] nums, int pivot)
    {
        int index = 0;
        int[] ans = new int[nums.Length];
        foreach (var num in nums)
            if(num < pivot)
                ans[index++] = num;
        foreach (var num in nums)
            if(num == pivot)
                ans[index++] = num;
        foreach (var num in nums)
            if(num > pivot)
                ans[index++] = num;
        return ans;
    }
}