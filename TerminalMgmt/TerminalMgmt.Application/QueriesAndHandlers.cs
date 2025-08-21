using MediatR;
using Microsoft.Extensions.Logging;
using TerminalMgmt.Domain.Entities;

namespace TerminalMgmt.Application.Terminals;

public interface ITerminalRepository
{
	Task<Terminal?> GetBySerialAsync(string serialNumber, CancellationToken ct);
	Task<Terminal?> GetByIdAsync(Guid id, CancellationToken ct);
	Task<IReadOnlyList<Terminal>> ListAsync(CancellationToken ct);
	Task<Terminal> AddAsync(Terminal terminal, CancellationToken ct);
	Task SaveChangesAsync(CancellationToken ct);
}

public interface ITerminalProfileRepository
{
	Task<TerminalProfile?> GetByIdAsync(Guid id, CancellationToken ct);
}

public interface IStatisticsRepository
{
	Task AddAsync(TerminalStatistic stat, CancellationToken ct);
	Task SaveChangesAsync(CancellationToken ct);
}

public class GetTerminalBySerialHandler : IRequestHandler<GetTerminalBySerialQuery, Terminal?>
{
	private readonly ITerminalRepository _repo;
	public GetTerminalBySerialHandler(ITerminalRepository repo) => _repo = repo;
	public Task<Terminal?> Handle(GetTerminalBySerialQuery request, CancellationToken cancellationToken)
		=> _repo.GetBySerialAsync(request.SerialNumber, cancellationToken);
}

public class ListTerminalsHandler : IRequestHandler<ListTerminalsQuery, IReadOnlyList<Terminal>>
{
	private readonly ITerminalRepository _repo;
	public ListTerminalsHandler(ITerminalRepository repo) => _repo = repo;
	public Task<IReadOnlyList<Terminal>> Handle(ListTerminalsQuery request, CancellationToken cancellationToken)
		=> _repo.ListAsync(cancellationToken);
}

public class RegisterTerminalHandler : IRequestHandler<RegisterTerminalCommand, Terminal>
{
	private readonly ITerminalRepository _repo;
	private readonly ITerminalProfileRepository _profileRepo;
	private readonly ILogger<RegisterTerminalHandler> _logger;
	public RegisterTerminalHandler(ITerminalRepository repo, ITerminalProfileRepository profileRepo, ILogger<RegisterTerminalHandler> logger)
	{
		_repo = repo;
		_profileRepo = profileRepo;
		_logger = logger;
	}

	public async Task<Terminal> Handle(RegisterTerminalCommand request, CancellationToken cancellationToken)
	{
		var existing = await _repo.GetBySerialAsync(request.SerialNumber, cancellationToken);
		if (existing is not null)
		{
			_logger.LogInformation("Terminal already exists: {Serial}", request.SerialNumber);
			return existing;
		}
		var profile = await _profileRepo.GetByIdAsync(request.ProfileId, cancellationToken)
			?? throw new InvalidOperationException("Profile not found");
		var terminal = new Terminal
		{
			SerialNumber = request.SerialNumber,
			Model = request.Model,
			FirmwareVersion = request.FirmwareVersion,
			TerminalProfileId = profile.Id,
			Profile = profile,
			LastSeenUtc = DateTime.UtcNow
		};
		await _repo.AddAsync(terminal, cancellationToken);
		await _repo.SaveChangesAsync(cancellationToken);
		return terminal;
	}
}

public class UpdateTerminalProfileHandler : IRequestHandler<UpdateTerminalProfileCommand>
{
	private readonly ITerminalRepository _repo;
	private readonly ITerminalProfileRepository _profileRepo;
	public UpdateTerminalProfileHandler(ITerminalRepository repo, ITerminalProfileRepository profileRepo)
	{
		_repo = repo;
		_profileRepo = profileRepo;
	}

	public async Task Handle(UpdateTerminalProfileCommand request, CancellationToken cancellationToken)
	{
		var terminal = await _repo.GetByIdAsync(request.TerminalId, cancellationToken)
			?? throw new InvalidOperationException("Terminal not found");
		var profile = await _profileRepo.GetByIdAsync(request.ProfileId, cancellationToken)
			?? throw new InvalidOperationException("Profile not found");
		terminal.TerminalProfileId = profile.Id;
		terminal.Profile = profile;
		await _repo.SaveChangesAsync(cancellationToken);
	}
}

public class IngestStatisticHandler : IRequestHandler<IngestStatisticCommand>
{
	private readonly IStatisticsRepository _stats;
	private readonly ITerminalRepository _repo;
	public IngestStatisticHandler(IStatisticsRepository stats, ITerminalRepository repo)
	{
		_stats = stats;
		_repo = repo;
	}

	public async Task Handle(IngestStatisticCommand request, CancellationToken cancellationToken)
	{
		var terminal = await _repo.GetByIdAsync(request.TerminalId, cancellationToken)
			?? throw new InvalidOperationException("Terminal not found");
		var stat = new TerminalStatistic
		{
			TerminalId = terminal.Id,
			TimestampUtc = DateTime.UtcNow,
			Metric = request.Metric,
			Value = request.Value
		};
		await _stats.AddAsync(stat, cancellationToken);
		await _stats.SaveChangesAsync(cancellationToken);
	}
}

