using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Domain.Entities;

namespace TerminalManagement.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssuersController : ControllerBase
    {
        private readonly IIssuerRepository _issuerRepository;

        public IssuersController(IIssuerRepository issuerRepository)
        {
            _issuerRepository = issuerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Issuer>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _issuerRepository.ListAsync(cancellationToken);
            return Ok(list);
        }
    }
}

