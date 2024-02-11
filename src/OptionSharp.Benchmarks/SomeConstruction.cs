using BenchmarkDotNet.Attributes;

namespace OptionSharp.Benchmarks;

[MemoryDiagnoser]
public class SomeConstruction
{
    [Benchmark]
    public Optional.Option<int> OptionalBench_CreateNoneOfInt()
        => Optional.Option.Some<int>(7);

    
    [Benchmark]
    public OptionSharp.Option<int> OptionSharpBench_CreateNoneOfInt()
        => OptionSharp.Constructors.Some<int>(7);

    [Benchmark]
    public LanguageExt.Some<int> LanguageExtBench_CreateNoneOfInt()
        => new LanguageExt.Some<int>(7);
    
    [Benchmark]
    public Optional.Option<string> OptionalBench_CreateNoneOfString()
        => Optional.Option.Some<string>("benchmark");

    
    [Benchmark]
    public OptionSharp.Option<string> OptionSharpBench_CreateNoneOfString()
        => OptionSharp.Constructors.Some<string>("benchmark");

    [Benchmark]
    public LanguageExt.Some<string> LanguageExtBench_CreateNoneOfString()
        => new LanguageExt.Some<string>("benchmark");
}