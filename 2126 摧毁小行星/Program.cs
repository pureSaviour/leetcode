using System.Numerics;

public class Solution {
    public bool AsteroidsDestroyed(int mass, int[] asteroids) {
        int n = asteroids.Length;
        uint maxMass = (uint)asteroids.Max();
        long m = mass;
        int maxBitLength = BitOperations.Log2(maxMass);
        var cnt = new (int min, long sum)[maxBitLength + 1];
        for (int i = 0; i <= maxBitLength; ++i)
        {
            cnt[i].min = int.MaxValue;
            cnt[i].sum = 0;
        }
        for (int i = 0; i < n; ++i)
        {
            int bitLength = BitOperations.Log2((uint)asteroids[i]);
            cnt[bitLength].min = Math.Min(cnt[bitLength].min, asteroids[i]);
            cnt[bitLength].sum += asteroids[i];
        }

        for (int i = 0; i <= maxBitLength; ++i)
        {
            if(m < cnt[i].min && cnt[i].min != int.MaxValue)
                return false;
            m += cnt[i].sum;
        }
        return true;
    }
}