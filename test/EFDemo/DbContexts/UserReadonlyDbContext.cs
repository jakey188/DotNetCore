using Microsoft.EntityFrameworkCore;
using EFDemo.Data;
using DotNetCore.Data.EntityFrameworkCore.DbContexts;

namespace EFDemo
{
    public class UserReadonlyDbContext : DbContextBase
    {
        public UserReadonlyDbContext(DbContextOptions<UserReadonlyDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
