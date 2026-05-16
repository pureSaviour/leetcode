Solution sol = new();
int[] nums = [2,0,1,1,1];
Console.WriteLine(sol.FindMin(nums));

public class Solution {
    public int FindMin(int[] nums)
    {
        int n = nums.Length;
        int l = 0;
        int r = n - 1;
        while (l < r)
        {
            int m = l + (r - l) / 2;
            if (nums[m] == nums[l] && nums[m] == nums[r])
                ++l;
            else if (nums[l] > nums[r])
            {
                if (nums[m] <= nums[r])
                    r = m;
                else 
                    l = m + 1;
            }
            else
            {
                if (nums[m] <= nums[r])
                    r = m;
                else
                    l = m + 1;
            }
        }

        return nums[r];
    }
}