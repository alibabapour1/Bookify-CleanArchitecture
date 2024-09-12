namespace Bookify.Domain.Abstractions;

public record Error(string Code, string Value)
{
    public static Error None = new (string.Empty, string.Empty);
    public static Error NullValue = new("NullValue", "Null Value Was Provided!");

}