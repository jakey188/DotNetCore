namespace DotNetCore.Data.Dapper
{
    public class DapperRepository<TDbContext> : IDapperRepository where TDbContext : DbContext
    {
        private readonly IDbContextFactory _dbContextFactory;
        public DapperRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IDbConnection DbConnection => _dbContextFactory.GetDbContext<TDbContext>().Database.GetDbConnection();

        public IDbTransaction? DbTransaction => _dbContextFactory.GetDbContext<TDbContext>().Database.CurrentTransaction?.GetDbTransaction();
    }
}
