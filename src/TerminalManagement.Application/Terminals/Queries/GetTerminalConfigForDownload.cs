using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Application.DTOs;
using TerminalManagement.Application.Mappings;

namespace TerminalManagement.Application.Terminals.Queries
{
    public record GetTerminalConfigForDownloadQuery(string TerminalId) : IRequest<TerminalConfigDto?>;

    public class GetTerminalConfigForDownloadHandler : IRequestHandler<GetTerminalConfigForDownloadQuery, TerminalConfigDto?>
    {
        private readonly ITerminalProfileRepository _repository;

        public GetTerminalConfigForDownloadHandler(ITerminalProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<TerminalConfigDto?> Handle(GetTerminalConfigForDownloadQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByTerminalIdAsync(request.TerminalId, cancellationToken);
            return entity?.ToConfigDto();
        }
    }
}

