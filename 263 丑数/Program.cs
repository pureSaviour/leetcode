Solution sol = new();
var ugly = sol.IsUgly(14);
Console.WriteLine(ugly);
public class Solution {
    public bool IsUgly(int n)
    {
        int[] cons = [5, 3, 2];
        if (n is 0 or 1) return false;
        foreach (var con in cons)
        {
            while (n % con == 0)
            {
                n /= con;
            }
        }

        if (n == 1) return true;
        return false;
    }
}