using Microsoft.EntityFrameworkCore;
using Tms.Application.Abstractions;
using Tms.Domain.Entities;

namespace Tms.Infrastructure.Services;

public class ApplicationPackageService : IApplicationPackageService
{
	private readonly TmsDbContext _dbContext;
	private readonly string _storageRoot;

	public ApplicationPackageService(TmsDbContext dbContext)
	{
		_dbContext = dbContext;
		_storageRoot = Path.Combine(AppContext.BaseDirectory, "storage", "apps");
		Directory.CreateDirectory(_storageRoot);
	}

	public async Task<ApplicationPackage> CreateAsync(ApplicationPackage package, Stream content, CancellationToken cancellationToken = default)
	{
		// Save content to disk
		var id = package.Id != Guid.Empty ? package.Id : Guid.NewGuid();
		package.Id = id;
		var fileName = string.IsNullOrWhiteSpace(package.FileName) ? $"{id}.bin" : package.FileName;
		var filePath = Path.Combine(_storageRoot, fileName);
		await using (var file = File.Create(filePath))
		{
			await content.CopyToAsync(file, cancellationToken);
		}
		var fileInfo = new FileInfo(filePath);
		package.FileName = fileInfo.Name;
		package.FileSizeBytes = fileInfo.Length;

		// Compute SHA-256
		fileInfo.Refresh();
		using var sha = System.Security.Cryptography.SHA256.Create();
		await using var fileStream = File.OpenRead(filePath);
		var hash = await sha.ComputeHashAsync(fileStream, cancellationToken);
		package.Sha256 = Convert.ToHexString(hash).ToLowerInvariant();

		_dbContext.ApplicationPackages.Add(package);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return package;
	}

	public async Task<ApplicationPackage?> GetAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _dbContext.ApplicationPackages.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
	}

	public async Task<IReadOnlyList<ApplicationPackage>> ListAsync(CancellationToken cancellationToken = default)
	{
		return await _dbContext.ApplicationPackages.AsNoTracking().OrderByDescending(a => a.UploadedAtUtc).ToListAsync(cancellationToken);
	}

	public async Task<(Stream Content, string ContentType, string FileName)?> GetContentAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var pkg = await _dbContext.ApplicationPackages.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
		if (pkg is null) return null;
		var filePath = Path.Combine(_storageRoot, pkg.FileName);
		if (!File.Exists(filePath)) return null;
		var stream = File.OpenRead(filePath);
		var contentType = string.IsNullOrWhiteSpace(pkg.ContentType) ? "application/octet-stream" : pkg.ContentType;
		return (stream, contentType, pkg.FileName);
	}
}