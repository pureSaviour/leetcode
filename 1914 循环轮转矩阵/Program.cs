Solution sol = new();
int[][] grid = [[1,2,3,4],[5,6,7,8],[9,10,11,12],[13,14,15,16]];
int k = 2;
var res = sol.RotateGrid(grid, k);
foreach (var row in res)
{
    Console.WriteLine(string.Join(" ", row));
}
public class Solution {
    public int[][] RotateGrid(int[][] grid, int k) {
        int m = grid.Length;
        int n = grid[0].Length;
        int layer = Math.Min(m, n) / 2;
        int[][] res = new int[m][];
        for (int i = 0; i < m; ++i)
            res[i] = new int[n];

        for (int i = 0; i < layer; ++i)
        {
            int mi = m - 2 * i;
            int ni = n - 2 * i;
            int ki = k % (2 * (mi + ni) - 4);
            int size = 2 * (mi + ni) - 4;

            for (int j = 0; j < size; ++j)
            {
                var originIndex = GetIndex((j + ki) % size, mi, ni, i);
                var targetIndex = GetIndex(j, mi, ni, i);
                res[targetIndex.i][targetIndex.j] = grid[originIndex.i][originIndex.j];
            }
        }
        return res;
    }

    private static (int i, int j) GetIndex(int k, int m, int n, int layer)
    {
        int initI = layer;
        int initJ = layer;
        m = m - 1;
        n = n - 1;
        if (k < n)
            initJ += k;
        else if (k < m + n)
        {
            initJ += n;
            initI += k - n;
        }
        else if (k < m + n * 2)
        {
            initJ += n - (k - m - n);
            initI += m;
        }
        else
            initI += (n + m) * 2 - k;
        return (initI, initJ);
    }
}