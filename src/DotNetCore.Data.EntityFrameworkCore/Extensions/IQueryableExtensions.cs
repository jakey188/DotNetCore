
namespace DotNetCore.Data.EntityFrameworkCore.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<int> DeleteAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default)
        {
            return await query.ExecuteDeleteAsync(cancellationToken);
        }

        /// <summary>
        /// 更新多个属性
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="setPropertyCalls"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<int> UpdateAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default)
        {
            return await query.ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static async Task<Page<TEntity>> ToPageAsync<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            if (pageIndex < 1) throw new ArgumentOutOfRangeException(nameof(pageIndex), "页码从1开始");

            int startIndex = (pageIndex - 1) * pageSize;

            int count = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            var items = await query.Skip(startIndex)
                                   .Take(pageSize)
                                   .ToListAsync(cancellationToken)
                                   .ConfigureAwait(false);

            return new Page<TEntity>(items, pageIndex, pageSize, count);
        }
    }
}
