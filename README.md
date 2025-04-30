# OptionSharp

Another option for options in C#

Disclaimer: If you are looking for a **stable**, production ready lib you are better off using something like `LanguageExt.Core`.

## Motivation

This library is primarily for myself to learn about functional programming but also takes 
a different opinion/approach compared to other more well established libs.

- Focuses only on Option and Result types
- Does not allow for null
- Uses records as discriminated unions which is as close as you can get in c# whilst still being idiomatic (AFAIK)
- Rust-like (ish) feel, promotes the use of strongly typed errors
- Source generated code to keep it readable

## Usage

Define your error types as below which will generate some constructor methods for convenience

```c#
[GenerateResult("Handle")]
public abstract record HandleError;

public sealed record FailedToDeserialize(string Message) : HandleError;
public sealed record InvalidId(int Id) : HandleError;
```

Return options and results in your domain

```c#
public Result<int, HandleError> TryGetIdOnlyIfSeven(int id) => 
        id == 7 
            ? Handle.Ok(id) 
            : Handle.Err<int>(new InvalidId(id));
```

Use extension methods to operate on values in a functional way

```c#
var eight = TryGetIdOnlyIfSeven(7)
    .Inspect(seven => Console.WriteLine($"seven is {seven}"))
    .Map(seven => seven + 1)
    .UnwrapOrDefault(0);
```