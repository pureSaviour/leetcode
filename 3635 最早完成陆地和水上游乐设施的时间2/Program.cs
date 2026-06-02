public class Solution {
    public int EarliestFinishTime(int[] landStartTime, int[] landDuration, int[] waterStartTime, int[] waterDuration) {
        int lW = Solve(landStartTime, landDuration, waterStartTime, waterDuration);
        int wL = Solve(waterStartTime, waterDuration, landStartTime, landDuration);
        return Math.Min(lW, wL);
    }

    private static int SolveLinq(int[] landStartTime, int[] landDuration, int[] waterStartTime, int[] waterDuration){
        int minFinish = landStartTime.Zip(landDuration).Min(item => item.First + item.Second);
        return waterStartTime.Zip(waterDuration).Min(item => Math.Max(item.First, minFinish) + item.Second);
    }
    
    private static int Solve(int[] landStartTime, int[] landDuration, int[] waterStartTime, int[] waterDuration){
        int minFinish = int.MaxValue;
        for(int i = 0; i < landStartTime.Length; ++i)
            minFinish = Math.Min(minFinish, landStartTime[i] + landDuration[i]);
        int ans = int.MaxValue;
        for(int i = 0; i < waterStartTime.Length; ++i)
            ans = Math.Min(ans, Math.Max(waterStartTime[i], minFinish) + waterDuration[i]);
        return ans;
    }
}