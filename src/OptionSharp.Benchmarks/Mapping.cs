using BenchmarkDotNet.Attributes;

namespace OptionSharp.Benchmarks;

[MemoryDiagnoser]
public class Mapping
{
    [Benchmark]
    public int OptionalBenchMap()
    {
        return Optional.Option.Some(1)
            .Map(i => i + 1)
            .Map(i => i + 1)
            .Map(i => i + 1)
            .Map(i => i + 1)
            .Map(i => i + 1)
            .ValueOr(0);
    }

    [Benchmark]
    public int OptionSharpBenchMap()
    {
        return OptionSharp.Constructors.Some(1)
            .Map(i => i + 1)
            .Map(i => i + 1)
            .Map(i => i + 1)
            .Map(i => i + 1)
            .Map(i => i + 1)
            .Reduce(() => 0);
    }
    
    public record Foo(double Value);
    
    [Benchmark]
    public Foo OptionalBenchMapMixed()
    {
        return Optional.Option.Some(1)
            .Map(i => $"{i}")
            .Map(s => s + "1")
            .Map(s => int.Parse(s))
            .Map(i => (double)i)
            .Map(d => new Foo(d))
            .ValueOr(new Foo(0));
    }

    [Benchmark]
    public Foo OptionSharpBenchMapMixed()
    {
        return OptionSharp.Constructors.Some(1)
            .Map(i => $"{i}")
            .Map(s => s + "1")
            .Map(s => int.Parse(s))
            .Map(i => (double)i)
            .Map(d => new Foo(d))
            .Reduce(() => new Foo(0));
    }
}

