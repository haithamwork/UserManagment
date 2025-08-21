using MediatR;
using Microsoft.AspNetCore.Mvc;
using TerminalMgmt.Application.Terminals;

namespace TerminalMgmt.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TerminalsController : ControllerBase
{
	private readonly IMediator _mediator;
	public TerminalsController(IMediator mediator) => _mediator = mediator;

	[HttpGet]
	public async Task<IActionResult> List()
	{
		var items = await _mediator.Send(new ListTerminalsQuery());
		return Ok(items.Select(t => new { t.Id, t.SerialNumber, t.Model, t.FirmwareVersion, t.TerminalProfileId }));
	}

	public record RegisterDto(string SerialNumber, string Model, string FirmwareVersion, Guid ProfileId);
	[HttpPost]
	public async Task<IActionResult> Register([FromBody] RegisterDto dto)
	{
		var terminal = await _mediator.Send(new RegisterTerminalCommand(dto.SerialNumber, dto.Model, dto.FirmwareVersion, dto.ProfileId));
		return Ok(new { terminal.Id, terminal.SerialNumber });
	}
}

