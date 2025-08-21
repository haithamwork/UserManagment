using Microsoft.EntityFrameworkCore;
using TerminalMgmt.Domain.Entities;
using TerminalMgmt.Infrastructure.Db;
using TerminalMgmt.Application.Terminals;

namespace TerminalMgmt.Infrastructure.Repositories;

public class TerminalRepository : ITerminalRepository
{
	private readonly AppDbContext _db;
	public TerminalRepository(AppDbContext db) => _db = db;

	public Task<Terminal?> GetBySerialAsync(string serialNumber, CancellationToken ct)
		=> _db.Terminals.Include(t => t.Profile).FirstOrDefaultAsync(t => t.SerialNumber == serialNumber, ct);

	public Task<Terminal?> GetByIdAsync(Guid id, CancellationToken ct)
		=> _db.Terminals.Include(t => t.Profile).FirstOrDefaultAsync(t => t.Id == id, ct);

	public async Task<IReadOnlyList<Terminal>> ListAsync(CancellationToken ct)
		=> await _db.Terminals.Include(t => t.Profile).AsNoTracking().ToListAsync(ct);

	public async Task<Terminal> AddAsync(Terminal terminal, CancellationToken ct)
	{
		_db.Terminals.Add(terminal);
		await _db.SaveChangesAsync(ct);
		return terminal;
	}

	public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}

public class TerminalProfileRepository : ITerminalProfileRepository
{
	private readonly AppDbContext _db;
	public TerminalProfileRepository(AppDbContext db) => _db = db;
	public Task<TerminalProfile?> GetByIdAsync(Guid id, CancellationToken ct)
		=> _db.TerminalProfiles.FirstOrDefaultAsync(p => p.Id == id, ct);
}

public class StatisticsRepository : IStatisticsRepository
{
	private readonly AppDbContext _db;
	public StatisticsRepository(AppDbContext db) => _db = db;
	public async Task AddAsync(TerminalStatistic stat, CancellationToken ct)
		=> await _db.TerminalStatistics.AddAsync(stat, ct);
	public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}

