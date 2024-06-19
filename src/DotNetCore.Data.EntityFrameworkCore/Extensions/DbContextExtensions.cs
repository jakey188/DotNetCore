using DotNetCore.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.Data.EntityFrameworkCore
{
    public static class DbContextExtensions
    {
        public static IQueryable<TEntity> FromSqlRaw<TEntity>(this DbContext dbContext, string sql, params object[] parameters) where TEntity : class
        {
            return dbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public static IQueryable<TEntity> FromSql<TEntity>(this DbContext dbContext, FormattableString sql) where TEntity : class
        {
            return dbContext.Set<TEntity>().FromSql(sql);
        }

        public static IQueryable<TEntity> FromSqlInterpolated<TEntity>(this DbContext dbContext, FormattableString sql) where TEntity : class
        {
            return dbContext.Set<TEntity>().FromSqlInterpolated(sql);
        }

        public static async Task<List<T>> GetFromRawSqlAsync<T>(this DbContext dbContext, string sql, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            IEnumerable<object> parameters = new List<object>();

            List<T> items = await dbContext.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
            return items;
        }

        public static async Task<List<T>> GetFromRawSqlAsync<T>(this DbContext dbContext, string sql, object parameter, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            List<object> parameters = new List<object>() { parameter };
            List<T> items = await dbContext.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
            return items;
        }

        public static async Task<List<T>> GetFromRawSqlAsync<T>(this DbContext dbContext, string sql, IEnumerable<DbParameter> parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            List<T> items = await dbContext.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
            return items;
        }

        public static async Task<List<T>> GetFromRawSqlAsync<T>(this DbContext dbContext, string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            List<T> items = await dbContext.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
            return items;
        }

        private static async Task<List<T>> GetFromQueryAsync<T>(
            this DbContext dbContext,
            string sql,
            IEnumerable<object> parameters,
            CancellationToken cancellationToken = default)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            using DbCommand command = dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;

            if (parameters != null)
            {
                int index = 0;
                foreach (object item in parameters)
                {
                    DbParameter dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = "@p" + index;
                    dbParameter.Value = item;
                    command.Parameters.Add(dbParameter);
                    index++;
                }
            }

            try
            {
                await dbContext.Database.OpenConnectionAsync(cancellationToken);

                using DbDataReader result = await command.ExecuteReaderAsync(cancellationToken);

                List<T> list = new List<T>();
                T obj = default;
                while (await result.ReadAsync(cancellationToken))
                {
                    if (!(typeof(T).IsPrimitive || typeof(T).Equals(typeof(string))))
                    {
                        obj = Activator.CreateInstance<T>();
                        foreach (PropertyInfo prop in obj.GetType().GetProperties())
                        {
                            string propertyName = prop.Name;
                            bool isColumnExistent = result.ColumnExists(propertyName);
                            if (isColumnExistent)
                            {
                                object columnValue = result[propertyName];

                                if (!Equals(columnValue, DBNull.Value))
                                {
                                    prop.SetValue(obj, columnValue, null);
                                }
                            }
                        }

                        list.Add(obj);
                    }
                    else
                    {
                        obj = (T)Convert.ChangeType(result[0], typeof(T), CultureInfo.InvariantCulture);
                        list.Add(obj);
                    }
                }

                return list;
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
        }

        private static async Task<List<T>> GetFromQueryAsync<T>(
            this DbContext dbContext,
            string sql,
            IEnumerable<DbParameter> parameters,
            CancellationToken cancellationToken = default)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            using DbCommand command = dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;

            if (parameters != null)
            {
                foreach (DbParameter dbParameter in parameters)
                {
                    command.Parameters.Add(dbParameter);
                }
            }

            try
            {
                await dbContext.Database.OpenConnectionAsync(cancellationToken);

                using DbDataReader result = await command.ExecuteReaderAsync(cancellationToken);

                List<T> list = new List<T>();
                T obj = default;
                while (await result.ReadAsync(cancellationToken))
                {
                    if (!(typeof(T).IsPrimitive || typeof(T).Equals(typeof(string))))
                    {
                        obj = Activator.CreateInstance<T>();
                        foreach (PropertyInfo prop in obj.GetType().GetProperties())
                        {
                            string propertyName = prop.Name;
                            bool isColumnExistent = result.ColumnExists(propertyName);
                            if (isColumnExistent)
                            {
                                object columnValue = result[propertyName];

                                if (!Equals(columnValue, DBNull.Value))
                                {
                                    prop.SetValue(obj, columnValue, null);
                                }
                            }
                        }

                        list.Add(obj);
                    }
                    else
                    {
                        obj = (T)Convert.ChangeType(result[0], typeof(T), CultureInfo.InvariantCulture);
                        list.Add(obj);
                    }
                }

                return list;
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
        }
    }
}
