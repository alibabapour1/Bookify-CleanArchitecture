<div dir="rtl">

# ایونت های کلاس Booking

هنگام رزرو

```csharp
namespace Bookify.Domain.Booking.Events
{
    public sealed record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent
    {
    }
}
```

هنگام رد شدن رزر

```csharp
namespace Bookify.Domain.Booking.Events
{
    public sealed record BookingRejectedDomainEvent(Guid Id) : IDomainEvent
    {
    }
}
```

هنگام تایید شدن رزرو

```csharp
namespace Bookify.Domain.Booking.Events
{
    public sealed record BookingConfirmedDomainEvent(Guid Id) : IDomainEvent
    {
    }
}
```

هنگام تایید کامل رزرو

```csharp
namespace Bookify.Domain.Booking.Events
{
    public record class BookingCompletedDomainEvent(Guid Id) : IDomainEvent
    {
    }
}
```

هنگام کنسلی رزرو

```csharp
namespace Bookify.Domain.Booking.Events
{
    public sealed record BookingCancelledDomainEvent(Guid Id) : IDomainEvent
    {
    }
}

```

<div dir="ltr">



</div>

</div>