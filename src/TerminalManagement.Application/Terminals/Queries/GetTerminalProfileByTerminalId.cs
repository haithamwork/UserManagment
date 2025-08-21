using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Application.DTOs;
using TerminalManagement.Application.Mappings;

namespace TerminalManagement.Application.Terminals.Queries
{
    public record GetTerminalProfileByTerminalIdQuery(string TerminalId) : IRequest<TerminalProfileDto?>;

    public class GetTerminalProfileByTerminalIdHandler : IRequestHandler<GetTerminalProfileByTerminalIdQuery, TerminalProfileDto?>
    {
        private readonly ITerminalProfileRepository _repository;

        public GetTerminalProfileByTerminalIdHandler(ITerminalProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<TerminalProfileDto?> Handle(GetTerminalProfileByTerminalIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByTerminalIdAsync(request.TerminalId, cancellationToken);
            return entity?.ToDto();
        }
    }
}

