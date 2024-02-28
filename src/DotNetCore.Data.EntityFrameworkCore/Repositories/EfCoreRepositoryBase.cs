namespace DotNetCore.Data.EntityFrameworkCore.Repositories
{
    public abstract class EfCoreRepositoryBase<TEntity, TKey> : IEfCoreRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        internal abstract DbContext DbContext { get; set; }
        protected IDbConnection GetDbConnection() => DbContext.Database.GetDbConnection();

        protected IDbTransaction? GetDbTransaction() => DbContext.Database.CurrentTransaction?.GetDbTransaction();

        public IQueryable<TEntity> Table => GetDbSet();

        public DbSet<TEntity> GetDbSet() => DbContext.Set<TEntity>();

        public DbContext CurrentDbContext => DbContext;

        public abstract IUnitOfWork UnitOfWork { get; }

        public virtual async Task<TEntity> InsertAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbContext.AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> InsertManyAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
           await DbContext.AddRangeAsync(entities, cancellationToken);
           return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
        {
            GetDbSet().Update(entity);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateManyAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            GetDbSet().UpdateRange(entities);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync([NotNull] TEntity entity,CancellationToken cancellationToken = default)
        {
            GetDbSet().RemoveRange(entity);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteManyAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            GetDbSet().RemoveRange(entities);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => await Table.FirstOrDefaultAsync(predicate, cancellationToken);

        public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default) => await Table.AnyAsync(cancellationToken);

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => await Table.AnyAsync(predicate, cancellationToken);

        public virtual async Task<long> CountAsync(CancellationToken cancellationToken = default) => await Table.LongCountAsync(cancellationToken);

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => await Table.LongCountAsync(predicate, cancellationToken);
    }
}
