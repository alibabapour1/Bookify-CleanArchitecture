using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;

namespace Bookify.Application.Reviews.AddReview;

public class AddReviewCommandHandler:ICommandHandler<AddReviewCommand,Guid>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBookingRepository _bookRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AddReviewCommandHandler(IDateTimeProvider dateTimeProvider, IBookingRepository bookRepository, IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
    {
        _dateTimeProvider = dateTimeProvider;
        _bookRepository = bookRepository;
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookRepository.GetByIdAsync(request.BookingId, cancellationToken);
        if (booking == null)
        {
            return Result.Failure<Guid>(BookingErrors.NotFound);
        } 
            
        var review = Review.Create(booking, request.Rating, request.Comment, _dateTimeProvider.UtcNow);

        _reviewRepository.AddReview(review.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return review.Value.Id;
    }
}