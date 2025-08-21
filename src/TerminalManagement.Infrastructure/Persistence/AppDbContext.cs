using Microsoft.EntityFrameworkCore;
using TerminalManagement.Domain.Entities;

namespace TerminalManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Issuer> Issuers => Set<Issuer>();
        public DbSet<TerminalProfile> TerminalProfiles => Set<TerminalProfile>();
        public DbSet<ApnSetting> ApnSettings => Set<ApnSetting>();
        public DbSet<TerminalStatistic> TerminalStatistics => Set<TerminalStatistic>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issuer>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired();
            });

            modelBuilder.Entity<TerminalProfile>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.TerminalId).IsRequired();
                b.HasIndex(x => x.TerminalId).IsUnique();
                b.HasOne(x => x.Issuer)
                    .WithMany(i => i.TerminalProfiles)
                    .HasForeignKey(x => x.IssuerId);
                b.HasMany(x => x.ApnSettings)
                    .WithOne(a => a.TerminalProfile!)
                    .HasForeignKey(a => a.TerminalProfileId);
                b.HasMany(x => x.Statistics)
                    .WithOne(s => s.TerminalProfile!)
                    .HasForeignKey(s => s.TerminalProfileId);
            });

            modelBuilder.Entity<ApnSetting>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Apn).IsRequired();
            });

            modelBuilder.Entity<TerminalStatistic>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Metric).IsRequired();
                b.Property(x => x.Value).IsRequired();
            });
        }
    }
}

