Solution solution = new Solution();
Console.WriteLine(solution.IsPalindrome(121));


public class Solution {
    public bool IsPalindrome(int x) {
        if(x < 0) return false;
        if(x ==0) return true;
        if(x % 10 == 0) return false;
        int original = x;
        long m = 0;
        while (x > m)
        {
            m = m * 10 + x % 10;
            x /= 10;
        }
        
        return m == x || m / 10 == x;
    }
}