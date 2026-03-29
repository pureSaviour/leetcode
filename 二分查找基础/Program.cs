using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

Solution solution = new();
int[] arr = RandomGenerator.RandomGenerator.GenerateSortedRandomArrayWithFixedSeed(100, 0, (int)10);
int target = 5;
int index = solution.GetSortedIndex(arr, target);
int correctIndex = Array.IndexOf(arr, target);
Console.WriteLine($"The index of {target} is: {index}"); // Output:
Console.WriteLine($"The correct index of {target} is: {correctIndex}");
BenchmarkRunner.Run<Test>();


public class Solution
{
    public int GetSortedIndex(int[] arr, int target)
    {
        if (arr.Length == 0)
            return -1;
        int n = arr.Length;
        int l = 0;
        int r = n - 1;
        while (l <= r)
        {
            int m = l + (r - l) / 2;
            if (arr[m] < target)
            {
                l = m + 1;
            }
            else
            {
                r = m - 1;
            }
        }
        
        return arr[l] == target ? l : -1;
    }
}

[MemoryDiagnoser]
public class Test
{
    private int[] _arr;
    private int _target;
    private Solution _solution;
    [Benchmark]
    public int GetSortedIndex()
    {
        return _solution.GetSortedIndex(_arr, _target);
    }

    [Benchmark]
    public int GetIndex()
    {
        return _arr.IndexOf(_target);
    }
    [GlobalSetup]
    public void Setup()
    {
        const int length = 1000_000_000;
        _solution = new Solution();
        _arr = RandomGenerator.RandomGenerator.GenerateSortedRandomArrayWithFixedSeed(length, 0, length *2);
        _target = RandomGenerator.RandomGenerator.GenerateRandomIntWithFixedSeed(0, length * 2);
    }
}