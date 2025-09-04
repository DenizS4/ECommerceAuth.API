using Microsoft.Extensions.DependencyInjection;

using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.DbContext;
using ECommerce.Infrastructure.Repositories;

namespace ECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<DapperDbContext>();
            return services;
        }
        
    }
}
