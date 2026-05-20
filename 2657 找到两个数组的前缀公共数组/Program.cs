using System.Numerics;

Solution sol = new();
int[] A = [1, 3, 2, 4];
int[] B = [3, 1, 2, 4];
var res = sol.FindThePrefixCommonArray(A, B);
Console.WriteLine(string.Join(", ", res));
public class Solution {
    public int[] FindThePrefixCommonArray(int[] A, int[] B)
    {
        int n = A.Length;
        bool[] visited = new bool[n + 1];
        int[] res = new int[n];
        for (int i = 0; i < n; ++i)
        {
            if(i >= 1)
                res[i] = res[i - 1];
            var numA = A[i];
            var numB = B[i];
            if (visited[numA])
                ++res[i];
            visited[numA] = true;
            if (visited[numB])
                ++res[i];
            visited[numB] = true;
        }
        return res;
    }

    public int[] FindThePrefixCommonArrayByBit(int[] A, int[] B)
    {
        int n =  A.Length; 
        int[] res = new int[n];
        ulong maskA = 0;
        ulong maskB = 0;
        for(int i = 0; i < n; ++i)
        {
            maskA |= 1UL << A[i];
            maskB |= 1UL << B[i];
            res[i] = BitOperations.PopCount(maskA & maskB);
        }

        return res;
    }
}