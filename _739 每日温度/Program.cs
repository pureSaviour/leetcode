Solution sol = new();
int[] temperature = [73,74,75,71,69,72,76,73];
var ans = sol.DailyTemperatures(temperature);
foreach (var an in ans)
{
    Console.WriteLine(an);
}

public class Solution
{
    public int[] DailyTemperatures(int[] temperatures) {
        int n = temperatures.Length;
        Stack<TemperatureInfo> stack = new(n);
        int[] ans = new int[n];
        for (int i = 0; i < n; ++i)
        {
            while (stack.Count != 0 && temperatures[i] > stack.Peek().Temperature)
            {
                var top = stack.Pop();
                ans[top.Index] = i - top.Index;
            }
            stack.Push(new TemperatureInfo()
            {
                Temperature = temperatures[i],
                Index = i
            });
        }

        while (stack.Count != 0)
        {
            var top = stack.Pop();
            ans[top.Index] = 0;
        }
        return ans;
    }
    
    private readonly struct TemperatureInfo
    {
        public int Temperature { get; init; }
        public int Index { get; init; }
    }
}