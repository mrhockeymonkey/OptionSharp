using OptionSharp;
using static OptionSharp.Constructors;

// using c# pattern matching

Option<string> maybeMessage = Some("Hello, World");

if (maybeMessage is Some<string> someMessage)
    Console.WriteLine(someMessage.Value);


Option<int> maybeValue = None<int>();

var valueResult = maybeValue switch
{
    Some<int> someValue => someValue.Value,
    None<int> => 0
};

// using optional methods

Option<string> something = Some("a");

var final = something
    .Map(value => $"{value}b")
    .Inspect(Console.WriteLine)
    .Bind(value => Some($"{value}c"))
    .Reduce(() => "zzz");
Console.WriteLine(final);

