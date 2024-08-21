using System.Diagnostics.CodeAnalysis;
using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Users;
using MediatR;

namespace Bookify.Application.Bookings.ReserveBooking;


internal sealed class ReserveBookingDomainEventHandler :INotificationHandler<BookingReservedDomainEvent>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _email;

   
    public ReserveBookingDomainEventHandler(IBookingRepository bookingRepository, IUserRepository userRepository, IEmailService email)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _email = email;
    }

    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await  _bookingRepository.GetBookingByIdAsync(notification.Id,cancellationToken);
        if (booking is null)
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(booking.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }

        await _email.SendEmailAsync(user.Email, "Booking Reserved !",
            "The Booking Has Been Reserved Successfully You Have To Confirm In 10 Minutes ");
    }
}