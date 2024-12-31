<div dir="rtl">

# Apartment repository

</div>

```csharp
namespace Bookify.Domain.Apartments
{
    public interface IApartmentRepository
    {
        Task<Apartment?> GetApartmentByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
```