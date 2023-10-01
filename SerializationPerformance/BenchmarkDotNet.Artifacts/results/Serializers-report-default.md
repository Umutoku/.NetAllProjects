
BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22621.674)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100-rc.2.22477.23
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2
  Job-TBRQHL : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2

Runtime=.NET 6.0  MaxIterationCount=5  MaxWarmupIterationCount=8  
MinIterationCount=3  

            Method | BookCount | Mean | Error |
------------------ |---------- |-----:|------:|
            **ToJson** |         **1** |   **NA** |    **NA** |
     ToMessagePack |         1 |   NA |    NA |
 ToMessagePackJson |         1 |   NA |    NA |
        ToProtobuf |         1 |   NA |    NA |
            **ToJson** |        **10** |   **NA** |    **NA** |
     ToMessagePack |        10 |   NA |    NA |
 ToMessagePackJson |        10 |   NA |    NA |
        ToProtobuf |        10 |   NA |    NA |
            **ToJson** |      **1000** |   **NA** |    **NA** |
     ToMessagePack |      1000 |   NA |    NA |
 ToMessagePackJson |      1000 |   NA |    NA |
        ToProtobuf |      1000 |   NA |    NA |
            **ToJson** |     **10000** |   **NA** |    **NA** |
     ToMessagePack |     10000 |   NA |    NA |
 ToMessagePackJson |     10000 |   NA |    NA |
        ToProtobuf |     10000 |   NA |    NA |
            **ToJson** |    **100000** |   **NA** |    **NA** |
     ToMessagePack |    100000 |   NA |    NA |
 ToMessagePackJson |    100000 |   NA |    NA |
        ToProtobuf |    100000 |   NA |    NA |

Benchmarks with issues:
  Serializers.ToJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=1]
  Serializers.ToMessagePack: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=1]
  Serializers.ToMessagePackJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=1]
  Serializers.ToProtobuf: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=1]
  Serializers.ToJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=10]
  Serializers.ToMessagePack: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=10]
  Serializers.ToMessagePackJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=10]
  Serializers.ToProtobuf: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=10]
  Serializers.ToJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=1000]
  Serializers.ToMessagePack: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=1000]
  Serializers.ToMessagePackJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=1000]
  Serializers.ToProtobuf: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=1000]
  Serializers.ToJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=10000]
  Serializers.ToMessagePack: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=10000]
  Serializers.ToMessagePackJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=10000]
  Serializers.ToProtobuf: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=10000]
  Serializers.ToJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=100000]
  Serializers.ToMessagePack: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=100000]
  Serializers.ToMessagePackJson: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=100000]
  Serializers.ToProtobuf: Job-TBRQHL(Runtime=.NET 6.0, MaxIterationCount=5, MaxWarmupIterationCount=8, MinIterationCount=3) [BookCount=100000]
