using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.Data.EntityFrameworkCore
{
    public static class DatabaseFacadeExtensions
    {
        /// <summary>
        /// 获取或使用当前事务,支持嵌套事务
        /// </summary>
        /// <param name="database">上下文databse</param>
        /// <param name="dbTransaction">外部事务</param>
        /// <remarks>
        /// 优先使用传入事务,也可使用DbContenxt当前事务
        /// </remarks>
        /// <returns></returns>
        public static IDbContextTransaction? BeginOrUseTransaction(this DatabaseFacade database, DbTransaction? dbTransaction = null)
        {
            if (dbTransaction != null)
            {
                return database.UseTransaction(dbTransaction);
            }
            if (database.CurrentTransaction != null && database.CurrentTransaction.GetDbTransaction() != null)
            {
                return database.UseTransaction(database.CurrentTransaction.GetDbTransaction());
            }
            return database.BeginTransaction();
        }

        /// <summary>
        /// 获取或使用当前事务,支持嵌套事务
        /// </summary>
        /// <param name="database">上下文databse</param>
        /// <param name="dbTransaction">外部事务</param>
        /// <remarks>
        /// 优先使用传入事务,也可使用DbContenxt当前事务
        /// </remarks>
        /// <returns></returns>
        /// <returns></returns>
        public static async Task<IDbContextTransaction?> BeginOrUseTransactionAsync(this DatabaseFacade database, DbTransaction? dbTransaction = null, CancellationToken cancellationToken = default)
        {
            if (dbTransaction != null)
            {
                return await database.UseTransactionAsync(dbTransaction, cancellationToken);
            }
            if (database.CurrentTransaction != null && database.CurrentTransaction.GetDbTransaction() != null)
            {
                return await database.UseTransactionAsync(database.CurrentTransaction.GetDbTransaction(), cancellationToken);
            }
            return await database.BeginTransactionAsync(cancellationToken);
        }
    }
}
