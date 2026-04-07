Solution sol = new();
var tickets = new int[] {2, 3, 2};
var k = 2;
var time = sol.TimeRequiredToBuy(tickets, k);
Console.WriteLine(time);

public class Solution {
    public int TimeRequiredToBuy(int[] tickets, int k) {
        int target = tickets[k];
        int n = tickets.Length;
        int time = 0;
        for (int i = 0; i < n; i++)
        {
            if (i <= k)
            {
                time += Math.Min(target, tickets[i]);
            }
            else
            {
                time += Math.Min(target - 1, tickets[i]);
            }
        }
        return time;
    }
}