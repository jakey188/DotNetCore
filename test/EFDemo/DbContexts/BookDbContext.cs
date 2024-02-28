using Microsoft.EntityFrameworkCore;
using EFDemo.Data;
using DotNetCore.Data.EntityFrameworkCore.DbContexts;

namespace EFDemo
{
    public class BookDbContext : DbContextBase
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
