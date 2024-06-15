global using static OptionSharp.Constructors;

using OptionSharp;
using OptionSharp.Option;
using OptionSharp.Result;
using OptionSharp.Sample;
// ReSharper disable UnusedVariable
// ReSharper disable UnusedParameter.Local

await EnumerableExamples.AllExamples();

MatchExamples.AllExamples();

// using c# pattern matching

Option<string> maybeMessage = Some("Hello, World");

if (maybeMessage is Some<string> someMessage)
    Console.WriteLine(someMessage.Value);


Option<int> maybeValue = None<int>();

var valueResult = maybeValue switch
{
    Some<int> someValue => someValue.Value,
    None<int> => 0,
    _ => throw new InvalidOperationException()
};

// using optional methods

Option<string> something = Some("a");

var final = something
    .Map(value => $"{value}b")
    .Inspect(Console.WriteLine)
    .Bind(value => Some($"{value}c"))
    .UnwrapOrDefault(() => "zzz");
Console.WriteLine(final);

// results

DummyApi api = new();

await Ok("my string")
    .Map(str => $"{str} mapped")
    .Bind(str => api.EchoStringResult(str))
    .Inspect(Console.WriteLine)
    .BindAsync(str => api.GetNewStringAsync())
    .MapAsync(s => $"{s} + some mapping")
    .InspectAsync(Console.WriteLine); // once in the async world you will use the async extension methods

// conversion between results and options
Result<string, ErrMessage> r = Ok("a")
    .ToOption()
    .Map(s => $"{s}b")
    .ToResult(() => new ErrMessage("Option was none"));