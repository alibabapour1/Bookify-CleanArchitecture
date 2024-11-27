using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Reviews;

namespace Bookify.Application.Reviews.GetReviews;

public record GetReviewsQuery(Guid ApartmentId):IQuery<IReadOnlyList<ReviewResponse>>;