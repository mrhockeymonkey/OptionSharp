
namespace OptionSharp;

public static class Constructors
{
    public static Option<T> Some<T>(T value) => new Some<T>(value);
    public static Option<T> None<T>() => new None<T>();

    public static Result<T, TErr> Ok<T, TErr>(T value) => new Ok<T, TErr>(value);
    public static Result<T, TErr> Err<T, TErr>(TErr error) => new Err<T, TErr>(error);
    
    public static Result<T, ErrMessage> Ok<T>(T value) => new Ok<T, ErrMessage>(value);
    public static Result<T, ErrMessage> Err<T>(string error) => new Err<T, ErrMessage>(new ErrMessage(error));
}