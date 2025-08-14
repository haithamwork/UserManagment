using Tms.Domain.Entities;

namespace Tms.Application.Abstractions;

public interface ITerminalService
{
	Task<Terminal?> GetBySerialAsync(string serialNumber, CancellationToken cancellationToken = default);
	Task<Terminal> RegisterAsync(string serialNumber, string? model, string? merchantCode, CancellationToken cancellationToken = default);
	Task AssignProfileAsync(Guid terminalId, Guid profileId, CancellationToken cancellationToken = default);
	Task AssignApplicationAsync(Guid terminalId, Guid appPackageId, CancellationToken cancellationToken = default);
}