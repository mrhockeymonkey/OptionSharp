namespace OptionSharp;

public abstract record Result<T, TErr>();

internal sealed record Ok<T, TErr>(T Value) : Result<T, TErr>();
internal sealed record Err<T, TErr>(TErr Error) : Result<T, TErr>();

public sealed record ErrMessage(string Message);
