namespace _3924_有限重边的最小阈值路径;

public class Solution
{
    public int MinimumThreshold(int n, int[][] edges, int source, int target, int k)
    {
        var graph = new List<(int to, int weight)>[n];
        int maxWeight = 0;
        for (int i = 0; i < n; ++i)
            graph[i] = [];
        
        foreach (var edge in edges)
        {
            var from = edge[0];
            var to = edge[1];
            var weight = edge[2];
            graph[from].Add((to, weight));
            graph[to].Add((from, weight));
            if(weight > maxWeight)
                maxWeight = weight;
        }

        int left = 0, right = maxWeight;
        int ans = -1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (Check(graph, source, target, k, mid))
            {
                ans = mid;
                right = mid - 1;
            }
                
            else
                left = mid + 1;
        }
        return ans;
    }


    private static bool Check(List<(int to, int weight)>[] graph, int source, int target, int k, int threshold)
    {
        int n = graph.Length;
        int[] dist = new int[n];
        Array.Fill(dist, int.MaxValue);
        LinkedList<int> deque = new();
        deque.AddFirst(source);
        dist[source] = 0;
        while (deque.Count > 0)
        {
            var node = deque.First!.Value;
            deque.RemoveFirst();
            if (node == target)
                return dist[node] <= k;
            foreach (var (to, weight) in graph[node])
            {
                int cost = weight > threshold ? 1 : 0;
                if (dist[to] > dist[node] + cost)
                {
                    dist[to] = dist[node] + cost;
                    if(cost == 0)
                        deque.AddFirst(to);
                    else 
                        deque.AddLast(to);
                }
            }
        }
        return dist[target] <= k;
    }
}