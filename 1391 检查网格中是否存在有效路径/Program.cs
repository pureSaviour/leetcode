Solution sol = new();
int[][] grid = [[6,1,3],[4,1,5]];
Console.WriteLine(sol.HasValidPath(grid).ToString());

public class Solution {
    public bool HasValidPath(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int r = 0, c = 0;
        
        var initDir = grid[0][0];
        Vector vector;
        switch (initDir)
        {
            case 4 :
                grid[0][0] = 1;
                var can1 = HasValidPath(grid);
                grid[0][0] = 2;
                var can2 = HasValidPath(grid);
                return can1 || can2;
            case 5:
                return false;
            case 6:
                vector = new Vector(0, 1);
                break;
            case 1:
                vector = new Vector(1, 0);
                break;
            case 2:
                vector = new Vector(0, 1);
                break;
            case 3:
                vector = new Vector(1, 0);
                break;
            default:
                return false;
        }

        while (r <= m - 1 && r >= 0 && c <= n - 1 && c>= 0)
        {
            var dir = grid[r][c];
            if (IsValid(vector, dir, out vector))
            {
                if(r == m - 1 && c == n - 1) return true;
                r += vector.Y;
                c += vector.X;
            }else break;
        }
        return r == m - 1 && c == n - 1 && IsValid(vector, grid[r][c], out _);
    }
    
    private static bool IsValid(Vector vector, int dir, out Vector newVector)
    {
        newVector = Vector.InValid;
        switch (dir)
        {
            case 1 when vector.Y != 0:
                return false;
            case 1:
                newVector = vector;
                return true;
            case 2 when vector.X != 0:
                return false;
            case 2:
                newVector = vector;
                return true;
            case 3 when vector.X == 1:
                newVector = new Vector(0, 1);
                return true;
            case 3 when vector.Y == -1:
                newVector = new Vector(-1, 0);
                return true;
            case 3:
                return false;
            case 4 when vector.X == -1:
                newVector = new Vector(0, 1);
                return true;
            case 4 when vector.Y == -1:
                newVector = new Vector(1, 0);
                return  true;
            case 4:
                return false;
            case 5 when vector.X == 1:
                newVector = new Vector(0, -1);
                return true;
            case 5 when vector.Y == 1:
                newVector = new Vector(-1, 0);
                return true;
            case 5:
                return false;
            case 6 when vector.X == -1:
                newVector = new Vector(0, -1);
                return true;
            case 6 when vector.Y == 1:
                newVector = new Vector(1, 0);
                return true;
            case 6:
                return false;
            default:
                return false;
        }
    }
    
    private readonly struct Vector(int x, int y)
    {
        public int X { get; init; } = x;
        public int Y { get; init; } = y;
        public static readonly Vector InValid = new Vector(-1, -1);
    }
}