using MediatR;
using Microsoft.AspNetCore.SignalR;
using TerminalMgmt.Application.Terminals;

namespace TerminalMgmt.Web.Hubs;

public class TerminalHub : Hub
{
	private readonly IMediator _mediator;
	public TerminalHub(IMediator mediator) => _mediator = mediator;

	// Terminal connects and requests configuration by serial number
	public async Task<object?> GetConfiguration(string serialNumber)
	{
		var terminal = await _mediator.Send(new GetTerminalBySerialQuery(serialNumber));
		if (terminal is null) return null;
		return new
		{
			TerminalId = terminal.Id,
			ProfileId = terminal.TerminalProfileId,
			ProfileName = terminal.Profile?.Name,
			ConfigJson = terminal.Profile?.ConfigJson,
			Apns = terminal.Profile?.Apns,
			Issuer = terminal.Profile?.Issuer
		};
	}

	public async Task Register(string serialNumber, string model, string firmwareVersion, Guid profileId)
	{
		await _mediator.Send(new RegisterTerminalCommand(serialNumber, model, firmwareVersion, profileId));
	}

	public async Task SendStatistic(Guid terminalId, string metric, double value)
	{
		await _mediator.Send(new IngestStatisticCommand(terminalId, metric, value));
	}
}

