Solution sol = new();
int[][] grid = [[529,529,989],[989,529,345],[989,805,69]];
int x = 92;

Console.WriteLine(sol.MinOperations(grid, x).ToString());


public class Solution {
    public int MinOperations(int[][] grid, int x) {
        List<int> nums = [];
        int m = grid.Length, n = grid[0].Length;
        int baseVal = grid[0][0];
        
        for (int i = 0; i < m; i++) 
            for (int j = 0; j < n; j++) {
                if ((grid[i][j] - baseVal) % x != 0) 
                    return -1;
                nums.Add(grid[i][j]);
            }
        
        nums.Sort();
        int choose = nums[nums.Count / 2];

        return nums.Sum(num => Math.Abs(num - choose) / x);
    }
}