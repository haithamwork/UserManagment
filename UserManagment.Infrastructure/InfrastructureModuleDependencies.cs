using Microsoft.Extensions.DependencyInjection;
using UserManagment.Infrastructure.Generic;
using UserManagment.Infrastructure.Interfaces;
using UserManagment.Infrastructure.Repositories;
namespace UserManagment.Infrastructure
{
    public static class InfrastructureModuleDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
