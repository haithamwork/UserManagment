using Microsoft.Extensions.DependencyInjection;
using UserManagment.Service.Interfaces;
using UserManagment.Service.Services;
namespace UserManagment.Service
{
    public static class ServiceModuleDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

            return services;
        }

    }
}
