namespace OptionSharp.Sample;


public class DummyApi
{
    public Result<string, ErrMessage> EchoStringResult(string str) => Ok($"{str} echoed!");
    public Result<string, ErrMessage> FailedEchoStringResult(string str) => Err<string>($"{str} echoed!");

    public async Task<Result<string, ErrMessage>> GetNewStringAsync()
    {
        await Task.Delay(1000);
        return Ok("GetAsyncString");
    }
}