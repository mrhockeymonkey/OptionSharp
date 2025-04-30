//HintName: GeneratedConstructors.g.cs
using MyApp.Errors;

namespace OptionSharp;

public static class Test
{
    public static Result<T, HandleError> Ok<T>(T value)
        where T : notnull
        => new Ok<T, HandleError>(value);
    
    public static Result<T, HandleError> Err<T>(HandleError error) 
        => new Err<T, HandleError>(error);
}