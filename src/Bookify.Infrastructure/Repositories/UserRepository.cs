using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Repositories;

internal sealed class UserRepository: BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }

    public override void Add(User entity)
    {
        foreach (var role in entity.Roles)
        {
            DbContext.Attach(role);
        }
        DbContext.Add(entity);
    }
}