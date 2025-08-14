using Microsoft.EntityFrameworkCore;
using Tms.Application.Abstractions;
using Tms.Domain.Entities;

namespace Tms.Infrastructure.Services;

public class TerminalService : ITerminalService
{
	private readonly TmsDbContext _dbContext;

	public TerminalService(TmsDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Terminal?> GetBySerialAsync(string serialNumber, CancellationToken cancellationToken = default)
	{
		return await _dbContext.Terminals.AsNoTracking().FirstOrDefaultAsync(t => t.SerialNumber == serialNumber, cancellationToken);
	}

	public async Task<Terminal> RegisterAsync(string serialNumber, string? model, string? merchantCode, CancellationToken cancellationToken = default)
	{
		var existing = await _dbContext.Terminals.FirstOrDefaultAsync(t => t.SerialNumber == serialNumber, cancellationToken);
		if (existing is not null)
		{
			existing.LastSeenAtUtc = DateTimeOffset.UtcNow;
			await _dbContext.SaveChangesAsync(cancellationToken);
			return existing;
		}

		var terminal = new Terminal
		{
			SerialNumber = serialNumber,
			Model = model,
			MerchantCode = merchantCode
		};
		_dbContext.Terminals.Add(terminal);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return terminal;
	}

	public async Task AssignProfileAsync(Guid terminalId, Guid profileId, CancellationToken cancellationToken = default)
	{
		var terminal = await _dbContext.Terminals.FirstOrDefaultAsync(t => t.Id == terminalId, cancellationToken)
			?? throw new InvalidOperationException("Terminal not found");
		var profileExists = await _dbContext.ConfigurationProfiles.AnyAsync(p => p.Id == profileId, cancellationToken);
		if (!profileExists) throw new InvalidOperationException("Profile not found");
		terminal.AssignedProfileId = profileId;
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task AssignApplicationAsync(Guid terminalId, Guid appPackageId, CancellationToken cancellationToken = default)
	{
		var terminal = await _dbContext.Terminals.FirstOrDefaultAsync(t => t.Id == terminalId, cancellationToken)
			?? throw new InvalidOperationException("Terminal not found");
		var appExists = await _dbContext.ApplicationPackages.AnyAsync(a => a.Id == appPackageId, cancellationToken);
		if (!appExists) throw new InvalidOperationException("Application package not found");
		terminal.AssignedAppPackageId = appPackageId;
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}