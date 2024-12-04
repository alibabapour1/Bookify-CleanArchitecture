using Bookify.Domain.Reviews;

namespace Bookify.Infrastructure.Repositories;

internal sealed class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context) : base(context)
    {
    }


}