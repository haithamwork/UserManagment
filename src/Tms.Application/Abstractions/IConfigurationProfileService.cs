using Tms.Domain.Entities;

namespace Tms.Application.Abstractions;

public interface IConfigurationProfileService
{
	Task<ConfigurationProfile> CreateAsync(ConfigurationProfile profile, CancellationToken cancellationToken = default);
	Task<ConfigurationProfile?> GetAsync(Guid id, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<ConfigurationProfile>> ListAsync(CancellationToken cancellationToken = default);
}