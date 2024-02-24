
namespace OptionSharp;

public static class Constructors
{
    public static Option<T> Some<T>(T value) where T : notnull
        => new Some<T>(value);
    public static Option<T> None<T>() where T : notnull 
        => new None<T>();

    public static Result<T, TErr> Ok<T, TErr>(T value) 
        where T : notnull
        => new Ok<T, TErr>(value);
    
    public static Result<T, TErr> Err<T, TErr>(TErr error) 
        where TErr : notnull
        => new Err<T, TErr>(error);
    
    public static Result<T, ErrMessage> Ok<T>(T value) 
        where T : notnull
        => new Ok<T, ErrMessage>(value);
    public static Result<T, ErrMessage> Err<T>(string error) 
        => new Err<T, ErrMessage>(new ErrMessage(error));
}