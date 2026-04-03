Solution solution = new Solution();
int n = 1563847412;
Console.WriteLine(n);
Console.WriteLine(solution.Reverse(n));


public class Solution {
    public int Reverse(int x)
    {
        if(x == int.MinValue) return 0;
        int a = 0;
        bool isOpposite = x > 0;
        x = Math.Abs(x);
        while (x > 0)
        {
            if((int.MaxValue - x % 10) / 10 < a) return 0;
            a = a * 10 + x % 10;
            x /= 10;
        }

        
        return isOpposite ? a : -a;
    }
}