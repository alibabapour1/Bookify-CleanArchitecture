<div dir="rtl">

# کلاس Entity

با توجه به مبحث abstractions این کلاس قرار است به نوعی کلاس خام و مادر تمام موجودیت ها باشد تا از پیاده سازی موارد یکسان در کلاس ها جلوگیری کند. 

```csharp
namespace Bookify.Domain.Abstractions
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public Guid Id { get; init; }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return [.. _domainEvents]; //---> _domainEvents.ToList();
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
```

![entityClass](/images/entityClass.png)

</div>