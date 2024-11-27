namespace Bookify.Application.Reviews.GetReviews;

public class ReviewResponse
{
    public Guid Id { get; set; }
    public Guid ReviewId { get; set; }
    public Guid BookingId { get; set; }
    public Guid ApartmentId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }


}