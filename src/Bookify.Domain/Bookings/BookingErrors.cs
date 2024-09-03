using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;

public static class BookingErrors
{
    public static Error NotFound = new("Booking.NotFound", "The Booking With The Specified Identifier Not Found. ");
    public static Error Overlap = new("Booking.Overlap", "The Booking Is Overlapping With An Existing One. ");
    public static Error NotReserved = new("Booking.NotReserved", "The Booking Is Not Pending. ");
    public static Error NotConfirmed = new("Booking.NotConfirmed", "The Booking Is Not Confirmed Yet. ");
    public static Error AlreadyStarted = new("Booking.AlreadyStarted", "The Booking Has Already Started. ");

}