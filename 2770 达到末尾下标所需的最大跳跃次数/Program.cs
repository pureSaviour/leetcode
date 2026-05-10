public class Solution {
    public int MaximumJumps(int[] nums, int target) {
        int n = nums.Length;
        int[] dp = new int[n];
        Array.Fill(dp, int.MinValue);
        dp[0] = 0;
        
        for (int i = 1; i < n; i++) {
            for (int j = 0; j < i; j++) {
                if (Math.Abs(nums[j] - nums[i]) <= target) {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }
        }
        
        return dp[n - 1] < 0 ? -1 : dp[n - 1];
    }
}