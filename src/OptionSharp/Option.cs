namespace OptionSharp;

public abstract record Option<T> where T : notnull;

public sealed record None<T>() : Option<T>()
    where T : notnull;

public sealed record Some<T>(T Value) : Option<T>()
    where T : notnull
{
    public T Value { get; } = Value is null
        ? throw new ArgumentNullException(nameof(Value))
        : Value;
}

