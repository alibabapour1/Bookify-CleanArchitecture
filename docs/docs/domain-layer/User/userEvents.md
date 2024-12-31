<div dir="rtl">

# Eventهای کلاس کاربر


<div dir="ltr">

UserCreatedEvent

```csharp
namespace Bookify.Domain.Users.Events
{
    public sealed record UserCreatedEvent(Guid UserId) : IDomainEvent
    {
    }
}
```

</div>


</div>