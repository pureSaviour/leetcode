public class Solution {
    public int MissingNumber(int[] nums)
    {
        int eorAll = nums.Length;
        for (int i = 0; i < nums.Length; ++i)
            eorAll ^= (i ^ nums[i]);
        return eorAll;
    }
}