using System.Data.SqlTypes;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews.Events;
using Bookify.Domain.Users;

namespace Bookify.Domain.Reviews;

public sealed class Review:Entity
{
    private Review(
        Guid id,
        Guid bookingId,
        Guid userId,
        Guid apartmentId,
        Rating rating,
        Comment comment,
        DateTime utcNow


        
        ) 
        : base(id)
    {
        BookingId = bookingId;
        UserId = userId;
        ApartmentId = apartmentId;
        Rating = rating;
        Comment = comment;
        CreatedDateUtc = utcNow;

    }

    public Guid BookingId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ApartmentId { get; private set; }
    public Rating Rating { get; private set; }
    public Comment Comment { get; private set; }
    public DateTime CreatedDateUtc { get; private set; }


    public static Result<Review> Create(Booking booking,Rating rating,Comment comment,DateTime utcNow)
    {
        if (booking.BookingStatus != BookingStatus.Completed)
        {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }

        var review = new Review(Guid.NewGuid(), booking.Id, booking.UserId, booking.ApartmentId, rating, comment, utcNow);

        review.RaiseDomainEvents(new ReviewCreatedDomainEvent(review.Id));

        return review;

    }

}