using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Domain.Entities;

namespace TerminalManagement.Application.Terminals.Commands
{
    public record SubmitTerminalStatisticCommand(
        string TerminalId,
        string Metric,
        string Value
    ) : IRequest<bool>;

    public class SubmitTerminalStatisticHandler : IRequestHandler<SubmitTerminalStatisticCommand, bool>
    {
        private readonly ITerminalProfileRepository _terminalRepository;
        private readonly IStatisticRepository _statRepository;

        public SubmitTerminalStatisticHandler(ITerminalProfileRepository terminalRepository, IStatisticRepository statRepository)
        {
            _terminalRepository = terminalRepository;
            _statRepository = statRepository;
        }

        public async Task<bool> Handle(SubmitTerminalStatisticCommand request, CancellationToken cancellationToken)
        {
            var terminal = await _terminalRepository.GetByTerminalIdAsync(request.TerminalId, cancellationToken);
            if (terminal == null)
            {
                return false;
            }

            var stat = new TerminalStatistic
            {
                TerminalProfileId = terminal.Id,
                TimestampUtc = DateTime.UtcNow,
                Metric = request.Metric,
                Value = request.Value
            };

            await _statRepository.AddAsync(stat, cancellationToken);
            await _statRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

