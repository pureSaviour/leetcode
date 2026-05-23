int[] nums = [1,3,2];
Solution sol = new();
var res = sol.Check(nums);
Console.WriteLine(res);

public class Solution {
    public bool Check(int[] nums)
    {
        int secondMax = int.MinValue;
        bool flag = false;
        for (int i = 1; i < nums.Length; ++i)
        {
            if (nums[i] < nums[i - 1])
            {
                if (flag)
                    return false;
                flag = true;
                secondMax = nums[i];
            }
            else if(flag)
                secondMax = nums[i];
        }
        return secondMax <= nums[0];
    }
}