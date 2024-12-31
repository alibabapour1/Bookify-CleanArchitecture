<div dir="rtl">

# Repositories

برای هر یک از موجودیت ها یک ریپازیتوری در پوشه مربوط به آن تعریف می شود که وظیفه انجام تراکنش های آن را برعهده دارد. علاوه بر این در پوشه Abstractions یک اینترفیس به نام IUnitOfWork تعریف شده که شامل متدی است که تمام تراکنش ها را انجام خواهد داد با نام SaveChanges.


```csharp
namespace Bookify.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChanges(CancellationToken cancellationToken = default);
    }
}
```

</div>