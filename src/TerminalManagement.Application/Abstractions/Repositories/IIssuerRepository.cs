using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TerminalManagement.Domain.Entities;

namespace TerminalManagement.Application.Abstractions.Repositories
{
    public interface IIssuerRepository
    {
        Task<Issuer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Issuer>> ListAsync(CancellationToken cancellationToken);
        Task AddAsync(Issuer issuer, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}

