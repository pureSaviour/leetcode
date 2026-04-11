int[] nums1 = [1, 7, 11];
int[] nums2 = [2, 4, 6];
var res = new Solution().KSmallestPairs(nums1, nums2, 3);
Console.WriteLine(string.Join(Environment.NewLine, res.Select(list => string.Join(",", list))));

public class Solution {
    public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k) {
        int n1 = nums1.Length;
        int n2 = nums2.Length;
        PriorityQueue<Pair, int> queue = new(k);
        queue.Enqueue(new Pair(0, 0), nums1[0] + nums2[0]);
        var res = new List<IList<int>>(k);
        while (k-- > 0)
        {
            var pair = queue.Dequeue();
            res.Add([nums1[pair.First],nums2[pair.Second]]);
            if (pair.Second == 0 && pair.First + 1 < n1)
                queue.Enqueue(new Pair(pair.First + 1, 0), nums1[pair.First + 1] + nums2[pair.Second]);
            if (pair.Second + 1 < n2)
                queue.Enqueue(new Pair(pair.First, pair.Second + 1), nums1[pair.First] + nums2[pair.Second + 1]);
        }
        return res;
    }
    
    private readonly struct Pair(int first, int second)
    {
        public int First { get; init; } = first;
        public int Second { get; init; } = second;
    }
    
}