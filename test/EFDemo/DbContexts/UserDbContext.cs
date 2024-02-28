using Microsoft.EntityFrameworkCore;
using EFDemo.Data;
using DotNetCore.Data.EntityFrameworkCore.DbContexts;

namespace EFDemo
{
    public class UserDbContext : DbContextBase
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
