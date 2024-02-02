using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class NoneConstruction
{
    [Benchmark]
    public void OptionalBenchNone()
    {
        foreach (var i in Enumerable.Range(0, 100))
        {
            Optional.Option.None<int>();
        }
    }
    
    [Benchmark]
    public void OptionSharpBenchAnotherNoneCtor()
    {
        foreach (var i in Enumerable.Range(0, 100))
        {
            OptionSharp.Constructors.None<int>();
        }
    }
}