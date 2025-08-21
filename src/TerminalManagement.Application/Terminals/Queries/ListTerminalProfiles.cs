using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Application.DTOs;
using TerminalManagement.Application.Mappings;

namespace TerminalManagement.Application.Terminals.Queries
{
    public record ListTerminalProfilesQuery() : IRequest<List<TerminalProfileDto>>;

    public class ListTerminalProfilesHandler : IRequestHandler<ListTerminalProfilesQuery, List<TerminalProfileDto>>
    {
        private readonly ITerminalProfileRepository _repository;

        public ListTerminalProfilesHandler(ITerminalProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TerminalProfileDto>> Handle(ListTerminalProfilesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.ListAsync(cancellationToken);
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}

