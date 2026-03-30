
var sol = new Solution();
int[] arr = [5, 7, 7, 8, 8, 10];
int target = 8;
int[] ans = sol.SearchRange(arr, target);
foreach (var item in ans)
{
    Console.WriteLine(item);
}

public class Solution
{
    public int[] SearchRange(int[] nums, int target) {
        int n = nums.Length;
        int l = 0;
        int r = n - 1;
        int[] ans = [-1, -1];
        while (l <= r)
        {
            int m = l + (r - l) / 2;
            if (nums[m] < target)
            {
                l = m + 1;
            }else if (nums[m] > target)
            {
                r = m - 1;
            }
            else
            {
                int l1 = l;
                int r1 = m;
                int l2 = m;
                int r2 = r;
                while (l1 <= r1)
                {
                    int m1 = l1 + (r1 - l1) / 2;
                    if (nums[m1] < target)
                        l1 = m1 + 1;
                    else
                    {
                        r1 = m1 - 1;
                    }
                }
                while (l2 <= r2)
                {
                    int m2 = l2 + (r2 - l2) / 2;
                    if(nums[m2] <= target)
                        l2 = m2 + 1;
                    else
                        r2 = m2 - 1;
                }
                ans[0] = l1;
                ans[1] = r2;
                break;
            }
        }
        
        return ans;
    }
}