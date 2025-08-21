using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TerminalManagement.Domain.Entities;
using TerminalManagement.Infrastructure.Persistence;

namespace TerminalManagement.Web.Startup
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.EnsureCreatedAsync();

            if (await db.Issuers.AnyAsync())
            {
                return;
            }

            var issuer = new Issuer { Name = "Default Issuer" };
            var terminal = new TerminalProfile
            {
                TerminalId = "TERM-0001",
                Model = "TM-100",
                FirmwareVersion = "1.0.0",
                Issuer = issuer,
                ApnSettings = new List<ApnSetting>
                {
                    new ApnSetting { Apn = "internet", Username = "user", Password = "pass" }
                }
            };

            await db.Issuers.AddAsync(issuer);
            await db.TerminalProfiles.AddAsync(terminal);
            await db.SaveChangesAsync();
        }
    }
}

