using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Application.DTOs;
using TerminalManagement.Application.Mappings;

namespace TerminalManagement.Application.Terminals.Queries
{
    public record GetTerminalProfileByIdQuery(Guid Id) : IRequest<TerminalProfileDto?>;

    public class GetTerminalProfileByIdHandler : IRequestHandler<GetTerminalProfileByIdQuery, TerminalProfileDto?>
    {
        private readonly ITerminalProfileRepository _repository;

        public GetTerminalProfileByIdHandler(ITerminalProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<TerminalProfileDto?> Handle(GetTerminalProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return entity?.ToDto();
        }
    }
}

