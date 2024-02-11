using BenchmarkDotNet.Running;
using OptionSharp.Benchmarks;

//var summary = BenchmarkRunner.Run<NoneConstruction>();
var summary = BenchmarkRunner.Run<SomeConstruction>();
//var summary = BenchmarkRunner.Run<Mapping>();