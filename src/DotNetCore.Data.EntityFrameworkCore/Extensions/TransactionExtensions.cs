using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCore.Data.EntityFrameworkCore
{
    public static class TransactionExtensions
    {
        /// <summary>
        /// 执行事务,支持嵌套事务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="func">逻辑委托</param>
        /// <param name="dbTransaction">外部事务</param>
        /// <remarks>优先使用传入事务,也可使用DbContenxt当前事务</remarks>
        /// <returns>执行结果</returns>
        public static TResult Transaction<TResult>(this IUnitOfWork unitOfWork, Func<TResult> func, DbTransaction? dbTransaction = null)
        {
            return unitOfWork.DbContext.Database.Execute(func, dbTransaction);
        }

        /// <summary>
        /// 执行事务,支持嵌套事务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="func">逻辑委托</param>
        /// <param name="dbTransaction">外部事务</param>
        /// <remarks>优先使用传入事务,也可使用DbContenxt当前事务</remarks>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>执行结果</returns>
        public static async Task<TResult> TransactionAsync<TResult>(this IUnitOfWork unitOfWork, Func<Task<TResult>> func, DbTransaction? dbTransaction = null, CancellationToken cancellationToken = default)
        {
            return await unitOfWork.DbContext.Database.ExecuteAsync(func, dbTransaction, cancellationToken);
        }

        /// <summary>
        /// 执行事务,支持嵌套事务
        /// </summary>
        /// <param name="context">DB上下文</param>
        /// <param name="func">逻辑委托</param>
        /// <param name="dbTransaction">外部事务</param>
        /// <remarks>优先使用传入事务,也可使用DbContenxt当前事务</remarks>
        /// <returns>执行结果</returns>
        public static TResult Transaction<TResult>(this DbContext context, Func<TResult> func, DbTransaction? dbTransaction = null)
        {
            return context.Database.Execute(func, dbTransaction);
        }

        /// <summary>
        /// 执行事务,支持嵌套事务
        /// </summary>
        /// <param name="context">DB上下文</param>
        /// <param name="func">逻辑委托</param>
        /// <param name="dbTransaction">外部事务</param>
        /// <remarks>优先使用传入事务,也可使用DbContenxt当前事务</remarks>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>执行结果</returns>
        public static async Task<TResult> TransactionAsync<TResult>(this DbContext context, Func<Task<TResult>> func, DbTransaction? dbTransaction = null, CancellationToken cancellationToken = default)
        {
            return await context.Database.ExecuteAsync(func, dbTransaction, cancellationToken);
        }

        /// <summary>
        /// 执行事务,支持嵌套事务
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="database"></param>
        /// <param name="func"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<TResult> ExecuteAsync<TResult>(this DatabaseFacade database, Func<Task<TResult>> func, DbTransaction? dbTransaction = null, CancellationToken cancellationToken = default)
        {
            using var transaction = await database.BeginOrUseTransactionAsync(dbTransaction, cancellationToken);
            try
            {
                var result = await func();
                await transaction?.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction?.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// 执行事务,支持嵌套事务
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="database"></param>
        /// <param name="func"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        private static TResult Execute<TResult>(this DatabaseFacade database, Func<TResult> func, DbTransaction? dbTransaction = null)
        {
            using var transaction = database.BeginOrUseTransaction(dbTransaction);
            try
            {
                var result = func();
                transaction?.Commit();
                return result;
            }
            catch (Exception)
            {
                transaction?.Rollback();
                throw;
            }
        }
    }
}
