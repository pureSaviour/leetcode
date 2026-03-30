

using System.Globalization;

// int[] arr = RandomGenerator.RandomGenerator.GenerateSortedRandomArrayWithFixedSeed(100, 0, 100);
var sol = new Solution();
int[] arr = [3, 4, 5, 1, 2];
int i = sol.Search(arr, 4);
Console.WriteLine(i);

public class Solution
{
    public int Search(int[] nums, int target) {
        int n = nums.Length;
        int last = nums.Last();
        int first = nums.First();
        if (target > last && target < first)
            return -1;
        if (target == first) return 0;
        if (target == last) return n - 1;
        
        int l = 0;
        int r = n - 1;
        
        while (l <= r)
        {
            int m = l + (r - l) / 2;
            if (target <= last)
            {


                if (nums[m] < target)
                {
                    l = m + 1;
                }
                else if (nums[m] > target)
                {
                    if (nums[m] > last)
                        l = m + 1;
                    else
                        r = m - 1;
                }
                else
                {
                    return m;
                }
            }
            else
            {
                if (nums[m] > target)
                {
                    r = m - 1;
                }else if (nums[m] < target)
                {
                    if (nums[m] >= first)
                        l = m + 1;
                    else
                        r = m - 1;
                }
                else
                {
                    return m;
                }
            }
        }
        

        return -1;
    }
}