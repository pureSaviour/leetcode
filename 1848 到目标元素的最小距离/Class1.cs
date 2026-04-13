public class Solution {
    public int GetMinDistance(int[] nums, int target, int start) {
        int minDistance = int.MaxValue;
        int n = nums.Length;
        for (int i = 0; i < n; ++i)
            if (nums[i] == target)
            {
                var dis = Math.Abs(start - i);
                if(dis < minDistance)
                    minDistance = dis;
            }
        return minDistance;
    }
}