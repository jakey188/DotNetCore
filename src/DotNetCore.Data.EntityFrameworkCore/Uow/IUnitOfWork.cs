using System.Data.Common;

namespace DotNetCore.Data.EntityFrameworkCore.UnitOfWorks
{
    public interface IUnitOfWork 
    {
        DbContext DbContext { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IDbContextTransaction? BeginOrUseTransaction(DbTransaction? dbTransaction = null);
        Task<IDbContextTransaction?> BeginOrUseTransactionAsync(DbTransaction? dbTransaction = null,CancellationToken cancellationToken = default);
    }

    public interface IUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {

    }
}
