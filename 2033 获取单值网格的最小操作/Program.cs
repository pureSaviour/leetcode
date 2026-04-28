Solution sol = new();
int[][] grid = [[529,529,989],[989,529,345],[989,805,69]];
int x = 92;

Console.WriteLine(sol.MinOperations(grid, x).ToString());


public class Solution {
    public int MinOperations(int[][] grid, int x) {
        int m = grid.Length;
        int n = grid[0].Length;
        int re = grid[0][0] % x;
        int count = 0;
        int cur = re;
        PriorityQueue<int, int> pq = new(m * n);
        
        foreach (var line in grid)
        {
            foreach (var item in line)
            {
                if (item % x != re) return -1;
                count += item / x;
                pq.Enqueue(item, item);
            }
        }
        
        while (pq.Count > 0)
        {
            var pq_in = pq.Count;
            var pq_out = m * n - pq_in;
            var top = pq.Dequeue();
            
            
            var mul = (top - cur) / x;
            var newCount = count + (pq_out - pq_in) * mul;
            cur = top;
            if(newCount <= count) count = newCount;
            else break;
        }
        return count;
    }
}