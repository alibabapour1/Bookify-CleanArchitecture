<div dir="rtl">

# ولیوآبجکت Currency
<br>

در آبجکت ارز یا همون کارنسی ما سه تا ارز از پیش تعریف شده ایجاد کردیم و با استفاده از پترن Singleton-like pattern  اجازه ایجاد اینستنس ازین رکورد رو در خارج ازش نمیدیم به واسطه کانستراکتور پرایوتی که داره. 

همچنین یک Factory method  هم توش تعریف شده که اگر کد کارنسی رو بدید خودش رو تحویل میگیرید. 


```csharp
namespace Bookify.Domain.Shared
{
    public record Currency
    {
        internal readonly static Currency Usd = new("USD");
        public readonly static Currency Eur = new("EUR");
        public readonly static Currency None = new("");

        public string Code { get; init; }

        private Currency(string code) => Code = code;

        public static Currency FromCode(string code)
        {
            return All.FirstOrDefault(c => c.Code == code) ?? throw new ApplicationException("The currency code is invalid");
        }

        public readonly static IReadOnlyCollection<Currency> All = [Usd, Eur, None];
    }
}
```
<br>

![currency](/images/currency.png)


</div>