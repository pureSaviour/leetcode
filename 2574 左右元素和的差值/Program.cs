public class Solution {
    public int[] LeftRightDifference(int[] nums) {
        int[] ans = new int[nums.Length];
        for (int i = 0, leftSum = 0, sum = nums.Sum(); i < nums.Length; leftSum += nums[i++])
            ans[i] = Math.Abs(2 * leftSum + nums[i] - sum);
        return ans;
    }
}