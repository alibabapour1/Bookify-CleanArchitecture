namespace Bookify.Domain.Abstractions;

public record Error(string Code, string Value)
{
    public static Error None = new Error(string.Empty, string.Empty);
    public static Error NullValue = new Error("NullValue", "Null Value Was Provided !");

}