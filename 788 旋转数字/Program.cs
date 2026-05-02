Solution sol = new();
Console.WriteLine(sol.RotatedDigits(10).ToString());


public class Solution {
    private static readonly int[] _rotateMap = new int[10000 + 1];     // 0: 有效但不旋转, -1: 无效, 1: 有效且旋转
    private static readonly int[] _baseRotateMap = [ 0, 0, 1, -1, -1, 1, 1, -1, 0, 1];
    public int RotatedDigits(int n)
    {
        int count = 0;
        for(int i = 2; i <= n; i++)
            if(IsRotateNum(i) == 1)
                ++count;
        return count;
    }

    private static int IsRotateNum(int num)
    {
        var rotateDigit = _baseRotateMap[num % 10];
        _rotateMap[num] = _rotateMap[num / 10] switch
        {
            1 => rotateDigit >= 0 ? 1 : -1,
            0 => rotateDigit switch
            {
                1 => 1,
                0 => 0,
                _ => -1
            },
            _ => -1
        };
        return _rotateMap[num];
    }
}