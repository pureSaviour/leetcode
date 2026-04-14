Solution sol = new();
int[] target = [8, 5];
var res = sol.IsPossible(target);
Console.WriteLine(res.ToString());

public class Solution {
    public bool IsPossible(int[] target)
    {
        Int128 sum = 0;
        var list = new List<(int, int)>(target.Length);
        foreach (var item in target)
        {
            sum += item;
            list.Add((item, -item));
        }
        var pq = new PriorityQueue<int, int>(list);
        while (pq.Peek() > 1) 
        {
            var top = pq.Dequeue();
            sum -= top;
            if (sum == 0 || top <= sum) return false;
            top = (int)((top - 1) % sum + 1);
            sum += top;
            pq.Enqueue(top, -top);
        }
        return true;
    }
}