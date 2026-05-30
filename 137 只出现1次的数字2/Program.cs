public class Solution {
    public int SingleNumber(int[] nums) {
        int n = nums.Length;
        int intSize = sizeof(int) * 8;
        int[] cnt = new int[intSize];
        int res = 0;
        for(int i = 0; i < n; ++i)
        for (int j = 0; j < intSize; ++j)
            cnt[j] += (nums[i] >> j) & 1;
        for(int i = 0; i < intSize; ++i)
            if (cnt[i] % 3 != 0)
                res |= (1 << i);
        return res;
    }
}