var sol = new Solution();
int[] nums = [1, 3, 1, 1, 2];
var res = sol.Distance(nums);
Console.WriteLine(string.Join(", ", res));


public class Solution {
    public long[] Distance(int[] nums)
    {
        int n = nums.Length;
        Dictionary<int, IList<int>> dic = new();
        long[] res = new long[n];
        for (int i = 0; i < n; ++i)
        {
            var num = nums[i];
            if (dic.TryGetValue(num, out var list))
                list.Add(i);
            else
                dic[num] = [i];
        }
        foreach (var item in dic)
        {
            var list = item.Value;
            int m = list.Count;
            int first = list[0];
            int l = 0, r = m - 1;
            long sum = 0;
            for (int i = m - 1; i >= 1; --i)
                sum += list[i] - first;
            res[first] = sum;
            for(int i = 1; i < m; ++i, ++l, --r)
            {
                var diff = list[i] - list[i - 1];
                sum += (l - r + 1) * diff;
                res[list[i]] = sum;
            }
        }
        return res;
    }
}