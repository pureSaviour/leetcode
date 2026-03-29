using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

Solution solution = new Solution();
Solution1 solution1 = new Solution1();

//Test case 1
int[] nums1 = [1, 2, 4, 4];
int[] nums2 = [1, 3, 3];
Array.Sort(nums1);
Array.Sort(nums2);
Console.WriteLine(solution.FindMedianSortedArrays(nums1, nums2)); // 2.0
Console.WriteLine(solution1.FindMedianSortedArrays(nums1, nums2)); // 2.0

// Test case 2
nums1 = [1, 2];
nums2 = [3, 4];
Console.WriteLine(solution.FindMedianSortedArrays(nums1, nums2)); // 2.5
Console.WriteLine(solution1.FindMedianSortedArrays(nums1, nums2)); // 2.5

// Test case 3
nums1 = [0, 0];
nums2 = [0, 0];
Console.WriteLine(solution.FindMedianSortedArrays(nums1, nums2)); // 0.0
Console.WriteLine(solution1.FindMedianSortedArrays(nums1, nums2)); // 0.0

// Test case 4
nums1 = [1, 3];
nums2 = [2];
Console.WriteLine(solution.FindMedianSortedArrays(nums1, nums2));
Console.WriteLine(solution1.FindMedianSortedArrays(nums1, nums2));

nums1 = RandomGenerator.RandomGenerator.GenerateRandomArrayWithFixedSeed(100, 0, 100);
nums2 = RandomGenerator.RandomGenerator.GenerateRandomArrayWithFixedSeed(100, 0, 100);
Array.Sort(nums1);
Array.Sort(nums2);
Console.WriteLine(solution.FindMedianSortedArrays(nums1, nums2));
Console.WriteLine(solution1.FindMedianSortedArrays(nums1, nums2));

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

public class Solution1
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int n1 = nums1.Length;
        int n2 = nums2.Length;

        if(n1 > n2)
            return FindMedianSortedArrays(nums2, nums1);
        int l = 0;
        int r = n1;
        while (l <= r)
        {
            int m1 = l + (r - l) / 2;
            int m2 = (n1 + n2 + 1) / 2 - m1;

            int left1 = m1 == 0 ? int.MinValue : nums1[m1 - 1];
            int right1 = m1 == n1 ? int.MaxValue : nums1[m1];
            int left2 = m2 == 0 ? int.MinValue : nums2[m2 - 1];
            int right2 = m2 == n2 ? int.MaxValue : nums2[m2];
            
            if(Math.Max(left1, left2) <= Math.Min(right1, right2))
            {
                if ((n1 + n2) % 2 == 1)
                {
                    return Math.Max(left1, left2);
                }
                else
                {
                    return (Math.Max(left1, left2) + Math.Min(right1, right2)) / 2.0;
                }
            }
            if (left1 > right2)
            {
                r = m1 - 1;
            }
            else
            {
                l = m1 + 1;
            }
        }
        return 0.0;
    }
}

[MemoryDiagnoser(false)]
[RankColumn]
public class MedianBenchmark
{
    // 测试数据（两个有序数组）
    private int[] _nums1;
    private int[] _nums2;

    // ==================== 你的最优解：分割二分法 ====================
    [Benchmark]
    public double BinarySplitSolution()
    {
        Solution1 sol = new Solution1();
        return sol.FindMedianSortedArrays(_nums1, _nums2);
    }

    // ==================== 对比方案：找第K小排除法 ====================
    [Benchmark]
    public double FindKthSolution()
    {
        Solution sol = new Solution();
        return sol.FindMedianSortedArrays(_nums1, _nums2);
    }

    [GlobalSetup]
    public void Setup()
    {
        // 生成随机测试数据
        _nums1 = RandomGenerator.RandomGenerator.GenerateRandomArrayWithFixedSeed(1000, 0, 10000);
        _nums2 = RandomGenerator.RandomGenerator.GenerateRandomArrayWithFixedSeed(1000, 0, 10000);
        Array.Sort(_nums1);
        Array.Sort(_nums2);
    }
}