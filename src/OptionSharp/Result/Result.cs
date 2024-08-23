namespace OptionSharp;

// ReSharper disable UnusedTypeParameter
public abstract record Result<T, TErr>;
// ReSharper restore UnusedTypeParameter

public sealed record Ok<T, TErr>(T Value) : Result<T, TErr>
{
    public T Value { get; } = Value is null
        ? throw new ArgumentNullException(nameof(Value))
        : Value;
}

public sealed record Err<T, TErr>(TErr Error) : Result<T, TErr>
{
    public TErr Error { get; } = Error is null
        ? throw new ArgumentNullException(nameof(Error))
        : Error;
}

public sealed record ErrMessage(string Message)
{
    public string Message { get; } = Message is null
        ? throw new ArgumentNullException(nameof(Message))
        : Message;
}
