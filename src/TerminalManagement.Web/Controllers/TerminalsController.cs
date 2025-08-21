using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TerminalManagement.Application.DTOs;
using TerminalManagement.Application.Terminals.Queries;

namespace TerminalManagement.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerminalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TerminalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{terminalId}/config")]
        public async Task<ActionResult<TerminalConfigDto>> GetConfig(string terminalId)
        {
            var cfg = await _mediator.Send(new GetTerminalConfigForDownloadQuery(terminalId));
            if (cfg is null) return NotFound();
            return Ok(cfg);
        }
    }
}

