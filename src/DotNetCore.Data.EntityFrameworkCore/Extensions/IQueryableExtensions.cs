
using DotNetCore.Data.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Microsoft.EntityFrameworkCore.Query;

namespace DotNetCore.Data.EntityFrameworkCore.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="condition">true时生效</param>
        /// <param name="predicate">lambda表达式</param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, bool>> predicate)
        {
            if (condition == false || predicate == null) return query;
            return query.Where(predicate);
        }

        /// <summary>
        /// 导航条件
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="navigationPropertyPath"></param>
        /// <returns></returns>
        public static IIncludableQueryable<TEntity, TProperty> IncludeIf<TEntity, TProperty>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
        {
            if (condition == false || navigationPropertyPath == null) return (IIncludableQueryable<TEntity, TProperty>)query;
            return query.Include(navigationPropertyPath);
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition">true时生效</param>
        /// <param name="keySelector">排序字段</param>
        /// <param name="descending">true:升序,false:降序</param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderByIf<TEntity, TKey>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, TKey>> keySelector, bool descending)
        {
            if (condition == false || keySelector == null) return query;
            if (descending) 
                query.OrderBy(keySelector);
            else
                query.OrderByDescending(keySelector);
            return query;
        }

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
