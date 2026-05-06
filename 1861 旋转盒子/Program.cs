Solution sol = new();
char[][] boxGrid =
[
    ['#', '#', '*', '.', '*', '.'],
    ['#', '#', '#', '*', '.', '.'],
    ['#', '#', '#', '.', '#', '.']
];
var res = sol.RotateTheBox(boxGrid);
foreach (var item in res)
{
    Console.WriteLine(string.Join(' ', item));
}

public class Solution
{
    public char[][] RotateTheBox(char[][] boxGrid)
    {
        int m = boxGrid.Length;
        int n = boxGrid[0].Length;

        var res = new char[n][];
        for (int i = 0; i < n; ++i)
        {
            res[i] = new char[m];
            for(int j = 0; j < m; ++j)
                res[i][j] = '.';
        }

        for (int i = 0; i < m; ++i)
        {
            int count = 0;
            int lastObstacleIndex = -1;
            for (int j = 0; j < n; ++j)
            {
                var item = boxGrid[i][j];
                if (item == '#')
                    ++count;
                else if (item == '*')
                {
                    for (int k = j - 1; k > lastObstacleIndex; --k)
                    {
                        var newIndex = RotateIndex(i, k, m);
                        var newI = newIndex.newI;
                        var newJ = newIndex.newJ;
                        if (count > 0)
                        {
                            res[newI][newJ] = '#';
                            --count;
                        }
                    }
                    res[RotateIndex(i, j, m).newI][RotateIndex(i, j, m).newJ] = '*';
                    lastObstacleIndex = j;
                }
            }

            if (count > 0)
                for (int k = n - 1; k > lastObstacleIndex; --k)
                {
                    var newIndex = RotateIndex(i, k, m);
                    var newI = newIndex.newI;
                    var newJ = newIndex.newJ;
                    if (count > 0)
                    {
                        res[newI][newJ] = '#';
                        --count;
                    }
                }
        }
        
        return res;
    }

    private static (int newI, int newJ) RotateIndex(int i, int j, int m)
    {
        int newI = j;
        int newJ = m - i - 1;
        return (newI, newJ);
    }
}