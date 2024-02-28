using DotNetCore.Data.EntityFrameworkCore.DbContexts;
using DotNetCore.Data.EntityFrameworkCore.Repositories;
using DotNetCore.Data.EntityFrameworkCore.UnitOfWorks;
using DotNetCore.DependencyInjection;
using EFDemo.Data;

namespace EFDemo
{
    public class UserRepository : EfCoreRepository<UserDbContext, User,string>, IUserRepository
    {
        public UserRepository(IUnitOfWork<UserDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<User> InsertAsync(User entity, CancellationToken cancellationToken = default)
        {
            entity.Id = Guid.NewGuid().ToString();
            return base.InsertAsync(entity, cancellationToken);
        }

        public  User Update(User entity)
        {
            entity.Name += $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            return entity;
        }
    }

    public interface IUserRepository : IEfCoreRepository<UserDbContext, User,string> , IScopedDependency
    {
    }
}
