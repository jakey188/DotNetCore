using Dapper;
using DotNetCore.Data.EntityFrameworkCore;
using EFDemo.DbContexts;
using EFDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFDemo
{
    public static class SeedDataExtensions
    {
        public static void InitSeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var book_db = scope.ServiceProvider.GetRequiredService<BookDbContext>();

           
            InitBookTable(app);
            InitUserTable(app);
            InitUserBookTable(app);
        }

        private static void InitBookTable(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var book_db = scope.ServiceProvider.GetRequiredService<BookDbContext>();

            book_db.Database.EnsureCreated();

            book_db.Database.Migrate();

            if(!book_db.Book.Any())
            {
                book_db.Book.Add(new Data.Book { Name = "C#" });
                book_db.SaveChanges();
            }
        }

        private static void InitUserTable(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var user_db = scope.ServiceProvider.GetRequiredService<UserDbContext>();

            user_db.Database.EnsureCreated();

            user_db.Database.Migrate();

            if (!user_db.User.Any())
            {
                user_db.User.Add(new Data.User { Id = "u1", Name = "张三" });
                user_db.SaveChanges();
            }
        }

        private static void InitUserBookTable(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var user_book_db = scope.ServiceProvider.GetRequiredService<UserBookDbContext>();

            user_book_db.Database.EnsureCreated();

            user_book_db.Database.Migrate();

            if (!user_book_db.UserBook.Any())
            {
                user_book_db.UserBook.Add(new UserBook { UserId ="u1" , Name = "张三",BookId = 1 });
                user_book_db.SaveChanges();
            }
        }
    }
}
