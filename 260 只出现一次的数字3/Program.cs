public class Solution {
    public int[] SingleNumber(int[] nums) {
        int n = nums.Length;
        int eor1 = 0;
        for(int i = 0; i < n; ++i)
            eor1 ^= nums[i];
        int eor2 = 0;
        int rightOne = eor1 & (-eor1);
        for(int i = 0; i < n; ++i)
            if((nums[i] & rightOne) == 0)
                eor2 ^= nums[i];

        return [eor2, eor1 ^ eor2];
    }
}