namespace DotNetCore.Data.EntityFrameworkCore.DbContexts
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IEnumerable<DbContext> _dbContexts;
        public DbContextFactory(IEnumerable<DbContext> dbContexts)
        {
            _dbContexts = dbContexts;
        }

        public DbContext GetDefaultDbContext()
        {
            var dbContext = _dbContexts.FirstOrDefault();
            if (dbContext == null)
                throw new ArgumentNullException($"DbContext获取失败,请检查是否已经注册到容器内。");
            return dbContext;
        }

        public DbContext GetDbContext<TDbContext>() where TDbContext : DbContext
        {
            var dbContext = _dbContexts.FirstOrDefault(x => x.GetType() == typeof(TDbContext));
            if (dbContext == null)
                throw new ArgumentNullException(typeof(TDbContext).Name, $"{typeof(TDbContext).Name}获取失败,请检查是否已经注册到容器内。");
            return dbContext;
        }
    }
}
