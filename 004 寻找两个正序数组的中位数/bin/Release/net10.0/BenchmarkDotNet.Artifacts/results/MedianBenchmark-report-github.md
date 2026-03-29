```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8037/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7945HX with Radeon Graphics 2.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 10.0.200-preview.0.26103.119
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v4
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v4


```
| Method              | Mean      | Error     | StdDev     |
|-------------------- |----------:|----------:|-----------:|
| BinarySplitSolution |  45.63 ns |  1.829 ns |   5.188 ns |
| FindKthSolution     | 990.99 ns | 46.464 ns | 135.538 ns |
