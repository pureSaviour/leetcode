public class Solution {
    public long MaxTotalValue(int[] nums, int k) => (long)(nums.Max() - nums.Min()) * (long)k;
}