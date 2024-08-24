using OptionSharp.Result;

namespace OptionSharp.Sample;

public class MultipleResults
{
    public Result<int, AdditionError> HandleImperative()
    {
        // you can type check results but this desnt let you handle both cases
        var oneResult = Operation1();
        if (oneResult is not Ok<int, ErrMessage> (var one))
            return Err<int, AdditionError>(new AdditionError());
        
        var twoResult = Operation2();
        if (twoResult is not Ok<int, ErrMessage> (var two))
            return Err<int, AdditionError>(new AdditionError());

        var sumResult = Add(one, two);
        if (sumResult is not Ok<int, ErrMessage> (var sum))
            return Err<int, AdditionError>(new AdditionError());

        return sumResult.MapErr(message => new AdditionError());
    }

    public Result<int, ErrMessage> HandleNested()
    {
        return Operation1()
            .Bind(op1 => Operation2()
                .Bind(op2 => Operation3()
                    .Map(op3 => op1 + op2 + op3)));
    }

    public Result<int, ErrMessage> HandleCollected()
    {
        Result<int, ErrMessage>[] results = [Operation1(), Operation2(), Operation3()];
        return results
            .Collect()
            .Map(numbers => numbers.Sum());
    }
    
    private Result<int, ErrMessage> Operation1() => Ok(1);
    private Result<int, ErrMessage> Operation2() => Ok(2);
    private Result<int, ErrMessage> Operation3() => Ok(3);
    private Result<int, ErrMessage> Add(int x, int y) => Ok(x + y);

    public record AdditionError;
}