using Microsoft.EntityFrameworkCore;
using TerminalMgmt.Domain.Entities;

namespace TerminalMgmt.Infrastructure.Db;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Terminal> Terminals => Set<Terminal>();
	public DbSet<TerminalProfile> TerminalProfiles => Set<TerminalProfile>();
	public DbSet<TerminalStatistic> TerminalStatistics => Set<TerminalStatistic>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Terminal>(e =>
		{
			e.HasIndex(x => x.SerialNumber).IsUnique();
			e.Property(x => x.SerialNumber).HasMaxLength(128);
		});
	}
}

