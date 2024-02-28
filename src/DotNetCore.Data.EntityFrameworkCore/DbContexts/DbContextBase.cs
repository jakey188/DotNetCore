namespace DotNetCore.Data.EntityFrameworkCore.DbContexts
{
    /// <summary>
    /// EntityFramework上下文基类
    /// </summary>
    public abstract class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions options) : base(options)
        {
        }
    }
}
