<div dir="rtl">

# ولیوآبجکت های کلاس Booking
<br><br>
PricingDetails
به جای اینکه چندین پراپرتی در کلاس برای قیمت های مختلف تعیین کنیم یک رکورد به عنوان ولیو آبجکت برایش تعریف میکنیم که تمام آن ها را شامل شود.


```csharp
namespace Bookify.Domain.Booking
{
    public record PricingDetails(
        Money PriceForPeriod,
        Money CleaningFee,
        Money AmenitiesChargeUp,
        Money TotalPrice);
}
```
<br>
DateRange
مخصوص نگهداری یک بازه زمانی بوده که برای ایجاد آن باید از کلاس Create آن استفاده کنیم که دو ورودی از جنس DateOnly دریافت می کند که اگر زمان شروع بزرگ تر یا بعد تر از پایان آن باشد اکسپشن پرتاب میکند و در غیر این صورت پراپرتی های Start و End را با آن پر میکند.

البته یکی دیگر از موارد مورد این رکورد محاسبه مقدار روزهای آن بازه زمانی در قالب یک پراپرتی پابلیک است که در زمان محاسبه قیمت به کار می رود.

```csharp
namespace Bookify.Domain.Booking
{
    public record DateRange
    {
        private DateRange()
        {

        }

        public DateOnly Start {  get; init; }
        public DateOnly End {  get; init; }

        public int LengthInDays => End.DayNumber - Start.DayNumber;

        public static DateRange Create(DateOnly start, DateOnly end)
        {
            if (start > end)
                throw new ApplicationException("End date precedes start date");

            return new DateRange
            {
                Start = start,
                End = end
            };
        }
    }
}
```
<br>

BookingStatus
برای نمایش وضعیت رزرو بوده که یک enum است.


```csharp
namespace Bookify.Domain.Booking
{
    public enum BookingStatus
    {
        Reserved = 1,
        Confirmed = 2,
        Rejected = 3,
        Cancelled = 4,
        Completed = 5
    }
}
```
<br>


</div>