using Tms.Domain.Entities;

namespace Tms.Application.Abstractions;

public interface IApplicationPackageService
{
	Task<ApplicationPackage> CreateAsync(ApplicationPackage package, Stream content, CancellationToken cancellationToken = default);
	Task<ApplicationPackage?> GetAsync(Guid id, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<ApplicationPackage>> ListAsync(CancellationToken cancellationToken = default);
	Task<(Stream Content, string ContentType, string FileName)?> GetContentAsync(Guid id, CancellationToken cancellationToken = default);
}