Solution solution = new Solution();

//Test case 1
int[] nums1 = [1, 3];
int[] nums2 = [2];
Console.WriteLine(solution.FindMedianSortedArrays(nums1, nums2)); // 2.0

// Test case 2
nums1 = [1, 2];
nums2 = [3, 4];
Console.WriteLine(solution.FindMedianSortedArrays(nums1, nums2)); // 2.5

// Test case 3
nums1 = [0, 0];
nums2 = [0, 0];
Console.WriteLine(solution.FindMedianSortedArrays(nums1, nums2)); // 0.0

public class Solution {
    public double FindMedianSortedArrays(int[] nums1, int[] nums2) {
        int n1 = nums1.Length;
        int n2 = nums2.Length;
        bool isOdd = (n1 + n2) % 2 == 1;
        int m = isOdd ? (n1 + n2) / 2 : (n1 + n2) / 2 - 1;
        
        int i1 = 0;
        int i2 = 0;
        while (i1 + i2 < m && i1 < n1 && i2 < n2)
        {
            if (nums1[i1] < nums2[i2])
            {
                ++i1;
            }
            else
            {
                ++i2;
            }
        }
        
        while (i1 + i2 < m)
        {
            if (i1 < n1)
            {
                ++i1;
            }

            if (i2 < n2)
            {
                ++i2;
            }
        }

        if (i1 >= n1)
        {
            return isOdd ? nums2[i2] : (nums2[i2] + nums2[i2 + 1]) / 2.0;
        }

        if (i2 >= n2)
        {
            return isOdd ? nums1[i1] : (nums1[i1] + nums1[i1 + 1]) / 2.0;
        }
        if(isOdd)
            return Math.Min(nums1[i1], nums2[i2]);
        
        var a = nums1[i1] < nums2[i2] ? nums1[i1++] : nums2[i2++];
        var b = 0;
        if (i1 >= n1)
        {
            b = nums2[i2];
        }else if (i2 >= n2)
        {
            b = nums1[i1];
        }
        else
        {
            b = Math.Min(nums1[i1], nums2[i2]);
        }
        return (a + b) / 2.0;
    }
}