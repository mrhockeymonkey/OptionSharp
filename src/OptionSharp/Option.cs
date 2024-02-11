namespace OptionSharp;

public abstract record Option<T>;

public sealed record None<T>() : Option<T>();

public sealed record Some<T>(T Value) : Option<T>()
{
    public T Value { get; init; } = Value is null
        ? throw new ArgumentNullException(nameof(Value))
        : Value;
}

