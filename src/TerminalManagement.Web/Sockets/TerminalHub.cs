using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using TerminalManagement.Application.Terminals.Commands;
using TerminalManagement.Application.Terminals.Queries;

namespace TerminalManagement.Web.Sockets
{
    public class TerminalHub : Hub
    {
        private readonly IMediator _mediator;

        public TerminalHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<object?> RequestConfig(string terminalId)
        {
            var cfg = await _mediator.Send(new GetTerminalConfigForDownloadQuery(terminalId));
            return cfg;
        }

        public async Task<bool> SubmitStatistic(string terminalId, string metric, string value)
        {
            return await _mediator.Send(new SubmitTerminalStatisticCommand(terminalId, metric, value));
        }
    }
}

