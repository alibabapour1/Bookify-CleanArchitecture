using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal sealed class BookingRepository :BaseRepository<Booking>, IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatus =
    [
        BookingStatus.Confirmed,
        BookingStatus.Completed,
        BookingStatus.Reserved
    ];
    public BookingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken )
    {
        return await DbContext.Set<Booking>()
            .AnyAsync(booking =>

                    booking.ApartmentId == apartment.Id &&
                    booking.Duration.Start <= duration.End &&
                    booking.Duration.End >= duration.Start &&
                    ActiveBookingStatus.Contains(booking.BookingStatus),
                    cancellationToken);
    }
}