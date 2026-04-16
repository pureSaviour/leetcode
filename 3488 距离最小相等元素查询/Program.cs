using static System.Console;

Solution sol = new();
int[] nums = [1, 3, 1, 4, 1, 3, 2];
int[] queries = [0,3,5];
var res = sol.PreSolveQueries(nums, queries);
foreach (var num in res)
{
    Write($"{num} ");
}

public class Solution {
    /// <summary>
    /// 二分查找 + 哈希表
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="queries"></param>
    /// <returns></returns>
    public IList<int> SolveQueries(int[] nums, int[] queries)
    {
        int nQuery = queries.Length;
        int n = nums.Length;
        var res = new List<int>(nQuery);
        Dictionary<int, List<int>> dict = new(nQuery);
        for (int i = 0; i < n; i++)
            if (dict.TryGetValue(nums[i], out var list))
                list.Add(i);
            else
                dict.Add(nums[i], [i]);
        foreach (var query in queries)
        {
            var num = nums[query];
            var list = dict[num];
            if (list.Count == 1) res.Add(-1);
            else
                res.Add(GetMinDis(list, query, n));
        }
        return res;
    }
    private static int GetMinDis(List<int> list, int index, int l)
    {
        var i = list.BinarySearch(index);
        var n = list.Count;
        var preIndex = (i + n - 1) % n;
        var nextIndex = (i + 1) % n;
        return Math.Min(GetDis(list[i], list[preIndex], l), GetDis(list[i], list[nextIndex], l));
        
        static int GetDis(int index1, int index2, int n) => Math.Min(Math.Abs(index1 - index2), n - Math.Abs(index1 - index2));
    }

    /// <summary>
    /// 预处理（遍历时找到左边相邻和右边相邻） + 哈希表 + 优化到1次遍历
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="queries"></param>
    /// <returns></returns>
    public IList<int> PreSolveQueries(int[] nums, int[] queries)
    {
        int n = nums.Length;
        int[] left = new int[n];
        int[] right = new int[n];
        var res = new List<int>(queries.Length);
        Dictionary<int, int> pos = new(n);
        for (int i = -n; i < n; i++) {
            if (i >= 0)
            {
                var pre = pos[nums[i]];
                if (pre >= 0) right[pre] = i;
                else right[pre + n] = i + n;
                left[i] = pre;
            }
            pos[nums[(i + n) % n]] = i;
        }
        foreach (var query in queries)
        {
            if(query - left[query] == n) res.Add(-1);
            else res.Add(Math.Min(query - left[query], right[query] - query));
        }
        return res;
    }
}