```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8037/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7945HX with Radeon Graphics 2.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 10.0.200-preview.0.26103.119
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v4
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v4


```
| Method              | Mean      | Error     | StdDev    | Median    | Rank | Allocated |
|-------------------- |----------:|----------:|----------:|----------:|-----:|----------:|
| BinarySplitSolution |  20.67 ns |  0.143 ns |  0.127 ns |  20.69 ns |    1 |      24 B |
| FindKthSolution     | 795.30 ns | 25.187 ns | 72.669 ns | 765.51 ns |    2 |      24 B |
