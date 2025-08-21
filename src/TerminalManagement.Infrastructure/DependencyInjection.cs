using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Infrastructure.Persistence;
using TerminalManagement.Infrastructure.Repositories;

namespace TerminalManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TerminalManagementDb");
            });

            services.AddScoped<ITerminalProfileRepository, TerminalProfileRepository>();
            services.AddScoped<IIssuerRepository, IssuerRepository>();
            services.AddScoped<IStatisticRepository, StatisticRepository>();

            return services;
        }
    }
}

