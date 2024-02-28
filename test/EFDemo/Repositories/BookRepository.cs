using DotNetCore.Data.EntityFrameworkCore.DbContexts;
using DotNetCore.Data.EntityFrameworkCore.Repositories;
using DotNetCore.Data.EntityFrameworkCore.UnitOfWorks;
using DotNetCore.DependencyInjection;
using EFDemo.Data;

namespace EFDemo
{
    public class BookRepository : EfCoreRepository<Book,int>, IBookRepository
    {
        public BookRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Book Update(Book entity)
        {
            entity.Name += $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            return entity;
        }

    }

    public interface IBookRepository : IEfCoreRepository<Book, int> , IScopedDependency
    {
    }
}
