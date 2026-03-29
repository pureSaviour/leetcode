```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8037/25H2/2025Update/HudsonValley2)
AMD Ryzen 9 7945HX with Radeon Graphics 2.50GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 10.0.200-preview.0.26103.119
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v4
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v4


```
| Method         | Mean            | Error         | StdDev        | Allocated |
|--------------- |----------------:|--------------:|--------------:|----------:|
| GetSortedIndex |        36.93 ns |      0.143 ns |      0.120 ns |         - |
| GetIndex       | 5,178,763.28 ns | 71,735.630 ns | 56,006.461 ns |         - |
