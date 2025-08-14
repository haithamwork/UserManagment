using Microsoft.EntityFrameworkCore;
using Tms.Application.Abstractions;
using Tms.Domain.Entities;

namespace Tms.Infrastructure.Services;

public class ConfigurationProfileService : IConfigurationProfileService
{
	private readonly TmsDbContext _dbContext;

	public ConfigurationProfileService(TmsDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<ConfigurationProfile> CreateAsync(ConfigurationProfile profile, CancellationToken cancellationToken = default)
	{
		_dbContext.ConfigurationProfiles.Add(profile);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return profile;
	}

	public async Task<ConfigurationProfile?> GetAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _dbContext.ConfigurationProfiles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
	}

	public async Task<IReadOnlyList<ConfigurationProfile>> ListAsync(CancellationToken cancellationToken = default)
	{
		return await _dbContext.ConfigurationProfiles.AsNoTracking().OrderBy(p => p.Name).ToListAsync(cancellationToken);
	}
}