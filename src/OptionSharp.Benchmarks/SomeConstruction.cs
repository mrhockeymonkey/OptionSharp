using BenchmarkDotNet.Attributes;

namespace OptionSharp.Benchmarks;

public class SomeConstruction
{
    [Benchmark]
    public void OptionalBenchNone()
    {
        foreach (var i in Enumerable.Range(0, 100))
        {
            Optional.Option.Some<int>(i);
        }
    }
    
    [Benchmark]
    public void OptionSharpBenchAnotherNoneCtor()
    {
        foreach (var i in Enumerable.Range(0, 100))
        {
            OptionSharp.Constructors.Some<int>(i);
        }
    }
}