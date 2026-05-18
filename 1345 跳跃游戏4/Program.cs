Solution sol = new();
int[] arr = [100, -23, -23, 404, 100, 23, 23, 23, 3, 404];
Console.WriteLine(sol.MinJumps(arr));

public class Solution {
    public int MinJumps(int[] arr)
    {
        int n = arr.Length;
        Dictionary<int, List<int>> map = new();
        for (int i = 0; i < n; ++i)
        {
            int num = arr[i];
            if (map.TryGetValue(num, out var list))
                list.Add(i);
            else
                map.Add(num, [i]);
        }

        Queue<int> queue = new();
        queue.Enqueue(0);
        int res = 0;
        bool[] visited = new bool[n];
        visited[0] = true;
        
        while (queue.Count != 0)
        {
            int count = queue.Count;
            for (int i = 0; i < count; ++i)
            {
                var index = queue.Dequeue();
                if (index == n - 1)
                    return res;
                int right = index + 1;
                int left = index - 1;
                if (right < n && !visited[right])
                {
                    visited[right] = true;
                    queue.Enqueue(right);
                }
                if (left >= 0 && !visited[left])
                {
                    visited[left] = true;
                    queue.Enqueue(left);
                }
            
                int num = arr[index];
                if(!map.TryGetValue(num, out var list))
                    continue;
                foreach (var sameIndex in list)
                    if (!visited[sameIndex])
                    {
                        queue.Enqueue(sameIndex);
                        visited[sameIndex] = true;
                    }
                map.Remove(num);
            }
            ++res;
        }
        return res;
    }
}