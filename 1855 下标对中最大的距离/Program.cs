Solution sol = new();
int[] nums1 = [55,30,5,4,2];
int[] nums2 = [100,20,10,10,5];
var res = sol.MaxDistance(nums1, nums2);
Console.WriteLine(res.ToString());

public class Solution
{
    public int MaxDistance(int[] nums1, int[] nums2)
    {
        int n1 = nums1.Length;
        int n2 = nums2.Length;
        int i1 = 0, i2 = 0;
        int maxValue = 0;
        while (i1 < n1 && i2 < n2)
        {
            if (i1 > i2) ++i2;
            else if (nums1[i1] <= nums2[i2])
            {
                maxValue = Math.Max(maxValue, i2 - i1);
                ++i2;
            }else ++i1;
        }
        return maxValue;
    }
}