Solution solution = new Solution();
Console.WriteLine(solution.PivotInteger(4));

public class Solution {
    public int PivotInteger(int n)
    {
        int sum = (n * n + n) / 2;
        int x = (int)Math.Sqrt(sum);
        if (x * x == sum) return x;
        return -1;
    }
}