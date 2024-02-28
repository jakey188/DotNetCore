using DotNetCore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore
{
    public static class ProgramExtensions
    {
        public static IServiceCollection AddAutoDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<DependencyPack>();
            using var scope = services.BuildServiceProvider().CreateScope();
            var pack = scope.ServiceProvider.GetRequiredService<DependencyPack>();
            return pack.AddServices(services);
        }
    }
}
