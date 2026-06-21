public class Solution {
    public int MaxIceCream(int[] costs, int coins) {
        int min = int.MaxValue, max = int.MinValue;
        foreach(var cost in costs){
            min = Math.Min(cost, min);
            max = Math.Max(cost, max);
        }

        int[] orderedCosts = new int [max - min + 1];
        foreach(var cost in costs)
            ++orderedCosts[cost - min];
        int count = 0;
        for (int i = 0; i < orderedCosts.Length; ++i)
        {
            if (orderedCosts[i] > 0)
            {
                int k = coins / (i + min);
                if (k >= orderedCosts[i])
                {
                    coins -= orderedCosts[i] * (i + min);
                    count += orderedCosts[i];
                }
                else
                {
                    count += k;
                    break;
                }
            }
        }
        return count;
    }
}