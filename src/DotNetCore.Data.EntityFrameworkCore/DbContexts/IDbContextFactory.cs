
namespace DotNetCore.Data.EntityFrameworkCore.DbContexts
{
    public interface IDbContextFactory
    {
        DbContext GetDbContext<TDbContext>() where TDbContext : DbContext;
        DbContext GetDefaultDbContext();
    }
}