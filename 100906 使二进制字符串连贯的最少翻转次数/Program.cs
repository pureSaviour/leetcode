// 100906 使二进制字符串连贯的最少翻转次数

public class Solution {
    public int MinFlips(string s)
    {
        int n = s.Length;
        int c0 = s.Count(x => x == '0');
        
        return Math.Min(c0, Math.Max((s[0] == '1' && s[n - 1] == '1') ? n - c0 - 2 : n - c0 - 1, 0));
    }
}