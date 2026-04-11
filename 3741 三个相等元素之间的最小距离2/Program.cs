Solution sol = new();
int[] nums = [1, 2, 1, 1, 3];
var res = sol.MinimumDistance(nums);
Console.WriteLine(res);

public class Solution {
    public int MinimumDistance(int[] nums)
    {
        int minDis = int.MaxValue;
        var dic = new Dictionary<int, Nums>();
        
        for (int i = 0; i < nums.Length; i++)
        {
            var index = nums[i];
            if (dic.TryGetValue(index, out var num))
            {
                if(num.Second != 0)
                {
                    minDis = Math.Min(minDis, 2 * (i - num.First));
                    num.First = num.Second;
                }
                num.Second = i;
            }
            else
            {
                dic.Add(index, new Nums(i, 0));
            }
        }
        
        return minDis == int.MaxValue ? -1 : minDis;
    }

    private sealed class Nums(int first, int second)
    {
        public int First { get; set; } = first;
        public int Second { get; set; } = second;
    }
}