<div dir="rtl">

# سرویس PricingService

این سرویس که مخصوص محاسبه ها مبالغ موجود در مجودیت Booking نوشته شده بسیار زیبا اومده DDD رو پیاده سازی کرده و در آخر یک PricingDetails برگشت می دهد.

در این سرویس متد CalculatePrice  دو رودی یکی یک شی از آپارتمان و دیگری یک DateRange دریافت میکند. سپس با توجه به روز های موجود در بازه زمانی قیمت خام رزرو مکان را محاسبه میکند.

سپس با توجه به amenity های مکان یک ضریب افزایش قیمت بر اساس آپشن های آن محاسبه کرده و قیمت اضافی را نیز به دست می آورد.

در آخر نیز با جمع زدن همه قیمت ها یک totalPrice به دست آورده و تمامی مبالغ را به صورت PricingDetails برگشت می دهد.


```csharp
namespace Bookify.Domain.Booking
{
    public class PricingService
    {
        public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
        {
            var currency = apartment.Price.Currency;

            var priceForPeriod = new Money(apartment.Price.Amount * period.LengthInDays, currency);

            decimal percentageChargeUp = 0;
            foreach (var amenity in apartment.Amenities)
            {
                percentageChargeUp += amenity switch
                {
                    Amenity.GardenView or Amenity.MountainView => 0.05m,
                    Amenity.AirConditioning => 0.01m,
                    Amenity.Parking => 0.01m,
                    _ => 0
                };
            }

            var amenitiesChargeUp = Money.Zero(currency);
            if (percentageChargeUp > 0)
            {
                amenitiesChargeUp = new Money(priceForPeriod.Amount * percentageChargeUp, currency);
            }

            var totalPrice = Money.Zero(currency);
            totalPrice += priceForPeriod;

            if (!apartment.CleaningFee.IsZero())
            {
                totalPrice += apartment.CleaningFee;
            }

            totalPrice += amenitiesChargeUp;

            return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenitiesChargeUp, totalPrice);

        }
    }
}
```
<br>

![pricingDetails](/images/pricingDetails.png)

</div>