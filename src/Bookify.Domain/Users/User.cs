﻿using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users
{
    public sealed class User:Entity
    {
        private readonly List<Role> _roles= new();
        private User(Guid id, Email email,FirstName firstName, LastName lastName) : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;

        }

        private User()
        {
        }
        public FirstName FirstName { get; private set; } 
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public string IdentityId { get;private set; } = string.Empty;
        public IReadOnlyCollection<Role> Roles => _roles.ToList();

        // benefits of this approach is 1.Encapsulation 2. Hiding the constructor implementations 3. implementing domain Events 
        public static User Create( FirstName firstName, LastName lastName, Email email)
        {
            var user = new User(Guid.NewGuid(),email, firstName, lastName);
            user.RaiseDomainEvents(new UserCreatedDomainEvent(user.Id) );
            
            user._roles.Add(Role.Registered);
            
            return user;
        }

        public void SetIdentityId(string identity) => IdentityId = identity;

    }
}
