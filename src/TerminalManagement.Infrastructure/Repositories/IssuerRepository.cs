using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TerminalManagement.Application.Abstractions.Repositories;
using TerminalManagement.Domain.Entities;
using TerminalManagement.Infrastructure.Persistence;

namespace TerminalManagement.Infrastructure.Repositories
{
    public class IssuerRepository : IIssuerRepository
    {
        private readonly AppDbContext _dbContext;

        public IssuerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Issuer issuer, CancellationToken cancellationToken)
        {
            await _dbContext.Issuers.AddAsync(issuer, cancellationToken);
        }

        public async Task<Issuer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Issuers.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task<List<Issuer>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Issuers.ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

