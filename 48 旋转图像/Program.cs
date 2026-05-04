Solution sol = new Solution();
int[][] matrix = [[5, 1, 9, 11], [2, 4, 8, 10], [13, 3, 6, 7], [15, 14, 12, 16]];

sol.Rotate(matrix);
foreach (var row in matrix)
{
    Console.WriteLine(string.Join(" ", row));
}

public class Solution {
    public void Rotate(int[][] matrix)
    {
        int n = matrix.Length;
        for (int i = 0; i < n; ++i)
            Array.Reverse(matrix[i]);
        for(int i = 0; i < n; ++i)
        for (int j = 0; j < n - i; ++j)
            (matrix[i][j], matrix[n - 1 - j][n - 1 - i]) = (matrix[n - 1 - j][n - 1 - i], matrix[i][j]);
    }
}