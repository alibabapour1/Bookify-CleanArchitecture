using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews;

public sealed record Rating
{
    private static readonly Error Invalid = new ("Review.Invalid", "The Given Review Rating Is Not Valid !");
    private Rating(int value)
    {
        Value = value;
    }
    public int Value { get; init; }

    public static Result<Rating> Create(int value)
    {
        if (value<1 || value> 5)
        {
           return Result.Failure<Rating>(Invalid);
        }

        return  new Rating(value);
    }
}