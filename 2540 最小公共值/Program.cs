public class Solution {
    public int GetCommon(int[] nums1, int[] nums2)
    {
        int index1 = 0;
        int index2 = 0;
        while (index1 < nums1.Length && index2 < nums2.Length)
        {
            var num1 = nums1[index1];
            var num2 = nums2[index2];
            if (num1 == num2)
                return num1;
            if (num1 > num2)
                ++index2;
            else
                ++index1;
        }

        return -1;
    }
}