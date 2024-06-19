
using DotNetCore.Data.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace DotNetCore.Data.EntityFrameworkCore.UnitOfWorks
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        public UnitOfWork(IDbContextFactory dbContextFactory, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            DbContext = dbContextFactory.GetDefaultDbContext();
        }

        public override DbContext DbContext { get; }
    }

    public class UnitOfWork<TDbContext> : UnitOfWorkBase, IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        public UnitOfWork(IDbContextFactory dbContextFactory, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            DbContext = dbContextFactory.GetDbContext<TDbContext>();
        }

        public override DbContext DbContext { get; }
    }

    public abstract class UnitOfWorkBase : IUnitOfWork, IDisposable
    {
        private ILogger _logger;
        public UnitOfWorkBase(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UnitOfWorkBase>();
        }
        public abstract DbContext DbContext { get; }
        public int SaveChanges() => DbContext.SaveChanges();
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await DbContext.SaveChangesAsync(cancellationToken);
        public IDbContextTransaction? BeginOrUseTransaction(DbTransaction? dbTransaction = null) => DbContext.Database.BeginOrUseTransaction(dbTransaction);
        public async Task<IDbContextTransaction?> BeginOrUseTransactionAsync(DbTransaction? dbTransaction = null, CancellationToken cancellationToken = default) => await DbContext.Database.BeginOrUseTransactionAsync(dbTransaction, cancellationToken);
        public void Dispose()
        {
            _logger.LogDebug("{UnitOfWork}释放{DbContext}被销毁", nameof(UnitOfWorkBase), typeof(DbContext).Name);
            DbContext.Dispose();
        }
    }
}
