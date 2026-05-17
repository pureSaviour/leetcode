Solution sol = new();
int[] arr = [3,0,2,1,2];
Console.WriteLine(sol.CanReach(arr, 2));


public class Solution {
    public bool CanReach(int[] arr, int start)
    {
        int n = arr.Length;
        bool[] visited = new bool[2 * n];
        Queue<int> q = new();
        q.Enqueue(start);
        while (q.Count > 0)
        {
            var t = q.Dequeue();
            if(arr[t] == 0)
                return true;
            var right = t + arr[t];
            var left = t - arr[t];
            if (right < n && !visited[right])
            {
                q.Enqueue(right);
                visited[right] = true;
            }
            if (left >= 0 && !visited[left])
            {
                q.Enqueue(left);
                visited[left] = true;
            }
        }

        return false;
    }
}