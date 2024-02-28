using DotNetCore.Data.EntityFrameworkCore.DbContexts;
using DotNetCore.Data.Models;
using System.Security.Principal;

namespace DotNetCore.Data.EntityFrameworkCore.Repositories
{
    public static class EfCoreRepositoryExtensions
    {
        //public static DbContext GetDbContext<TEntity>(this IEfCoreRepository<TEntity> repository) where TEntity : class
        //{
        //    return repository.ToEfCoreRepository().GetDbContext();
        //}


        //public static DbSet<TEntity> GetDbSet<TEntity>(this IEfCoreRepository<TEntity> repository) where TEntity : class
        //{
        //    return repository.ToEfCoreRepository().GetDbSet();
        //}


        //public static IEfCoreRepository<TEntity> ToEfCoreRepository<TEntity>(this IEfCoreRepository<TEntity> repository) where TEntity : class
        //{
        //    if (repository is IEfCoreRepository<TEntity> efCoreRepository)
        //    {
        //        return efCoreRepository;
        //    }

        //    throw new ArgumentException("当前对象不能空 " + typeof(IEfCoreRepository<TEntity>).AssemblyQualifiedName, nameof(repository));
        //}
    }
}
