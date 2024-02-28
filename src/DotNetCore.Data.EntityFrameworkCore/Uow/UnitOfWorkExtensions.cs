namespace DotNetCore.Data.EntityFrameworkCore.Uow
{
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// 创建仓储对象
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="unitOfWork"></param>
        /// <remarks>特殊需求,不推荐使用</remarks>
        /// <returns></returns>
        public static IEfCoreRepository<TDbContext, TEntity, TKey> GetGenericRepository<TDbContext, TEntity, TKey>(this IUnitOfWork<TDbContext> unitOfWork)
            where TEntity : EntityBase<TKey>
            where TDbContext : DbContext
        {
            return new EfCoreRepository<TDbContext, TEntity, TKey>(unitOfWork);
        }

        /// <summary>
        /// 创建仓储对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="unitOfWork"></param>
        /// <remarks>特殊需求,不推荐使用</remarks>
        /// <returns></returns>
        public static IEfCoreRepository<TEntity, TKey> GetGenericRepository<TEntity, TKey>(this IUnitOfWork unitOfWork)
            where TEntity : EntityBase<TKey>
        {
            return new EfCoreRepository<TEntity, TKey>(unitOfWork);
        }
    }
}
