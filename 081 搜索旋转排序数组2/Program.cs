
int[] nums = [1, 0, 1, 1, 1];
Solution solution = new Solution();
bool canFind = solution.Search(nums,0);
Console.WriteLine(canFind);

public class Solution {
    public bool Search(int[] nums, int target)
    {
        // nums中的每个数并不是独一无二的
        if (nums.Length == 0)
            return false;
        int l = 0;
        int r = nums.Length - 1;
        int first = nums[0];
        int last = nums[^1];
        if (first == target || last == target) return true;

        while (l <= r)
        {
            int m = l + (r - l) / 2;
            if (nums[m] == target) return true;
            if (nums[l] == nums[m] && nums[m] == nums[r])
            {
                l++;
                r--;
            }
            // 左有序
            else if (nums[l] <= nums[m])
            {
                // target在左区间，查左边
                if (nums[l] <= target && target < nums[m])
                    r = m - 1;
                else l = m + 1;
            }
            // 右有序
            else if(nums[m] <= nums[r])
            {
                // target在右区间，查右边
                if (nums[m] < target && target <= nums[r])
                    l = m + 1;
                else r = m - 1;
            }
        }
        
        return false;
    }
}