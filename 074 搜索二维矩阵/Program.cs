
int[][] matrix = [[1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60]];
Solution solution = new Solution();
bool canFind = solution.SearchMatrix(matrix, 3);
Console.WriteLine(canFind);

public class Solution
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        // m行n列
        int m = matrix.Length;
        if (m == 0)
            return false;
        int n = matrix[0].Length;
        if (n == 0) return false;

        int t = m * n;
        int l = 0;
        int r = t - 1;
        while (l <= r)
        {
            int mid = l + (r - l) / 2;
            int row = mid / n;
            int col = mid % n;
            if (matrix[row][col] < target)
                l = mid + 1; 
            else if (matrix[row][col] > target)
                r = mid - 1;
            else return true;
        }
        
        return false;
    }
}