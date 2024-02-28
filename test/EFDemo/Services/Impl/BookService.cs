using DotNetCore.Data.EntityFrameworkCore.Repositories;
using EFDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Services.Impl
{
    public class BookService : IBookService 
    {
        private readonly IEfCoreRepository<Book, int> _repository;
        public BookService(IEfCoreRepository<Book, int> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            var books = await _repository.Table.ToListAsync();
            return books.OrderByDescending(x => x.Id).ToList();
        }
    }
}
