using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Application.DTOs;
using TerminalManagement.Application.Mappings;

namespace TerminalManagement.Application.Terminals.Commands
{
    public record UpdateTerminalProfileCommand(
        Guid Id,
        string Model,
        string FirmwareVersion
    ) : IRequest<TerminalProfileDto?>;

    public class UpdateTerminalProfileHandler : IRequestHandler<UpdateTerminalProfileCommand, TerminalProfileDto?>
    {
        private readonly ITerminalProfileRepository _repository;

        public UpdateTerminalProfileHandler(ITerminalProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<TerminalProfileDto?> Handle(UpdateTerminalProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return null;
            }

            entity.Model = request.Model;
            entity.FirmwareVersion = request.FirmwareVersion;

            await _repository.UpdateAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return entity.ToDto();
        }
    }
}

