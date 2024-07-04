using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UserManagment.Application
{
    public static class ApplicationModuleDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            //Configuration of Mediator 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //Configuration Of AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Get Validators
           
            return services;
        }
    }
}
