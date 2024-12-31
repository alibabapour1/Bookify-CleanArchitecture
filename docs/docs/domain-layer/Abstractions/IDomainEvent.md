<div dir="rtl">

# Events

یک اینترفیس IDomainEvent برای ایونت های مربوط به لایه دومین تعریف میکنیم که وظیفه هندل کردن دومین ها را برعهده دارد. 
این اینترفیس باید در پوشه Abstraction ایجاد شود.

همچنین این اینترفیس باید از اینترفیس INotification  مربوط به لایبرری MediatR نیز ارث بری کند تا بتواند بعد از raise شدن ایونت ها به سابسکرابر های آن اطلاع دهد.


```csharp
namespace Bookify.Domain.Users.Events
{
    public sealed record UserCreatedEvent(Guid UserId) : IDomainEvent
    {
    }
}
```

</div>