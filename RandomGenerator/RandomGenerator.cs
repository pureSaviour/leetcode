using System.Numerics;

namespace RandomGenerator;

public class RandomGenerator
{
    private readonly Random _random = new();
    
    private static readonly RandomGenerator _randomGenerator = new();

    public int[] GenerateRandomArray(int length, int minValue, int maxValue)
    {
        return Enumerable.Range(0, length)
                         .Select(_ => _random.Next(minValue, maxValue))
                         .ToArray();
    }
    
    public double[] GenerateRandomArray(int length, double minValue, double maxValue)
    {
        return Enumerable.Range(0, length)
                         .Select(_ => _random.NextDouble() * (maxValue - minValue) + minValue)
                         .ToArray();
    }

    public float[] GenerateRandomArray(int length, float minValue, float maxValue)
    {
        return Enumerable.Range(0, length)
            .Select(_ => _random.NextSingle() * (maxValue - minValue) + minValue)
            .ToArray();
    }
    
    public int[] GenerateSortedRandomArray(int length, int minValue, int maxValue, IComparer<int>? comparer = null)
    {
        var arr = GenerateRandomArray(length, minValue, maxValue);
        Array.Sort(arr, comparer);
        return arr;
    }

    public double[] GenerateSortedRandomArray(int length, double minValue, double maxValue,
        IComparer<double>? comparer = null)
    {
        var arr = GenerateRandomArray(length, minValue, maxValue);
        Array.Sort(arr, comparer);
        return arr;
    }

    public float[] GenerateSortedRandomArray(int length, float minValue, float maxValue,
        IComparer<float>? comparer = null)
    {
        var arr = GenerateRandomArray(length, minValue, maxValue);
        Array.Sort(arr, comparer);
        return arr;
    }

    public int GenerateRandomInt(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    
    public static int[] GenerateRandomArrayWithFixedSeed(int length, int minValue, int maxValue) => _randomGenerator.GenerateRandomArray(length, minValue, maxValue);
    public static double[] GenerateRandomArrayWithFixedSeed(int length, double minValue, double maxValue) => _randomGenerator.GenerateRandomArray(length, minValue, maxValue);
    public static float[] GenerateRandomArrayWithFixedSeed(int length, float minValue, float maxValue) => _randomGenerator.GenerateRandomArray(length, minValue, maxValue);
    public static int[] GenerateSortedRandomArrayWithFixedSeed(int length, int minValue, int maxValue, IComparer<int>? comparer = null) => _randomGenerator.GenerateSortedRandomArray(length, minValue, maxValue, comparer);
    public static double[] GenerateSortedRandomArrayWithFixedSeed(int length, double minValue, double maxValue) => _randomGenerator.GenerateSortedRandomArray(length, minValue, maxValue);
    public static float[] GenerateSortedRandomArrayWithFixedSeed(int length, float minValue, float maxValue) => _randomGenerator.GenerateSortedRandomArray(length, minValue, maxValue);
    public static int GenerateRandomIntWithFixedSeed(int minValue, int maxValue) => _randomGenerator.GenerateRandomInt(minValue, maxValue);
}