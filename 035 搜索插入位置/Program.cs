Solution solution = new Solution();
int[] arr = RandomGenerator.RandomGenerator.GenerateSortedRandomArrayWithFixedSeed(100, 0, 10);
Console.WriteLine(solution.SearchInsert(arr, 5));

public class Solution {
    public int SearchInsert(int[] nums, int target)
    {
        int n = nums.Length;
        int l = 0;
        int r = n - 1;
        while (l <= r)
        {
            int m = l + (r - l) / 2;
            if (nums[m] == target)
            {
                return m;
            }

            if (nums[m] > target)
                r = m - 1;
            else
                l = m + 1;
        }
        return l;
    }
}