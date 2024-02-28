
namespace DotNetCore.Data.EntityFrameworkCore.Repositories
{
    public class EfCoreRepository<TDbContext, TEntity, TKey>
        : EfCoreRepositoryBase<TEntity, TKey>, IEfCoreRepository<TDbContext, TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TDbContext : DbContext
    {
        internal override DbContext DbContext { get; set; }
        public override IUnitOfWork<TDbContext> UnitOfWork { get; }

        public EfCoreRepository(IUnitOfWork<TDbContext> unitOfWork)
        {
            DbContext = unitOfWork.DbContext;
            UnitOfWork = unitOfWork;
        }
    }

    public class EfCoreRepository<TEntity, TKey>
        : EfCoreRepositoryBase<TEntity, TKey>, IEfCoreRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>
    {
        internal override DbContext DbContext { get; set; }
        public override IUnitOfWork UnitOfWork { get; }

        public EfCoreRepository(IUnitOfWork unitOfWork)
        {
            DbContext = unitOfWork.DbContext;
            UnitOfWork = unitOfWork;
        }
    }
}
