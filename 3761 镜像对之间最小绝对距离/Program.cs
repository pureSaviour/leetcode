Solution sol = new();
int[] nums = [12,34,46,21,12];
var res = sol.MinMirrorPairDistance(nums);
Console.WriteLine(res.ToString());

public class Solution {
    public int MinMirrorPairDistance(int[] nums) {
        int n = nums.Length;
        int minValue = int.MaxValue;
        Dictionary<int, int> dic = new();
        for (int i = 0; i < n; ++i)
        {
            var num = nums[i];
            if(dic.TryGetValue(num, out var val))
                minValue = Math.Min(minValue, Math.Abs(i - val));
            dic[Reverse(num)] = i;
        }
        return minValue == int.MaxValue ? -1 : minValue;
    }

    private static int Reverse(int x)
    {
        int a = 0;
        while (x > 0)
        {
            a = a * 10 + x % 10;
            x /= 10;
        }
        return a;
    }
}