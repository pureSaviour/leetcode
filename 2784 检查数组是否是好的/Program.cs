Solution sol = new();
int[] nums = [5, 7, 3, 1, 5, 2, 6, 4];
Console.WriteLine(sol.IsGood(nums).ToString());

public class Solution {
    public bool IsGood(int[] nums)
    {
        int n = nums.Length;
        bool[] visited = new bool[n];
        bool visitMax = false;
        for (int i = 0; i < n; ++i)
        {
            var num = nums[i];
            if (num == 0 || num >= n)
                return false;
            if (visited[num])
            {
                if(num != n - 1)
                    return false;
                if (visitMax)
                    return false;
                visitMax = true;
            }
            visited[num] = true;
        }

        return true;
    }
}