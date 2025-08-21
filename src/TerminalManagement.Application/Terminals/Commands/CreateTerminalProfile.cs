using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Application.DTOs;
using TerminalManagement.Application.Mappings;
using TerminalManagement.Domain.Entities;

namespace TerminalManagement.Application.Terminals.Commands
{
    public record CreateTerminalProfileCommand(
        string TerminalId,
        string Model,
        string FirmwareVersion,
        Guid IssuerId,
        List<ApnSettingDto> ApnSettings
    ) : IRequest<TerminalProfileDto>;

    public class CreateTerminalProfileHandler : IRequestHandler<CreateTerminalProfileCommand, TerminalProfileDto>
    {
        private readonly ITerminalProfileRepository _repository;

        public CreateTerminalProfileHandler(ITerminalProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<TerminalProfileDto> Handle(CreateTerminalProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = new TerminalProfile
            {
                TerminalId = request.TerminalId,
                Model = request.Model,
                FirmwareVersion = request.FirmwareVersion,
                IssuerId = request.IssuerId
            };

            foreach (var apn in request.ApnSettings)
            {
                entity.ApnSettings.Add(new ApnSetting
                {
                    Apn = apn.Apn,
                    Username = apn.Username,
                    Password = apn.Password
                });
            }

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}

