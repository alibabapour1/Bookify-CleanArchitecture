namespace Bookify.Domain.Reviews;

public interface IReviewRepository
{
    void AddReview(Review review);

    Task<List<Review>> GetApartmentAllReviewsAsync(Guid apartmentId);
}