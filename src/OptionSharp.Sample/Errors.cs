using OptionSharp.Result;

namespace OptionSharp.Sample;

public class Errors
{
    public void CreatingErrors()
    {
        // ErrMessage is a simple result type provided for convenience...
        Result<int, ErrMessage> basicOk = Ok(7);
        Result<int, ErrMessage> basicErr = Err<int, ErrMessage>(new ErrMessage("something went wrong"));
        Result<int, ErrMessage> basicErrShortcut = Err<int>("something went awry");
        
        // ... however a better approach is to define your own error types and use the
        // source generated constructors (See HandleError and PublishError examples below)
        Result<int, HandleError> handleOk = Handle.Ok(7);
        Result<int, HandleError> handleErr = Handle.Err<int>(new FailedToDeserialize(""));

        Result<int, PublishError> publishOk = Publish.Ok(7);
        Result<int, PublishError> publishErr = Publish.Err<int>(new BrokerUnavailable());

        var eight = TryGetIdOnlyIfSeven(7)
            .Inspect(seven => Console.WriteLine($"seven is {seven}"))
            .Map(seven => seven + 1)
            .UnwrapOrDefault(0);
    }
    
    public Result<int, HandleError> TryGetIdOnlyIfSeven(int id) => 
        id == 7 
            ? Handle.Ok(id) 
            : Handle.Err<int>(new InvalidId(id));

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


[GenerateResult("Handle")]
public abstract record HandleError;

public sealed record FailedToDeserialize(string Message) : HandleError;
public sealed record InvalidId(int Id) : HandleError;


[GenerateResult("Publish")]
public abstract record PublishError;
public sealed record BrokerUnavailable : PublishError;
public sealed record AuthenticationFailed : PublishError;

// custom error types
public abstract record MyCustomError;
public record NotFoundError : MyCustomError;
public record BadInputError(int Input) : MyCustomError;






public static class MyHelpers
{
    public static MyCustomError AsMyCustomError(this MyCustomError error) => error;
}