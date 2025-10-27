using Microsoft.EntityFrameworkCore;
using Tms.Domain.Entities;

namespace Tms.Infrastructure;

public class TmsDbContext : DbContext
{
	public TmsDbContext(DbContextOptions<TmsDbContext> options) : base(options)
	{
	}

	public DbSet<Terminal> Terminals => Set<Terminal>();
	public DbSet<ConfigurationProfile> ConfigurationProfiles => Set<ConfigurationProfile>();
	public DbSet<ApplicationPackage> ApplicationPackages => Set<ApplicationPackage>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Terminal>()
			.HasIndex(t => t.SerialNumber)
			.IsUnique();

		modelBuilder.Entity<ConfigurationProfile>()
			.Property(p => p.Name)
			.HasMaxLength(200);

		modelBuilder.Entity<ApplicationPackage>()
			.Property(a => a.Name)
			.HasMaxLength(200);
	}
}