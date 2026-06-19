public class Solution {
    public int LargestAltitude(int[] gain) {
        int max = 0;
        for(int i = 0, cur = 0; i < gain.Length; ++i){
            cur += gain[i];
            max = Math.Max(cur, max);
        }
        return max;
    }
}