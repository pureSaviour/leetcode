public class Solution {
    public int TotalWaviness(int num1, int num2)
    {
        int sum = 0;
        for(int i = Math.Max(num1, 100); i <= num2; ++i)
            sum += GetCount(i);
        return sum;
    }

    private static int GetCount(int num){
        string str = num.ToString();
        int count = 0;
        for(int i = 1; i < str.Length - 1; ++i)
            if ((str[i - 1] < str[i] && str[i] > str[i + 1]) || (str[i - 1] > str[i] && str[i] < str[i + 1]))
                ++count;
        return count;
    }
}