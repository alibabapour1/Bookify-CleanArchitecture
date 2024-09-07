using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions
{
    public abstract class Entity : IEquatable<Entity>
    {
        private readonly List<IDomainEvents> _domainEvents = new();
        protected Entity(Guid EntityId)
        {
            EntityId = Id;
        }

        protected Entity()
        {
        }
        public Guid Id { get; set; }

      

        public bool Equals(Entity? other) => other?.Id == Id;

        public List<IDomainEvents> GetDomainEvents() => _domainEvents.ToList();


        public void ClearDomainEvents() => _domainEvents.Clear();


        protected void RaiseDomainEvents(IDomainEvents domainEvents) => _domainEvents.Add(domainEvents);


    }

    
}
