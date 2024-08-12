using System.Runtime.CompilerServices;

namespace Bookify.Domain.Apartments;

public record Money(decimal Amount , Currency Currency)
{
    public static Money operator +(Money First, Money Second)
    {
        if (First.Currency != Second.Currency)
        {
            throw new ApplicationException("The Currencies are not the same ");
        }

        return new Money(First.Amount + Second.Amount,First.Currency);
    }

    public static Money Zero() => new(0, Currency.None);
    public static Money Zero(Currency currency) => new(0, currency);

    public bool IsZero() => this == Zero(Currency);

}