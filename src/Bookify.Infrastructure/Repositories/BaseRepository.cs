using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal abstract class BaseRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected BaseRepository(ApplicationDbContext context)
    {
        DbContext = context;
    }

    public async Task<T?> GetByIdAsync(Guid id ,CancellationToken cancellationToken)
    {
        return await DbContext.Set<T>()
            .FirstOrDefaultAsync(entity => entity.Id ==id,cancellationToken);
    }

    public void Add(T entity)
    {
        DbContext.Add(entity); 
        //I didn't use AddAsync method because this is an inMemory operation the persisting will happen when we call SaveChangesAsync method
    }
}