int[] arr = [-10000, 10000];
Solution solution = new Solution();
Console.WriteLine(solution.CanMakeArithmeticProgression(arr));


public class Solution {
    public bool CanMakeArithmeticProgression(int[] arr) {
        int n = arr.Length;
        int min = int.MaxValue;
        int max = int.MinValue;
        foreach (var num in arr)
        {
            if (num < min) min = num;
            if (num > max) max = num;
        }
        if((max - min) % (n - 1) != 0) return false;
        int d = (max - min) / (n - 1);
        if (d == 0) return true;
        bool[] seen = new bool[n];
        for (int i = 0; i < n; i++)
        {
            if((arr[i] - min) % d != 0) return false;
            int index = (arr[i] - min) / d;
            if(seen[index]) return false;
            seen[index] = true;
        }
        return true;
    }
}