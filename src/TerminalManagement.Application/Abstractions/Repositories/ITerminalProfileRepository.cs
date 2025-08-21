using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TerminalManagement.Domain.Entities;

namespace TerminalManagement.Application.Abstractions.Repositories
{
    public interface ITerminalProfileRepository
    {
        Task<TerminalProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<TerminalProfile?> GetByTerminalIdAsync(string terminalId, CancellationToken cancellationToken);
        Task<List<TerminalProfile>> ListAsync(CancellationToken cancellationToken);
        Task AddAsync(TerminalProfile profile, CancellationToken cancellationToken);
        Task UpdateAsync(TerminalProfile profile, CancellationToken cancellationToken);
        Task DeleteAsync(TerminalProfile profile, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}

