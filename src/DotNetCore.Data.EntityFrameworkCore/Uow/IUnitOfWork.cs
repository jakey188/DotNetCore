namespace DotNetCore.Data.EntityFrameworkCore.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IDbContextTransaction BeginTransaction();
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
    }

    public interface IUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {

    }
}
