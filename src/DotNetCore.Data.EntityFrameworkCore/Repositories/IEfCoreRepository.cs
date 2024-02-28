namespace DotNetCore.Data.EntityFrameworkCore.Repositories
{
    public interface IEfCoreRepository<TDbContext, TEntity, TKey> : IEfCoreRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TDbContext : DbContext
    {
    }

    public interface IEfCoreRepository<TEntity, TKey> : IEfCoreRepository<TEntity>
        where TEntity : EntityBase<TKey>
    {
    }

    public interface IEfCoreRepository<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }
        DbContext CurrentDbContext { get; }
        DbSet<TEntity> GetDbSet();
        IQueryable<TEntity> Table { get; }
        Task<TEntity> InsertAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);
        Task<int> InsertManyAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateManyAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteManyAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<long> CountAsync(CancellationToken cancellationToken = default);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
