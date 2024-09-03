using Bookify.Domain.Apartments;
using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Repositories;

internal sealed class ApartmentRepository : BaseRepository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}