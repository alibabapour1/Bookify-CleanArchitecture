using Bookify.Domain.Abstractions;
using MediatR;

namespace Bookify.Domain.Users.Events;

public record UserCreatedDomainEvent(Guid UserId) : IDomainEvents;