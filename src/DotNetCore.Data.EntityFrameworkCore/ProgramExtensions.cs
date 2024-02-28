
namespace DotNetCore.Data.EntityFrameworkCore
{
    public static class ProgramExtensions
    {
        public static IServiceCollection AddRepository<TDbContext>(this IServiceCollection services,
           Action<DbContextOptionsBuilder> options, bool dbContextPool = false , ServiceLifetime repositoryLifetime = ServiceLifetime.Scoped) where TDbContext : DbContextBase
        {
            services.AddScoped<DbContext, TDbContext>();
            if (dbContextPool)
            {
                services.AddDbContextPool<TDbContext>(options);
            }
            else
            {
                services.AddDbContext<TDbContext>(options);
            }
            services.AddRepository(repositoryLifetime);
            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            services.AddScoped<IDbContextFactory, DbContextFactory>();
            
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            services.AddTransient(typeof(IEfCoreRepository<,>), typeof(EfCoreRepository<,>));
            services.AddTransient(typeof(IEfCoreRepository<,,>), typeof(EfCoreRepository<,,>));
            return services;
        }
    }
}
