using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{


}