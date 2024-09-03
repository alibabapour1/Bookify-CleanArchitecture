using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews;

public sealed record Rating
{
    private static readonly Error Inavlid = new ("Review.Invalid", "The Given Review Rating Is Not Valid !");
    private Rating(int value)
    {
        value = Value;
    }
    public int Value { get; init; }

    public static Result<Rating> Create(int value)
    {
        if (value<1 || value> 5)
        {
           return Result.Failure<Rating>(Inavlid);
        }

        return  new Rating(value);
    }
}