using OptionSharp.Result;
using static OptionSharp.Sample.MyConstructors;

namespace OptionSharp.Sample;

public class Errors
{
    public void CreatingErrors()
    {
        // ErrMessage is provided for convenience...
        var messageErr = Err<int, ErrMessage>(new ErrMessage("something went wrong"));
        var legacyMessageErr = Err<int>("something went awry");

        // ... but it's better to define your own custom error types
        var notFoundErr = Err<int, MyCustomError>(new NotFoundError());
        var badInputErr = Err<int, MyCustomError>(new BadInputError(0));
        
        // you can also build your own typed constructors for a cleaner syntax
        Result<int, MyCustomError> ok7 = OkNumber(7);
        Result<int, MyCustomError> err7 = ErrNumber(new BadInputError(7));
    }

    public Result<int, MyCustomError> MappingErrors()
    {
        Result<int, ErrMessage> someResult = Err<int>("something threw an exception");
        
        // you can map received errors into your own however...
        // because covariance is only supported in interface and delegate types you may need to cast your error
        // to its base type occasionally to match a method signature
        return someResult
            .MapErr(errMessage => new NotFoundError().AsMyCustomError());
    }

    public void LoggingErrors()
    {
        MappingErrors()
            .Match(
                ok => Console.WriteLine($"We got an int: {ok}"),
                err =>
                {
                    switch (err)
                    {
                        case NotFoundError nf:
                            Console.WriteLine("Not found!");
                            break;
                        case BadInputError bi:
                            Console.WriteLine($"Bad input: {bi.Input}");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                });
    }
}

// custom error types
public abstract record MyCustomError;
public record NotFoundError : MyCustomError;
public record BadInputError(int Input) : MyCustomError;


// custom ctors
public static class MyConstructors
{
    public static Result<int, MyCustomError> OkNumber(int value) 
        => new Ok<int, MyCustomError>(value);
    
    public static Result<int, MyCustomError> ErrNumber(MyCustomError error) 
        => new Err<int, MyCustomError>(error);
}

public static class MyHelpers
{
    public static MyCustomError AsMyCustomError(this MyCustomError error) => error;
}