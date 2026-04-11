public class Solution {
    public int MinimumDistance(int[] nums)
    {
        var n = nums.Length;
        var firstIndex = new int[101];
        var secondIndex = new int[101];
        int minDis = int.MaxValue;
        for (int i = 0; i < firstIndex.Length; i++)
        {
            firstIndex[i] = -1;
        }
        for (var i = 0; i < n; i++)
        {
            var index = nums[i];
            if (firstIndex[index] == -1)
            {
                firstIndex[index] = i;
            }else
            {
                if(secondIndex[index] == 0)
                    secondIndex[index] = i;
                else
                {
                    minDis = Math.Min(minDis, 2 * (i - firstIndex[index]));
                    firstIndex[index] = secondIndex[index];
                    secondIndex[index] = i;
                }
            }
        }
        return minDis == int.MaxValue ? -1 : minDis;
    }
}