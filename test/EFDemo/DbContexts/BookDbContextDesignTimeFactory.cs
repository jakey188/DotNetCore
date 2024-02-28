using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFDemo.DbContexts
{
    public class BookDbContextDesignTimeFactory : IDesignTimeDbContextFactory<BookDbContext>
    {
        public BookDbContextDesignTimeFactory() { }
        public BookDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>();

            var conStr = "server=localhost;Database=BookDb;Uid=root;Pwd=123456;charset=utf8";

            optionsBuilder.UseMySql(conStr, ServerVersion.AutoDetect(conStr));

            return new BookDbContext(optionsBuilder.Options);
        }
    }
}
