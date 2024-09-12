namespace Bookify.Domain.Shared;

public record Currency
{
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    internal static readonly Currency None = new("None");
    public string Code { get; init; }

    private Currency(string code) => Code = code;

    public static readonly IReadOnlyCollection<Currency> All = new[] { Usd, Eur };

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(p => p.Code == code) ??
               throw new ArgumentException("The Currency Code You Entered Is Not Valid ! ");
    }
    
}   

