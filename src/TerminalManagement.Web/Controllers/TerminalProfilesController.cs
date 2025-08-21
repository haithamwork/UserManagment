using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TerminalManagement.Application.DTOs;
using TerminalManagement.Application.Terminals.Commands;
using TerminalManagement.Application.Terminals.Queries;

namespace TerminalManagement.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerminalProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TerminalProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TerminalProfileDto>>> GetAll()
        {
            var result = await _mediator.Send(new ListTerminalProfilesQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TerminalProfileDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetTerminalProfileByIdQuery(id));
            if (result is null) return NotFound();
            return Ok(result);
        }

        public record CreateRequest(string TerminalId, string Model, string FirmwareVersion, Guid IssuerId, List<ApnSettingDto> ApnSettings);
        [HttpPost]
        public async Task<ActionResult<TerminalProfileDto>> Create([FromBody] CreateRequest request)
        {
            var result = await _mediator.Send(new CreateTerminalProfileCommand(request.TerminalId, request.Model, request.FirmwareVersion, request.IssuerId, request.ApnSettings));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        public record UpdateRequest(string Model, string FirmwareVersion);
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TerminalProfileDto>> Update(Guid id, [FromBody] UpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateTerminalProfileCommand(id, request.Model, request.FirmwareVersion));
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var ok = await _mediator.Send(new DeleteTerminalProfileCommand(id));
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}

