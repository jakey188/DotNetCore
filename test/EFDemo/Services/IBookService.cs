using DotNetCore.DependencyInjection;
using EFDemo.Data;

namespace EFDemo.Services.Impl
{
    public interface IBookService : IScopedDependency
    {
        Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken);
    }
}
