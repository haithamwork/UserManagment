using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TerminalManagement.Application.Abstractions.Repositories;

namespace TerminalManagement.Application.Terminals.Commands
{
    public record DeleteTerminalProfileCommand(Guid Id) : IRequest<bool>;

    public class DeleteTerminalProfileHandler : IRequestHandler<DeleteTerminalProfileCommand, bool>
    {
        private readonly ITerminalProfileRepository _repository;

        public DeleteTerminalProfileHandler(ITerminalProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTerminalProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            await _repository.DeleteAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

