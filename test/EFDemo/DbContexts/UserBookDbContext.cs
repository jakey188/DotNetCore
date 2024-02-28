using Microsoft.EntityFrameworkCore;
using EFDemo.Data;
using EFDemo.Entities;
using DotNetCore.Data.EntityFrameworkCore.DbContexts;

namespace EFDemo
{
    public class UserBookDbContext : DbContextBase
    {
        public UserBookDbContext(DbContextOptions<UserBookDbContext> options) : base(options)
        {
        }

        public DbSet<UserBook> UserBook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 指定复合主键
            //modelBuilder.Entity<UserBook>().HasKey(x => new { x.UserId, x.BookId });

        }
    }
}
