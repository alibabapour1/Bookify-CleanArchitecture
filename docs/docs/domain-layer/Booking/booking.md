<div dir="rtl">

# کلاس Booking

منحصر به رزرو آپارتمان بوده و یک سازنده private  دارد و موارد مورد نیاز برای رزرو یک مکان را دریافت میکند. 
همچنین این کلاس که بسیار خفن طراحی شده چون برای رزرو نیاز دارد که بداند کدام آپارتمان باید برای کدام کاربر رزرو شود آیدی های آن ها را نیز در بر میگیرد. 

همچنین شامل متد هایی برای رزرو، تایید رزرو، رد رزرو، کامل شدن فرآیند رزرو و کنسلی بوده  هرکدام یک زمان به عنوان ورودی دریافت میکنند و در هرکدام ایونت های مخصوص به آن ها نیز که در پوشه ایونت های Booking هستند raise می شوند.

-	متد های کلاس Booking

Confirm و Reject:

برای تایید یا رد رزروی بوده و  اگر وضعیت چیزی جز Reserved باشد یک Result.Failure با ارور Booking.NotReserved صدا زده خواهد شد.(result  و BookingErrors در بخش های بعدی توضیح داده شده است)
بعد از آن پراپرتی مخصوص آن زمان ورودی را دریافت کرده مثلا برای کانفرم ConfirmedOnUtc بوده و همچنین وضعیت نیز به Confirmed تغییر پیدا میکند.

Complete و Cancel:

اگر وضعیت چیزی جز Confirm باشد یعنی رزروی ما حتما باید قبلا تایید شده باشد که بتوان وضعیت را به کامل و یا کنسلی تغییر دهیم و در غیر این صورت یه result با ارور NotConfirmed برگشت داده خواهد. 
در متد کنسلی همچنین چک می شود که اگر زمان رزرو شروع شده باشد امکان کنسلی وجود نخواهد داشت و ارور AlreadyStarted برگشت داده خواهد شد.


```csharp
namespace Bookify.Domain.Booking
{
    public class Booking : Entity
    {
        private Booking(
            Guid id,
            Guid apartmentId,
            Guid userId,
            DateRange duration,
            Money priceForPeriod,
            Money cleaningFee,
            Money aminitiesChargeUp,
            Money totalPrice,
            BookingStatus status,
            DateTime createdOnUtc) 
            : base(id)
        {
            ApartmentId = apartmentId;
            UserId = userId;
            Duration = duration;
            PriceForPeriod = priceForPeriod;
            CleaningFee = cleaningFee;
            AmenitiesUpCharge = aminitiesChargeUp;
            TotalPrice = totalPrice;
            Status = status;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid ApartmentId { get; private set; }
        public Guid UserId { get; private set; }
        public DateRange Duration { get; private set; }
        public Money PriceForPeriod { get; private set; }
        public Money CleaningFee { get; private set; }
        public Money AmenitiesUpCharge { get; private set; }
        public Money TotalPrice { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ConfirmedOnUtc { get; private set; }
        public DateTime? RejectedOnUtc { get; private set; }
        public DateTime? CompletedOnUtc { get; private set; }
        public DateTime? CancelledOnUtc { get; private set; }

        public static Booking Reserve(
            Apartment apartment, 
            Guid userId, 
            DateRange duration, 
            DateTime utcNow, 
            PricingService pricingService)
        {
            var pricingDetails = pricingService.CalculatePrice(apartment, duration);

            var booking = new Booking(
                Guid.NewGuid(),
                apartment.Id,
                userId,
                duration,
                pricingDetails.PriceForPeriod,
                pricingDetails.CleaningFee,
                pricingDetails.AmenitiesChargeUp,
                pricingDetails.TotalPrice,
                BookingStatus.Reserved,
                utcNow);

            booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));

            apartment.LastBookedOnUtc = utcNow;

            return booking;
        }

        public Result Confirm(DateTime utcNow)
        {
            if (Status != BookingStatus.Reserved)
                return Result.Failure(BookingErrors.NotReserved);

            Status = BookingStatus.Confirmed;
            ConfirmedOnUtc = utcNow;

            RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));

            return Result.Success();
        }

        public Result Reject(DateTime utcNow)
        {
            if (Status != BookingStatus.Reserved)
                return Result.Failure(BookingErrors.NotReserved);

            Status = BookingStatus.Rejected;
            RejectedOnUtc = utcNow;

            RaiseDomainEvent(new BookingRejectedDomainEvent(Id));

            return Result.Success();
        }

        public Result Complete(DateTime utcNow)
        {
            if (Status != BookingStatus.Confirmed)
                return Result.Failure(BookingErrors.NotConfirmed);

            Status = BookingStatus.Completed;
            CompletedOnUtc = utcNow;

            RaiseDomainEvent(new BookingCompletedDomainEvent(Id));

            return Result.Success();
        }

        public Result Cancel(DateTime utcNow)
        {
            if (Status != BookingStatus.Confirmed)
                return Result.Failure(BookingErrors.NotConfirmed);

            var currentDate = DateOnly.FromDateTime(utcNow);
            if(currentDate > Duration.Start)
                return Result.Failure(BookingErrors.AlreadyStarted);

            Status = BookingStatus.Cancelled;
            CancelledOnUtc = utcNow;

            RaiseDomainEvent(new BookingCancelledDomainEvent(Id));

            return Result.Success();
        }
    }
}
```

</div>