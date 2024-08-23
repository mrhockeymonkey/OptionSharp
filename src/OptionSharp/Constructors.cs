
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
    
    public static Result<T, Error> Ok<T>(T value) 
        where T : notnull
        => new Ok<T, Error>(value);
    public static Result<T, Error> Err<T>(Error error) 
        => new Err<T, Error>(error);    
    
    public static Result<T, Error> Err<T>(string message) 
        => new Err<T, Error>(new ErrMessage(message));
}