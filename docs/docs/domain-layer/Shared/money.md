<div dir="rtl">

# ولیوآبجکت Money
<br>

توی آبجکت Money  هم فقط یک آپریتور + داریم که مقادیر رو اگر کارنسی ها یکی باشن جمع میزنه و یک مانی برمیگردونه.

یک متد هم داریم که مقدار صفر رو بر میگردونه. 

همچنین یک متد IsZero() داریم که با استفاده از متد Zero(Currency) چک میکنه که ببینه اینستنس حال حاضر پول مقدارش خالیه یا نه.

*اینجا وقتی برای ایجاد مانی یک مقدار و یک کارنس میگیره در واقع اینطوریه که هر دو رو همزمان داریم و نیازی نیست دیگه اگر جایی قراره پراپرتی برای پول تعریف کنیم یک پراپرتی هم برای کارنسیش در نظر بگیریم. 

```csharp
namespace Bookify.Domain.Shared
{
    public record Money(decimal Amount, Currency Currency)
    {
        public static Money operator +(Money first, Money second)
        {
            if (first.Currency != second.Currency)
                throw new InvalidOperationException("Currencies have to be equal");

            return new Money(first.Amount + second.Amount, first.Currency);
        }

        public static Money Zero() => new(0, Currency.None);
        public static Money Zero(Currency currency) => new(0, currency);

        public bool IsZero() => this == Zero(Currency);
    }
}
```
<br>

![money](/images/money.png)



</div>