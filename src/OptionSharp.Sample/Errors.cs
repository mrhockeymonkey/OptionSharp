namespace OptionSharp.Sample;

public class Errors
{
    public void CreatingErrors()
    {
        // Error is the base class for all error types
        var defaultErr = Err<int>(new Error());
        var explicitErr = Err<int, Error>(new Error());
        
        // ErrMessage is provided for convenience...
        var messageErr = Err<int>(new ErrMessage("something went wrong"));
        var legacyMessageErr = Err<int>("something went awry");

        // ... but it's better to define your own custom error types
        var notFoundErr = Err<int>(new NotFoundError());
        var badInputErr = Err<int>(new BadInputError(0));
    }

    public void MappingAndLoggingErrors()
    {
        var someResult = Err<int>("something threw an exception");
        
        // you can map received errors into your own 
    }
}

public abstract record MyCustomError : Error;
public record NotFoundError : MyCustomError;
public record BadInputError(int Input) : MyCustomError;

//public static 