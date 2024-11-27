using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;

namespace Bookify.Application.Reviews.AddReview;

public record AddReviewCommand(Guid BookingId,Rating Rating,Comment Comment) : ICommand<Guid>;