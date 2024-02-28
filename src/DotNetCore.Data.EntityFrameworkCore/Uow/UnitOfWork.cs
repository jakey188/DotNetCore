
namespace DotNetCore.Data.EntityFrameworkCore.UnitOfWorks
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        public UnitOfWork(IDbContextFactory dbContextFactory)
        {
            DbContext = dbContextFactory.GetDefaultDbContext();
        }

        public override DbContext DbContext { get; }
    }

    public class UnitOfWork<TDbContext> : UnitOfWorkBase, IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        public UnitOfWork(IDbContextFactory dbContextFactory)
        {
            DbContext = dbContextFactory.GetDbContext<TDbContext>();
        }

        public override DbContext DbContext { get; }
    }

    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private bool _disposed = false;
        public abstract DbContext DbContext { get; }
        public int SaveChanges() => DbContext.SaveChanges();
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await DbContext.SaveChangesAsync(cancellationToken);
        public IDbContextTransaction BeginTransaction() => DbContext.Database.BeginTransaction();
        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel) => DbContext.Database.BeginTransaction(isolationLevel);
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => DbContext.Database.BeginTransactionAsync(cancellationToken);
        public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default) => DbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
